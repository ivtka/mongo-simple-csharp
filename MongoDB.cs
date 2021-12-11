using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDBSimple
{

  abstract class Query
  {
    public abstract ICollection Create();

    public void Insert(IMongoDatabase db)
    {
      var collection = Create();
      var cursor = db.GetCollection<BsonDocument>(collection.Collection());
      cursor.InsertOne(collection.Document(db));
    }

    public void Show(IMongoDatabase db)
    {
      var collection = Create();
      var cursor = db.GetCollection<BsonDocument>(collection.Collection());
      var objects = cursor.Find(new BsonDocument()).Project("{_id: 0}").ToList();
      objects.ForEach(doc =>
      {
        Console.WriteLine(doc);
      });
    }

    public ObjectId Find(IMongoDatabase db, BsonDocument doc)
    {
      var collection = Create();
      var cursor = db.GetCollection<BsonDocument>(collection.Collection());
      var obj = cursor.Find(doc).ToList();
      return ObjectId.Parse(obj[0].GetValue("_id").ToString());
    }
  };

  class CustomersQuery : Query
  {
    public override ICollection Create()
    {
      return new CustomersCollection();
    }
  };

  class ProvidedLoansQuery : Query
  {
    public override ICollection Create()
    {
      return new ProvidedLoansCollection();
    }
  }

  class TypesLoansQuery : Query
  {
    public override ICollection Create()
    {
      return new TypesLoansCollection();
    }
  }

  abstract class ICollection
  {
    public abstract string Collection();

    public abstract BsonDocument Document(IMongoDatabase db);
  };

  class CustomersCollection : ICollection
  {
    public override string Collection()
    {
      return "Customers";
    }

    public override BsonDocument Document(IMongoDatabase db)
    {
      Console.WriteLine("Enter surname: ");
      string? surname = Console.ReadLine();
      Console.WriteLine("Enter name: ");
      string? name = Console.ReadLine();
      Console.WriteLine("Enter patronymic: ");
      string? patronymic = Console.ReadLine();
      Console.WriteLine("Enter passport_series: ");
      string? passport_series = Console.ReadLine();
      Console.WriteLine("Enter phone: ");
      string? phone = Console.ReadLine();
      Console.WriteLine("Enter address: ");
      string? address = Console.ReadLine();
      Console.WriteLine("Enter salary: ");
      string? salary = Console.ReadLine();

      return new BsonDocument {
        {"surname", surname}, {"name", name},
        {"patronymic", patronymic},
        {"passport_series", passport_series},
        {"phone", phone},
        {"address", address},
        {"salary", Int32.Parse(salary)}
      };
    }
  }

  class ProvidedLoansCollection : ICollection
  {
    public override string Collection()
    {
      return "ProvidedLoans";
    }

    public override BsonDocument Document(IMongoDatabase db)
    {
      Console.WriteLine("Enter client's passport_series: ");
      string? passport_series = Console.ReadLine();
      Console.WriteLine("Enter type of loan: ");
      string? type_loan = Console.ReadLine();
      Console.WriteLine("Enter term: ");
      string? term = Console.ReadLine();
      Console.WriteLine("Enter sum: ");
      string? sum = Console.ReadLine();
      Console.WriteLine("Enter mark: ");
      string? mark = Console.ReadLine();

      ObjectId client = new CustomersQuery().Find(db, new BsonDocument{
        {"passport_series", passport_series}
      });
      ObjectId loan = new TypesLoansQuery().Find(db, new BsonDocument{
        {"name", type_loan}
      });

      return new BsonDocument {
        {"client", client},
        {"loan", loan},
        {"date", DateTime.UtcNow},
        {"term", term},
        {"return_date", DateTime.UtcNow.AddYears(5)},
        {"sum", Double.Parse(sum)},
        {"mark", mark}
      };
    }
  }

  class TypesLoansCollection : ICollection
  {
    public override string Collection()
    {
      return "TypesLoans";
    }

    public override BsonDocument Document(IMongoDatabase db)
    {
      Console.WriteLine("Enter name: ");
      string? name = Console.ReadLine();
      Console.WriteLine("Enter rate: ");
      string? rate = Console.ReadLine();
      Console.WriteLine("Enter terms: ");
      string? terms = Console.ReadLine();

      return new BsonDocument {
        {"name", name},
        {"rate", Double.Parse(rate)},
        {"terms", terms}
      };
    }
  }

  class MongoDB
  {
    private MongoClient _dbClient;
    private IMongoDatabase _db;

    private void Insert(Query query)
    {
      query.Insert(_db);
    }

    private void Show(Query query)
    {
      query.Show(_db);
    }

    public MongoDB(MongoClient dbClient, string database)
    {
      _dbClient = dbClient;
      _db = _dbClient.GetDatabase(database);
    }

    public void Insert(string collection)
    {
      if (collection == "Customers")
      {
        Insert(new CustomersQuery());
      }
      else if (collection == "ProvidedLoans")
      {
        Insert(new ProvidedLoansQuery());
      }
      else if (collection == "TypesLoans")
      {
        Insert(new TypesLoansQuery());
      }
    }

    public void Show(string collection)
    {
      if (collection == "Customers")
      {
        Show(new CustomersQuery());
      }
      else if (collection == "ProvidedLoans")
      {
        Show(new ProvidedLoansQuery());
      }
      else if (collection == "TypesLoans")
      {
        Show(new TypesLoansQuery());
      }
    }
  }
}