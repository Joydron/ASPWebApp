using Microsoft.AspNetCore.Identity;

namespace ASPWeb.Modeles
{
    public class Account
    {
        // каждый аккаунт имеет 4 свойства
        public Guid Id { get; set; }
        public string Email { get; set; }
        // чтобы показать работу авторизации пользователя через пароль

        public string Password {get; set;}
        // 
        public Role[] Roles { get; set; }
    }
    
    public enum Role
    {
        // принимает 2 типа значения
        User,
        Admin
    }
}
