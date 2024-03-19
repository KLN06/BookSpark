using BookSpark.Models.AuthorViewModels;
using BookSpark.Models.GenreViewModels;

namespace BookSpark.Services.Interfaces
{
    public interface IAuthorService
    {
        void Add(AddAuthorViewModel author);

        IEnumerable<AuthorViewModel> GetAll();

        void Delete(int id);

        void Edit(EditAuthorViewModel author);

        AuthorViewModel Get(int id);

        EditAuthorViewModel GetEditable(int id);
    }
}
