using APIProject.Core.Entities;
using APIProject.Service.Dtos.Books;
using APIProject.Service.Dtos.Categories;
using Azure;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIProject.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Book> books = new List<Book>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7244/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Book");
                if (Res.IsSuccessStatusCode)
                {
                     var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                     dynamic response = JsonConvert.DeserializeObject(EmpResponse);
                    books = response.items.ToObject<List<Book>>();
                }
                return View(books);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new List<Category>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7244/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Category");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    dynamic response = JsonConvert.DeserializeObject(EmpResponse);
                    ViewBag.Categories = response.items.ToObject<List<Category>>();
                }
            }
            return View();
        }

		private static byte[] GetFileArray(IFormFile file)
		{
			using (var ms = new MemoryStream())
			{
				file.CopyTo(ms);
				return ms.ToArray();
			}
		}
		[HttpPost]
        public async Task<IActionResult> Create(BookPostDto dto)
        {
			ViewBag.Categories = new List<Category>();
			using (var client1 = new HttpClient())
			{
				client1.BaseAddress = new Uri("https://localhost:7244/");
				client1.DefaultRequestHeaders.Clear();
				client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage Res = await client1.GetAsync("api/Category");
				if (Res.IsSuccessStatusCode)
				{
					var EmpResponse = Res.Content.ReadAsStringAsync().Result;
					dynamic response1 = JsonConvert.DeserializeObject(EmpResponse);
					ViewBag.Categories = response1.items.ToObject<List<Category>>();
				}
			}
			using (HttpClient client = new HttpClient())
			{
                string endpoint = "https://localhost:7244/api/book";

				var multipartContent = new MultipartFormDataContent();
               
                var fileContent = new ByteArrayContent( GetFileArray(dto.file));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(dto.file.ContentType);
                multipartContent.Add(fileContent, "file", dto.file.FileName);

                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(dto.Name), Encoding.UTF8, "application/json"), "Name");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(dto.Author), Encoding.UTF8, "application/json"), "Author");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(dto.Price), Encoding.UTF8, "application/json"), "Price");
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(dto.CategoryId), Encoding.UTF8, "application/json"), "CategoryId");


               var response =await client.PostAsync(endpoint,multipartContent);

                if (response.IsSuccessStatusCode)
                {
					return RedirectToAction(nameof(Index));
				}
			}
			return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CategoryUpdateDto dto = new CategoryUpdateDto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7244/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Book/"+id.ToString());
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    dynamic response = JsonConvert.DeserializeObject(EmpResponse);
                    dto = response.items.ToObject<CategoryUpdateDto>();
                }
                return View(dto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id ,CategoryUpdateDto dto)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7244/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await client.PutAsJsonAsync("api/Category/" + id.ToString(), dto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
        public async Task<IActionResult> Remove(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7244/api/");
                var deleteTask = client.DeleteAsync("book/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
