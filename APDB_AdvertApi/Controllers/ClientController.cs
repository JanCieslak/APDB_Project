using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using APDB_AdvertApi.DTOs.ClientController;
using APDB_AdvertApi.Models;
using APDB_AdvertApi.Service;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APDB_AdvertApi.Controllers
{
    [Route("api/clients/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IDbService service;
        private readonly IConfiguration configuration;

        public ClientController(IConfiguration configuration, IDbService service)
        {
            this.configuration = configuration;
            this.service = service;
        }

        [HttpPost("register")]
        public IActionResult register(RegisterRequest request)
        {
            if (service.ClientExists(request.Login))
                return BadRequest("User with this login already exists.");

            // generate salt
            string salt = "";
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                salt = Convert.ToBase64String(randomBytes);
            }

            // hash password
            request.Password = hashPassword(request.Password, salt);
            request.Salt = salt;

            // register client
            var isRegistered = service.RegisterClient(request);
            if (isRegistered == false)
                return BadRequest("Something went wrong we couldn't register this user.");

            // return token + refToken
            return Ok(genResponse());
        }

        [HttpPost("login")]
        public IActionResult login(LoginRequest request)
        {
            if (!service.ClientExists(request.Login))
                return NotFound("User not found.");

            // get salt
            var salt = service.GetSalt(request.Login);
            request.Password = hashPassword(request.Password, salt);

            // match passwords with given login
            if (!service.LoginClient(request))
                return Unauthorized("Wrong password.");

            // return token + refToken
            return Ok(genResponse());
        }

        private string hashPassword(string password, string salt)
        {
            // hash password
            var valueBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10_000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        private TokenResponse genResponse()
        {
            // generate token + refToken
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    issuer: "AdvertApi",
                    audience: "Clients",
                    claims: new[] { new Claim(ClaimTypes.Role, "Client") },
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credencials
                );

            var refToken = Guid.NewGuid();
            service.addRefreshToken(refToken);

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refToken
            };
        }

    }
}