using InMemoryTrans.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryTrans.Store
{
    public sealed class InMemoryStore
    {
        static InMemoryStore store;

        public Dictionary<string, int> valueStore;
        public readonly Stack<string> commandStack;
        public readonly Dictionary<string, int> tempMemory;

        private InMemoryStore()
        {
            valueStore = new Dictionary<string, int>();
            commandStack = new Stack<string>();
            tempMemory = new Dictionary<string, int>();
        }
        public static InMemoryStore assignedObject()
        {
            /*checking whether the object of the class is already created or not if yes
             then returning the same object else assigning new object
            So at the end only one object will create*/
            if(store == null)
            {
                store = new InMemoryStore();
                return store;
            }
            return store;
        }
        public void setValueStore(Dictionary<string, int> values)
        {
            valueStore.Clear();
            foreach (var entry in values)
            {
                valueStore.Add(entry.Key, entry.Value);
            }
        }
    }
}
