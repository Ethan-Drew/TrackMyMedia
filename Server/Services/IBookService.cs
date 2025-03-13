using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

public interface IBookService
{
    Task<BookModel> CreateBookAsync(BookModel Book);
    Task<List<BookModel>> GetBooksAsync();
    Task<BookModel> GetBookByIdAsync(int id);
    Task<BookModel> UpdateBookAsync(int id, BookModel updatedBook);
    Task<bool> DeleteBookAsync(int id);
}
