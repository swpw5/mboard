using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Note : INodeModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Color { get; set; }
        public int Height { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
    }
}