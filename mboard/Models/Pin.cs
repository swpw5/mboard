using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Pin
    {
        public int Id { get; private set; }
        public int IdBoard { get; private set; }
        public string Color { get; set; }
        public int Poz_X { get; set; }
        public int Poz_Y { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
    }
}