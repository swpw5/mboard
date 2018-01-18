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
        [ValidateAntiForgeryToken]
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

        public ActionResult InvitationsSent()
        {
            var users = db.ReadRelatedNodesWithRelationsTo<User, FriendRelation>(User.Identity.GetUserId(), "FriendType", FriendsTypeRel.Invited.ToString());
            return View(users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UndoInv(FriendRelation rel)
        {
            db.DeleteRelation(rel.Id.ToString());
            return RedirectToAction("InvitationsSent");
        }

        public ActionResult InvitationsReceived()
        {
            var users = db.ReadRelatedNodesWithRelationsFrom<User, FriendRelation>(User.Identity.GetUserId(), "FriendType", FriendsTypeRel.Invited.ToString());
            return View(users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptInv(FriendRelation rel)
        {
            try
            {
                if (db.ReadRelation<FriendRelation>(rel.Id).FirstNodeId == User.Identity.GetUserId() || db.ReadRelation<FriendRelation>(rel.Id).SecondNodeId == User.Identity.GetUserId())
                    db.UpdateRelationSingleProperty(rel.Id, "FriendType", FriendsTypeRel.Friend.ToString());
                else
                {
                    return HttpNotFound();
                }
            }
            catch { return HttpNotFound(); }
            return RedirectToAction("InvitationsReceived");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectInv(FriendRelation rel)
        {
            try
            {
                if (db.ReadRelation<FriendRelation>(rel.Id).FirstNodeId == User.Identity.GetUserId() || db.ReadRelation<FriendRelation>(rel.Id).SecondNodeId == User.Identity.GetUserId())
                db.DeleteRelation(rel.Id.ToString());
                else
                {
                    return HttpNotFound();
                }
            }
            catch { return HttpNotFound(); }
            return RedirectToAction("InvitationsReceived");
        }

        public ActionResult Friend()
        {
            var users = db.ReadRelatedNodesWithRelations<User, FriendRelation>(User.Identity.GetUserId(), "FriendType", FriendsTypeRel.Friend.ToString());
            return View(users.ToList());
        }

        public ActionResult FriendRecommendation()
        {
            UserPotentialFriendModelView recom;
            try
            {
                recom = db.FriendRecommendation(User.Identity.GetUserId());
            }
            catch { return HttpNotFound(); }
                return PartialView(recom);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FriendRemove(FriendRelation rel)
        {
            try
            {
                if (db.ReadRelation<FriendRelation>(rel.Id).FirstNodeId == User.Identity.GetUserId() || db.ReadRelation<FriendRelation>(rel.Id).SecondNodeId == User.Identity.GetUserId())
                    db.DeleteRelation(rel.Id.ToString());
                else
                {
                    return HttpNotFound();
                }
            }
            catch { return HttpNotFound(); }
            return RedirectToAction("Friend");
        }

        public ActionResult FriendDetails(User user)
        {
            List<RelationWithNode<Board, UserBoardRelation>> boards = null;
            try
            {
                var rel = db.ReadRelationData<FriendRelation>(User.Identity.GetUserId(), user.Id.ToString());
                if (rel.FriendType.ToString() == FriendsTypeRel.Friend.ToString())
                {
                    boards = db.ReadRelatedNodesAttrWithRelations<Board, UserBoardRelation>(user.Id.ToString(), "VisibleForFriends", "true").ToList();
                }
            }
            catch { return HttpNotFound(); }
            if (boards == null)
            {
                return HttpNotFound();
            }
            else
            {
                //ViewBag.Id = user.Id;
                //ViewBag.UserName = user.Email;
                return View(boards);
            }
        }
    }
}