using BookSpark.Data.Entities;
using BookSpark.Models.GenreViewModels;

namespace BookSpark.Services.Interfaces
{
    public interface IGenreService
    {
        void Add(AddGenreViewModel genre);

        IEnumerable<GenreViewModel> GetAll();

        void Delete(int id);

        void Edit(EditGenreViewModel genre);

        GenreViewModel Get(int id);

        EditGenreViewModel GetEditable(int id);
    }
}
