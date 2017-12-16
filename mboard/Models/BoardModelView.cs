using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class BoardModelView
    {
        public Board Board { get; set; }
        public List<RelationWithNodes<Pin,Note,PinNoteConnection>> PinNote { get; set; }
        //public List<PinModelView> PinToPin  { get; set; }



    }
}