using Neo4j.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class UserPotentialFriendModelView
    {
        public User User { get; set; }
        public int FriendsInCommon { get; set; }
    }
}