using Chronoshub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Chronoshub.DAL
{

    //Connection to the sqlite database, making sure tables are created and mock data have been added.
    public class SQLiteDB : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Journal> Journals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Journal)
                .WithMany(p => p.Articles)
                .HasForeignKey(p => p.JournalId);


            modelBuilder.Entity<Author>()
                .HasMany(p => p.Articles)
                .WithMany(p => p.Authors)
                .UsingEntity<ArticleAuthor>();


            modelBuilder.Entity<Journal>()
                .HasMany(p => p.Articles)
                .WithOne(p => p.Journal)
                .HasForeignKey(p => p.JournalId);
        }

        public SQLiteDB(DbContextOptions<SQLiteDB> options) : base(options)
        {
            this.Database.EnsureCreated();
            this.SaveChanges();

            if(this.Articles == null)
                this.GetService<IRelationalDatabaseCreator>().CreateTables();
            
        }
    }
}
