using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Message : INodeModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [DisplayName("Data wysłania:")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Tytuł:")]
        [MaxLength(50, ErrorMessage = "Wiadomość nie może być dłuższa niż 50 znaków.")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Treść:")]
        [MaxLength(255, ErrorMessage = "Wiadomość nie może być dłuższa niż 255 znaków.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Required]
        [DisplayName("Od:")]
        public string UserTo { get; set; }
        [Required]
        [DisplayName("Do:")]
        public string UserFrom { get; set; }
    }
}