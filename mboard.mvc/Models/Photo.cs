using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.mvc.Models
{
    public class Photo : ICrud<Photo>
    {
        public string Id { get; set; }
        public int IdPin { get; private set; }
        public int Height { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public string path { get; set; }

        public string Create(Photo obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Photo Read(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Photo obj)
        {
            throw new NotImplementedException();
        }
    }
}