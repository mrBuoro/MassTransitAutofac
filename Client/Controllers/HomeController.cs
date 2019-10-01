using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using MassTransit;
using Messages;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        IRequestClient<CheckOrderStatus> _client;

        public HomeController(IRequestClient<CheckOrderStatus> client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetResponse<OrderStatusResult>(new { OrderId = "Prova" });
            Console.WriteLine(response.Message.OrderId);

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
    }
}
