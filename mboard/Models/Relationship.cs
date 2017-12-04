using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mboard.Models
{
    public virtual class Relationship : IRelationship
    {
        public int IdFirst { get; set; }
        public int IdSecond { get; set; }
    }
}
