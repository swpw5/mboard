using Neo4j.AspNet.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class User : ApplicationUser
    {
        [Display(Name = "E-mail:")]
        override public string Email { get; set; }
    }
}