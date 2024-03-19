using BookSpark.Models.AuthorViewModels;

namespace BookSpark.Services.Interfaces
{
    public interface IAuthorService
    {
        void Add(AddAuthorViewModel author);

       //IEnumerable<AuthorViewModel> GetAll();
    }
}
