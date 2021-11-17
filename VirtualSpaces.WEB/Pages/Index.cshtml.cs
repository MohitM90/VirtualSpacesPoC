using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace VirtualSpaces.WEB.Pages
{
    public class IndexModel : PageModel
    {

        public string MessageMyApi { get; private set; } = "PageModel in C#";
        public string Token { get; private set; } = "";

        private readonly ILogger<IndexModel> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;

        private readonly string baseUrl;

        public IndexModel(ILogger<IndexModel> logger, ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;

            baseUrl = _configuration.GetValue<string>("VirtualSpacesBackend:BaseUrl");
        }

        public async Task OnGet()
        {
            try
            {
                HttpClient client = new HttpClient();

                var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(_configuration.GetValue<string>("VirtualSpacesBackend:Scopes")?.Split(' '));
                Token = accessToken; // Pass Token to ViewModel to show it on the View

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var data = await client.GetStringAsync($"{baseUrl}/Graph");
                MessageMyApi = data; // Pass Response of REST call to ViewModel to show it on the View
            } catch (Exception e)
            {
                MessageMyApi = e.Message + " | " + e.InnerException;
            }

        }
        
    }
}
