using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using mboard.Models;

namespace mboard.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private NeoDbContext db = new NeoDbContext();
        // GET: Tag
        public ActionResult Create(string id)
        {
            ViewBag.boardId = id;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagModelView tag)
        {
            if (db.CheckRelationExist<UserBoardRelation>(tag.TableId, User.Identity.GetUserId()))
            {
                string id = null;
                TagRelation tagRel = new TagRelation();
                try { id = db.ReadNode<Tag>("Title", tag.Title).Id; }
                catch { };
                if (string.IsNullOrEmpty(id))
                {
                    db.CreateNodeWithRelation(new Tag { Title = tag.Title }, tag.TableId, tagRel);
                    return RedirectToAction("Edit", "Test", new { Id = tag.TableId });
                }
                else
                {
                    tag.Id = id;
                    if (!db.CheckRelationExist<TagRelation>(tag.TableId, tag.Id))
                    {
                        db.CreateRelation(tag.TableId, tag.Id, tagRel);
                        return RedirectToAction("Edit", "Test", new { Id = tag.TableId });
                    }
                }
            }
            return HttpNotFound();
        }

        public ActionResult Index()
        {
            return PartialView(db.TagIndex(User.Identity.GetUserId()));
        }

        public ActionResult IndexBoard(string boardId)
        {
            ViewBag.boardId = boardId;
            return PartialView(db.BoardTagIndex(boardId));
        }

        public ActionResult Delete(TagModelView tag)
        {
            return PartialView(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(TagModelView tag)
        {
            if (db.CheckRelationExist<UserBoardRelation>(tag.TableId, User.Identity.GetUserId()))
            {
                try
                {
                    db.DeleteRelation<TagRelation>(tag.TableId, tag.Id);
                }
                catch { return HttpNotFound(); }
                try
                {
                    db.DeleteNode(tag.Id);
                }
                catch { }
                return RedirectToAction("Edit", "Test", new { Id = tag.TableId });
            }
            return HttpNotFound();
        }
    }
}