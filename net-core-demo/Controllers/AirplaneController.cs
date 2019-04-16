using System.Linq;
using Microsoft.AspNetCore.Mvc;
using insignia.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace insignia.Controllers
{
    public class AirplaneController : Controller
    {
        private IOptionsSnapshot<MySqlServiceOption> _mySqlOptions;
        private MySqlServiceOption MySqlOptions
        {
            get
            {
                return _mySqlOptions.Get("mySql2");
            }
        }

        public AirplaneController(IConfiguration configuration, IOptionsSnapshot<MySqlServiceOption> mySqlOptions)
        {
            _mySqlOptions = mySqlOptions;
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // GET: /<controller>/
        public JsonResult Index([FromServices] AirplaneContext context)
        {
            var a = context.Airplanes.ToArray();
            return Json(a);
        }
    }
}
