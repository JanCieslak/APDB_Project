using APDB_AdvertApi.DTOs.ClientController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APDB_AdvertApi.Service
{
    public interface IDbService
    {
        public string GetSalt(string login);
        public bool ClientExists(string login);
        public bool LoginClient(LoginRequest request);
        public bool RegisterClient(RegisterRequest request);
        public bool tokenExists(Guid refreshToken);
        public bool addRefreshToken(Guid refreshToken);
    }
}
