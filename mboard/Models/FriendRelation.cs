using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class FriendRelation : IRelation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public FriendsTypeRel FriendType { get; set; } = FriendsTypeRel.Invited;
    }
}