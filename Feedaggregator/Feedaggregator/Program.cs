using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Data;
using Feedaggregator.EntityLayer;
using Feedaggregator.BusinessLayer;
using System.Text.RegularExpressions;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;
using Feedaggregator.Search;
using System.Configuration;
using SVIMPH.Shared;
namespace Feedaggregator
{
    class Program
    {
        //test again just
        #region Variable
        static string strTitle = "Title";
        static string strRedirectURL = "RedirectURL";
        static string strDescription = "Description";
        static string strPublishDate = "PublishDate";

        #endregion

        private static void Main(string[] args)
        {
            try
            {
                GetFeedSource();
            }
            catch (Exception ex)
            {
                SVIMPH.Shared.EntityLayer.ErrorLogEntity errorLogentity = new SVIMPH.Shared.EntityLayer.ErrorLogEntity();
                errorLogentity.ErrorDescription = ex.Message;
                errorLogentity.Exception= ex.InnerException.ToString();
                errorLogentity.Stacktrace = ex.StackTrace;
                errorLogentity.ErrorSource = "Feedaggregator - " + ex.Source;
                errorLogentity.ErrorMetadata = ex.Source;
                SVIMPH.Shared.BusinessLayer.ErrorLogDTO.Insert(errorLogentity);
                //throw ex;
            }
        }

