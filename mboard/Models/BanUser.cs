using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class BanUser
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        [DisplayName("Do:")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}