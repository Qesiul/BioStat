using System;
using System.Collections.Generic;
using System.Linq;
using BioStat.MVVM.Model;
using BioStat.MVVM.Services;
using Microsoft.Extensions.Configuration;

namespace BioStat.MVVM.Model
{
    public interface IMeasurementRepository
    {
        Dictionary<DateTime, List<Measurement>> GetMeasurementsByDateRange(DateTime startDate, DateTime endDate);
        List<Measurement> GetMeasurementsByDate(DateTime date);
        void SaveMeasurement(Measurement measurement);
    }
    
    // This is the implementation class that connects to your existing database
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly MeasurmentsTrackerDataAccess _dbContext;
        
        public MeasurementRepository(IConfiguration configuration)
        {
            _dbContext = new MeasurmentsTrackerDataAccess(configuration);
        }
        
        public Dictionary<DateTime, List<Measurement>> GetMeasurementsByDateRange(DateTime startDate, DateTime endDate)
        {
            // Query your database to get measurements in the date range
            var measurements = _dbContext.Measurements
                .Where(m => m.Date >= startDate && m.Date <= endDate)
                .ToList();
                
            // Group them by date
            return measurements
                .GroupBy(m => m.Date.Date)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
        
        public List<Measurement> GetMeasurementsByDate(DateTime date)
        {
            return _dbContext.Measurements
                .Where(m => m.Date.Date == date.Date)
                .ToList();
        }
        
        public void SaveMeasurement(Measurement measurement)
        {
            if (measurement.Id == 0)
            {
                _dbContext.Measurements.Add(measurement);
            }
            else
            {
                // Update existing measurement
                _dbContext.Update(measurement);
            }
            
            _dbContext.SaveChanges();
        }
    }
}