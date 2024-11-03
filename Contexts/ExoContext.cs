using Microsoft.EntityFrameworkCore;
using Exo.WebApi.Models;

namespace Exo.WebApi.Contexts
{
    public class ExoContext: DbContext
    {
        public ExoContext()
        {
        }
        public ExoContext(DbContextOptions<ExoContext> options) : base(options)
        {   
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=localhost;Database=ExoApi;User=root;Password=;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
        public DbSet<Projeto> Projetos { get; set; }
    }
}