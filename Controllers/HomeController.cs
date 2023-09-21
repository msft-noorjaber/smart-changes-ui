using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RssFeedDashbaord.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
       static List<PostModel> posts = new List<PostModel>();

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<IActionResult> GetPostDetails(string id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                PostModel post = posts.FirstOrDefault(obj => obj.Sha == id);

                string apiUrl = $"https://rssutils.com/api/changes/MicrosoftDocs/azure-docs/articles/{post.Path}";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    
                    List<RssFeedDashbaord.Models.Root> data = JsonConvert.DeserializeObject<List<RssFeedDashbaord.Models.Root>>(responseContent);

                   // return Json(responseContent); // Return JSON response

                    return PartialView("_PersonListPartial", data);

                }
                else
                {
                    return PartialView("_ErrorPartialView", $"HTTP request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                return PartialView("_ErrorPartialView", $"HTTP request failed: {ex.Message}");
            }
        }


        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                string apiUrl = "https://rssutils.com/api/azure/products";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of items
                    posts   = JsonConvert.DeserializeObject<List<PostModel>>(responseContent);

                    // Create a list of SelectListItem for the drop-down
                    List<SelectListItem> dropdownItems = new List<SelectListItem>();
                    foreach (var post in posts)
                    {
                        dropdownItems.Add(new SelectListItem
                        {
                            Text = post.Name,
                            Value = post.Sha
                            
                        });
                    }

                    // Pass the list of items to the view
                    ViewData["DropdownItems"] = dropdownItems;
                    //ViewBag.Myposts = posts;

                }
                else
                {
                    ViewData["Error"] = $"HTTP request failed with status code: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                ViewData["Error"] = $"HTTP request failed: {ex.Message}";
            }

            return View();
        }
    }

    public class PostModel
    {
        public string Sha { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string TreeUrl { get; set; }
        public string Type { get; set; }
    }


}
