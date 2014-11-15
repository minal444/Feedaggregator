using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedaggregator.EntityLayer
{
    public class Category
    {
        #region Public Properties

        /// <summary>
        /// Id of the Category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category_Desc
        /// </summary>
        public string Category_Desc { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }

        #endregion
    }

    public class CategoryFeeds
    {
        #region Public Properties

        /// <summary>
        /// Id of the Category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// CategoryId
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// FeedId
        /// </summary>
        public int FeedId { get; set; }

        #endregion
    }


}
