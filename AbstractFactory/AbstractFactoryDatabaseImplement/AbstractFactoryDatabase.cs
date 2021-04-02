using AbstractFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFactoryDatabaseImplement
{
    public class AbstractFactoryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=SIS08\SQLEXPRESS; Initial Catalog = AbstractFactoryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Aircraft> Aircrafts { set; get; }
        public virtual DbSet<AircraftComponent> AircraftComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}
