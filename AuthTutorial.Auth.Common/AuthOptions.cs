using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace AuthTutorial.Auth.Common
{
    // этот класс будет использоваться в стандартном механизме внедрения зависимостей
    public class AuthOptions
    {
        // тот кто сгенерировал токен
        public string Issuer { get; set; }
        // для кого токен
        public string Audience { get; set; }
        //секртеная строка для генерации ключа симметричного шифрования (статья в конце)
        public string Secret { get; set; }
        // длительность жизни токена, когда он становится невалидным
        public int TokenLifeTime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecutiryKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}