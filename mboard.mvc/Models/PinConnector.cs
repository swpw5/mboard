using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.mvc.Models
{
    public class PinConnector
    {
        public Pin Pin1 { get; set; }
        public Pin Pin2 { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
    }
}