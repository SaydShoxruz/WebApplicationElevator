using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApplicationElevator.Models;

namespace WebApplicationElevator.Context;

public class AppDbContext : DbContext
{
    public DbSet<ElevatorRequest> ElevatorRequests { get; set; }
    public DbSet<ElevatorState> ElevatorStates { get; set; }

    private readonly IConfiguration config;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
    {
        this.config = config;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()!));
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }
    }

    private string ToSnakeCase(string name)
    {
        if (string.IsNullOrEmpty(name)) return name;
        return Regex.Replace(name, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}
