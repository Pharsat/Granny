using Granny.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Granny.DAO.Context
{
    public class GrannyContext : DbContext
    {
        #region Propiedades Entidades

        public DbSet<Location> Locations { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Product> Products { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=CO-IT005535\SQLEXPRESS;Database=Granny;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Server=tcp:granny.database.windows.net,1433;Initial Catalog=grannyProduct;Persist Security Info=False;User ID=grannyAdmin;Password=asdf@1994;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(c=>c.ProductId);
            modelBuilder.Entity<Product>()
                .Property(c => c.ProductId)
                .ValueGeneratedNever();
        }
    }
}
