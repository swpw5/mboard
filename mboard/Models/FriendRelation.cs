using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class FriendRelation : IRelation
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [DisplayName("Data wysłania:")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; } = DateTime.Now;
        public FriendsTypeRel FriendType { get; set; } = FriendsTypeRel.Invited;
    }
}