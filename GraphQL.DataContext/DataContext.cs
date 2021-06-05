using System;
using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.DataContext
{
    public class GraphQLDataContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public GraphQLDataContext(DbContextOptions<GraphQLDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

}