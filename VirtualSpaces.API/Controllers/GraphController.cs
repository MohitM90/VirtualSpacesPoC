using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualSpaces.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [AuthorizeForScopes(Scopes = new[] { "user.read" })]
    public class GraphController : ControllerBase
    {
        GraphServiceClient _graphServiceClient;

        public GraphController(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        // GET: /<GraphController>
        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                var user = await _graphServiceClient.Me.Request().GetAsync();
                return user.DisplayName;
            }
            catch (Exception e)
            {
                return "error";
            }
        }

    }
}
