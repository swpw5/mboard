using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Message : INodeModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserTo { get; set; }
        public string UserFrom { get; set; }
    }
}