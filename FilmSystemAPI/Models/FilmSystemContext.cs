using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FilmSystemAPI.Models
{
    public partial class FilmSystemContext : DbContext
    {
        public FilmSystemContext()
        {

        }

        public FilmSystemContext(DbContextOptions<FilmSystemContext> options)
            : base(options)
        {

        }

        public virtual DbSet<FavGenre> FavGenres { get; set; } = null!;
        public virtual DbSet<Genres> Genre { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieReview> MovieReviews { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavGenre>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("FavGenre");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Genre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavGenre_Genre");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Person)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavGenre_People");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Link)
                    .HasMaxLength(120)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(55);

                entity.HasOne(d => d.MovieGenreNavigation)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.MovieGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_Genre");

                entity.HasOne(d => d.UploaderNavigation)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.Uploader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_People");
            });

            modelBuilder.Entity<MovieReview>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MovieReview");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Review).HasMaxLength(200);

                entity.HasOne(d => d.MovieNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Movie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieReview_Movies");

                entity.HasOne(d => d.ReviewPersonNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ReviewPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieReview_People");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(25);

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
