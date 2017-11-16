using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.mvc.Models
{
    public class Note : ICrud<Note>
    {
        public string Id { get; set; }
        public int IdPin { get; private set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }

        public string Create(Note obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Note Read(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Note obj)
        {
            throw new NotImplementedException();
        }
    }
}