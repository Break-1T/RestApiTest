using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using Contex;
namespace Api.API
{
    public class HomeController : Controller
    {
        HttpResponseMessage response;

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet, ActionName("GetUsersList")]
        public HttpResponseMessage GetUsersList()
        {
            Result result;
            try
            {
                if (DBControl.GetUserList().Any())
                {
                    response = Request
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        
        }
    }
}
