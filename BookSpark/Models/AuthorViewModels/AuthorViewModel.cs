namespace BookSpark.Models.AuthorViewModels
{
    public class AuthorViewModel
    {
        public AuthorViewModel(int id, string name, DateOnly? birthdate, string? biography)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            Biography = biography;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? Biography { get; set; }
    }
}
