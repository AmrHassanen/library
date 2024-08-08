using eLoclizationTask.Dtos;
using PagedList;
using System.Threading.Tasks;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber, int pageSize);
    Task<Book> Details(int bookId);
    Task AddBookAsync(BookDto book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int bookId);
    Task SaveAsync();
}
