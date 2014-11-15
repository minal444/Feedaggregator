using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVIMPH.Shared.EntityLayer
{
    public class ErrorLogEntity
    {
        #region public properties
        
        public Int64 ErrorID { get; set; }

        public string ErrorDescription { get; set; }

        public string Exception { get; set; }

        public string Stacktrace { get; set; }

        public string ErrorMetadata { get; set; }

        public string ErrorSource { get; set; }

        #endregion
    }
}
