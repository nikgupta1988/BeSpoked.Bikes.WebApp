using System.Diagnostics;
using BeSpoked.Bikes.WebApp.DAL;
using BeSpoked.Bikes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeSpoked.Bikes.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductApiRepo _apiService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _apiService = new ProductApiRepo();  // Initialize the API service

        }

        public async Task <IActionResult> Index()
        {
            var productList = await _apiService.GetProductListAsync<ProductList>("api/Product");
            return View(productList);
        }
        public async Task <IActionResult> Edit(Guid ProductID)
        {
            // Fetch the record to edit 
            var productDetail = await _apiService.GetProductByIdAsync(ProductID);
            
            return View(productDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductList Productdetail)
        {
            // Fetch the record to edit 
            var isupdated = await _apiService.UpdateProductAsync(Productdetail);
            if (isupdated)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "There was an error creating the product.");
            return View(Productdetail);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(CreateProduct crpRequest)
        {
            bool isadded = await _apiService.PostProductAsync(crpRequest);
            if (isadded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "There was an error creating the product.");
            return View(crpRequest);

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
