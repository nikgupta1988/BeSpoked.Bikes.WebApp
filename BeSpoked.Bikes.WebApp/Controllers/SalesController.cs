using BeSpoked.Bikes.WebApp.DAL;
using BeSpoked.Bikes.WebApp.Models;
using BeSpoked.Bikes.WebApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Net.Http;

namespace BeSpoked.Bikes.WebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalesApiRepo _apiService;
        public SalesController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _apiService = new SalesApiRepo();  // Initialize the API service
        }


        public async Task<ActionResult> Index()
        {
            // Get all sale data 
            var saleListdetail = await _apiService.GetsaleListAsync<ViesSaleDetail>("api/Sales");


            //Map this data only what to view to user
            List<ViewSaleDetailDTO> viewSale = new List<ViewSaleDetailDTO>();
            foreach (var item in saleListdetail)
            {
                ViewSaleDetailDTO  detail = new ViewSaleDetailDTO()
                {
                    salePerson = item.salesPerson.firstName,
                    Productname = item.products.prodName,
                    customerName = item.customer.firstName,
                    saleDate = item.sellDate,
                    purchasePrise = item.products.purchase_Price,
                    Commision = item.products.commission_Percentage,
                    ComminsionAmount = (item.products.purchase_Price * item.products.commission_Percentage)/100
                };
                viewSale.Add(detail);

            }
            ViewBag.message = TempData["Message"];

            return View("SaleDetail", viewSale);
        }
        [HttpPost]
        public async Task<ActionResult> FilterRecord(DateTime? selectedDateTime)
        {
            // Get all sale data 
            var APIurl = $"api/Sales?{"startDate"}={selectedDateTime}";
            var saleListdetail = await _apiService.GetsaleListAsync<ViesSaleDetail>(APIurl);


            //Map this data only what to view to user
            List<ViewSaleDetailDTO> viewSale = new List<ViewSaleDetailDTO>();
            foreach (var item in saleListdetail)
            {
                ViewSaleDetailDTO detail = new ViewSaleDetailDTO()
                {
                    salePerson = item.salesPerson.firstName,
                    Productname = item.products.prodName,
                    customerName = item.customer.firstName,
                    saleDate = item.sellDate,
                    purchasePrise = item.products.purchase_Price,
                    Commision = item.products.commission_Percentage,
                    ComminsionAmount = (item.products.purchase_Price * item.products.commission_Percentage) / 100
                };
                viewSale.Add(detail);

            }
            ViewBag.message = TempData["Message"];

            return View("SaleDetail", viewSale);
        }
        [HttpGet]
        // GET: SalesController
        public async Task<ActionResult> Create()
        {
            // Display sale data first 
            // Call get product + get customer + get sale person
            var productList = await _apiService.GetListAsync<ProductList>("api/Product");
            var customerList = await _apiService.GetListAsync<CustomerList>("api/Customer");
            var salePersonList = await _apiService.GetListAsync<SalePersonList>("api/SalePerson");

            ViewBag.Products = new SelectList(productList, "ProductID", "ProdName");
            ViewBag.Customer = new SelectList(customerList, "CUST_ID", "firstName");
            ViewBag.salePerson = new SelectList(salePersonList, "SP_ID", "firstName");

            

            return View("CreateSale");
        }
        [HttpPost]
        public async Task<ActionResult> CreateSale(Guid ProductID, Guid CUST_ID, Guid SP_ID)
        {
            // Create sale post action
            CreatesaleModel objCreatesale = new CreatesaleModel()
            {
                sellDate = DateTime.Now,
                CustomerCUST_ID = CUST_ID,
                ProductsProductID = ProductID,
                SalesPersonSP_ID = SP_ID
            };
            bool isadded = await _apiService.CreteSaleAsync(objCreatesale);
            if (isadded)
            {
                TempData["Message"] = "Sales record added succesfull!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "There was an error creating the product.");           

            return View("Index");
        }

        #region not in use
        //private List<products> GetproductList(List<ProductList> prodlist)
        //{
        //    var prod = new List<products>();

        //    foreach (var item in prodlist)
        //    {
        //        var product = new products
        //        {
        //            id = item.ProductID,        // Assign ProductID to id
        //            prodName = item.ProdName    // Assign ProdName to prodName
        //        };

        //        prod.Add(product);
        //    }

        //    return prod;  // Return the populated list of products
        //}
        #endregion

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
