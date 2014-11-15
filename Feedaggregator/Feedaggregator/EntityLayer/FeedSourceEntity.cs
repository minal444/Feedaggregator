using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedaggregator.EntityLayer
{
    public class FeedSourceEntity
    {
        #region Public Properties

        /// <summary>
        /// Id of the Source
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sourcename
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// SourceURL
        /// </summary>
        public string SourceURL { get; set; }

        /// <summary>
        /// If the source is active or not
        /// </summary>
        public Boolean Active { get; set; }

        /// <summary>
        /// Start Date of the source
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End Date of the source
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// End Date of the source
        /// </summary>
        public string SiteURL { get; set; }

        public string ImageSource { get; set; }

        public string ImageNameSpace { get; set; }

        public string DescriptionNameSpace { get; set; }

        public string Timezone { get; set; }
        #endregion

    }
}
