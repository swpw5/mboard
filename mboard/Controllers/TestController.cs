using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using mboard.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace mboard.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private NeoDbContext db = new NeoDbContext();

        // GET: Test
        public ActionResult Index(string searchString)
        {

            // Guid g = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            ViewBag.Message = User.Identity.GetUserId();
            string userId = User.Identity.GetUserId();
            IEnumerable<Board> boards = null;
            try
            {
                boards = db.ReadRelatedNodes<UserBoardRelation, Board>(userId);
                if (!String.IsNullOrEmpty(searchString))
                {
                    boards = boards.Where(s => s.Title.Contains(searchString));
                }
            }
            catch (Exception)
            {
                // throw;
            }
            if (boards == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(boards.ToList());
            }

            //return View(db.ReadNodeType<Board>());
        }

        public ActionResult Details(Board board)
        {
            try
            {
                string userId = db.ReadRelatedNodes<UserBoardRelation, User>(board.Id).FirstOrDefault().Id;
                var rel = db.ReadRelationData<FriendRelation>(User.Identity.GetUserId(), userId);
                if (rel.FriendType.ToString() == FriendsTypeRel.Friend.ToString() && db.ReadNode<Board>(board.Id).VisibleForFriends == true)
                {
                    board = db.ReadNode<Board>(board.Id);
                    return View(board);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch { return HttpNotFound(); }
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, VisibleForFriends,Id,DiagramData,BoardModel")] Board board)
        {
            if (ModelState.IsValid)
            {
                //string userId = "cdf72cae-adb0-4188-9b1c-69692eb56707";
                //string userId = "6049202b-bd31-47b4-94ab-3fde19200e6c";
                //Board b = new Board();
                //b.Title = board.Title;
                //Debug.WriteLine(b.name);
                string userId = User.Identity.GetUserId();
                UserBoardRelation rel = new UserBoardRelation();
                // ctx.CreateNode(user);
                db.CreateNodeWithRelation(board, userId, rel);
                //board.DiagramData =
                //db.CreateNode(board);
                return RedirectToAction("Index");
            }
            return View();
        }



        // GET: Test/Edit/5
        public ActionResult Edit(string id)
        {
            if (db.CheckRelationExist<UserBoardRelation>(id, User.Identity.GetUserId()))
            {
                Board board = db.ReadNode<Board>(id);
                if (board == null)
                {
                    return HttpNotFound();
                }
                return View(board);
            }
            else { return HttpNotFound(); }
        }

        public ActionResult Search(Tag tag)
        {
            return View("Index", db.TagSearch(User.Identity.GetUserId(), tag.Id));
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Board board)
        {
            if (db.CheckRelationExist<UserBoardRelation>(board.Id, User.Identity.GetUserId()))
            {
                if (ModelState.IsValid)
                {
                    db.UpdateAllPropNode(board.Id, board);
                    //var d = board.DiagramData;
                    if (board.DiagramData != null)
                    {
                        string replacement = Regex.Replace(board.DiagramData, @"\t|\n|\r", "");
                        string diag = replacement.Replace("'", "\"");
                        db.UpdateSinglePropNode(board.Id, diag, "DiagramData");
                    }
                    return RedirectToAction("Index");
                }
                return View(board);
            }
            else { return HttpNotFound(); }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(string id)
        {
            if (db.CheckRelationExist<UserBoardRelation>(id, User.Identity.GetUserId()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Board board = db.ReadNode<Board>(id);
                if (board == null)
                {
                    return HttpNotFound();
                }
                return PartialView(board);
            }
            else { return HttpNotFound(); }
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (db.CheckRelationExist<UserBoardRelation>(id, User.Identity.GetUserId()))
            {
                Board board = db.ReadNode<Board>(id);
                db.DeleteNodeWithRelations(id);
                return RedirectToAction("Index");
            }
            else { return HttpNotFound(); }
        }
    }
}
