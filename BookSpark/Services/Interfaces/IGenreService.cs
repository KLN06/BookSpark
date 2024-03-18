using BookSpark.Data.Entities;
using BookSpark.Models;

namespace BookSpark.Services.Interfaces
{
    public interface IGenreService
    {
        void Add(AddGenreViewModel genre);
    }
}
