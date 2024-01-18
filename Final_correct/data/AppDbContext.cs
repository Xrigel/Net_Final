using Final_correct.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Final_correct.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
        public DbSet<InsuranceProduct> InsuranceProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Model.Type> Types { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<AuthorizedUser> AuthorizedUsers { get; set; }
        public DbSet<Price> Prices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsuranceProduct>()
        .HasOne(ip => ip.ProductPrice)
        .WithOne(p => p.InsuranceProduct)
        .HasForeignKey<Price>(p => p.PriceId)  // Specify the foreign key
        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                     .HasKey(u => u.UserId);

            modelBuilder.Entity<InsuranceProduct>()
                .HasKey(p => p.ProductId);


            modelBuilder.Entity<User>()
                .HasMany(u => u.InsuranceProducts)
                .WithMany(p => p.Users)
                .UsingEntity(j => j.ToTable("UserInsuranceProducts"));
        }
    }
}
