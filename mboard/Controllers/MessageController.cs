using System;
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

        public ActionResult MessagesSended()
        {
            var mes = db.ReadRelatedNodesWithRelations<Message, MessageRelations>(User.Identity.GetUserId(), "MesType", MessageTypeRel.Send.ToString());
            return View(mes);
        }

        public ActionResult SendMessage(User user)
        {

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(Message message, User user)
        {
            MessageRelations send = new MessageRelations()
            {
                MesType = MessageTypeRel.Send
            };
            MessageRelations receiv = new MessageRelations()
            {
                MesType = MessageTypeRel.Received
            };

            Relation<MessageRelations> rel = new Relation<MessageRelations>()
            {
                FirstNodeId = user.Id.ToString(),
                SecondNodeId = message.Id.ToString(),
                Rel = receiv
            };
            db.CreateNodeWithRelation(message, User.Identity.GetUserId(), send);
            db.CreateRelation(rel);
            return RedirectToAction("MessagesSended");
        }

        public ActionResult Details(Message message)
        {
            db.UpdateRelationSingleProperty<MessageRelations>(User.Identity.GetUserId(), message.Id.ToString(), "MesType", MessageTypeRel.ReceivedReaded.ToString());
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Message message)
        {
            db.UpdateRelationSingleProperty<MessageRelations>(User.Identity.GetUserId(), message.Id.ToString(), "MesType", MessageTypeRel.ReceivedReaded.ToString());
            return RedirectToAction("MessagesReceived");
        }
    }
}