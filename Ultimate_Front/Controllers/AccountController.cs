using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Ultimate_Front.Models;

namespace Ultimate_Front.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> GetAccountName(string AccountCode)
        {
            var baseUrl = "https://localhost:7030/";

            try
            {
                Account accountData = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Construct the endpoint URL with the id parameter
                    string endpoint = $"/api/Account/accountCode?AccountCode={AccountCode}";

                    // Fetch customer data
                    HttpResponseMessage customerResponse = await client.GetAsync(endpoint);
                    if (customerResponse.IsSuccessStatusCode)
                    {
                        var contentResponse = await customerResponse.Content.ReadAsStringAsync();

                        // Deserialize as a single Customer object
                        accountData = JsonConvert.DeserializeObject<Account>(contentResponse);
                    }
                    else
                    {
                        return StatusCode((int)customerResponse.StatusCode, "Failed to retrieve customer data.");
                    }

                    // Map to ViewModel or return directly if no mapping is needed
                    if (accountData != null)
                    {
                        var entityModel = new Account
                        {
                            AccountName = accountData.AccountName,
                            AccountCode = accountData.AccountCode,

                        };

                        //return Ok(entityModel.CustomerName);
                        return Ok(entityModel);

                    }
                    else
                    {
                        return NotFound("Customer not found.");
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
