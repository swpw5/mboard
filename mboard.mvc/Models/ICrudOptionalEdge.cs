using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mboard.mvc.Models
{
    public interface ICrudOptionalEdge<T>
    {
        string CreateEdge(T obj);
        T ReadEdge(int Id);
        void UpdateEdge(T obj);
        void DeleteEdge(int Id);
    }
}
