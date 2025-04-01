using System.Diagnostics.Metrics;
using BioStat.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BioStat.MVVM.Services;

public class   MeasurmentsTrackerDataAccess : DbContext
{
    private readonly IConfiguration _configuration;
    public MeasurmentsTrackerDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<Measurement> Measurements => Set<Measurement>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connection = _configuration.GetConnectionString("DefaultConnection");
        options.UseSqlite(connection);
    }
}