using Microsoft.EntityFrameworkCore;
using PS.Data.Configurations;
using PS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Data
{
    public class PSContext : DbContext
    {
        //ctor + 2tab
        public PSContext()
        {
          Database.EnsureCreated(); //création de la bdd pour la 1ére fois
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Biological> Biologicals { get; set; }
        public DbSet<Invoice> Factures { get; set; }
        public DbSet<Client> Clients { get; set; }


        //override OnConfiguring
        //Compléter le code
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ZEIDIdb;Integrated Security=true;MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
        }
        //appel des configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ChemicalConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

            //Configurer toute les propriétés de type string et dont le nom commence par “Name”

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string) && p.Name.StartsWith("Name")))
            {
                property.SetColumnName("MyName");
            }

            //TPH: Table per Hierarchy
            /*modelBuilder.Entity<Product>().HasDiscriminator<int>("IsBiological")
                .HasValue<Biological>(1)
                .HasValue<Chemical>(2)
                .HasValue<Product>(0);*/

            //TPT: Table per Type
            modelBuilder.Entity<Biological>().ToTable("Biologicals");
            modelBuilder.Entity<Chemical>().ToTable("Chemicals");
        }

    }
}
