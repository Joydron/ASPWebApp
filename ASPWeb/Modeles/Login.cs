using System.ComponentModel.DataAnnotations;

namespace ASPWeb.Modeles
{
    public class Login
    {
        // модель реквест полностью дублирует параметры методы логин

        // Required - обозначает что поле должно обязательно присутствовать 
        // в теле запроса

        // атрибут EmailAddress - диктует формат значение емайл
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
