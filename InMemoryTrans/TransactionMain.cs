
using InMemoryTrans.Operations;
using InMemoryTrans.Store;
using InMemoryTrans.Transactions;

public class TransactionMain
{
    private Operationss operation;
    Transaction transac;
    public TransactionMain()
    {
        operation =  new Operationss();
        transac = new Transaction();
    }

    public void getUserInput()
    {
        Console.WriteLine("Please Enter the command");
        string inputLine = "";
        while ((inputLine = Console.ReadLine()) != null)
        {
            if (inputLine == "END")
                break;
            else
            {
                HandleUserInput(inputLine);
            }
        }
    }
    private void HandleUserInput(string inputLine)
    {
        string[] parameters = inputLine.Split(' ');
        string cmd = parameters[0];
        string field;
        int value;
       
        if ((inputLine.ToUpper().Contains("SET") || inputLine.ToUpper().Contains("DELETE"))) 
        {
            /* Making copy of input command having SET and DELETE to Rollback*/
            InMemoryStore.assignedObject().commandStack.Push(inputLine);
        }
           
        switch (cmd.ToUpper())
        {
            case "GET":
                field = parameters[1];
                Console.WriteLine(operation.Get(field) == -1 ? "NULL" : operation.Get(field));
                break;

            case "SET":
                field = parameters[1];
                value = int.Parse(parameters[2]);
                operation.Set(field, value);
                break;

            case "DELETE":
                field = parameters[1];
                operation.Delete(field);
                break;

            case "COUNT":
                value = int.Parse(parameters[1]);
                Console.WriteLine(operation.Count(value));
                break;

            case "BEGIN":
                 transac.Begin();
                break;

            case "ROLLBACK":
                transac.Rollback();
                break;

            case "COMMIT":
               transac.Commit();
                break;

            default:
                Console.WriteLine("Invalid operation: " + cmd + "\nPlease try again...");
                break;
        }
    }

    public static void Main(string[] args)
    {
        try
        {
            TransactionMain transaction = new TransactionMain();
            transaction.getUserInput();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        

    }
}