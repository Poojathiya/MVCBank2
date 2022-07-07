using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVCBank2.Models
{
    public partial class BankContext : DbContext
    {
        public BankContext()
        {
        }

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Newaccount> Newaccounts { get; set; }
        public virtual DbSet<Transaction1> Transaction1s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Bank;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Newaccount>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__Newaccou__BE2ACD6E27BB9873");

                entity.ToTable("Newaccount");

                entity.Property(e => e.AccountNumber).ValueGeneratedNever();

                entity.Property(e => e.CurrentBalance).HasDefaultValueSql("((500))");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction1>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__55433A6BA1A622D2");

                entity.ToTable("Transaction1");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.Transaction1s)
                    .HasForeignKey(d => d.AccountNumber)
                    .HasConstraintName("FK__Transacti__Accou__2A4B4B5E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        // public DbSet<MVCBank2.Models.User> User { get; set; }
    }
}
