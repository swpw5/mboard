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

        [HttpPost]
        public ActionResult Details([Bind(Include = "Id,Title,name,DiagramData")] Board board)
        {
            if (board == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(board);
            }
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title")] Board board)
        {
            if (ModelState.IsValid)
            {
                //string userId = "cdf72cae-adb0-4188-9b1c-69692eb56707";
                //string userId = "6049202b-bd31-47b4-94ab-3fde19200e6c";
                //Board b = new Board();
                //b.Title = board.Title;
                //Debug.WriteLine(b.name);
                string userId = User.Identity.GetUserId();
                UserBoardRelation rel = new UserBoardRelation()
                {
                    Description = "testwidok"
                };
                NeoDbContext ctx = new NeoDbContext();
                // ctx.CreateNode(user);
                ctx.CreateNodeWithRelation(board, userId, rel);
                //board.DiagramData =
                //db.CreateNode(board);
                return RedirectToAction("Index");
            }
            return View();
        }



        // GET: Test/Edit/5
        public ActionResult Edit(string id)
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
            return View(board);
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
            if (ModelState.IsValid)
            {
                db.UpdateAllPropNode<Board>(board.Id, board);
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

        // GET: Test/Delete/5
        public ActionResult Delete(string id)
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
            return View(board);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Board board = db.ReadNode<Board>(id);
            db.DeleteNodeWithRelations(id);
            return RedirectToAction("Index");
        }
    }
}
