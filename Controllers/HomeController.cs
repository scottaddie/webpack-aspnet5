using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Linq;
using webpack_aspnet5.Helpers;

namespace webpack_aspnet5.Controllers
{
    public class HomeController : Controller
    {
        private string _applicationBasePath = null;
        
        public HomeController(IApplicationEnvironment env) {
            _applicationBasePath = env.ApplicationBasePath;
        }
        
        public IActionResult Index()
        {
            const string JAVASCRIPT_KEY = "js";
            
            JObject json = WebpackHelper.GetWebpackAssetsJson(_applicationBasePath);
            ViewBag.VendorScripts = json.Root.SelectToken("vendor").Value<string>(JAVASCRIPT_KEY);
            ViewBag.AppScripts = json.Root.SelectToken("app").Value<string>(JAVASCRIPT_KEY);
                        
            return View();
        }
        public IActionResult Error() => View();
    }
}
