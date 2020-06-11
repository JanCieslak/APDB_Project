using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APDB_AdvertApi.DTOs.ClientController
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
