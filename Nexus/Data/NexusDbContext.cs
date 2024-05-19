using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NEXUS.Models;

namespace NEXUS.Data;

public partial class NexusDbContext : DbContext
{
    public NexusDbContext()
    {
    }

    public NexusDbContext(DbContextOptions<NexusDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalService> AdditionalServices { get; set; }

    public virtual DbSet<BroadService> BroadServices { get; set; }

    public virtual DbSet<CategoryAdmin> CategoryAdmins { get; set; }

    public virtual DbSet<ConnectionPackage> ConnectionPackages { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<DialService> DialServices { get; set; }

    public virtual DbSet<InternetService> InternetServices { get; set; }

    public virtual DbSet<PackagePlan> PackagePlans { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PlanDb2> PlanDb2s { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SecurityDeposit> SecurityDeposits { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeleService> TeleServices { get; set; }

    public virtual DbSet<TypeOfConnection> TypeOfConnections { get; set; }

    public virtual DbSet<UserRecord> UserRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=Nexus_Db;Persist Security Info=False;User ID=sa;Password=aptech;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<AdditionalService>(entity =>
        {
            entity.HasKey(e => e.AddId).HasName("PK__Addition__A0E1AD8ED9CEAFAF");

            entity.ToTable("AdditionalService");

            entity.Property(e => e.AddName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BroadService>(entity =>
        {
            entity.HasKey(e => e.BroadId).HasName("PK__BroadSer__1164E83113DBE63C");

            entity.ToTable("BroadService");

            entity.Property(e => e.BroadName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoryAdmin>(entity =>
        {
            entity.HasKey(e => e.CatgId).HasName("PK__Category__20F311CD67CFBB3F");

            entity.ToTable("CategoryAdmin");

            entity.Property(e => e.CatgName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ConnectionPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Connecti__322035CC918F0888");

            entity.ToTable("ConnectionPackage");

            entity.Property(e => e.PackageName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContId).HasName("PK__CONTACT__33DC4C7908DD030E");

            entity.ToTable("CONTACT");

            entity.Property(e => e.ContId).HasColumnName("CONT_ID");
            entity.Property(e => e.ContAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONT_ADDRESS");
            entity.Property(e => e.ContEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONT_EMAIL");
            entity.Property(e => e.ContMessage)
                .HasColumnType("text")
                .HasColumnName("CONT_MESSAGE");
            entity.Property(e => e.ContName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONT_NAME");
            entity.Property(e => e.ContPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CONT_PHONE");
        });

        modelBuilder.Entity<DialService>(entity =>
        {
            entity.HasKey(e => e.DialId).HasName("PK__DialServ__720C8D524592AF46");

            entity.ToTable("DialService");

            entity.Property(e => e.DialName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InternetService>(entity =>
        {
            entity.HasKey(e => e.InternetId).HasName("PK__Internet__EB4601216C89EE54");

            entity.ToTable("InternetService");

            entity.Property(e => e.InternetName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PackagePlan>(entity =>
        {
            entity.HasKey(e => e.PackId).HasName("PK__Package___FA676569E9533C88");

            entity.ToTable("Package_Plan");

            entity.Property(e => e.PackDesc).HasColumnType("text");
            entity.Property(e => e.PackName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PackValidity).HasColumnType("text");

            entity.HasOne(d => d.Package).WithMany(p => p.PackagePlans)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Package_P__Packa__440B1D61");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PayId).HasName("PK__PaymentM__EE8FCECF1DFAB9E6");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PayName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlanDb2>(entity =>
        {
            entity.HasKey(e => e.Plan2Id).HasName("PK__Plan_Db2__B51D21FA2FF96095");

            entity.ToTable("Plan_Db2");

            entity.Property(e => e.PlanBasis1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanBasis2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanBasis3)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanBasis4)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanHours1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanHours2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanHours3)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanHours4)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanHours5)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanName1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanName2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanName3)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanSpeed1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanSpeed2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanSpeed3)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity3)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity4)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity5)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity6)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity7)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanValidity8)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PK__Product__042785E50EFC5431");

            entity.ToTable("Product");

            entity.Property(e => e.ProdDesc).HasColumnType("text");
            entity.Property(e => e.ProdImage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProdName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ProdCatg).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdCatgId)
                .HasConstraintName("FK__Product__ProdCat__44FF419A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ROLE__8AFACE1AEF40DAEB");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SecurityDeposit>(entity =>
        {
            entity.HasKey(e => e.SecurityId).HasName("PK__Security__9F8B0930DFF048A8");

            entity.ToTable("Security_Deposit");

            entity.Property(e => e.SecurityName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Team__AA2FFBE5A3F5BDCF");

            entity.ToTable("Team");

            entity.Property(e => e.PersonImage)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PersonName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeleService>(entity =>
        {
            entity.HasKey(e => e.TeleId).HasName("PK__TeleServ__B608344FC183D632");

            entity.ToTable("TeleService");

            entity.Property(e => e.TeleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TypeOfConnection>(entity =>
        {
            entity.HasKey(e => e.ConnId).HasName("PK__TypeOfCo__F5D6B0E33FECE180");

            entity.ToTable("TypeOfConnection");

            entity.Property(e => e.ConnName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRecord>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserReco__1788CC4CB0F87398");

            entity.ToTable("UserRecord");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRole).WithMany(p => p.UserRecords)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("FK__UserRecor__UserR__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
