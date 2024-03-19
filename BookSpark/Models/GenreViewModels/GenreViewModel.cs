namespace BookSpark.Models.GenreViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GenreViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
