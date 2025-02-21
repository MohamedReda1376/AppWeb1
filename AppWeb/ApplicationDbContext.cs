namespace AppWeb
{

    using Microsoft.EntityFrameworkCore; // Ajout de l'espace de noms correct pour DbContext et DbSet
    using AppWeb.Models; // Ajout de l'espace de noms correct pour Users
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

    }
}