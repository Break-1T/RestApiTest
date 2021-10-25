using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Api.Infrastructure.Attributes
{
    public class ApiRouteAttribute:RouteAttribute
    {
        public ApiRouteAttribute():base("api/v{version:apiVersion}/[controller]")
        {
            
        }
    }
}
