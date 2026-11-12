namespace LibServer.Models
{
    public class ModelBook
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;

        public ModelBook(LibCore.Book book)
        {
            Title = book.Title;
            Publisher = book.Publisher;
            Genre = book.Genre == 0 ? "?" : LibCore.Genre.Genres.First(g => g.Id == book.Genre).Name;

            switch (book.Grade)
            {
                case LibCore.BookGrage.Excelent: Grade = "+"; break;
                case LibCore.BookGrage.Good: Grade = "+"; break;
                case LibCore.BookGrage.Bad: Grade = "-"; break;
                case LibCore.BookGrage.Worst: Grade = "-"; break;
                default: Grade = string.Empty; break;
            }

            Author = string.Join(", ", LibCore.Author.Authors.Where(a => book.Authors.Contains(a.Id))
                .Select(a => a.Name).OrderBy(s => s));
        }
    }
}