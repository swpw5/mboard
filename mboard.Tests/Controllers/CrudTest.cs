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
    public class CrudTest
    {
        const int guid = 184;
        [TestMethod]
        public void Create()
        {
            Board board = new Board() {Id = guid, Title = "Nazwa", Color = "Red", Height = 111, Width= 222, Created = DateTime.Now, Last_mod=DateTime.Now, List_position=2};
            Crud crud = new Crud();
            crud.Create(board);
        }
        [TestMethod]
        public void ReadAfterCreate()
        {
            Board board = new Board();
            Crud crud = new Crud();
            board = crud.Read<Board>(guid);
            Debug.WriteLine(board.Title);
        }
        /*[TestMethod]
        public void UpdateAll()
        {
            Board board = new Board() {Title = "Nazwa123", Color = "Red", Height = 111, Width = 222, Created = DateTime.Now, Last_mod = DateTime.Now, List_position = 2 }; ;
            Crud crud = new Crud();
            crud.UpdateAll(guid ,board);
            Debug.WriteLine(board.Title);
        }

        [TestMethod]
        public void Delete()
        {
            Crud crud = new Crud();
            crud.Delete<Board>(guid);
        }
        [TestMethod]
        public void ReadAfterDelete()
        {
            Board board = new Board();
            Crud crud = new Crud();
            board = crud.Read<Board>(guid);
            Debug.WriteLine(board.Title);
        }*/
    }
}
