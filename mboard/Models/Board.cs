using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    

    public class Board : INodeModel
    {

            public string Id { get; set; } = Guid.NewGuid().ToString();
            
            [DisplayName("Nazwa")]
            public string Title { get; set; }
       
            public  string name { get { return Title; } }
            public bool VisibleForFriends { get; set; } = true;
            public string DiagramData { get; set; }
            public int BoardModel { get; set; }
                                         
    }
}