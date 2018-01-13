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
using System.Text.RegularExpressions;

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


        [TestMethod]
        public void SaveDiagram()
        {
         
            string diagData = @"{
                        'nodeDataArray': [
                        { 'id': 0, 'loc': '120 120', 'text': 'Initial', 'comments': 'test comment'},
                        { 'id': 1, 'loc': '330 120', 'text': 'First down' },
                        { 'id': 2, 'loc': '226 376', 'text': 'First up' },
                        { 'id': 3, 'loc': '60 276', 'text': 'Second down' },
                        { 'id': 4, 'loc': '226 226', 'text': 'Wait' }
                        ],
                        'linkDataArray': [
                        { 'from': 0, 'to': 0, 'text': 'up or timer', 'curviness': -20 },
                        { 'from': 0, 'to': 1, 'text': 'down', 'curviness': 20 },
                        { 'from': 1, 'to': 0, 'text': 'up (moved)\nPOST', 'curviness': 20 },
                        { 'from': 1, 'to': 1, 'text': 'down', 'curviness': -20 },
                        { 'from': 1, 'to': 2, 'text': 'up (no move)' },
                        { 'from': 1, 'to': 4, 'text': 'timer' },
                        { 'from': 2, 'to': 0, 'text': 'timer\nPOST' },
                        { 'from': 2, 'to': 3, 'text': 'down' },
                        { 'from': 3, 'to': 0, 'text': 'up\nPOST\n(dblclick\nif no move)' },
                        { 'from': 3, 'to': 3, 'text': 'down or timer', 'curviness': 20 },
                        { 'from': 4, 'to': 0, 'text': 'up\nPOST' },
                        { 'from': 4, 'to': 4, 'text': 'down' }
                        ]


                        }";

            


            
            string replacement = Regex.Replace(diagData, @"\t|\n|\r", "");
            replacement = replacement.Replace("'", "\"");
            Debug.Write(replacement);
            

            //string diagData = "jakis json";
            NeoDbContext db = new NeoDbContext();
            string boardId = "8efa2bfd-539a-469b-af63-f759ffb4a3e3";
            //Board board = db.ReadNode<Board>(boardId);
            db.UpdateSinglePropNode(boardId, replacement, "DiagramData");


        }

        [TestMethod]
        public void ReadDiagramDataTest()
        {
            NeoDbContext db = new NeoDbContext();
            string boardId = "c81fcc0a-8f98-4af3-935d-8e0f83a31e73";
            Board board = db.ReadNode<Board>(boardId);
            Debug.WriteLine(board.DiagramData);
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
