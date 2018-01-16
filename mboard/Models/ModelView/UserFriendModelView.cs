using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class UserFriendModelView
    {
        User UserNode { get; set; }
        FriendRelation Rel { get; set; }
    }
}