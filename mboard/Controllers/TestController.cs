using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using mboard.Models;

namespace mboard.Controllers
{
    public class TestController : Controller
    {
        private NeoDbContext db = new NeoDbContext();
        // GET: Test
        public ActionResult Index()
        {
            return View(db.ReadNodeType<Board>());
        }

        // GET: Test/Details/5
        public ActionResult Details(string id)
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

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Color,Height,Width,Last_mod,Created,List_position")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.CreateNode(board);
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

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Color,Height,Width,Last_mod,Created,List_position")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.UpdateAllPropNode<Board>(board.Id, board);
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
            db.DeleteNode(id);
            return RedirectToAction("Index");
        }
    }
}
