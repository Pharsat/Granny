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

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CO-IT005535\SQLEXPRESS;Database=Granny;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
