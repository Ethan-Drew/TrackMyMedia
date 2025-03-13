using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

public class BookService : IBookService
{
    private readonly TrackMyMediaDbContext Context;

    public BookService(TrackMyMediaDbContext context)
    {
        Context = context;
    }

    public async Task<BookModel> CreateBookAsync(BookModel Book)
    {
        Book.DateAdded= DateTime.Now;
        Context.Books.Add(Book);
        await Context.SaveChangesAsync();
        return Book;
    }

    public async Task<List<BookModel>> GetBooksAsync()
    {
        return await Context.Books.ToListAsync();
    }

    public async Task<BookModel> GetBookByIdAsync(int id)
    {
        return await Context.Books
            .FirstOrDefaultAsync(m => m.MediaId == id);
    }

    public async Task<BookModel> UpdateBookAsync(int id, BookModel updatedBook)
    {
        var existingBook = await Context.Books.FindAsync(id);
        if (existingBook == null)
            return null; 

        // Update fields
        existingBook.Title = updatedBook.Title;
        existingBook.Description = updatedBook.Description;
        existingBook.ReleaseDate = updatedBook.ReleaseDate;
        existingBook.MyRating = updatedBook.MyRating;

        await Context.SaveChangesAsync();
        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var Book = await Context.Books.FindAsync(id);
        if (Book == null)
            return false; // Book not found

        Context.Books.Remove(Book);
        await Context.SaveChangesAsync();
        return true;
    }
}
