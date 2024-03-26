using BudgetingAPIService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BudgetingAPIService.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<MostUserTransaction> MostUserTransactions { get; set; }

    public virtual DbSet<MostUserTransactionView> MostUserTransactionViews { get; set; }

    public virtual DbSet<MostUserTransactionView2> MostUserTransactionView2s { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionCategory> TransactionCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    public virtual DbSet<WalletType> WalletTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\BSISQLEXPRESS;Initial Catalog=BudgetingApp;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.BudgetId).HasName("PK__Budget__E38E79C4F4F3F464");

            entity.HasOne(d => d.TransactionCategory).WithMany(p => p.Budgets).HasConstraintName("FK_Budget_TransactionCategory");

            entity.HasOne(d => d.User).WithMany(p => p.Budgets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Budget_Users");
        });

        modelBuilder.Entity<MostUserTransaction>(entity =>
        {
            entity.ToView("MostUserTransaction");
        });

        modelBuilder.Entity<MostUserTransactionView>(entity =>
        {
            entity.ToView("MostUserTransactionView");
        });

        modelBuilder.Entity<MostUserTransactionView2>(entity =>
        {
            entity.ToView("MostUserTransactionView2");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B97E82AAE");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TransactionCategory).WithMany(p => p.Transactions).HasConstraintName("FK__Transacti__Trans__34C8D9D1");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Users");

            entity.HasOne(d => d.Wallet).WithMany(p => p.Transactions).HasConstraintName("FK__Transacti__Walle__33D4B598");
        });

        modelBuilder.Entity<TransactionCategory>(entity =>
        {
            entity.HasKey(e => e.TransactionCategoryId).HasName("PK__Transact__348808422E1C05CD");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Wallet__84D4F92E33AFB891");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallet_Users");

            entity.HasOne(d => d.WalletType).WithMany(p => p.Wallets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallet_WalletType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
