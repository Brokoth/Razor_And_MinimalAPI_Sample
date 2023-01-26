using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using minimal_api_plus_razor.Objects;

namespace minimal_api_plus_razor.Pages
{
    public class homeModel : PageModel
    {
        public void OnGet()
        {
        }
        public async void OnPostCallAPI()
        {
            string Baseurl = "https://localhost:44346/WeatherForecast";
            responseObj response1 = new responseObj();
            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(Baseurl);
                    request.Method = HttpMethod.Get;
                    //request.Headers.Add("SecureApiKey", "12345");
                    HttpResponseMessage response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["resp"] = "Pass";
                        //API call success, Do your action
                    }

                    else
                    {
                        ViewData["resp"] = "Fail";
                    }
                }
            }
            catch (Exception ex)
            {

                ViewData["resp"] = ex.Message;
            }
        }
    }
}
