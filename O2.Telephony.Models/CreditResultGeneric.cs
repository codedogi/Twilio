using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models
{
    public class CreditResult<T> : CreditResult
    {
        #region Public Properties
        public T Value { get; set; }
        #endregion

        #region Constructors

        public CreditResult(CreditResultCode code, string message = null)
            : base(code, message)
        {
            if (code == CreditResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public CreditResult(T value)
        {
            Value = value;
        }

        public CreditResult()
        {
            Value = default(T);
        }
        #endregion
    }
}
