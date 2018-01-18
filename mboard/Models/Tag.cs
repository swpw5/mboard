using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Tag :INodeModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [DisplayName("Tag:")]
        [MaxLength(50, ErrorMessage = "Wiadomość nie może być dłuższa niż 50 znaków.")]
        public string Title { get; set; }
    }
}