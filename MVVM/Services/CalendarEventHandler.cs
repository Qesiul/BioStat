using System.Runtime.InteropServices.JavaScript;
using BioStat.MVVM.Model;
using Microsoft.EntityFrameworkCore;

namespace BioStat.MVVM.Services;

public class CalendarEventHandler
{
    private readonly MeasurmentsTrackerDataAccess _context;

    public CalendarEventHandler(MeasurmentsTrackerDataAccess context)
    {
        _context = context;
    }

    public List<Measurement> GetMeasurementsByDate(DateTime Date)
    {
        return _context.Measurements
            .Where(e => e.Date.Date == Date)
            .ToList();
    }
}