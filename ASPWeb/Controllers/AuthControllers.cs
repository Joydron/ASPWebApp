using ASPWeb.Modeles;
using AuthTutorial.Auth.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ASPWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Данный репозиторий представлен в виде простого свойства контролера, называется _accounts
    // создан для замены БД

    // здесь пароли в открытом доступе, для тестового проекта
    // в реальном проекте пароли нужно хранить через алгоритм одностороннего шифрования
    // как например bcrypt (bcrypt — адаптивная криптографическая хеш-функция формирования ключа, используемая для защищенного хранения паролей)
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;

        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }

        private List<Account> _accounts => new List<Account>
        {
            new Account()
            {
                Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"),
                Email = "user@email.com",
                Password = "user",
                Roles = new Role[] { Role.User }
            },
            new Account()
            {
                Id = Guid.Parse("7b0a1ec1-80f5-46b5-a108-fb938d3e26c0"),
                Email = "user2@email.com",
                Password = "user2",
                Roles = new Role[] { Role.User }
            },
            new Account()
            {
                Id = Guid.Parse("8e7eb047-e1o0-4801-ba41-f83609e46a7"),
                Email = "admin@email.com",
                Password = "admin",
                Roles = new Role[] { Role.User }
                }
        };

        // 

        // Здесь два атрибута Rout и HttpsPost
        // 
        [Route("login")] // говорит ASP.NET о пути к данному метода контроля
        [HttpPost] // сообщает ASP.NET что нужно использовать Http глагол Post
        // метод контроля логин
        // принимающий емайл и пароль как параметры, однозначно идент. пользователя
        public IActionResult Login([FromBody]Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            // проверка пользователя что он аутентифицирован 
            if (user != null)
            {
                // Generate JWT (токена
                // его нужно подписывать с помощью секретного кода
                // и он должен содержать определнные параметры чтобы его
                // провалидировать на ресурсном сервере)

                var token = GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });
            }
            // это ошибка 404 
            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return _accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        // возвращает токен для аут. пользователя 
        // это код генерации токена
        private string GenerateJWT(Account user)
        {
            // хэдер автоматически генерируется при создании токена
            var authParams = authOptions.Value;

            // это подпись токена сгенерированная семмитричным ключом шифрования
            var securityKey = authParams.GetSymmetricSecutiryKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // пэйлоад токена состоит из Claims 
            // Claim - это некое утверждение об объекте
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            // создали свой тип Claim чтобы добавить информацию о ролях
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            // передача пользователю в респонзе
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
