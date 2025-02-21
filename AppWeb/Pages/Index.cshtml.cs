using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppWeb.Models;  // Remplace avec ton namespace pour le modèle User

namespace AppWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public User FirstUser { get; set; }

        public void OnGet()
        {
            // Récupère la première ligne de la table Users (si la table n'est vide)
            FirstUser = _context.Users.FirstOrDefault();  // Utilisation de LINQ pour obtenir le premier utilisateur
        }
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}