using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnterpriseClientApp.Models;
using EnterpriseClientApp.Services;

namespace EnterpriseClientApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly INewsAPIClient apiClient;
        private readonly IReceipeAPIClient receipeAPIClient;

        public HomeController(INewsAPIClient apiClient,IReceipeAPIClient receipeAPIClient)
        {
            this.apiClient = apiClient;
            this.receipeAPIClient = receipeAPIClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> About()
        {
            var news = await this.apiClient.GetValuesforNews();
            var receipe = await this.receipeAPIClient.GetValuesforNews();
            var viewModel = new ViewModel
            {
                NewsArr = news,
                ReceipeArr = receipe

            };
            return View(viewModel);
        }

        public IActionResult Contact()
        {
           
            return View();

          
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
