using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Exo.WebApi.Contexts
{
    public class ExoContext : DbContext
    {
        public ExoContext()
        {
        }

        public ExoContext(DbContextOptions<ExoContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Essa string de conexão depende da SUA máquina.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;");

                // Se você quiser usar o exemplo 1 ou 2, certifique-se de que eles também estejam corretos.
                // Exemplo 1 de string de conexão:
                // "User ID=sa;Password=admin;Server=localhost;Database=ExoApi;Trusted_Connection=False;"

                // Exemplo 2 de string de conexão:
                // "Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;"
            }
        }

        public DbSet<Projeto> Projetos { get; set; }
    }
}