using AuthTutorial.Resourse.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthTutorial.Resourse.Api.Controllers
{
    // Контролер для работы с заказами 
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly BookStore store;

        private Guid UserID => Guid.Parse(User.Claims.Single( c => c.Type 
                                       == ClaimTypes.NameIdentifier).Value);

        public OrdersController(BookStore store)
        {
            this.store = store;
        }

        [HttpGet]
        [Authorize (Roles = "User")]
        [Route("")]
        public IActionResult GetOrder()
        {
            if (!store.Orders.ContainsKey(UserID)) return Ok(Enumerable.Empty<Book>());

            var orderedBookIds = store.Orders.Single(o => o.Key == UserID).Value;
            var orderedBooks = store.Books.Where(b => orderedBookIds.Contains(b.Id));

            return Ok(orderedBooks);
        }
    }
}
