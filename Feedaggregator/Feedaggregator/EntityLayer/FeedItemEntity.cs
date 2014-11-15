using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedaggregator.EntityLayer
{
    public class FeedItemEntity
    {
        #region Public Properties

        /// <summary>
        /// Id of the Source
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Sourcename
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// SourceURL
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If the source is active or not
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Publish date
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Redirect URL
        /// </summary>
        public string RedirectURL { get; set; }

        /// <summary>
        /// Source Name
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// FeedSourceXML Name
        /// </summary>
        public string FeedSourceXML{ get; set; }

        /// <summary>
        /// Active Name
        /// </summary>
        public bool Active{ get; set; }

        /// <summary>
        /// Publish date as per UTC time
        /// </summary>
        public DateTime UTCPublishDate { get; set; }

        #endregion

    }
}
