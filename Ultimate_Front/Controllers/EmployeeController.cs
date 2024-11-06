using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ultimate_Front.Dto;
using Ultimate_Front.Models;
using Ultimate_Front.Views.ViewModel;

namespace Ultimate_Front.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UltimateContext _context;
        private readonly HttpClient _httpClient;


        public EmployeeController(UltimateContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetAll()
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
              //  List<Employee> employees = new List<Employee>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Employees
                    HttpResponseMessage employeesResponse = await client.GetAsync("api/Employee/GetAllEmployee");
                    if (employeesResponse.IsSuccessStatusCode)
                    {
                        var contentResponse = await employeesResponse.Content.ReadAsStringAsync();

                        // Deserialize JSON content into List<Employee>
                       // employees = JsonConvert.DeserializeObject<List<Employee>>(contentResponse);
                        var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(contentResponse);

                        var selectedData = employees.Select(e => new
                        {
                            EmployeeName = e.EmployeeName,
                            Orders = e.Orders.Select(o => new
                            {
                                o.OrderId,
                                o.CustomerName,
                                o.OrderDate,
                                o.AccountNo,
                                o.AccountName,
                                o.TotalPrice
                            }).ToList()
                        }).ToList();
                     
                        // Return the mapped employee DTOs
                        return Ok(selectedData);

                    }
                    else
                    {
                        return StatusCode((int)employeesResponse.StatusCode, "Failed to retrieve employees.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
