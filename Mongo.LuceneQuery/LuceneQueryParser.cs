using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Util;

namespace Mongo.LuceneQuery
{
    public static class LuceneQueryParser
    {
        private static readonly Analyzer DefaultAnalyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48, CharArraySet.EMPTY_SET);
        private static readonly QueryParser DefaultQueryParser;
        
        static LuceneQueryParser()
        {
            DefaultQueryParser = CreateParser("*");
        }

        public static void Parse(string query, string defaultField = "*")
        {
            var parser = DefaultQueryParser;

            if (query != "*")
            {
                parser = CreateParser(defaultField);
            }

            return parser.Parse(query);
        }

        private static QueryParser CreateParser(string defaultField)
        {
            return new QueryParser(LuceneVersion.LUCENE_48, defaultField, DefaultAnalyzer);
        }
    }
}
