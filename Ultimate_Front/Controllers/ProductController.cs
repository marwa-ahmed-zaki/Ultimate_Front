using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ultimate_Front.Models;

namespace Ultimate_Front.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


       
        public async Task<IActionResult> GetProductOfCategory(string name)
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                List<Product> products = new List<Product>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Orders
                    //https://localhost:7030/api/Product/GetProductOfCategory/Category1
                    HttpResponseMessage ordersResponse = await client.GetAsync($"api/Product/GetProductOfCategory/{name}");
                    if (ordersResponse.IsSuccessStatusCode)
                    {
                        var contentResponse = await ordersResponse.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<List<Product>>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)ordersResponse.StatusCode, "Failed to retrieve orders.");
                    }
                    // Map to ViewModel
                    var entityModel = products.Select(p => new Product
                    {
                        ProductName = p.ProductName,
                        ProductId = p.ProductId,

                    }).ToList();

                    return Ok(entityModel);
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

    }
}
