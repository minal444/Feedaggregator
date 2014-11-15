using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedaggregator.EntityLayer
{
    public class SourceColumnNameEntity
    {
        #region Public Properties

        
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
        public string PublishDate { get; set; }

        /// <summary>
        /// Redirect URL
        /// </summary>
        public string RedirectURL { get; set; }

        /// <summary>
        /// Source Name
        /// </summary>
        public string Source { get; set; }

        #endregion

    }
}