        private static void GetFeedSource()
        {
            try
            {
                //Read all active feed source
                List<FeedSourceEntity> feedSourceEntity = FeedSourceDTO.GetList();

                using (WebClient client = new WebClient())
                {
                    //Get data for one by one each active feed source to insert items on the database
                    foreach (FeedSourceEntity fse in feedSourceEntity)
                    {
                        try
                        {
                            var feed = fse.SourceURL;
                            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                            Stream data = client.OpenRead(feed);
                            if (data != null)
                            {
                                StreamReader reader = new StreamReader(data);
                                string rssOutput = reader.ReadToEnd();

                                rssOutput = EscapeXMLValue(rssOutput);
                                if (Common.SaveXML.Equals("1"))
                                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + fse.SourceName + ".xml", rssOutput);
                                ReadXML(fse, rssOutput);
                                reader.Close();
                            }
                            data.Close();
                        }
                        catch (Exception ex)
                        {
                            
                            SVIMPH.Shared.EntityLayer.ErrorLogEntity errorLogentity = new SVIMPH.Shared.EntityLayer.ErrorLogEntity();
                            errorLogentity.ErrorDescription = ex.Message;
                            errorLogentity.Exception= ex.InnerException.ToString();
                            errorLogentity.Stacktrace = ex.StackTrace;
                            errorLogentity.ErrorSource = ex.Source;
                            errorLogentity.ErrorMetadata = "Feedaggregator - GetFeedSource";
                            SVIMPH.Shared.BusinessLayer.ErrorLogDTO.Insert(errorLogentity);
                            //throw ex;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static void ReadXML(FeedSourceEntity fse, string rssOutput)
        {
            try
            {
                IEnumerable<XElement> items;
                try
                {
                    XElement xelement = XElement.Parse(rssOutput);
                    items = xelement.Elements(@"channel").Elements("item");//.Elements("title");
                }
                catch (Exception ex)
                {
                    //If exceprion occure while rading the xml data then move to the next source
                    Console.Write("Error while parsing data for " + fse.SourceName + " Error: " + ex.Message);
                    SVIMPH.Shared.EntityLayer.ErrorLogEntity errorLogentity = new SVIMPH.Shared.EntityLayer.ErrorLogEntity();
                    errorLogentity.ErrorDescription = ex.Message;
                    errorLogentity.Exception = ex.InnerException.ToString();
                    errorLogentity.Stacktrace = ex.StackTrace;
                    errorLogentity.ErrorSource = ex.Source;
                    errorLogentity.ErrorMetadata = "Feedaggregator - ReadXML";
                    SVIMPH.Shared.BusinessLayer.ErrorLogDTO.Insert(errorLogentity);
                    return;
                }

                GetFeedItems(fse, items);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //fse.Id
        }

        private static void GetFeedItems(FeedSourceEntity fse, IEnumerable<XElement> items)
        {
            //XNamespace content;
            XNamespace descContent;
            SourceColumnNameEntity sourceColumnName;

            //Get source columns for the reading data
            try
            {

                sourceColumnName = FillSourceCoumnName(fse);
                descContent = fse.DescriptionNameSpace;
            }
            catch (Exception ex)
            {
                //If exceprion occure while rading the xml data then move to the next source
                Console.Write("Error while getting source column for " + fse.SourceName + " Error: " + ex.Message);
                SVIMPH.Shared.EntityLayer.ErrorLogEntity errorLogentity = new SVIMPH.Shared.EntityLayer.ErrorLogEntity();
                errorLogentity.ErrorDescription = ex.Message;
                errorLogentity.Exception = ex.InnerException.ToString();
                errorLogentity.Stacktrace = ex.StackTrace;
                errorLogentity.ErrorSource = ex.Source;
                errorLogentity.ErrorMetadata = "Feedaggregator - FillSourceCoumnName";
                SVIMPH.Shared.BusinessLayer.ErrorLogDTO.Insert(errorLogentity);
                return;
            }

            string strImageURL = string.Empty;
            foreach (var item in items)
            {
                FeedItemEntity feedItemEntity = new FeedItemEntity();

                try
                {
                    //Fill feed Title
                    feedItemEntity.Title = GetPlainTextFromHtml(HttpUtility.HtmlDecode(item.Element(sourceColumnName.Title).Value));

                    //Fill feed description
                    if (fse.DescriptionNameSpace != string.Empty)
                    {
                        feedItemEntity.Description = GetPlainTextFromHtml(HttpUtility.HtmlDecode(item.Element(descContent + sourceColumnName.Description).Value));
                    }
                    else
                    {
                        feedItemEntity.Description = GetPlainTextFromHtml(HttpUtility.HtmlDecode(item.Element(sourceColumnName.Description).Value));
                    }

                    //HtmlDecode again to remmove &#8216; and other symbole
                    feedItemEntity.Description = HttpUtility.HtmlDecode(feedItemEntity.Description);

                    //Fill feed Publish Dat
                    //DateTime publishdatetest = DateTimeOffset.Parse(item.Element(sourceColumnName.PublishDate).Value).UtcDateTime;
                    //DateTime publishdate = Convert.ToDateTime(item.Element(sourceColumnName.PublishDate).Value);
                    DateTime publishdate = DateTimeOffset.Parse(item.Element(sourceColumnName.PublishDate).Value).DateTime;
                    feedItemEntity.PublishDate = publishdate;

                    ////get publish date and convert date to UTC date based on timezone
                    //publishdate = DateTime.SpecifyKind(publishdate, DateTimeKind.Unspecified);
                    publishdate = DateTime.SpecifyKind(publishdate, DateTimeKind.Unspecified);
                    if (string.IsNullOrEmpty(fse.Timezone))
                        fse.Timezone = Feedaggregator.Common.DefaultTimezone;//if no time zone added then defaulted to india standard time
                    //DateTime UTCPublishdate = TimeZoneInfo.ConvertTimeToUtc(publishdate, TimeZoneInfo.FindSystemTimeZoneById(fse.Timezone));
                    DateTime UTCPublishdate = TimeZoneInfo.ConvertTimeToUtc(publishdate, TimeZoneInfo.FindSystemTimeZoneById(fse.Timezone));
                    feedItemEntity.UTCPublishDate = UTCPublishdate;
                    
                    //Fill feed Redirect URL
                    feedItemEntity.RedirectURL = item.Element(sourceColumnName.RedirectURL).Value;
                    //Fill feed Source
                    feedItemEntity.Source = fse.Id;
                    ////Fill feed Active or Inactive
                    feedItemEntity.Active = true;
                    //Fill feed Source XML
                    feedItemEntity.FeedSourceXML = string.Empty;
                    //Fill feed Image
                    strImageURL = GetImageURL(fse, item);
                    feedItemEntity.ImageURL = strImageURL;
                    FeedItemDTO.Insert(ref feedItemEntity);
                    
                    //add search and releted feed mapping
                    if (Feedaggregator.Common.GenerateSearchIndex.Equals("1"))
                    {
                        SearchHelper search = new SearchHelper();
                        search.InsertFeedtoSearchIndex(feedItemEntity);
                        search.AddFeedMapping(feedItemEntity);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("Error while inserting data into database for " + fse.SourceName + " Error: " + ex.Message);
                    SVIMPH.Shared.EntityLayer.ErrorLogEntity errorLogentity = new SVIMPH.Shared.EntityLayer.ErrorLogEntity();
                    errorLogentity.ErrorDescription = ex.Message;
                    errorLogentity.Exception = ex.InnerException.ToString();
                    errorLogentity.Stacktrace = ex.StackTrace;
                    errorLogentity.ErrorSource = fse.SourceName  + " - " + ex.Source;
                    errorLogentity.ErrorMetadata = "Feedaggregator - GetFeedItems-"+ fse.SourceName +"-Insert";
                    SVIMPH.Shared.BusinessLayer.ErrorLogDTO.Insert(errorLogentity);
                }
            }

        }

        private static string GetImageURL(FeedSourceEntity fse, XElement item)
        {

            XNamespace content;
            string strImagePath = string.Empty;
            string strImageURL = string.Empty;
            try
            {
                strImagePath = fse.ImageSource;
                content = fse.ImageNameSpace;
                HtmlDocument doc = new HtmlDocument();
                string[] strPathSplit = strImagePath.Split('\\');

                if (strImagePath != "")
                {

                    for (int i = 0; i < strPathSplit.Length; i++)
                    {
                        if (i == 0 && i < strPathSplit.Length - 1)
                        {
                            if (fse.ImageNameSpace != string.Empty)
                            {
                                doc.LoadHtml((HttpUtility.HtmlDecode(item.Element(content + strPathSplit[0]).Value)));
                            }
                            else
                            {
                                doc.LoadHtml((HttpUtility.HtmlDecode(item.Element(strPathSplit[0]).Value)));
                            }
                        }
                        else if (strPathSplit.Length == 1)
                        {
                            if (strPathSplit[0].Contains("[@"))
                            {
                                string strElement;
                                string strAttribute;
                                strElement = strPathSplit[0].Substring(0, strPathSplit[0].IndexOf("[@"));
                                strAttribute = strPathSplit[0].Substring(strPathSplit[0].IndexOf("[@") + 2, (strPathSplit[0].LastIndexOf("]")) - (strPathSplit[0].IndexOf("[@") + 2));
                                if (fse.ImageNameSpace == string.Empty)
                                {
                                    if (item.Element(strElement) != null && item.Element(strElement).HasAttributes)
                                    {
                                        XAttribute att = item.Element(strElement).Attribute(strAttribute);
                                        if (att != null)
                                        {
                                            strImageURL = att.Value;
                                        }
                                    }
                                }
                                else
                                {
                                    if (item.Element(content + strElement).HasAttributes)
                                    {
                                        XAttribute att = item.Element(content + strElement).Attribute(strAttribute);
                                        if (att != null)
                                        {
                                            strImageURL = att.Value;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (fse.ImageNameSpace == string.Empty)
                                {
                                    if (item.Element(strPathSplit[0]) != null)
                                        strImageURL = (HttpUtility.HtmlDecode(item.Element(strPathSplit[0]).Value));
                                }
                                else
                                {
                                    if (item.Element(content + strPathSplit[0]) != null)
                                        strImageURL = (HttpUtility.HtmlDecode(item.Element(content + strPathSplit[0]).Value));
                                }
                            }

                            break;
                        }

                        if (i == strPathSplit.Length - 1)
                        {
                            if (doc.DocumentNode.SelectSingleNode("//" + strPathSplit[1]) != null)
                            {
                                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//" + strPathSplit[1]))
                                {
                                    HtmlAttribute att = link.Attributes["src"];
                                    HtmlAttribute attWidth = link.Attributes["width"];
                                    HtmlAttribute attHeight = link.Attributes["height"];
                                    if (attHeight != null && attWidth != null && (Convert.ToInt32(attWidth.Value) <= 1 || Convert.ToInt32(attHeight.Value) <= 1))
                                    {

                                    }
                                    else
                                    {
                                        if (strImageURL != "")
                                            strImageURL = strImageURL + ';' + att.Value;
                                        else
                                            strImageURL = att.Value;

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return strImageURL;
            }
            catch (Exception ex)
            {
                return strImageURL;
            }


        }
        private static SourceColumnNameEntity FillSourceCoumnName(FeedSourceEntity fse)
        {
            SourceColumnNameEntity returnvalue = new SourceColumnNameEntity();
            List<FeedMappingEntity> feedMappingEntity = FeedMappingDTO.GetList(fse.Id);

            returnvalue.Title = feedMappingEntity.Single(x => x.DestinationColumnName == strTitle).SourceColumnName;
            returnvalue.PublishDate = feedMappingEntity.Single(x => x.DestinationColumnName == strPublishDate).SourceColumnName;
            returnvalue.Description = feedMappingEntity.Single(x => x.DestinationColumnName == strDescription).SourceColumnName;
            returnvalue.RedirectURL = feedMappingEntity.Single(x => x.DestinationColumnName == strRedirectURL).SourceColumnName;
            returnvalue.Source = feedMappingEntity.Single(x => x.DestinationColumnName == strTitle).FeedSourceName;
            return returnvalue;

        }
        private static string GetPlainTextFromHtml(string HTMLCode)
        {

            // Remove new lines since they are not visible in HTML
            HTMLCode = HTMLCode.Replace("\n", " ");

            // Remove tab spaces
            HTMLCode = HTMLCode.Replace("\t", " ");

            // Remove multiple white spaces from HTML
            HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

            // Remove HEAD tag
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Replace special characters like &, <, >, " etc.
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
            // Note: There are many more special characters, these are just
            // most common. You can add new characters in this arrays if needed
            string[] OldWords = { "&nbsp;", "&amp;", "&quot;", "&lt;", "&gt;", "&reg;", "&copy;", "&bull;", "&trade;" };
            string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                sbHTML.Replace(OldWords[i], NewWords[i]);
            }

            // Check if there are line breaks (<br>) or paragraph (<p>)
            sbHTML.Replace("<br>", "\n<br>");
            sbHTML.Replace("<br ", "\n<br ");
            sbHTML.Replace("<p ", "\n<p ");

            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");


        }

        public static string EscapeXMLValue(string xmlString)
        {
            //Replace the characters which is causing the problem in XML parsing
            //return xmlString.Replace("'","&apos;").Replace( "\"", "&quot;").Replace(">","&gt;").Replace( "<","&lt;").Replace( "&","&amp;");
            return xmlString.Replace("&", "&amp;");
        }

    }

}
