using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BioStat.MVVM.Services
{
    public class MeasurmentsTrackerDbContextFactory : IDesignTimeDbContextFactory<MeasurmentsTrackerDataAccess>
    {
        public MeasurmentsTrackerDataAccess CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return new MeasurmentsTrackerDataAccess(config);
        }
    }
    
    //REMOVE THE WHOLE FILE AFTER DONE TESTING!!!
}