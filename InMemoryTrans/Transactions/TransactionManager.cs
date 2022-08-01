using InMemoryTrans.Operations;
using InMemoryTrans.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryTrans.Transactions
{
    public class Transaction : ITransactionManager
    {

        /* keyValueCache will conatin the keys and values of Begin block*/
        private readonly Dictionary<string,int> keyValueCache;

        /* stackInputCache will contain the input command having SET and DELETE to Rollback*/
        private readonly Stack<string> stackInputCache;

        /* Instanciated store using Singleton Design Patter
        so that only one object will create of InMemoryStore class*/
        InMemoryStore store = InMemoryStore.assignedObject();

        public Transaction()
        {
            keyValueCache = new Dictionary<string, int>();
            stackInputCache = new Stack<string>();
        }

        public void Begin()
        {
            try
            {
                /*Making copy of tempMemory to keyValueCache*/
                foreach (var entry in store.tempMemory)
                {
                    if (!keyValueCache.ContainsKey(entry.Key))
                        keyValueCache.Add(entry.Key, entry.Value);
                    else keyValueCache[entry.Key] = entry.Value;
                }

                /*Making copy of commandStack to stackInputCache having SET and DELETE to Rollback*/
                foreach (var entry in store.commandStack)
                {
                    stackInputCache.Push(entry);
                }

                /*clearing tempMemory and commandStack so that keyValueCache and stackInputCache
                 should contain the data of one Begin Block at a time*/
                store.tempMemory.Clear();
                store.commandStack.Clear();
                //store.tempMemory.Add("BEGIN", 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        
        }

        public void Commit()
        {
            try
            {
                /* Setting to ValueStore(final) and clearing the data*/
                store.commandStack.Clear();
                store.setValueStore(store.tempMemory);
                store.tempMemory.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Rollback()
        {
            try
            {
                int stackLength = store.commandStack.Count;
                if (stackLength !=0)
                {
                    /*Code to Rollback SET or updated value
                         */
                    for (int i = 0; i < stackLength; i++)
                    {
                        
                        string lastValue = "";
                        string lastCommand = "";
                        if (store.commandStack.Count != 0)
                        {
                            lastCommand = store.commandStack.Pop();
                        }
                        else
                        {
                            lastCommand = stackInputCache.Pop();
                        }

                        if (store.commandStack.Count != 0)
                        {
                            lastValue = store.commandStack.Pop();
                        }
                        else
                        {
                            if (stackInputCache.Count != 0)
                            {
                                lastValue = stackInputCache.Pop();
                            }
                        }
                        store.commandStack.Push(lastValue);
                        string[] parameters = lastCommand.Split(' ');
                        string[] value = new string[4 - 1];
                        value = lastValue.Split(' ');
                        string cmd = parameters[0];
                        if (cmd.ToUpper() == "SET")
                        {
                            if (value[0] != "" && parameters[1] == value[1])
                            {
                                store.tempMemory[parameters[1]] = int.Parse(value[2]);
                            }
                            else
                            {
                                store.tempMemory.Remove(parameters[1]);
                            }
                        }
                    }
                    foreach (var entry in keyValueCache)
                    {
                        if (!store.tempMemory.ContainsKey(entry.Key))
                            store.tempMemory.Add(entry.Key, entry.Value);
                    }
                    keyValueCache.Clear();
                    store.setValueStore(store.tempMemory);
                }
                else
                {
                    Console.WriteLine("NO TRANSACTION");

                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

