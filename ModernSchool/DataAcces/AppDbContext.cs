
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ModernSchool.Models;

namespace ModernSchool.DataAcces;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // To connect to PostGreSQL

    }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Professeur> Profs => Set<Professeur>();
    public DbSet<Matiere> Matieres => Set<Matiere>();
    public DbSet<Enseigne> Enseignes => Set<Enseigne>();
    public DbSet<Classe> Classes => Set<Classe>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configuration de la clé composite pour la table enseigne
        modelBuilder.Entity<Enseigne>()
        .HasKey(e => new { e.ProfId, e.MatiereId });

        // configuration des relations Many-to-Many
        modelBuilder.Entity<Enseigne>()
        .HasOne(e => e.Professeur)
        .WithMany(p => p.Enseignes)
        .HasForeignKey(e => e.ProfId);


        modelBuilder.Entity<Enseigne>()
        .HasOne(e => e.Matiere)
        .WithMany(p => p.Enseignes)
        .HasForeignKey(e => e.MatiereId);

        // configuration de la relation prof principal 
        modelBuilder.Entity<Classe>()
        .HasOne(c => c.ProfPrincipal)
        .WithMany(p => p.ClassesPrincipales)
        .HasForeignKey(c => c.ProfPrincipalId)
        .OnDelete(DeleteBehavior.Restrict); // Empeche la suppression en cascade
        }
    }
    

    