using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using timelinejob.Models;

namespace timelinejob.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this._configuration = configuration;
        }

        public async ValueTask<IActionResult> Index(string jobid)
        {
            if(string.IsNullOrWhiteSpace(jobid))
                return View();          
            var client = new RestClient();
            var request = new RestRequest($"{_configuration["LinkAPI:ISeeServices"]}api/v2/ISEEStatus/get_substatus/{jobid}", Method.Get);
            var response = await client.ExecuteAsync<List<tbt_job_substatus>>(request);
            Console.WriteLine(response.Content);
            ViewBag.Jobid = jobid;
            return View(response.Data);
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
    }
}