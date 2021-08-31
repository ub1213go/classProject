using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class WorkoutHunterContext : DbContext
    {
       

        public WorkoutHunterContext(DbContextOptions<WorkoutHunterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CharacterItemSkill> CharacterItemSkills { get; set; }
        public virtual DbSet<GameProgress> GameProgresses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Monster> Monsters { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<ForIndex> forindex { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<CharacterItemSkill>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("character_item_skill");

                entity.Property(e => e.Uid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UID")
                    .IsFixedLength(true);

                entity.Property(e => e.ChaPic).HasMaxLength(10);

                entity.Property(e => e.Items).HasMaxLength(100);

                entity.Property(e => e.Skills).HasMaxLength(50);

                entity.HasOne(d => d.UidNavigation)
                    .WithOne(p => p.CharacterItemSkill)
                    .HasForeignKey<CharacterItemSkill>(d => d.Uid)
                    .HasConstraintName("FK_character_item_skill_user_info");
            });

            modelBuilder.Entity<GameProgress>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("game_progress");

                entity.Property(e => e.Uid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UID")
                    .IsFixedLength(true);

                entity.Property(e => e.StartTime).HasMaxLength(20);

                entity.HasOne(d => d.UidNavigation)
                    .WithOne(p => p.GameProgress)
                    .HasForeignKey<GameProgress>(d => d.Uid)
                    .HasConstraintName("FK_game_progress_user_info");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Iid);

                entity.ToTable("item");

                entity.HasIndex(e => e.ItemName, "IN_Unique")
                    .IsUnique();

                entity.Property(e => e.Iid).HasColumnName("IID");

                entity.Property(e => e.ItemInfo).HasMaxLength(50);

                entity.Property(e => e.ItemName).HasMaxLength(20);

                entity.Property(e => e.ItemPic).HasMaxLength(20);
            });

            modelBuilder.Entity<Monster>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("monster");

                entity.HasIndex(e => e.MonsterName, "MN_Unique")
                    .IsUnique();

                entity.Property(e => e.Mid).HasColumnName("MID");

                entity.Property(e => e.MonsterName).HasMaxLength(10);

                entity.Property(e => e.MonsterPic).HasMaxLength(10);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.Sid);

                entity.ToTable("skill");

                entity.HasIndex(e => e.SkillName, "SN_Unique")
                    .IsUnique();

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.Cd).HasColumnName("CD");

                entity.Property(e => e.SkillName).HasMaxLength(10);

                entity.Property(e => e.SkillPic).HasMaxLength(10);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_user_Info");

                entity.ToTable("user_info");

                entity.HasIndex(e => e.Email, "IX_user_info")
                    .IsUnique();

                entity.Property(e => e.Uid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UID")
                    .IsFixedLength(true);

                entity.Property(e => e.Class)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email).HasMaxLength(20);

                entity.Property(e => e.PassWord).HasMaxLength(50);

                entity.Property(e => e.Role)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Salt)
                    .HasMaxLength(50)
                    .HasColumnName("salt");

                entity.Property(e => e.SignDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("user_status");

                entity.Property(e => e.Uid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.UidNavigation)
                    .WithOne(p => p.UserStatus)
                    .HasForeignKey<UserStatus>(d => d.Uid)
                    .HasConstraintName("FK_user_status_user_info");
            });

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
