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
        Sent,
        SentDelete,
        Received,
        ReceivedReaded,
        ReceivedDeleted
    }
}