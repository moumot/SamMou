using Microsoft.EntityFrameworkCore;
using SamMou.Api.Models;

namespace SamMou.Api.DataContext
{
    public class SamMouDataContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public SamMouDataContext(DbContextOptions<SamMouDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().ToContainer("WeatherForecast").HasPartitionKey(o => o.Summary);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

}