using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using insignia.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace insignia.Controllers
{
    [Route("api/[controller]")]
    public class AirplaneController : Controller
    {
        // GET: /<controller>/
        public JsonResult Index()
        {
            using (var context = new AirplaneDbContext()) {
                var a = context.Airplanes.ToArray();
                //var a = new Airplane[] { new Airplane { ID = "AID-0", Name = "Red Baron" },
                //    new Airplane { ID = "AID-1", Name = "Blue Baron" } };
                return Json(a);
            }
        }
    }
}
