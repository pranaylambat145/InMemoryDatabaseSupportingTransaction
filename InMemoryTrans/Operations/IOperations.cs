using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryTrans.Operations
{
    public interface IOperations
    {
        /*All required methods to implement SET, COUNT, DELETE, GET
         -- Implemented in Operations Class*/
        public int Get(string field);
        public void Set(string field, int value);
        public int Count(int value);
        public void Delete(string field);

    }
}
