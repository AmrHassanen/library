using System;
using System.Threading.Tasks;
using eLoclizationTask.Dtos;
using LibararyTask.Data;
using Microsoft.EntityFrameworkCore;

public class BorrowRepository : IBorrowRepository
{
    private readonly ApplicationDbContext _context;

    public BorrowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BorrowResult> BorrowBookAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            return new BorrowResult { Success = false, Message = "Book not found." };
        }

        if (book.Count <= 0)
        {
            return new BorrowResult { Success = false, Message = "The book is out of stock." };
        }

        var borrow = new Borrow
        {
            BookId = bookId,
            BorrowDate = DateTime.Now
        };

        book.Count--;
        _context.Borrows.Add(borrow);
        await _context.SaveChangesAsync();

        return new BorrowResult { Success = true };
    }
}
