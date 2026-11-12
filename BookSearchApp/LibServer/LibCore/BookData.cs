namespace LibCore
{
    public enum BookGrade { None = 0, Excelent = 1, Good = 2, Neutral = 3, Bad = 4, Worst = 5 }
    public enum BookGrage { None = 0, Excelent = 1, Good = 2, Bad = 3, Worst = 4 }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public static List<Genre> Genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Фантастика" },
            new Genre { Id = 2, Name = "Детектив" }
        };
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "Лев Толстой" },
            new Author { Id = 2, Name = "Федор Достоевский" }
        };
    }

    public class Book
    {
        public string Title { get; set; } = "";
        public string Publisher { get; set; } = "";
        public int Genre { get; set; }
        public BookGrage Grade { get; set; }
        public List<int> Authors { get; set; } = new List<int>();
    }

    public class RetrievalBook
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string Genre { get; set; } = "";
        public BookGrade Grade { get; set; } = BookGrade.None;
    }

    public static class DataManager
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Title = "Война и мир", Publisher = "АСТ", Genre = 1, Grade = BookGrage.Excelent, Authors = new List<int> { 1 } },
            new Book { Title = "Идиот", Publisher = "Эксмо", Genre = 2, Grade = BookGrage.Good, Authors = new List<int> { 2 } }
        };

    public static List<Book> GetBooks(RetrievalBook rb)
    {
        var query = _books.AsQueryable();
        
        if (!string.IsNullOrEmpty(rb.Title)) 
            query = query.Where(b => b.Title.Contains(rb.Title));
            
        if (!string.IsNullOrEmpty(rb.Genre)) 
        {
            var genreId = Genre.Genres.FirstOrDefault(g => g.Name == rb.Genre)?.Id;
            if (genreId != null)
                query = query.Where(b => b.Genre == genreId);
        }

        if (rb.Grade != BookGrade.None) 
        {
            var dbGrade = (BookGrage)((int)rb.Grade);
            query = query.Where(b => b.Grade == dbGrade);
        }
        return query.ToList();
    }
    }
}