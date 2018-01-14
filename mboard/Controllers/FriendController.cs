using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mboard.Models;
using Microsoft.AspNet.Identity;


namespace mboard.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private NeoDbContext db = new NeoDbContext();
        // GET: Friend
        public ActionResult Index(string searchString)
        {
            var users = db.ReadNonRelatedNodes<FriendRelation, User>(User.Identity.GetUserId());
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Email.Contains(searchString));
            }
            users = users.Where(s => !s.Email.Contains(User.Identity.GetUserName()));
            return View(users.ToList());
        }

        [HttpPost]
        public ActionResult Invite(User user)
        {
            string id = user.Id.ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FriendRelation rel = new FriendRelation();
            db.CreateRelation(User.Identity.GetUserId(), id, rel);
            return RedirectToAction("Index");
        }

        public ActionResult InvitationsSend()
        {
            var users = db.ReadRelatedNodesWithRelationsFrom<User, FriendRelation>(User.Identity.GetUserId(), "FriendType", FriendsTypeRel.Invited.ToString());
            return View(users.ToList());
        }

        [HttpPost]
        public ActionResult UndoInv(FriendRelation rel)
        {
            db.DeleteRelation(rel.Id.ToString());
            return RedirectToAction("InvitationSend");
        }

        public ActionResult InvitationsReceived()
        {
            var users = db.ReadRelatedNodesWithRelationsTo<User, FriendRelation>(User.Identity.GetUserId(), "FriendType", "Invited");
            return View(users.ToList());
        }

        [HttpPost]
        public ActionResult RejectInv(FriendRelation rel)
        {
            db.DeleteRelation(rel.Id.ToString());
            return RedirectToAction("InvitationsReceived");
        }
        public ActionResult Friends()
        {
            var users = db.ReadRelatedNodesWithRelations<User, FriendRelation>(User.Identity.GetUserId(), "FriendType", "Friend");
            return View(users.ToList());
        }

        [HttpPost]
        public ActionResult FriendRemove(FriendRelation rel)
        {
            db.DeleteRelation(rel.Id.ToString());
            return RedirectToAction("Friends");
        }

        public ActionResult FriendsDetails(User user)
        {
            List<RelationWithNode<UserBoardRelation, Board>> boards = null;
            var rel = db.ReadRelationData<FriendRelation>(User.Identity.GetUserId(), user.Id.ToString());
            if (rel.FriendType.ToString() == FriendsTypeRel.Friend.ToString())
            {
                try
                {
                    boards = db.ReadRelatedNodesWithRelations<UserBoardRelation, Board>(user.Id.ToString(), "VisibleForFriends", true.ToString()).ToList();
                }
                catch (Exception)
                {
                }
            }
            if (boards == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Id = user.Id;
                ViewBag.UserName = user.Email;
                return View(boards);
            }
        }
    }
}