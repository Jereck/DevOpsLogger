using Microsoft.EntityFrameworkCore;
using DevOpsLogger.Api.Models;

namespace DevOpsLogger.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Incident> Incidents { get; set; }
}