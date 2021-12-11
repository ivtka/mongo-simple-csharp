using MongoDB.Driver;

namespace MongoDBSimple
{
  class Program
  {
    static void Main(string[] args)
    {
      var mongo = new MongoDB(new MongoClient("mongodb+srv://admin:admin@cluster0.eqn0h.mongodb.net/vetclinic?retryWrites=true&w=majority"), "Bank");
      string option = "0";
      while (option != "3")
      {
        Console.WriteLine("[1] Show Collection");
        Console.WriteLine("[2] Insert to Collection");
        Console.WriteLine("[3] Exit");
        Console.Write("> ");
        option = Console.ReadLine();
        if (option == "3") break;
        else if (option == "2")
        {
          string? collection = ReadCollection();
          mongo.Insert(collection);
        }
        else if (option == "1")
        {
<<<<<<< HEAD
          string? collection = ReadCollection();
||||||| e266b50
          Console.WriteLine("Enter name of Collection to Show: <Customers> | <TypesLoans> | <ProvidedLoans>");
          Console.Write("> ");
          string? collection = Console.ReadLine();
          while (collection != "Customers" || collection != "TypesLoans"
            || collection != "ProvidedLoans")
          {
            if (collection == "Customers" || collection == "TypesLoans" || collection == "ProvidedLoans") break;
            Console.Write("> "); collection = Console.ReadLine();
          }
=======
          Console.WriteLine("Enter name of Collection to Show: <Customers> | <TypesLoans> | <ProvidedLoans>");
          Console.Write("> ");
          string? collection = Console.ReadLine();
          while (collection != "Customers" || collection != "TypesLoans" || collection != "ProvidedLoans")
          {
            if (collection == "Customers" || collection == "TypesLoans" || collection == "ProvidedLoans") break;
            Console.Write("> "); collection = Console.ReadLine();
          }
>>>>>>> 7d7eae2087c85c2c608f9697892822fd9f68f810
          mongo.Show(collection);
        }
        else
        {
          Console.WriteLine("> Not found this option.");
        }
      }
    }

    static public string ReadCollection()
    {
      Console.WriteLine("Enter name of Collection to Show: <Customers> | <TypesLoans> | <ProvidedLoans>");
      Console.Write("> ");
      string? collection = Console.ReadLine();
      while (collection != "Customers" || collection != "TypesLoans"
        || collection != "ProvidedLoans")
      {
        if (collection == "Customers" || collection == "TypesLoans" || collection == "ProvidedLoans") return collection;
        Console.Write("> "); collection = Console.ReadLine();
      }

      return null;
    }
  }
}
