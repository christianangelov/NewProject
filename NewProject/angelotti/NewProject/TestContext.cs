using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewProject;

namespace ef_demo
{
    public class TestContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql(
                    "Host=localhost;Database=postgres;Username=postgres;Password=postgres;Search Path=first_steps")
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Person> person = modelBuilder.Entity<Person>();
            person.HasKey(p => p.SSN);
            person.HasMany(p => p.Addresses).WithOne(a => a.Person).HasForeignKey(a => a.SSN);

            EntityTypeBuilder<Address> address = modelBuilder.Entity<Address>();
            address.HasKey(a => new {a.SSN, a.AddressNo});
        }
    }
}