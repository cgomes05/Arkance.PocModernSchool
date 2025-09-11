
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


    public DbSet<Student> Students => Set<Student>();
    public DbSet<Note> StudentNote => Set<Note>();
    public DbSet<Professeur> Prof => Set<Professeur>();
    public DbSet<Matiere> Matiere => Set<Matiere>();
    public DbSet<Enseigne> Enseigne => Set<Enseigne>();
    public DbSet<Classe> Classe => Set<Classe>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configuration de la clé composite pour la table enseigne
        modelBuilder.Entity<Enseigne>()
        .HasKey(e => new { e.ProfId, e.MatiereId });

        // configuration des rel    tions Many-to-Many
        modelBuilder.Entity<Enseigne>()
        .HasOne(e => e.Professeur)
        .WithMany(e => e.Enseignes)
        .HasForeignKey(e => e.ProfId);


        modelBuilder.Entity<Enseigne>()
        .HasOne(e => e.Matiere)
        .WithMany(e => e.Enseignes)
        .HasForeignKey(e => e.MatiereId);

        // configuration de la relation prof principal 
        modelBuilder.Entity<Classe>()
        .HasOne(e => e.ProfPrincipal)
        .WithMany(e => e.ClassesPrincipales)
        .HasForeignKey(e => e.ProfPrincipalId)
        .OnDelete(DeleteBehavior.Restrict); // Empeche la suppression en cascade

        // Contrainte sur la valeur de note 
        modelBuilder.Entity<Note>()
        .ToTable("CK_Note_Valeur", "[Valeur] >= 0 AND [Valeur] <= 20");

        }
    }
    

    