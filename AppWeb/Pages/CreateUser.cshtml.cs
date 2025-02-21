using AppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppWeb.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
            // Logique à exécuter lors du chargement de la page (si nécessaire)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ajouter le nouvel utilisateur à la base de données
            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();

            // Rediriger vers une autre page après l'insertion
            return RedirectToPage("/Index"); // Ou toute autre page de ton choix
        }
    }
}