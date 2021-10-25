using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class V2 : ApiVersionAttribute
    {
        public V2() : base(new ApiVersion(2, 0))
        {
        }
    }
}
