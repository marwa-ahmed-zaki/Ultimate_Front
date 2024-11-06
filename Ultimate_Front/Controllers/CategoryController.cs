using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ultimate_Front.Dto;
using Ultimate_Front.Models;
using Ultimate_Front.Views.ViewModel;

namespace Ultimate_Front.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCategoryName(int id)
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                List<CategoryDto> categories = new List<CategoryDto>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Categories
                    HttpResponseMessage response = await client.GetAsync($"GetCategory/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<CategoryDto>>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to retrieve categories.");
                    }

                    // Map to ViewModel
                    var entityModel = categories.Select(c => new Category
                    {
                        //CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                    }).ToList();

                    return Ok(entityModel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

        public async Task<IActionResult> AddCategory()
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                List<CategoryDto> categories = new List<CategoryDto>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Categories
                    HttpResponseMessage response = await client.GetAsync("CreateCategory");
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<CategoryDto>>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to retrieve categories.");
                    }

                    // Map to ViewModel
                    var entityModel = categories.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                    }).ToList();

                    return Ok(entityModel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }



        public async Task<IActionResult> GetCategories()
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                List<CategoryDto> categories = new List<CategoryDto>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Categories
                    HttpResponseMessage response = await client.GetAsync("Category/GetCategories");
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<CategoryDto>>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Failed to retrieve categories.");
                    }

                    // Map to ViewModel
                    var entityModel = categories.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                    }).ToList();

                    return Ok(entityModel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                List<CategoryDto> categories = new List<CategoryDto>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Fetch Category
                    HttpResponseMessage ordersResponse = await client.GetAsync("GetCategoryOnly");
                    if (ordersResponse.IsSuccessStatusCode)
                    {
                        var contentResponse = await ordersResponse.Content.ReadAsStringAsync();
                         categories = JsonConvert.DeserializeObject<List<CategoryDto>>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)ordersResponse.StatusCode, "Failed to retrieve orders.");
                    }



                    // Map to ViewModel
                    var entityModel = categories.Select(o => new CategoryDto
                    {
                        CategoryId = o.CategoryId,
                        CategoryName = o.CategoryName,

                    }).ToList();

                    return Ok(entityModel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error: " + ex.Message);
            }
        }

    }
}
