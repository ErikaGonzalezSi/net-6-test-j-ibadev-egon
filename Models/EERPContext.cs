/****************************************************************
-Creator: Erika Gonzalez 
-Creation date: 2022-08-29
-Project: EERP Project name
-Epic: EP003 
-UH: UH002, UH003
***************************************************************/

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace net_6_test_j_ibadev_egon_pr.Models
{
    public partial class EERPContext : DbContext
    {
        public EERPContext()
        {
        }

        public EERPContext(DbContextOptions<EERPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Survey> Surveys { get; set; } = null!;
        public virtual DbSet<SurveyType> SurveyTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(e => e.IdSurvey);

                entity.ToTable("Survey");

                entity.HasIndex(e => e.Title, "IX_Unique_Title")
                    .IsUnique();

                entity.Property(e => e.Template).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSurveyTypeNavigation)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.IdSurveyType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Survey_SurveyType");
            });

            modelBuilder.Entity<SurveyType>(entity =>
            {
                entity.HasKey(e => e.IdSurveyType);

                entity.ToTable("SurveyType");

                entity.HasIndex(e => e.NameTypeSurvey, "IX_Unique_NameType")
                    .IsUnique();

                entity.Property(e => e.NameTypeSurvey)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
