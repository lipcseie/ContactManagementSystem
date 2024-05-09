using ContactManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ContactManagementSystem.DataAccessLayer.Context
{
    public class ContactsContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Contact>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.Address)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.PhoneNumber)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.Zip)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.Country)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.City)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
