using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Pin : INodeModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Color { get; set; }
        public int Poz_X { get; set; }
        public int Poz_Y { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
    }
    public class PinConnection
    {
        public string Color { get; set; }
        public int Width { get; set; }
    }
}