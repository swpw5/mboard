using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mboard.mvc.Models
{
    public interface ICrud<T>
    {
        string Create(T obj);
        T Read(int Id);
        void Update(T obj);
        void Delete(int Id);
    }
}
