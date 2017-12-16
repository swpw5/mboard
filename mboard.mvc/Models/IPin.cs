using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mboard.mvc.Models
{
    interface IPin
    {
        int Id { get; set; }
        string name { get; }
        string Name { get; set; }
        PVector Location { get; set; }
        PinContent Content { get; set; }
        List<Pin> Pins { get; set; }

        Board Board { get; }


         //void UpdatePins(IPin pin);
    }
}
