using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.mvc.Models
{
    public class Pin : ICrudOptionalEdge<Pin>
    {
        public int Id { get; private set; }
        public int IdBoard { get; private set; }
        public string Color { get; set; }
        public int Poz_X { get; set; }
        public int Poz_Y { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public List<PinConnector> ConnectedPin { get; set; }

        public string Create(Board obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Board Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Board obj)
        {
            throw new NotImplementedException();
        }

        public string CreateEdge(Pin obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteEdge(int Id)
        {
            throw new NotImplementedException();
        }

        public Pin ReadEdge(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEdge(Pin obj)
        {
            throw new NotImplementedException();
        }
    }
}