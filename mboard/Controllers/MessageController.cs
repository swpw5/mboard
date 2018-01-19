﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mboard.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace mboard.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private NeoDbContext db = new NeoDbContext();
        public ActionResult MessagesReceived()
        {
            var mes = db.ReadRelatedNodesWithRelations<Message, MessageRelations>(User.Identity.GetUserId(), "MesType", MessageTypeRel.Received.ToString());
            return View(mes);
        }

        public ActionResult MessagesReaded()
        {
            var mes = db.ReadRelatedNodesWithRelations<Message, MessageRelations>(User.Identity.GetUserId(), "MesType", MessageTypeRel.ReceivedReaded.ToString());
            return View(mes);
        }

        public ActionResult MessagesSent()
        {
            var mes = db.ReadRelatedNodesWithRelations<Message, MessageRelations>(User.Identity.GetUserId(), "MesType", MessageTypeRel.Sent.ToString());
            return View(mes);
        }

        public ActionResult SendMessage(User user)
        {
            ViewBag.Email = user.Email.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(Message message)
        {
            string userId = db.ReadNode<User>("Email", message.UserTo).Id;
            if (db.CheckRelationExist<FriendRelation>(userId, User.Identity.GetUserId()))
            {
                message.UserFrom = User.Identity.GetUserName();
                MessageRelations send = new MessageRelations()
                {
                    MesType = MessageTypeRel.Sent
                };
                MessageRelations receiv = new MessageRelations()
                {
                    MesType = MessageTypeRel.Received
                };
                Relation<MessageRelations> rel = new Relation<MessageRelations>()
                {
                    FirstNodeId = userId,
                    SecondNodeId = message.Id.ToString(),
                    Rel = receiv
                };
                db.CreateNodeWithRelation(message, User.Identity.GetUserId(), send);
                db.CreateRelation(rel);
                return RedirectToAction("MessagesSent");
            }
            else
                return HttpNotFound();
        }

        public ActionResult Details(Message message)
        {
            if (db.ReadRelationData<MessageRelations>(message.Id, User.Identity.GetUserId()).MesType!= MessageTypeRel.Sent)
            {
                db.UpdateRelationSingleProperty<MessageRelations>(User.Identity.GetUserId(), message.Id.ToString(), "MesType", MessageTypeRel.ReceivedReaded.ToString());
            }
            if (db.CheckRelationExist<MessageRelations>(message.Id, User.Identity.GetUserId()))
            {
                message = db.ReadNode<Message>(message.Id);
                return View(message);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Message message)
        {
            if (db.CheckRelationExist<MessageRelations>(message.Id, User.Identity.GetUserId()))
            {
                db.UpdateRelationSingleProperty<MessageRelations>(User.Identity.GetUserId(), message.Id.ToString(), "MesType", MessageTypeRel.ReceivedReaded.ToString());
                return RedirectToAction("MessagesReceived");
            }
            else
                return HttpNotFound();
        }
    }
}