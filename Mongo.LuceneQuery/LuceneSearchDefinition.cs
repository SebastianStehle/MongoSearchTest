using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Labs.Search;

namespace Mongo.LuceneQuery
{
    public sealed class LuceneSearchDefinition<TDocument> : SearchDefinition<TDocument>
    {
        private readonly FieldDefinition<TDocument> defaultPath;
        private readonly LuceneQueryVisitor visitor;
        private readonly string query;

        public LuceneSearchDefinition(FieldDefinition<TDocument> defaultPath, string query, Func<string, string>? fieldConverter = null)
        {
            this.defaultPath = defaultPath;

            if (fieldConverter != null)
            {
                visitor = LuceneQueryVisitor.Default;
            }
            else
            {
                visitor = new LuceneQueryVisitor(fieldConverter);
            }

            this.query = query;
        }

        public override BsonDocument Render(IBsonSerializer<TDocument> documentSerializer, IBsonSerializerRegistry serializerRegistry)
        {
            var renderedField = defaultPath.Render(documentSerializer, serializerRegistry);
            var renderedQuery = LuceneQueryParser.Parse(query, renderedField.FieldName);

            var rendered = visitor.Visit(renderedQuery);

            return rendered;
        }
    }
}
