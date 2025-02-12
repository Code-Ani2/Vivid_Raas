using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VividRasV2.Models
{
    public partial class MovieReviewContext : DbContext
    {
        public MovieReviewContext()
        {
        }

        public MovieReviewContext(DbContextOptions<MovieReviewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContentType> ContentTypes { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<TelevisionDrama> TelevisionDramas { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WebSeries> WebSeries { get; set; } = null!;
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=RoyAditi_0102\\MSSQLSERVERADITI; Database=MovieReview; User id=sa; Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.ToTable("ContentType");

                entity.Property(e => e.ContentTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("ContentTypeID");

                entity.Property(e => e.ContentName).HasMaxLength(50);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("GenreID");

                entity.Property(e => e.GenreName).HasMaxLength(50);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LangId);

                entity.ToTable("Language");

                entity.Property(e => e.LangId)
                    .ValueGeneratedNever()
                    .HasColumnName("LangID");

                entity.Property(e => e.LangName).HasMaxLength(50);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("MovieID");

                entity.Property(e => e.ContentTypeId).HasColumnName("ContentTypeID");

                entity.Property(e => e.Duration).HasMaxLength(50);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.LangId).HasColumnName("LangID");

                entity.Property(e => e.MovieName).HasMaxLength(50);

                entity.Property(e => e.ReleaseYear).HasMaxLength(50);

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_ContentType");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_Genre");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.LangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_Language");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReviewID");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_User");
            });

            modelBuilder.Entity<TelevisionDrama>(entity =>
            {
                entity.HasKey(e => e.TvdramasId);

                entity.Property(e => e.TvdramasId)
                    .ValueGeneratedNever()
                    .HasColumnName("TVDramasID");

                entity.Property(e => e.ContentTypeId).HasColumnName("ContentTypeID");

                entity.Property(e => e.DramasName).HasMaxLength(50);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.LangId).HasColumnName("LangID");

                entity.Property(e => e.StartingYear).HasMaxLength(50);

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.TelevisionDramas)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TelevisionDramas_ContentType");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.TelevisionDramas)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TelevisionDramas_Genre");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.TelevisionDramas)
                    .HasForeignKey(d => d.LangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TelevisionDramas_Language");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<WebSeries>(entity =>
            {
                entity.Property(e => e.WebSeriesId)
                    .ValueGeneratedNever()
                    .HasColumnName("WebSeriesID");

                entity.Property(e => e.ContentTypeId).HasColumnName("ContentTypeID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.LangId).HasColumnName("LangID");

                entity.Property(e => e.ReleaseYear).HasMaxLength(50);

                entity.Property(e => e.WebSeriesName).HasMaxLength(50);

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.WebSeries)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebSeries_ContentType");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.WebSeries)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebSeries_Genre");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.WebSeries)
                    .HasForeignKey(d => d.LangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WebSeries_Language");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
