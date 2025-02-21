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
            // Logique � ex�cuter lors du chargement de la page (si n�cessaire)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ajouter le nouvel utilisateur � la base de donn�es
            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();

            // Rediriger vers une autre page apr�s l'insertion
            return RedirectToPage("/Index"); // Ou toute autre page de ton choix
        }
    }
}