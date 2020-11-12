using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    /// <summary>
    /// Controller responsible for testing. Currently has only 1 GET method
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {

        /// <summary>
        /// This method is for testing purposes. Must return "IT WORKS"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetTest()
        {
            return "IT WORKS";
        }

    }
}
