using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class TagModelView : Tag
    {
        public string TableId { get; set; }
    }
}