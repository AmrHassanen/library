using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eLoclizationTask.Dtos;
using LibararyTask.Data;
using Microsoft.EntityFrameworkCore;
using Root.API.Repositories;
using Ubotics.Interfaces;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IPhotoService _photoService;

    public BookRepository(ApplicationDbContext context,IPhotoService photoService)
    {
        _context = context;
        _photoService = photoService;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber, int pageSize)
    {
        return await _context.Books
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
    }

    public async Task<Book> Details(int bookId)
    {
        return await _context.Books.FindAsync(bookId);
    }

    public async Task AddBookAsync(BookDto book)
    {
        var uploadResult = book.ImageUrl != null ? await _photoService.AddPhotoAsync(book.ImageUrl) : null;

        if (uploadResult?.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        var addBook = new Book
        {
            Title = book.Title,
            Author = book.Author,
            IsBorrowed = book.IsBorrowed,
            ImageUrl = uploadResult?.SecureUrl.AbsoluteUri,
            Count =book.Count
        };

        _context.Books.Add(addBook);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await SaveAsync();
    }

    public async Task DeleteBookAsync(int bookId)
    {
        var book = await Details(bookId);
        if (book != null)
        {
            _context.Books.Remove(book);
            await SaveAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
