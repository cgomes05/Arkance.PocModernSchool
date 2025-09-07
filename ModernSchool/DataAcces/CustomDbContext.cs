
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ModernSchool.Models;

namespace ModernSchool.DataAcces;

    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students => Set<Student> ();
    }