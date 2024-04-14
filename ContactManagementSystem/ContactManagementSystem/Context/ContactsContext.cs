﻿using ContactManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementSystem.Context
{
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

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
                .Property(c => c.City)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.PhoneNumber)
                .IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}