using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class Board : INodeModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [DisplayName("Nazwa:")]
        [MaxLength(50, ErrorMessage = "Wiadomość nie może być dłuższa niż 255 znaków.")]
        public string Title { get; set; }
        public string name { get { return Title; } }
        //public string Color { get; set; }
        //public int Height { get; set; }
        //public int Width { get; set; }
        [DisplayName("Widoczne dla przyjaciół:")]
        public bool VisibleForFriends { get; set; }
        public string DiagramData { get; set; }

        [DisplayName("Typ Diagramu:")]
        public int BoardModel { get; set; }
        //public DateTime Last_mod { get; set; }
        //public DateTime Created { get; set; }
        //public int List_position { get; set; }
    }
}