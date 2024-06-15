using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trail_Tracker_App.Entities;

public partial class MountaintrailsContext : DbContext
{
    public MountaintrailsContext()
    {
    }

    public MountaintrailsContext(DbContextOptions<MountaintrailsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Mountain> Mountains { get; set; }

    public virtual DbSet<Mountainrange> Mountainranges { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Trail> Trails { get; set; }

    public virtual DbSet<Userfavorite> Userfavorites { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=127.0.0.1;Port=3306;User=root;Database=mountaintrails;Pwd=110494;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasColumnType("datetime");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY");
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("PRIMARY");

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PRIMARY");

            entity.ToTable("aspnetusertokens");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.HasIndex(e => e.TrailId, "TrailID");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CommentDate).HasColumnType("datetime");
            entity.Property(e => e.CommentText).HasColumnType("text");
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("comments_ibfk_1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Mountain>(entity =>
        {
            entity.HasKey(e => e.MountainId).HasName("PRIMARY");

            entity.ToTable("mountains");

            entity.HasIndex(e => e.RangeId, "RangeID");

            entity.Property(e => e.MountainId).HasColumnName("MountainID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RangeId).HasColumnName("RangeID");

            entity.HasOne(d => d.Range).WithMany(p => p.Mountains)
                .HasForeignKey(d => d.RangeId)
                .HasConstraintName("mountains_ibfk_1");
        });

        modelBuilder.Entity<Mountainrange>(entity =>
        {
            entity.HasKey(e => e.RangeId).HasName("PRIMARY");

            entity.ToTable("mountainranges");

            entity.Property(e => e.RangeId).HasColumnName("RangeID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.PictureId).HasName("PRIMARY");

            entity.ToTable("pictures");

            entity.HasIndex(e => e.TrailId, "TrailID");

            entity.Property(e => e.PictureId).HasColumnName("PictureID");
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UploadedBy).HasMaxLength(255);

            entity.HasOne(d => d.Trail).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("pictures_ibfk_1");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.ToTable("reviews");

            entity.HasIndex(e => e.TrailId, "TrailID");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.ReviewText).HasColumnType("text");
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("reviews_ibfk_1");
        });

        modelBuilder.Entity<Trail>(entity =>
        {
            entity.HasKey(e => e.TrailId).HasName("PRIMARY");

            entity.ToTable("trails");

            entity.HasIndex(e => e.MountainId, "MountainID");

            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Difficulty).HasMaxLength(50);
            entity.Property(e => e.Length).HasPrecision(5);
            entity.Property(e => e.MountainId).HasColumnName("MountainID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Mountain).WithMany(p => p.Trails)
                .HasForeignKey(d => d.MountainId)
                .HasConstraintName("trails_ibfk_1");
        });

        modelBuilder.Entity<Userfavorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PRIMARY");

            entity.ToTable("userfavorites");

            entity.HasIndex(e => e.TrailId, "TrailID");

            entity.HasIndex(e => e.UserId, "UserID");

            entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Userfavorites)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("userfavorites_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userfavorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("userfavorites_ibfk_1");
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("PRIMARY");

            entity.ToTable("userprofiles");

            entity.HasIndex(e => e.UserId, "UserID").IsUnique();

            entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.ProfilePicture).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.Userprofile)
                .HasForeignKey<Userprofile>(d => d.UserId)
                .HasConstraintName("userprofiles_ibfk_1");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PRIMARY");

            entity.ToTable("videos");

            entity.HasIndex(e => e.TrailId, "TrailID");

            entity.Property(e => e.VideoId).HasColumnName("VideoID");
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UploadedBy).HasMaxLength(255);

            entity.HasOne(d => d.Trail).WithMany(p => p.Videos)
                .HasForeignKey(d => d.TrailId)
                .HasConstraintName("videos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
