using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class TagRelation : IRelation
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}