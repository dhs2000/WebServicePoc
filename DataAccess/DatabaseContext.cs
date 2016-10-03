using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

using DomainModel;

namespace DataAccess
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            DbInterception.Add(new NLogDbInterceptor());
        }

        public DatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<DatabaseContext>(null);            
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectItem> ProjectItems { get; set; }

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

            modelBuilder.Entity<ProjectItem>().Property(i => i.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();

            modelBuilder.Entity<ProjectItem>()
                .HasRequired(i => i.Project)
                .WithMany(i => i.Items)
                .Map(i => i.MapKey("ForeignKeyConstraint_ProjectItem_ProjectId"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Project>().Property(i => i.RowVersion).IsRowVersion();
        }
    }
}