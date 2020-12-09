using DogusTeknoloji.SmartKPIMiner.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace DogusTeknoloji.SmartKPIMiner.Data
{
    public class SmartKPIDbContext : DbContext
    {
        private string _connectionString;
        protected SmartKPIDbContext()
        {

        }
        public SmartKPIDbContext(string connectionString) : base()
        {
            this._connectionString = connectionString;
            this.Database.SetCommandTimeout(timeout: TimeSpan.FromMinutes(3));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder.UseSqlServer(this._connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComputeRule>(entity => entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()"));
            modelBuilder.Entity<ExcludedFileFormat>(entity => entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()"));
            modelBuilder.Entity<KPIMetric>(entity =>
            {
                entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()");
                entity.Property(property => property.FailedAverageResponseTime).HasDefaultValue(0.0);
            });

            modelBuilder.Entity<RootAppFeed>(entity => entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()"));
            modelBuilder.Entity<SearchIndex>(entity =>
            {
                entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()");
                entity.Property(property => property.IsActive).HasDefaultValue(true);
                entity.HasMany(property => property.KPIMetrics).WithOne(x => x.SearchIndex);
            });

            modelBuilder.Entity<SimilarAppFeed>(entity => entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()"));
            modelBuilder.Entity<ServiceLog>(entity => entity.Property(property => property.CreateDate).HasDefaultValueSql("getdate()"));

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ComputeRule> ComputeRules { get; set; }
        public DbSet<ExcludedFileFormat> ExcludedFileFormats { get; set; }
        public DbSet<SearchIndex> SearchIndices { get; set; }
        public DbSet<KPIMetric> KPIMetrics { get; set; }
        public DbSet<RootAppFeed> RootAppFeeds { get; set; }
        public DbSet<SimilarAppFeed> SimilarAppFeeds { get; set; }
        public DbSet<ServiceLog> ServiceLogs { get; set; }
    }
}