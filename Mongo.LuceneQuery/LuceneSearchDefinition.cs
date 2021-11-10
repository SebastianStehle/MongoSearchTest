using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Util;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Labs.Search;

namespace Mongo.LuceneQuery
{
    public sealed class LuceneSearchDefinition<TDocument> : SearchDefinition<TDocument>
    {
        private static readonly Analyzer DefaultAnalyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        private readonly FieldDefinition<TDocument> defaultPath;
        private readonly LuceneQueryVisitor visitor;
        private readonly string query;

        public LuceneSearchDefinition(FieldDefinition<TDocument> defaultPath, string query, Func<string, string>? fieldConverter = null)
        {
            this.defaultPath = defaultPath;
            this.query = query;

            visitor = new LuceneQueryVisitor(fieldConverter);
        }

        public override BsonDocument Render(IBsonSerializer<TDocument> documentSerializer, IBsonSerializerRegistry serializerRegistry)
        {
            var renderedField = defaultPath.Render(documentSerializer, serializerRegistry);

            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, renderedField.FieldName, DefaultAnalyzer);
            var queryResult = queryParser.Parse(query);

            var rendered = visitor.Visit(queryResult);

            return rendered;
        }
    }
}
