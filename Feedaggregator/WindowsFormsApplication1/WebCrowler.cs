using Corsis.Xhtml;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class WebCrowler : Form
    {
        public WebCrowler()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtLink.Text.Trim()))
            {
                txtResult.Text = "no search URL";
                return;
            }

            //http://articles.runtings.co.uk/2009/09/htmlagilitypack-article-series.html
            //http://articles.runtings.co.uk/2012/07/a-straightforward-method-to-detecting.html
            
            
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(txtLink.Text);
            //doc.DocumentNode.SelectNodes("//link[(@type='application/rss+xml' or @type='application/atom+xml') and @rel='alternate']")
            
            #region firstpost
            foreach (HtmlNode sectionNode in doc.DocumentNode.SelectNodes("//section[@id='main-content']"))
            {
                foreach (HtmlNode divNode in sectionNode.SelectNodes("//div[@class='artWarp']"))
                {
                    HtmlNode imgNode = divNode.SelectSingleNode("//img");
                    if (imgNode != null)
                    {
                        HtmlAttribute attSrc = imgNode.Attributes["src"];
                        HtmlAttribute attWidth = imgNode.Attributes["width"];
                        HtmlAttribute attHeight = imgNode.Attributes["height"];
                        if (attHeight != null && attWidth != null && (Convert.ToInt32(attWidth.Value) <= 1 || Convert.ToInt32(attHeight.Value) <= 1))
                        {
                            //do nothing if image is very small or invalid

                        }
                        else
                        {
                            string imageURL = attSrc.Value;
                        }
                    }
                    
                }
            }
            #endregion

            #region IBNLIVE
            foreach (HtmlNode sectionNode in doc.DocumentNode.SelectNodes("//div[@class='articl_cont']"))
            {
                foreach (HtmlNode divNode in sectionNode.SelectNodes("//div[@id='photo']"))
                {
                    HtmlNode imgNode = divNode.SelectSingleNode("//img");
                    if (imgNode != null)
                    {
                        HtmlAttribute attSrc = imgNode.Attributes["src"];
                        HtmlAttribute attWidth = imgNode.Attributes["width"];
                        HtmlAttribute attHeight = imgNode.Attributes["height"];
                        if (attHeight != null && attWidth != null && (Convert.ToInt32(attWidth.Value) <= 1 || Convert.ToInt32(attHeight.Value) <= 1))
                        {
                            //do nothing if image is very small or invalid

                        }
                        else
                        {
                            string imageURL = attSrc.Value;
                        }
                    }
                    
                }
            }
            #endregion
     

        }
    }
}
