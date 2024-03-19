using BookSpark.Data.Entities;
using BookSpark.Models.AuthorViewModels;
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

      //public IEnumerable<AuthorViewModel> GetAll()
        //{
          //  var authorEntities = authorRepository.GetAll();

            //var products = authorEntities
              //  .Select(author => new AuthorViewModel(author.Id, author.Name, author.Birthdate, author.Biography));

            //return products;
       // }
    }
}
