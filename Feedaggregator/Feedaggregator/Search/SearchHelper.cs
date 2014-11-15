using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuceneStore = Lucene.Net.Store;
using System.Configuration;
using Feedaggregator.EntityLayer;
using Feedaggregator.DataLayer;
namespace Feedaggregator.Search
{
    public class SearchHelper
    {

        string directoryPath = ConfigurationManager.AppSettings["SearchIndexDirectoryPath"];

        public void InsertFeedtoSearchIndex(FeedItemEntity feedEntity)
        {
            LuceneStore.Directory directory = LuceneStore.FSDirectory.Open(directoryPath);
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            //IndexWriter writer = new IndexWriter(directory, analyzer,false, IndexWriter.MaxFieldLength.UNLIMITED);
            using (IndexWriter writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                Document doc = new Document();
                doc.Add(new Field("ID",
                      feedEntity.Id.ToString(),
                      Field.Store.YES,
                      Field.Index.NO));

                doc.Add(new Field("TITLE",
                      feedEntity.Title,
                      Field.Store.YES,
                      Field.Index.ANALYZED));

                doc.Add(new Field("DATE",
                      feedEntity.UTCPublishDate.ToString(),
                      Field.Store.YES,
                      Field.Index.NO));

                writer.AddDocument(doc);

                writer.Optimize();
                //writer.Close();
            }
            
        }

        public void AddFeedMapping(FeedItemEntity feedEntity)
        {

            //using (DBDataContext db = new DBDataContext())
            //{
            //    List<Feed> feeds = db.Feeds.ToList();// .Where(x => x.ID > 0).ToList();//db.Feeds.Where(x => x.ID <= 7312 && x.ID>0).ToList();
            //    foreach (Feed feed in feeds)
            //    {
            List<result> searchResult = SearchTerm(ReplaceSPLCharforSearch(feedEntity.Title), SearchType.TITLE);
            List<result> searchResultScoreGraterthan1 = searchResult.Where(x => x.Score > 1.0 && x.Date > DateTime.Today.AddDays(-15)).ToList();

            foreach (result singleResult in searchResultScoreGraterthan1)
            {
                FeedMappingDAL.SetFeedMapping(feedEntity.Id, singleResult.ID);
            }
        }

        public List<result> SearchTerm(string searchText, SearchType searchType)
        {
            List<result> restultTerm = new List<result>();

            Lucene.Net.Analysis.Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, Enum.GetName(typeof(SearchType), searchType), analyzer);
            Lucene.Net.Search.Query query = parser.Parse(searchText);

            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(directoryPath);
            Lucene.Net.Search.Searcher searcher = new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(directory, true));
            TopScoreDocCollector collector = TopScoreDocCollector.Create(50, true);
            searcher.Search(query, collector);
            ScoreDoc[] hits = collector.TopDocs().ScoreDocs;

            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].Doc;
                float score = hits[i].Score;

                Lucene.Net.Documents.Document doc = searcher.Doc(docId);
                result rslt = new result();
                rslt.Score = score;
                rslt.ID = Convert.ToInt32(doc.Get("ID"));
                rslt.Title = doc.Get("TITLE");
                rslt.Date = Convert.ToDateTime(doc.Get("DATE"));
                restultTerm.Add(rslt);
                //string result = "Score: " + score.ToString() +
                //                " Field: " + doc.Get("ID") +
                //                " Field2: " + doc.Get("CONTENT");

            }
            restultTerm.Where(x => x.Score > 1.0).ToArray();
            return restultTerm;
        }

        private string ReplaceSPLCharforSearch(string searchText)
        {
            searchText = searchText.Replace("!", "");
            searchText = searchText.Replace("*", "");
            searchText = searchText.Replace(":", "");
            searchText = searchText.Replace("[", "");
            searchText = searchText.Replace("]", "");
            searchText = searchText.Replace("?", "");
            searchText = searchText.Replace("-", "");
            searchText = searchText.Replace("\"", "'");
            return searchText;
        }


        public class result
        {
            public float Score { get; set; }
            public Int32 ID { get; set; }
            public string Title { get; set; }
            public DateTime Date { get; set; }
        }


        public enum SearchType
        {
            TITLE,
            CONTENT,
            ID,
            DATE
        }
    }
}
