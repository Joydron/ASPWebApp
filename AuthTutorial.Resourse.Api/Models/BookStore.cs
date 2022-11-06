namespace AuthTutorial.Resourse.Api.Models
{
    public class BookStore
    {
        public List<Book> Books => new List<Book>
        {
            new Book {Id = 1, Author = "Jeffrey Richter", Title = "CLR via C#", Price = 10.45M},
            new Book {Id = 2, Author = "Ландсберг Г.С.", Title = "Элементарный учебник физики", Price = 8.5M},
            new Book {Id = 3, Author = "Адитья Бхаргава", Title = "Грокаем алгоритмы", Price = 7.11M},
            new Book {Id = 4, Author = "Сергей Язев", Title = "Вселенная. Путешествие во времени и пространстве", Price = 7.11M}
        };

        // заказы пользователей
        public Dictionary<Guid, int[]> Orders => new Dictionary<Guid, int[]>
        {
            {Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"), new int [] {1, 2 } },
            {Guid.Parse("8e7eb047-e1o0-4801-ba41-f83609e46a7"), new int [] {2, 3, 4} },

        };
    }
}
