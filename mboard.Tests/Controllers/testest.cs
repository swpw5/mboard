using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mboard.Models;
using System.Diagnostics;
using mboard.Controllers;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;



namespace mboard.Tests.Models
{
    [TestClass]
    public class testest
    {
        [TestMethod]
        public void testBoardName()
        {
            //Board b = new Board();
            //b.Title = "tablica";
            //Debug.WriteLine(b.name);

            //UserTest user = new UserTest();
            //user.Id = Guid.NewGuid().ToString();
            //user.Name = "Piotr";

            NeoDbContext ctx = new NeoDbContext();
            //   ctx.CreateNode(user);
            // ctx.CreateNodeWithRelation(b, user.Id, "relacjakamila");

            string userId = "cdf72cae-adb0-4188-9b1c-69692eb56707";
            List<Board> boards = ctx.ReadRelatedNodes<RelType, Board>(userId).ToList<Board>();
            foreach (Board item in boards)
            {
                Debug.WriteLine(string.Format("{0} \t {1} \t {2}", item.Id, item.name, item.Title));
            }

        }


        [TestMethod]
        public void userFind()
        {
            NeoDbContext db = new NeoDbContext();
            string userId = "cdf72cae-adb0-4188-9b1c-69692eb56707";
            UserTest test = db.ReadNode<UserTest>(userId);
            Debug.WriteLine(test.Name);
        }

        [TestMethod]
        public void CreateBoardForUserTest()
        {
            string userId = "cdf72cae-adb0-4188-9b1c-69692eb56707";
            Board b = new Board();
            b.Title = "tablicaTest";
            //Debug.WriteLine(b.name);



            NeoDbContext ctx = new NeoDbContext();
           // ctx.CreateNode(user);
            ctx.CreateNodeWithRelation(b, userId, "RelType");
        }


        [TestMethod]
        public void CreateBoardForUserWithRelatypeTest()
        {
            //string userId = "cdf72cae-adb0-4188-9b1c-69692eb56707";
            string userId = "6049202b-bd31-47b4-94ab-3fde19200e6c";
            Board b = new Board();
            b.Title = "tablica5";
            //Debug.WriteLine(b.name);

            UserBoardRelation rel = new UserBoardRelation();
            rel.Description = "atrybut";

            NeoDbContext ctx = new NeoDbContext();
            // ctx.CreateNode(user);
            ctx.CreateNodeWithRelation(b, userId, rel);
        }

        [TestMethod]
        public void EditBoardTest()
        {
            NeoDbContext db = new NeoDbContext();
            string id = "c436c76c-909d-4551-a87a-0d991f88c371";
            Board board = db.ReadNode<Board>(id);

            Debug.WriteLine(board.Title);
        }


    }

    public class UserTest : INodeModel
    {
        public string Id
        {
            get; set;
        }

        public string Name { get; set; }
    }

    public class RelType
    {
        public string Name { get; set; }
    }
}
