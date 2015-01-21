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

using SVIMPH.Shared.DataLayer;

namespace DBArchival
{
    class Program
    {
         
        static void Main(string[] args)
        {
            try
            {

                SqlHelper.ExecuteDataset(Common.AuditCon, CommandType.StoredProcedure, "SP_SyncDB");
                /*
                string FieldID = string.Empty;

                LuceneStore.Directory directory = LuceneStore.FSDirectory.Open(Common.directoryPath);
                Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                using (IndexWriter w = new IndexWriter(directory,analyzer,IndexWriter.MaxFieldLength.UNLIMITED ))
                {
                    Term trm = new Term("ID",FieldID);
                    w.DeleteDocuments(trm);
                    //w.Commit();
                    w.Optimize();
                }
                 * */
            }
            catch (Exception ex)
            {
                //throw;
            }
            
        }
    }
}
