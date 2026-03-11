using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SIMFranchise.Models;

namespace SIMFranchise.Data;

public partial class SimfranchiseManagementDbContext : DbContext
{
    public SimfranchiseManagementDbContext()
    {
    }

    public SimfranchiseManagementDbContext(DbContextOptions<SimfranchiseManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountBalance> AccountBalances { get; set; }

    public virtual DbSet<CardProduct> CardProducts { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DailyClosing> DailyClosings { get; set; }

    public virtual DbSet<DistributionLog> DistributionLogs { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Franchise> Franchises { get; set; }

    public virtual DbSet<FranchiseStock> FranchiseStocks { get; set; }

    public virtual DbSet<KpiType> KpiTypes { get; set; }

    public virtual DbSet<LedgerTransaction> LedgerTransactions { get; set; }

    public virtual DbSet<LoadOperator> LoadOperators { get; set; }

    public virtual DbSet<MemberStock> MemberStocks { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SimDistribution> SimDistributions { get; set; }

    public virtual DbSet<SimMilestone> SimMilestones { get; set; }

    public virtual DbSet<SimProduct> SimProducts { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamKpiTarget> TeamKpiTargets { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<TeamMemberKpiTarget> TeamMemberKpiTargets { get; set; }

    public virtual DbSet<User> Users { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountBalance>(entity =>
        {
            entity.HasKey(e => new { e.FranchiseId, e.AccountType }).HasName("PK__AccountB__996DD0B95AD186B6");

            entity.Property(e => e.AccountType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Franchise).WithMany(p => p.AccountBalances)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountBa__Franc__41EDCAC5");
        });

        modelBuilder.Entity<CardProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CardProd__3214EC07F8754611");

            entity.Property(e => e.CardName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.CardProducts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CARD_Company");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC07A34775FB");

            entity.HasIndex(e => e.Name, "UQ__Companie__737584F649786CD0").IsUnique();

            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DailyClosing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DailyClo__3214EC07B5F71B26");

            entity.Property(e => e.ActualCash).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ClosingDate).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Shortage)
                .HasComputedColumnSql("([ActualCash]-[SystemCash])", false)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.SystemCash).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ClosedByNavigation).WithMany(p => p.DailyClosings)
                .HasForeignKey(d => d.ClosedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyClosing_User");

            entity.HasOne(d => d.Franchise).WithMany(p => p.DailyClosings)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyClosing_Franchise");
        });

        modelBuilder.Entity<DistributionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distribu__3214EC07B713DDE6");

            entity.Property(e => e.EntryType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LogDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProductType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Franchise).WithMany(p => p.DistributionLogs)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Distribut__Franc__51300E55");

            entity.HasOne(d => d.IssuedByNavigation).WithMany(p => p.DistributionLogs)
                .HasForeignKey(d => d.IssuedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Distribut__Issue__531856C7");

            entity.HasOne(d => d.TeamMember).WithMany(p => p.DistributionLogs)
                .HasForeignKey(d => d.TeamMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Distribut__TeamM__5224328E");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expenses__3214EC0722AB4C58");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PaidFrom)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RecordedBy).HasDefaultValue(1);

            entity.HasOne(d => d.Franchise).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Expenses__Franch__18EBB532");

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.RecordedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_User");
        });

        modelBuilder.Entity<Franchise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Franchis__3214EC070112AAE4");

            entity.HasIndex(e => e.Name, "UQ__Franchis__737584F6592A7BD7").IsUnique();

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Company).WithMany(p => p.Franchises)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_franchieses_company");
        });

        modelBuilder.Entity<FranchiseStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Franchis__3214EC07FC4BC5F7");

            entity.ToTable("FranchiseStock");

            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProductType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Franchise).WithMany(p => p.FranchiseStocks)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Franchise");
        });

        modelBuilder.Entity<KpiType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KpiTypes__3214EC07C33105BC");

            entity.HasIndex(e => e.Name, "UQ__KpiTypes__737584F608FAA555").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LedgerTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LedgerTr__3214EC072542D19A");

            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Direction)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Note)
                .HasMaxLength(225)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Franchise).WithMany(p => p.LedgerTransactions)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ledger_Franchise");
        });

        modelBuilder.Entity<LoadOperator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoadOper__3214EC0712AFF004");

            entity.Property(e => e.CommissionPercent).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OperatorName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.LoadOperators)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOAD_Company");
        });

        modelBuilder.Entity<MemberStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberSt__3214EC077952523E");

            entity.ToTable("MemberStock");

            entity.Property(e => e.CurrentQuantity)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ProductType)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Franchise).WithMany(p => p.MemberStocks)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberSto__Franc__4D5F7D71");

            entity.HasOne(d => d.TeamMember).WithMany(p => p.MemberStocks)
                .HasForeignKey(d => d.TeamMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberSto__TeamM__4C6B5938");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3214EC078239EBA9");

            entity.Property(e => e.ProductType).HasMaxLength(10);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_Company");

            entity.HasOne(d => d.Franchise).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchase_Franchise");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07DD1F1776");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F6D65A6153").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Salaries__3214EC07FEAC177C");

            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonus)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Commission)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPaid).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.TeamMember).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.TeamMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salaries__TeamMe__0E6E26BF");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sales__3214EC0768980015");

            entity.Property(e => e.PaymentMode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProductType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitCostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitSalePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Franchise).WithMany(p => p.Sales)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__Franchise__08B54D69");

            entity.HasOne(d => d.TeamMember).WithMany(p => p.Sales)
                .HasForeignKey(d => d.TeamMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__TeamMembe__09A971A2");
        });

        modelBuilder.Entity<SimDistribution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SimDistr__3214EC070D317D59");

            entity.Property(e => e.AllocatedQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DistributedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.SimProduct).WithMany(p => p.SimDistributions)
                .HasForeignKey(d => d.SimProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SimDistri__SimPr__46B27FE2");

            entity.HasOne(d => d.TeamMember).WithMany(p => p.SimDistributions)
                .HasForeignKey(d => d.TeamMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SimDistri__TeamM__47A6A41B");
        });

        modelBuilder.Entity<SimMilestone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SimMiles__3214EC079FAE1B6C");

            entity.Property(e => e.BonusAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SimProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SimProdu__3214EC07B3C9E5F6");

            entity.Property(e => e.ActivationTarget).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.SimProducts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SIM_Company");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teams__3214EC07CE9B73F6");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Responsibility).HasMaxLength(20);

            entity.HasOne(d => d.Franchise).WithMany(p => p.Teams)
                .HasForeignKey(d => d.FranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team_Franchise");
        });

        modelBuilder.Entity<TeamKpiTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamKpiT__3214EC07CF691E13");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TargetValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.KpiType).WithMany(p => p.TeamKpiTargets)
                .HasForeignKey(d => d.KpiTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamKpiTa__KpiTy__01142BA1");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamKpiTargets)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamKpiTa__TeamI__00200768");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamMemb__3214EC07DC09961E");

            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Member_Team");
        });

        modelBuilder.Entity<TeamMemberKpiTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamMemb__3214EC0722BC56C2");

            entity.Property(e => e.CommissionAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TargetValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.KpiType).WithMany(p => p.TeamMemberKpiTargets)
                .HasForeignKey(d => d.KpiTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KPI_Type");

            entity.HasOne(d => d.TeamMember).WithMany(p => p.TeamMemberKpiTargets)
                .HasForeignKey(d => d.TeamMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KPI_Member");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A790CEC9");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053405DD5E94").IsUnique();

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(225)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
