using System;
using System.Collections.Generic;
using System.Text;

namespace SSOAPI.Domain.Core.Models
{
    public class RequestResult
    {
        public bool isValid { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public object Result { get; set; }
    }
}
