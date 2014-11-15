using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
////using System.Threading.Tasks;

namespace Feedaggregator.EntityLayer
{
    public class FeedMappingEntity
    {
        public int SourceColumnId { get; set; }

        public string SourceColumnName { get; set; }

        public int FeedSourceId { get; set; }
        public string FeedSourceName { get; set; }

        public int DestinationColumnId { get; set; }
        public string DestinationColumnName { get; set; }
        
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
