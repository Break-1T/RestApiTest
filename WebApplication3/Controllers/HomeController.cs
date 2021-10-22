//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.Extensions.Logging;

//namespace Api.Controllers
//{
//    [Controller]
//    [Route("[controller]")]
//    public class HomeController : ControllerBase
//    {
//        public HomeController(ILogger<HomeController> _logger)
//        {
//            this._logger = _logger;
//        }
        
//        private readonly ILogger _logger;

//        // <summary>
//        /// Error action.
//        /// </summary>
//        [Route("error")]
//        public IActionResult Error()
//        {
//            var reExecute = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
//            var message =
//                $"Unexpected Status Code: {this.HttpContext.Response?.StatusCode}, OriginalPath: {reExecute?.OriginalPath}";
//            this._logger.LogInformation(message);
//            return new ObjectResult(new
//            { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier })
//            { StatusCode = (int)HttpStatusCode.BadRequest };
//        }
//    }
//}
