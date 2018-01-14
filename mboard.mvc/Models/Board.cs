using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.mvc.Models
{
  public class Board : ICrud<Board>
  {
        public string Id { get; set; }
        public string Title { get; private set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public int List_position { get; set; }

        public string Create(Board obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Board Read(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Board obj)
        {
            throw new NotImplementedException();
        }
    }
}