using APDB_AdvertApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APDB_AdvertApi.Controllers
{
    [Route("api/campaign/")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IDbService service;
        private readonly IConfiguration configuration;

        public CampaignController(IConfiguration configuration, IDbService service)
        {
            this.configuration = configuration;
            this.service = service;
        }

        [HttpGet("list")]
        [Authorize(Roles = "Client")]
        public IActionResult ListCampaigns()
        {

            return Ok();
        }

        public IActionResult AddCampaign()
        {

            return Ok();
        }
    }
}