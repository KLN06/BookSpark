namespace BookSpark.Models.AuthorViewModels
{
    public class AddAuthorViewModel
    {
         public string Name { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? Biography { get; set; }
    }
}
