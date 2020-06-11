using APDB_AdvertApi.Context;
using APDB_AdvertApi.DTOs.ClientController;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APDB_AdvertApi.Service
{
    public class DbService : IDbService
    {
        private readonly DatabaseContext context;

        public DbService(DatabaseContext context)
        {
            this.context = context;
        }

        public bool ClientExists(string login)
        {
            return context.Clients.Where(c => c.Login == login).ToList().Count > 0;   
        }

        public bool RegisterClient(RegisterRequest request)
        {
            try
            {
                context.Clients.Add(new Models.Client
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Login = request.Login,
                    Password = request.Password,
                    Salt = request.Salt
                });

                context.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool tokenExists(Guid refreshToken)
        {
            return context.RefreshTokens.Where(rt => rt.Token == refreshToken).ToList().Count > 0;
        }

        public bool addRefreshToken(Guid refreshToken)
        {
            try
            {
                context.RefreshTokens.Add(new Models.RefreshToken
                {
                    Token = refreshToken
                });

                context.SaveChanges();

                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public string GetSalt(string login)
        {
            return context.Clients.Where(c => c.Login == login).ToList()[0].Salt;
        }

        public bool LoginClient(LoginRequest request)
        {
            return context.Clients.Where(c => c.Login == request.Login).ToList()[0].Password == request.Password;

        }
    }
}
