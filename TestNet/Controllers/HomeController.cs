using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestNet.Entities;
using TestNet.Models;

namespace TestNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }   
        
        public async Task<IActionResult> Download()
        {
            HttpClient api = new HttpClient();
            var response = await api.GetAsync(ConfigSettings.APIURL + ConfigSettings.GetRoute);
            var result = response.Content.ReadAsStringAsync().Result;
            var files = JsonConvert.DeserializeObject<IEnumerable<FileData>>(result);

            return View(files);
        }
    }
}
