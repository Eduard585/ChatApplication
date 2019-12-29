using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
namespace Chat.Auth
{
    public static class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "http://localhost:8080/"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 60 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
