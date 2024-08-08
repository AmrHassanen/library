using eLoclizationTask.Dtos;
using System.Threading.Tasks;

public interface IBorrowRepository
{
    Task<BorrowResult> BorrowBookAsync(int bookId);
}
