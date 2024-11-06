using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ultimate_Front.Models;
using Ultimate_Front.Views.ViewModel;

namespace Ultimate_Front.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> GetCustomerName(int id)
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Construct the endpoint URL with the id parameter
                    string endpoint = $"/api/Customer/byid?id={id}";

                    // Fetch customer data
                    HttpResponseMessage customerResponse = await client.GetAsync(endpoint);
                    if (customerResponse.IsSuccessStatusCode)
                    {
                        var contentResponse = await customerResponse.Content.ReadAsStringAsync();

                        // Deserialize the response content as a single Customer object
                        var customerData = JsonConvert.DeserializeObject<Customer>(contentResponse);

                        // Check if customer data is available
                        if (customerData != null)
                        {
                            return Ok(customerData.CustomerName);
                        }
                        else
                        {
                            return NotFound("Customer not found.");
                        }
                    }
                    else
                    {
                        return StatusCode((int)customerResponse.StatusCode, "Failed to retrieve customer data.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }


    }
}
