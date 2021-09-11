using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult index()
        {

            return Ok();
        }
    }
}
