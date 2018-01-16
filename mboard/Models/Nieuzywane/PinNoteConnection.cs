using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class PinNoteConnection : IRelation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}