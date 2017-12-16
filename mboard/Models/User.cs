using Neo4j.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mboard.Models
{
    public class User : ApplicationUser
    {

        public string name { get { return base.Email; } }
    }
}