using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Ultimate_Front.Models;
using Ultimate_Front.Views.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;


namespace Ultimate_Front.Controllers
{
    public class OrderMasterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UltimateContext _context;
        private readonly HttpClient _httpClient;

        public OrderMasterController(UltimateContext context, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;


        }

        string Baseurl = "https://localhost:7030/";

        public async Task<IActionResult> _Edit(int id)
        {
            // Create an instance of OrderMasterEditViewModel to hold the data to pass to the view
            OrderMasterEditViewModel OrderMasterModel = new OrderMasterEditViewModel();

            using (var client = new HttpClient())
            {
                // Passing service base URL
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                // Define the request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Sending request to find web API REST service resource GetById using HttpClient
                HttpResponseMessage Res = await client.GetAsync($"api/OrderMaster/{id}");


                // Checking the response is successful or not
                if (Res.IsSuccessStatusCode)
                {
                    // Storing the response details received from web API
                    var orderResponse = await Res.Content.ReadAsStringAsync();

                    // Deserializing the response received from web API
                    var orderMaster = JsonConvert.DeserializeObject<OrderMaster>(orderResponse); // Make sure your OrderMaster model is correct here

                    // Map the OrderMaster data to the OrderMasterEditViewModel
                    OrderMasterModel = new OrderMasterEditViewModel
                    {
                        // Assuming OrderMasterEditViewModel has properties to hold the relevant data
                        OrderId = orderMaster.OrderId,
                        AccountName = orderMaster.AccountCodeNavigation.AccountName,
                        OrderDate = orderMaster.OrderDate,
                        // Continue mapping other properties from orderMaster to OrderMasterModel as needed
                    };
                }
            }

            // Returning the populated model to the view
            return View(OrderMasterModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            List<OrderMasterViewModel> orders = new List<OrderMasterViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/OrderMaster/getAll");

                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();
                    orders = JsonConvert.DeserializeObject<List<OrderMasterViewModel>>(result);
                }
            }
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> _Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"api/orders/{id}");
                return response.IsSuccessStatusCode ? Ok(true) : Ok(false);
                // return PartialView();

            }
        }

        public async Task<IActionResult> _Index()

        {
            IEnumerable<OrderMaster> orders = new List<OrderMaster>();

            //   IEnumerable<OrderMasterViewModel> orders = new List<OrderMasterViewModel>();
            var baseUrl = " https://localhost:7030/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/OrderMaster/GetOrders");

                // Send an HTTP GET request to retrieve the list of orders

                try
                {
                    HttpResponseMessage response2 = await client.GetAsync("api/OrderMaster/GetOrders");
                    response.EnsureSuccessStatusCode(); // Throws if the response status code is not 2xx
                                                        // Process the response
                }
                catch (HttpRequestException e)
                {
                    // Log the exception message
                    Console.WriteLine($"Request error: {e.Message}");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string
                    var result = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON into the list of OrderMasterViewModel objects
                    // orders = JsonConvert.DeserializeObject<List<OrderMasterViewModel>>(result);
                    orders = JsonConvert.DeserializeObject<List<OrderMaster>>(result);

                }
                else
                {
                    // Handle API errors (optional logging or error message)
                    ModelState.AddModelError(string.Empty, "Unable to retrieve orders from the API.");
                }
            }

            // Pass the



            return View(orders);
        }

        public async Task<IActionResult> EditOrder(int orderId, OrderMasterViewModel updatedOrder)
        {
            // Retrieve all orders using the GetAll method to get the current entityModel list
            IActionResult result = await GetAll();

            if (result is OkObjectResult okResult && okResult.Value is List<OrderMasterViewModel> entityModel)
            {
                // Find the specific order to update by OrderId
                var orderToEdit = entityModel.FirstOrDefault(o => o.OrderId == orderId);

                if (orderToEdit != null)
                {
                    // Update the fields of the order based on the updatedOrder input
                    orderToEdit.OrderDate = updatedOrder.OrderDate;
                    orderToEdit.CustomerName = updatedOrder.CustomerName;
                    orderToEdit.AccountNumber = updatedOrder.AccountNumber;
                    orderToEdit.AccountName = updatedOrder.AccountName;
                    orderToEdit.totalPrice = updatedOrder.totalPrice;

                    using (var client = new HttpClient())
                    {
                        // Set the base URL for the client
                        client.BaseAddress = new Uri("https://localhost:7030/");
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // Prepare the updated order data
                        var orderUpdateData = new OrderMaster
                        {
                            OrderId = orderToEdit.OrderId,
                            OrderDate = orderToEdit.OrderDate,
                            Customer = new Customer { CustomerName = orderToEdit.CustomerName },
                            AccountCodeNavigation = new Account
                            {
                                AccountCode = orderToEdit.AccountNumber,
                                AccountName = orderToEdit.AccountName
                            },
                            //OrderDetails = updatedOrder.or  // Assuming OrderDetails are updated in the updatedOrder parameter
                        };

                        // Serialize the updated order data to JSON
                        var content = new StringContent(JsonConvert.SerializeObject(orderUpdateData), Encoding.UTF8, "application/json");

                        // Send a PUT request to update the order on the server
                        HttpResponseMessage updateResponse = await client.PutAsync($"api/OrderMaster/{orderId}", content);

                        if (updateResponse.IsSuccessStatusCode)
                        {
                            // Return the updated entityModel after successful update
                            return Ok(entityModel);
                        }
                        else
                        {
                            return StatusCode((int)updateResponse.StatusCode, "Failed to update the order on the server.");
                        }
                    }
                }
                else
                {
                    return NotFound("Order not found.");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve orders.");
            }
        }
    
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                List<OrderMaster> orders = new List<OrderMaster>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Orders
                    HttpResponseMessage ordersResponse = await client.GetAsync("api/OrderMaster");
                    if (ordersResponse.IsSuccessStatusCode)
                    {
                        var contentResponse = await ordersResponse.Content.ReadAsStringAsync();
                        orders = JsonConvert.DeserializeObject<List<OrderMaster>>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)ordersResponse.StatusCode, "Failed to retrieve orders.");
                    }



                    // Map to ViewModel
                    var entityModel = orders.Select(o => new OrderMasterViewModel
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,

                        CustomerName = o.Customer.CustomerName,
                        AccountNumber = o.AccountCodeNavigation.AccountCode,
                        AccountName = o.AccountCodeNavigation.AccountName,
                        totalPrice = o.OrderDetails.Sum(d => d.Quantity * d.Price).ToString()
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

     
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Index()
    
        
        
        
        {
            return View();
        }
    }
}
