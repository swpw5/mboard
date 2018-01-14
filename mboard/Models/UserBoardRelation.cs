using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class UserBoardRelation : IRelation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
    }
}