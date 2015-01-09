using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string url = "http://192.168.1.147/BollywoodGossip/BGDataService.aspx?Method=GetFeeds";// "http://search.twitter.com/search.json?q=xamarin&rpp=10&include_entities=false&result_type=mixed";

            //WebClient wc = new WebClient();
            //Stream s = wc.OpenRead(url);
            //if (s != null)
            //{
            //    //StreamReader reader = new StreamReader(s);
            //    //string rss = reader.ReadToEnd();
            //    var j = (JsonObject)JsonObject.Load(s);
            //    var results = (from result in  (JsonArray)j["Feeds"]["Feed"]
            //                   let jResult = result as JsonObject
            //                   select new { title = jResult["title"], id = jResult["id"], imageurl = jResult["imageurl"], publishdate = jResult["publishdate"] });

            //}



            string url = "http://89.255.130.196:8090/bgdata.asmx/GetFeedsXML";// "http://search.twitter.com/search.json?q=xamarin&rpp=10&include_entities=false&result_type=mixed";

            //List<LineItem> tl = new List<LineItem>();

            using (WebClient wc = new WebClient())
            {
                using (Stream s = wc.OpenRead(url))
                {
                    if (s != null)
                    {
                        XDocument xmlDoc = XDocument.Load(s);
                        var files = (from x in xmlDoc.Root.Elements("Feed")
                                 select
                                 new 
                                 {
                                     Server = (string)x.Element("id").Value ?? string.Empty,
                                     Drive = (string)x.Element("title").Value ?? string.Empty,
                                     Folder = (string)x.Element("imageurl").Value ?? string.Empty,
                                     FileName = (string)x.Element("publishdate").Value ?? string.Empty,
                                     SendMail = (string)x.Element("pubtime").Value ?? string.Empty,
                                     FullPath = x.Element("sourcename").Value ?? string.Empty
                                     
                                 }).ToList();
                    }
                }
            }

            //WebClient wc = new WebClient();
            //Stream s = null;
            //try
            //{

            //    s = wc.OpenRead(url);
            //    if (s != null)
            //    {
            //        var j = (JsonObject)JsonObject.Load(s);
            //        var results = (from result in (JsonArray)j["Feeds"]["Feed"]
            //                       let jResult = result as JsonObject
            //                       select new { title = jResult["title"], id = jResult["id"], imageurl = jResult["imageurl"], publishdate = jResult["publishdate"] });

            //        //tl = results as List<LineItem>;

            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    //s.Close();
            //    //s.Dispose();
            //    //wc.Dispose();
            //}

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search frmsearch = new Search();
            frmsearch.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TimeZone frmTimeZone = new TimeZone();
            frmTimeZone.Show();
        }

       
    }
}
