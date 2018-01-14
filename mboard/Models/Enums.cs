using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public enum FriendsTypeRel
    {
        Invited,
        Friend,
        Blocked
    }

    public enum MessageTypeRel
    {
        Send,
        SendDelete,
        Received,
        ReceivedReaded,
        ReceivedDeleted
    }
}