using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class BoardModelViewTEST
    {
        public Board Board { get; set; }
        public List<RelationWithNodes<Pin,Note,PinNoteConnection>> PinNote { get; set; }
        public List<PinConnectionModelView> PinToPin  { get; set; }
    }
}