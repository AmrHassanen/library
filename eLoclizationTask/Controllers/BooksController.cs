using eLoclizationTask.Dtos;
using Microsoft.AspNetCore.Mvc;
using Root.API.Repositories;
using System.Threading.Tasks;

namespace eLoclizationTask.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var books = await _bookRepository.GetAllBooksAsync(pageNumber, pageSize);
            return View(books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: /Books/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.Details(id);
            if (book == null)
            {
                return NotFound(); // Returns a 404 if the book is not found
            }

            return View(book); // Passes the book to the Details view
        }


        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.AddBookAsync(bookDto);
                return RedirectToAction(nameof(Index));
            }

            return View(bookDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.Details(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.Details(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
