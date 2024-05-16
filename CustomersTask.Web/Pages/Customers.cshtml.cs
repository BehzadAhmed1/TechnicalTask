using CustomersTask.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomersTask.Web.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public CustomersModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _client = clientFactory.CreateClient();
            _configuration=configuration;

        }

        public IList<Customer> Customers { get; set; }
        public string? BaseUrl { get; private set; }

        public async Task OnGetAsync()
        {
            BaseUrl = _configuration["AppSettings:BaseUrl"];

            var response = await _client.GetAsync(BaseUrl+ "/Customers");

            if (response.IsSuccessStatusCode)
            {
                Customers = await response.Content.ReadAsAsync<IList<Customer>>();
            }
        }
    }
}
