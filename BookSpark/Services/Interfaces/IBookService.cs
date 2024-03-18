using BookSpark.Data.Entities;
using BookSpark.Models;

namespace BookSpark.Services.Interfaces
{
    public interface IBookService
    {
        void Add(AddBookViewModel book);
    }
}
