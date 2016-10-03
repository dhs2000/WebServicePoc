using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

using DomainModel;

using NLog;

namespace DataAccess
{
    public class DatabaseContext : DbContext
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        private DbContextTransaction currentTransaction;

        static DatabaseContext()
        {
            DbInterception.Add(new NLogDbInterceptor());
            Database.SetInitializer<DatabaseContext>(null);
        }

        public DatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectItem> ProjectItems { get; set; }

        public void BeginTransaction()
        {
            try
            {
                if (this.currentTransaction != null)
                {
                    return;
                }

                this.currentTransaction = this.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Begin trnsaction error: {e.Message}");
                throw;
            }
        }

        public void CloseTransaction()
        {
            this.CloseTransaction(null);
        }

        public void CloseTransaction(Exception exception)
        {
            try
            {
                if ((this.currentTransaction != null) && (exception != null))
                {
                    Log.Error(exception, exception.Message);
                    this.currentTransaction.Rollback();
                    return;
                }

                this.SaveChanges();

                this.currentTransaction?.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, $"Close trnsaction error: {e.Message}");

                if ((this.currentTransaction != null)
                    && (this.currentTransaction.UnderlyingTransaction.Connection != null))
                {
                    this.currentTransaction.Rollback();
                }

                throw;
            }
            finally
            {
                if (this.currentTransaction != null)
                {
                    this.currentTransaction.Dispose();
                    this.currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapProject(modelBuilder);
        }

        private static void MapProject(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Projects").HasKey(i => i.Id);

            modelBuilder.Entity<Project>().Property(i => i.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Project>().Property(i => i.RootRevision).HasColumnName("RootRevision").IsRequired();

            modelBuilder.Entity<Project>().Property(i => i.RowVersion).IsRowVersion();

            modelBuilder.Entity<ProjectItem>().ToTable("ProjectItems").HasKey(i => i.Id);

            modelBuilder.Entity<ProjectItem>()
                .Property(i => i.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<ProjectItem>()
                .HasRequired(i => i.Project)
                .WithMany(i => i.Items)
                .Map(i => i.MapKey("ProjectId"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Project>().Property(i => i.RowVersion).IsRowVersion();
        }
    }
}