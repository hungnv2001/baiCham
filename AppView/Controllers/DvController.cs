using AppData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
	public class DvController : Controller
	{
        public DvController()
        {
            
        }
		[HttpGet]
		public async Task<IActionResult> Index()
		{

			List<DongVat> dongVatList = new List<DongVat>();
			using (var httpClient = new HttpClient())
			{
				string requestUrl = "https://localhost:7083/api/dongvats";
				var response = await httpClient.GetAsync(requestUrl);
				string apiResponse = await response.Content.ReadAsStringAsync();
				dongVatList = JsonConvert.DeserializeObject<List<DongVat>>(apiResponse);
			}

			return View(dongVatList);

		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetReservation(Guid id)
		{
			DongVat dongVat = new DongVat();
			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync("https://localhost:7083/api/dongvats/getByID/" + id);
				{

					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{

						string apiResponse = await response.Content.ReadAsStringAsync();
						dongVat = JsonConvert.DeserializeObject<DongVat>(apiResponse);


					}					
				}

			}
			return View(dongVat);

		}

		public ViewResult AddReservation() => View();

		[HttpPost]
		public async Task<IActionResult> AddReservation(DongVat dongVat)
		{
			DongVat receivedReservation = new DongVat();
			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.PostAsJsonAsync("https://localhost:7083/api/dongvats", dongVat);
				if (response.IsSuccessStatusCode)
				{

					return RedirectToAction("Index");
				}
				return View();
			}

		}
		[HttpGet]
		public async Task<IActionResult> EditReservation(Guid id)
		{
			DongVat dongVat = new DongVat();
			using (var httpClient = new HttpClient())
			{

				using (var response = await httpClient.GetAsync("https://localhost:7083/api/dongvats/getByID/" + id))
				{

					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{

						string apiResponse = await response.Content.ReadAsStringAsync();
						dongVat = JsonConvert.DeserializeObject<DongVat>(apiResponse);
					}
					else
						ViewBag.StatusCode = response.StatusCode;
				}

			}
			return View(dongVat);
		}
		[HttpPost]
		public async Task<IActionResult> EditReservation(DongVat dongVat)
		{

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.PostAsJsonAsync("https://localhost:7083/api/dongvats/update", dongVat);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return View(dongVat);
			}


		}
		[HttpGet]
		public async Task<IActionResult> DeleteView(Guid id)
		{
			DongVat dongVat = new DongVat();
			using (var httpClient = new HttpClient())
			{

				using (var response = await httpClient.GetAsync("https://localhost:7083/api/dongvats/getByID/" + id))
				{

					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{

						string apiResponse = await response.Content.ReadAsStringAsync();
						dongVat = JsonConvert.DeserializeObject<DongVat>(apiResponse);
					}
					else
						ViewBag.StatusCode = response.StatusCode;
				}

			}
			return View(dongVat);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.DeleteAsync("https://localhost:7083/api/dongvats/" + id);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return RedirectToAction("DeleteView", id);
			}


		}
	}
}
