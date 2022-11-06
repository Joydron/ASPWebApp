using AuthTutorial.Resourse.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthTutorial.Resourse.Api.Controllers
{
    //Контролер для работы с данными магазина
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStore store;

        public BooksController(BookStore store)
        {
            this.store = store;
        }
        
        // метод который возвращает список всех книг

        [HttpGet]
        [Route("")]
        public IActionResult GetAvailableBooks()
        {
            return Ok(store.Books);
        }
    }
}
