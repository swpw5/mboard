using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mboard.Models;
using System.Diagnostics;
using mboard.Controllers;
using System.Threading.Tasks;

namespace mboard.Tests.Models
{
    [TestClass]
    public class NeoDbContextTest
    {
        static string guid = Guid.NewGuid().ToString();
        static string guid2 = Guid.NewGuid().ToString();
        static string guid3 = Guid.NewGuid().ToString();
        static string guid4 = Guid.NewGuid().ToString();
        static string guid5 = Guid.NewGuid().ToString();
        static string guid6 = Guid.NewGuid().ToString();
        static string guid7 = Guid.NewGuid().ToString();
        [TestMethod]
        public void CreateNode()
        {
            Debug.Write(guid);
            Board board = new Board() {Id = guid, Title = "Nazwa", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            Board board2 = new Board() { Id = guid2, Title = "Nazwa2", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            Board board3 = new Board() { Id = guid3, Title = "Nazwa3", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            Board board4 = new Board() { Id = guid4, Title = "Nazwa4", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            Board board5 = new Board() { Id = guid5, Title = "Nazwa5", Color = "Purple", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            Board board6 = new Board() { Id = guid6, Title = "Nazwa6", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            NeoDbContext db = new NeoDbContext();
            db.CreateNode(board);
            db.CreateNode(board2);
            db.CreateNode(board3);
            db.CreateNode(board4);
            db.CreateNode(board5);
            db.CreateNode(board6);
            Board boardTest = db.ReadNode<Board>(guid2);
            Debug.WriteLine(boardTest.Title);
            Assert.IsTrue(board2.Title == boardTest.Title);
        }
        [TestMethod]
        public void CreateNodeWithRel()
        {
            Board board7 = new Board() { Id = guid7, Title = "Nazwa7", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            NeoDbContext db = new NeoDbContext();
            db.CreateNodeWithRelation(board7, guid5, "PinConnection");
            Board boardTest = db.ReadNode<Board>(guid7);
            Debug.WriteLine(boardTest.Title);
            Assert.IsTrue(board7.Title == boardTest.Title);
        }
        [TestMethod]
        public void ReadNode1()
        {
            Board board = new Board();
            NeoDbContext db = new NeoDbContext();
            board = db.ReadNode<Board>(guid);
            Board boardTest = db.ReadNode<Board>(guid);
            Debug.WriteLine(boardTest.Title);
            Assert.IsTrue(board.Title == boardTest.Title);
        }
        [TestMethod]
        public void ReadNode2()
        {
            Board board = new Board();
            NeoDbContext db = new NeoDbContext();
            board = db.ReadNode<Board>(guid5);
            Board boardTest = db.ReadNode<Board>("Color", "Purple");
            Debug.WriteLine(boardTest.Title);
            Assert.IsTrue(board.Color == boardTest.Color);
        }

        [TestMethod]
        public void CreateSimpleRelation()
        {
            NeoDbContext db = new NeoDbContext();
            SimpleRelation simple = new SimpleRelation();
            db.CreateRelation(guid, guid2, simple);
        }

        [TestMethod]
        public void RelationExist1()
        {
            NeoDbContext db = new NeoDbContext();
            Assert.IsTrue(db.CheckRelationExist<SimpleRelation>(guid, guid2));
        }
        [TestMethod]
        public void RelationExist2()
        {
            NeoDbContext db = new NeoDbContext();
            SimpleRelation simple = new SimpleRelation();
            Relation<SimpleRelation> rel = new Relation<SimpleRelation> { FirstNodeId = guid, SecondNodeId = guid2, Rel = simple };
            Assert.IsTrue(db.CheckRelationExist(rel));
        }
        [TestMethod]
        public void CreateRelation1()
        {
            NeoDbContext db = new NeoDbContext();
            PinConnection rel = new PinConnection { Color="Red", Width=10 };
            Relation<PinConnection> simple = new Relation<PinConnection> { FirstNodeId = guid3, SecondNodeId = guid4, Rel = rel };
            db.CreateRelation(simple);
        }
        [TestMethod]
        public void CreateRelation2()
        {
            NeoDbContext db = new NeoDbContext();
            PinConnection rel = new PinConnection { Color = "Red", Width = 10 };
            db.CreateRelation(guid3, guid2, rel);
            db.CreateRelation(guid5, guid6, rel);
        }
        [TestMethod]
        public void ReadRelationData()
        {
            NeoDbContext db = new NeoDbContext();
            PinConnection test = db.ReadRelationData<PinConnection>(guid3, guid4);
            Debug.WriteLine(test.Width+" "+test.Color);
            Assert.IsTrue(test.Color == "Red");
        }
        [TestMethod]
        public void ReadRelation()
        {
            NeoDbContext db = new NeoDbContext();
            Relation<PinConnection> test = db.ReadRelation<PinConnection>(guid3, guid4);
            Debug.WriteLine(test.Rel.Width + " " + test.Rel.Color);
            Assert.IsTrue(test.Rel.Color == "Red");
        }
        [TestMethod]
        public void UpdateRelationSingleProp()
        {
            NeoDbContext db = new NeoDbContext();
            db.UpdateRelationSingleProperty<PinConnection>(guid3, guid4, "Color", "Blue");
            Relation<PinConnection> test = db.ReadRelation<PinConnection>(guid3, guid4);
            Debug.WriteLine(test.Rel.Width + " " + test.Rel.Color);
            Assert.IsTrue(test.Rel.Color == "Blue");
        }
        [TestMethod]
        public void ReadRealatedNode()
        {
            NeoDbContext db = new NeoDbContext();
            IEnumerable<Board> test = db.ReadRelatedNodes<PinConnection, Board>(guid3);
            Debug.WriteLine(test.First().Id);
            Assert.IsTrue(test.First().Id == guid2 || test.ToList()[0].Id==guid4);
        }
        [TestMethod]
        public void ReadRealatedNodeWithRelations()
        {
            NeoDbContext db = new NeoDbContext();
            IEnumerable<RelationWithNode<Board, PinConnection>> test = db.ReadRelatedNodesWithRelations<PinConnection, Board>(guid3);
            Debug.WriteLine(test.First().NodeData.Id);
            Assert.IsTrue(test.First().NodeData.Id == guid2 && test.ToList()[1].NodeData.Id == guid4);
        }
        [TestMethod]
        public void UpdateAll()
        {
            Board board = new Board() { Id = guid, Title = "Nazwa123", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 };
            NeoDbContext db = new NeoDbContext();
            db.UpdateAllPropNode(guid, board);
            Board boardTest = db.ReadNode<Board>(guid);
            Debug.WriteLine(boardTest.Title);
            Assert.IsTrue(board.Title == boardTest.Title);
        }
        [TestMethod]
        public void UpdateSingle()
        {
            NeoDbContext db = new NeoDbContext();
            db.UpdateSinglePropNode(guid2, "Nowa nazwa", "Title");
            Board boardTest = db.ReadNode<Board>(guid2);
            Debug.WriteLine(boardTest.Title);
            Assert.IsTrue("Nowa nazwa" == boardTest.Title);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteRelation1()
        {
            NeoDbContext db = new NeoDbContext();
            PinConnection rel = new PinConnection();
            Relation<PinConnection> simple = new Relation<PinConnection> { FirstNodeId = guid3, SecondNodeId = guid4, Rel = rel };
            db.DeleteRelation(simple);
            Relation<PinConnection> test = db.ReadRelation<PinConnection>(guid3, guid4);
            Debug.WriteLine(test.Rel.Width + " " + test.Rel.Color);
            Assert.IsFalse(test.Rel.Color == "Blue");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
       public void DeleteRelation2()
        {
            NeoDbContext db = new NeoDbContext();
            db.DeleteRelation<PinConnection>(guid3, guid2);
            Relation<PinConnection> test = db.ReadRelation<PinConnection>(guid3, guid2);
            Debug.WriteLine(test.Rel.Width + " " + test.Rel.Color);
            Assert.IsFalse(test.Rel.Color == "Red");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteNodeWithRelationship()
        {
            NeoDbContext db = new NeoDbContext();
            db.DeleteNodeWithRelations(guid);
            Board boardTest = db.ReadNode<Board>(guid);
        }
        [TestMethod]
        public void ReadNodeType()
        {
            NeoDbContext db = new NeoDbContext();
            List<Board> test = (List<Board>)db.ReadNodeType<Board>();
            foreach (var item in test)
            {
                Debug.WriteLine(item.Title);
            }
            Assert.IsTrue(test.Count != 0);
        }
        [TestMethod]
        public void DeleteAllRelation()
        {
            NeoDbContext db = new NeoDbContext();
            db.DeleteAllRelation(guid5);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteNode()
        {
            NeoDbContext db = new NeoDbContext();
            db.DeleteNode(guid2);
            db.DeleteNode(guid3);
            db.DeleteNode(guid4);
            db.DeleteNode(guid5);
            db.DeleteNode(guid6);
            db.DeleteNode(guid7);
            Board boardTest = db.ReadNode<Board>(guid6);
        }
    }
}
