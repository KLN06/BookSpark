using BookSpark.Data.Entities;
using BookSpark.Models.AuthorViewModels;
using BookSpark.Models.GenreViewModels;
using BookSpark.Repositories;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services.Interfaces;

namespace BookSpark.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public void Add(AddAuthorViewModel author)
        {
            var authorEntity = new Author(author.Name, author.Birthdate, author.Biography);

            authorRepository.Add(authorEntity);
        }

        public IEnumerable<AuthorViewModel> GetAll()
        {
            var authorEntities = authorRepository.GetAll();

            var authors = authorEntities
               .Select(author => new AuthorViewModel(author.Id, author.Name, author.Birthdate, author.Biography, author.Books));

            return authors;
        }

        public AuthorViewModel Get(int id)
        {
            var author = authorRepository.Get(id);
            return new AuthorViewModel(author.Id, author.Name, author.Birthdate, author.Biography, author.Books);
        }

        public EditAuthorViewModel GetEditable(int id)
        {
            var author = authorRepository.Get(id);
            return new EditAuthorViewModel(author.Id, author.Name, author.Birthdate, author.Biography); //, author.Books);

        }

        public void Edit(EditAuthorViewModel author)
        {
            var authorEntity = new Author(author.Id, author.Name, author.Birthdate, author.Biography); //, author.Books);

            authorRepository.Edit(authorEntity);
        }

        public void Delete(int id)
        {
            authorRepository.Delete(id);
        }
    }
}
