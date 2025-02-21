
using AppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeb.Pages
{
    public class FindUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FindUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        public List<User> FoundUsers { get; set; } = new();

        [BindProperty]
        public User EditUser { get; set; } // Utilisateur à modifier

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                // Recherche tous les utilisateurs ayant le même prénom
                FoundUsers = await _context.Users
                    .Where(u => u.FirstName == Name)
                    .ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Utilisateur supprimé : {user.FirstName} {user.LastName}");
            }
            else
            {
                Console.WriteLine($"Utilisateur avec ID {UserId} non trouvé.");
            }

            return RedirectToPage(new { Name });
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (EditUser == null || EditUser.Id == 0)
            {
                Console.WriteLine("Aucun utilisateur sélectionné pour la modification.");
                return RedirectToPage(new { Name });
            }

            var user = await _context.Users.FindAsync(EditUser.Id);
            if (user != null)
            {
                user.FirstName = EditUser.FirstName;
                user.LastName = EditUser.LastName;
                user.Email = EditUser.Email;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Utilisateur modifié : {user.FirstName} {user.LastName}");
            }
            else
            {
                Console.WriteLine($"Utilisateur avec ID {EditUser.Id} non trouvé.");
            }

            return RedirectToPage(new { Name });
        }
    }
}
