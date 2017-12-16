using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Neo4j.AspNet.Identity;

namespace mboard.Models
{
    public class Board : INodeModel
    {

        public string UserEmail { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public DateTime Last_mod { get; set; }
        public DateTime Created { get; set; }
        public int List_position { get; set; }


        public string GoJs { get; set; }


    }
}