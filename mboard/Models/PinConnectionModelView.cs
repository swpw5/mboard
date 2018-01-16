using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class PinConnectionModelView : PinConnection
    {
        public string FirstNodeId { get; set; }
        public string SecondNodeId { get; set; }
    }
}