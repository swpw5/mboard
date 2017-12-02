using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Photo
    {
        public string Id { get; set; }
        public int IdPin { get; private set; }
        public int Height { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public string path { get; set; }
    }
}