namespace BookSpark.Models.GenreViewModels
{
    public class EditGenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EditGenreViewModel() { }

        public EditGenreViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
