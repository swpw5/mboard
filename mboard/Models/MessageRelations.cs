using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class MessageRelations
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public MessageTypeRel MesType { get; set; }
    }
}