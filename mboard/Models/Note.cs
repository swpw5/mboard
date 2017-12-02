using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Note
    {
        public string Id { get; set; }
        public int IdPin { get; }
        public string Color { get; set; }
        public int Height { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
    }
}