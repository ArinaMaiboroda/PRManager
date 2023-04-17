using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PRManager.Classes {
    public class PRContext : DbContext {
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<ClientFactor> ClientFactors { get; set; }
        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<Network> Networks { get; set; }
        public PRContext(DbContextOptions<PRContext> options) : base(options) { }
        public PRContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PRContent;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Person>()
           .HasOne<Cluster>(p => p.Cluster)
           .WithMany(c => c.Persons)
           .HasForeignKey(g => g.IdCluster);
            modelBuilder.Entity<Person>().Navigation(p => p.Cluster).AutoInclude();
            modelBuilder.Entity<Cluster>().Navigation(c => c.Persons).AutoInclude();
            modelBuilder.Entity<Cluster>().Navigation(c => c.AgeCategory).AutoInclude();
            modelBuilder.Entity<Person>().Navigation(p => p.Preference).AutoInclude();
            modelBuilder.Entity<Client>().Navigation(p => p.Cluster).AutoInclude();
            modelBuilder.Entity<Client>().Navigation(p => p.Network).AutoInclude();
        }

    }
}
