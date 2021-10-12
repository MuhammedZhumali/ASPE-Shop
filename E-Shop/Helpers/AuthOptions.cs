using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Helpers
{
  public class AuthOptions
  {
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public const int LIFETIME = 1140; // время жизни токена - 1140 минут (24 часа)
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
  }
}
