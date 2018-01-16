using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class UserFriendModelView
    {
        [Required]
        User UserNode { get; set; }
        [Required]
        FriendRelation Rel { get; set; }
    }
}