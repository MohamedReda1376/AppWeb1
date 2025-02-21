using AppWeb.Models;
using AppWeb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class DeleteUserModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteUserModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User User { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        User = await _context.Users.FindAsync(id);
        if (User == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (User == null)
        {
            return NotFound();
        }

        var userToDelete = await _context.Users.FindAsync(User.Id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}