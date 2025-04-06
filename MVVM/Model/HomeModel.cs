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
    
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly MeasurmentsTrackerDataAccess _dbContext;
        
        public MeasurementRepository(IConfiguration configuration)
        {
            _dbContext = new MeasurmentsTrackerDataAccess(configuration);
        }
        
        public Dictionary<DateTime, List<Measurement>> GetMeasurementsByDateRange(DateTime startDate, DateTime endDate)
        {
            var allMeasurements = _dbContext.Measurements
                .Where(m => m.Date >= startDate && m.Date <= endDate)
                .ToList();
            
            var result = new Dictionary<DateTime, List<Measurement>>();
            
            foreach (var dateGroup in allMeasurements.GroupBy(m => m.Date.Date))
            {
                var date = dateGroup.Key;
                var latestMeasurements = new List<Measurement>();
                
                foreach (var typeGroup in dateGroup.GroupBy(m => m.Type))
                {
                    var latestMeasurement = typeGroup
                        .OrderByDescending(m => m.Date)
                        .ThenByDescending(m => m.Id)
                        .First();
                        
                    latestMeasurements.Add(latestMeasurement);
                }
                
                result[date] = latestMeasurements;
            }
            
            return result;
        }
        
        public List<Measurement> GetMeasurementsByDate(DateTime date)
        {
            var allMeasurements = _dbContext.Measurements
                .Where(m => m.Date.Date == date.Date)
                .ToList();
                
            var latestMeasurements = new List<Measurement>();
            
            foreach (var typeGroup in allMeasurements.GroupBy(m => m.Type))
            {
                var latestMeasurement = typeGroup
                    .OrderByDescending(m => m.Date)
                    .ThenByDescending(m => m.Id)
                    .First();
                    
                latestMeasurements.Add(latestMeasurement);
            }
            
            return latestMeasurements;
        }
        
        public void SaveMeasurement(Measurement measurement)
        {
            if (measurement.Id == 0)
            {
                _dbContext.Measurements.Add(measurement);
            }
            else
            {
                _dbContext.Update(measurement);
            }
            
            _dbContext.SaveChanges();
        }
    }
}