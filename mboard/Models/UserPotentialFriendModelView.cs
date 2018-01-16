using Neo4j.AspNet.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class UserPotentialFriendModelView
    {
        public User User { get; set; }
        [Required]
        [DisplayName("Wspólni znajomi:")]
        public int FriendsInCommon { get; set; }
    }
}