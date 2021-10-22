using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;


namespace Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute,IRouteTemplateProvider
    {
        public string Template { get; }
        public int? Order { get; }
        public string Name { get; set; }
        public VersionAttribute([ApiExplorerSettings] )
        {
            Name = name;
            Ge
        }

        public void Show()
        {

        }
    }
}
