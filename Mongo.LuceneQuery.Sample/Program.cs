using Microsoft.Extensions.Configuration;
using Mongo.LuceneQuery;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Labs.Search;

var basePath = Path.GetFullPath("../../../");

var configuration = new ConfigurationBuilder()
    .SetBasePath(basePath)
    .AddJsonFile("appsettings.json", true)
    .AddJsonFile("appsettings.Development.json", true)
    .Build();

var mongoConfiguration = configuration["atlas:configuration"];

var mongoClient = new MongoClient(mongoConfiguration);

var mongoDatabaseName = configuration["atlas:database"];
var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);

var mongoCollectionName = configuration["atlas:collectionName"];
var mongoCollection = mongoDatabase.GetCollection<BsonDocument>(mongoCollectionName);

var builder = SearchBuilders<BsonDocument>.Search;

var results = mongoCollection.Aggregate()
    .Search(
        SearchBuilders<BsonDocument>.Search.LuceneQuery("hello AND world"
    )).ToList();

foreach (var result in results)
{
    Console.WriteLine(result.ToJson());
    Console.WriteLine("---");
}

