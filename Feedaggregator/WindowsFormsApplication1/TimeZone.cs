using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
namespace WindowsFormsApplication1
{
    public partial class TimeZone : Form
    {
        public TimeZone()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
                //    Console.WriteLine(z.Id + "--" + z.StandardName + "--" + z.DisplayName + "--" + z.SupportsDaylightSavingTime.ToString());
                //Console.ReadLine();
                //using (DBDataContext db = new DBDataContext())
                string con = Convert.ToString(ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.BollywoodGossipConnectionString1"]);
                DBDataContext db = new DBDataContext();
                //{
                    List<Feed> f = db.Feeds.ToList();
                    for (int i = 0; i < f.Count; i++)
                    {
                        FeedSource fs = db.FeedSources.First(x => x.FeedSourceID == Convert.ToInt32(f[i].Source));
                        if (!string.IsNullOrEmpty(fs.Timezone) && f[i].UTCPublishDate == null) 
                        {
                            DateTime dt = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(f[i].PublishDate), TimeZoneInfo.FindSystemTimeZoneById(fs.Timezone));
                            string dtFormated = dt.ToString("yyyy-MM-dd HH:mm:ss");
                            //f[i].UTCPublishDate = dt;
                            SqlHelper.ExecuteNonQuery(con, CommandType.Text, "UPDATE FEEDS SET UTCPUBLISHDATE = '" + dtFormated.ToString() + "' WHERE ID = " + f[i].FeedID.ToString());
                            //db.SubmitChanges();
                        }
                    }
                    //db.SubmitChanges();
                    MessageBox.Show("submit changes");
               // }

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("completed");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strDateGMT = "Sat, 20 Sep 2014 13:32:41 GMT";
            string strDateIST = "09/12/2014 5:07:33 AM";

            DateTime DTGMT =Convert.ToDateTime( String.Format("{0:r}", strDateGMT));
         //   DateTime DTIST = Convert.ToDateTime(String.Format("dd/MM/yyyy hh:mm:ss", strDateIST));

           // DateTime DTIST = DateTime.ParseExact(strDateIST, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            DateTime date;
            if(DateTime.TryParseExact(strDateIST, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out date))
            {

            }



            }
    }
}
