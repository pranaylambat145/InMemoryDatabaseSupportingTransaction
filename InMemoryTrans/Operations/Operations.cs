using InMemoryTrans.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace InMemoryTrans.Operations
{
    public class Operationss : IOperations
    {
       /* Instanciated store using Singleton Design Patter
        so that only one object will create of InMemoryStore class*/
        InMemoryStore store = InMemoryStore.assignedObject();
       
        public int Get(string variable)
        {
            if (store.valueStore.ContainsKey(variable))
            {
                return store.valueStore[variable];
            }
            return -1;
        }

        public void Set(string variable, int value)
        {
            /* Maintaining tempMemory and valueStore Dictionary in store where valueStore will conatains the final data
             and tempMemory will help to do the transactions*/
                if (!store.tempMemory.ContainsKey(variable))
                {
                    store.tempMemory.Add(variable, value);
                }
                else 
                {
                    store.tempMemory[variable] = value;

                }
                if (!store.valueStore.ContainsKey(variable))
                {
                store.valueStore.Add(variable, value);
                }
                else
                {
                store.valueStore[variable] = value;
                }
            
        }

        public int Count(int value)
        {
            int count = 0;
            foreach (var entry in store.valueStore)
            {
                if (entry.Value == value)
                {
                    count++;
                }
            }
            return count;
        }

        public void Delete(string field)
        {
            store.valueStore.Remove(field);
        }
    }
}
