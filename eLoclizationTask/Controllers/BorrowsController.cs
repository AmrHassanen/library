using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class BorrowsController : Controller
{
    private readonly IBorrowRepository _borrowRepository;

    public BorrowsController(IBorrowRepository borrowRepository)
    {
        _borrowRepository = borrowRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Borrow(int bookId)
    {
        var result = await _borrowRepository.BorrowBookAsync(bookId);

        if (result.Success)
        {
            TempData["SuccessMessage"] = "Book borrowed successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = result.Message;
        }

        return RedirectToAction("Details", "Books", new { id = bookId });
    }
}
