using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibararyTask.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: /User/Index
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return View(users);
    }

    // GET: /User/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: /User/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        if (ModelState.IsValid)
        {
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveAsync();
            return RedirectToAction(nameof(Index)); // Redirect to the list of users or another page
        }
        return View(user);
    }

    // GET: /User/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: /User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _userRepository.UpdateUserAsync(user);
                await _userRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: /User/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: /User/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _userRepository.DeleteUserAsync(id);
        await _userRepository.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> UserExists(int id)
    {
        return (await _userRepository.GetUserByIdAsync(id)) != null;
    }
}
