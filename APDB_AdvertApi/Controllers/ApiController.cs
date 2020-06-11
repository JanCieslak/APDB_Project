using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APDB_AdvertApi.DTOs.ApiController;
using APDB_AdvertApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APDB_AdvertApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IDbService service;

        public ApiController(IConfiguration configuration, IDbService service)
        {
            this.configuration = configuration;
            this.service = service;
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            if (!service.tokenExists(request.RefreshToken))
                return NotFound("Refresh token doesn't exist.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: "AdvertApi",
                    audience: "Clients",
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}