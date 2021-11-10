using MongoDB.Driver;
using MongoDB.Labs.Search;
using System.Linq.Expressions;

#pragma warning disable IDE0060 // Remove unused parameter

namespace Mongo.LuceneQuery
{
    public static class LuceneSearchDefinitionExtensions
    {
        public static LuceneSearchDefinition<TDocument> LuceneQuery<TDocument>(this SearchDefinitionBuilder<TDocument> builder,
            string query, 
            string defaultPath = "*",
            Func<string, string>? fieldConverter = null)
        {
            return new LuceneSearchDefinition<TDocument>(defaultPath, query, fieldConverter);
        }

        public static LuceneSearchDefinition<TDocument> LuceneQuery<TDocument>(this SearchDefinitionBuilder<TDocument> builder,
            string query,
            FieldDefinition<TDocument> defaultPath,
            Func<string, string>? fieldConverter = null)
        {
            return new LuceneSearchDefinition<TDocument>(defaultPath, query, fieldConverter);
        }

        public static LuceneSearchDefinition<TDocument> LuceneQuery<TDocument, TField>(this SearchDefinitionBuilder<TDocument> builder,
            string query,
            Expression<Func<TDocument, TField>> defaultPath,
            Func<string, string>? fieldConverter = null)
        {
            return new LuceneSearchDefinition<TDocument>(new ExpressionFieldDefinition<TDocument>(defaultPath), query, fieldConverter);
        }
    }
}
