# MongoSearchTest

This Repository extends the C# extension for Mongo Atlas search: https://github.com/mongodb-labs/mongo-csharp-search/

## Why?

The mongo search supports the query_string operator, which can parse a query. But this query is a little bit strange:

1. It seems to be a custom format and not the lucene format.
2. It does not support fuzzy or prefix search.
3. You cannot use wildcard paths.

## What?

This repository uses Lucene query parser to convert a lucene string to a Mongo atlas operator.

## How to use it?

```
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
```