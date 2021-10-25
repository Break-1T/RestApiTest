using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;


namespace Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class V1 : ApiVersionAttribute
    {
        public V1() : base(new ApiVersion(1, 0))
        {
        }
    }
}
