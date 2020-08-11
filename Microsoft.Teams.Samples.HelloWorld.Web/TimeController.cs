using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    [Route("[controller]")]
    public class TimeController : Controller
    {
        [HttpGet]
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }
}
