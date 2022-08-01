using InMemoryTrans.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryTrans.Transactions
{
     public interface ITransactionManager
    {
        /* All required Transaction Methods implemented in TransactionManager class*/
        public void Begin();
        public void Commit();
        public void Rollback();
    }
}
