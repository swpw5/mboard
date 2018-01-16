using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class UserBoardRelation : IRelation
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [DisplayName("Opis:")]
        [MaxLength(50, ErrorMessage = "Opis nie może być dłuższy niż 50 znaków.")]
        public string Description { get; set; }
    }
}