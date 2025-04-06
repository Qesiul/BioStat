using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BioStat.Core;
using BioStat.MVVM.Model;
using Microsoft.Extensions.Configuration;

namespace BioStat.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    private readonly IMeasurementRepository _measurementRepository;
    private int _selectedYear;
    private int _selectedMonth;
    private ObservableCollection<DayCellViewModel> _visibleDays;
    private string _currentMonthYear;
    
    public ObservableCollection<DayCellViewModel> VisibleDays 
    { 
        get => _visibleDays; 
        set { _visibleDays = value; OnPropertyChanged(); }
    }
    
    public int SelectedYear 
    { 
        get => _selectedYear; 
        set { _selectedYear = value; OnPropertyChanged(); UpdateCalendar(); }
    }
    
    public int SelectedMonth
    { 
        get => _selectedMonth; 
        set { _selectedMonth = value; OnPropertyChanged(); UpdateCalendar(); }
    }
    
    public string CurrentMonthYear 
    { 
        get => _currentMonthYear; 
        set { _currentMonthYear = value; OnPropertyChanged(); }
    }
    
    public ICommand NextMonthCommand { get; private set; }
    public ICommand PreviousMonthCommand { get; private set; }
    public ICommand RefreshCommand { get; private set; }

    public HomeViewModel(IConfiguration configuration)
    {
        _measurementRepository = new MeasurementRepository(configuration);
        
        _selectedYear = DateTime.Now.Year;
        _selectedMonth = DateTime.Now.Month;
        
        NextMonthCommand = new RelayCommand(_ => GoToNextMonth());
        PreviousMonthCommand = new RelayCommand(_ => GoToPreviousMonth());
        RefreshCommand = new RelayCommand(_ => UpdateCalendar());
        
        // Subscribe to the measurement added event
        App.MeasurementAdded += (s, e) => UpdateCalendar();
        
        UpdateCalendar();
    }
    
    private void GoToNextMonth()
    {
        if (SelectedMonth == 12)
        {
            SelectedMonth = 1;
            SelectedYear++;
        }
        else
        {
            SelectedMonth++;
        }
    }
    
    private void GoToPreviousMonth()
    {
        if (SelectedMonth == 1)
        {
            SelectedMonth = 12;
            SelectedYear--;
        }
        else
        {
            SelectedMonth--;
        }
    }
    
    public void RefreshCalendar()
    {
        UpdateCalendar();
    }
    
    private void UpdateCalendar()
    {
        VisibleDays = new ObservableCollection<DayCellViewModel>();
        CurrentMonthYear = new DateTime(SelectedYear, SelectedMonth, 1).ToString("MMMM yyyy");
        
        var firstDay = new DateTime(SelectedYear, SelectedMonth, 1);
        var offset = (int)firstDay.DayOfWeek;
        var today = DateTime.Today;
        
        var startDate = firstDay.AddDays(-offset);
        var endDate = startDate.AddDays(42);
        var measurementsByDate = _measurementRepository.GetMeasurementsByDateRange(startDate, endDate);
        
        for (int i = 0; i < 42; i++)
        {
            var currentDate = firstDay.AddDays(i - offset);
            var isCurrentMonth = currentDate.Month == SelectedMonth;
            var isToday = currentDate.Date == today;
            var hasData = measurementsByDate.TryGetValue(currentDate.Date, out var measurements);
            
            var vm = new DayCellViewModel
            {
                Date = currentDate,
                DayNumber = currentDate.Day.ToString(),
                TooltipText = hasData ? FormatMeasurementsForTooltip(measurements) : "",
                BackgroundColor = isToday ? Brushes.LightBlue : Brushes.Transparent,
                DayNumberColor = isCurrentMonth ? Brushes.Black : Brushes.Gray,
                DayNumberWeight = isToday ? FontWeights.Bold : FontWeights.Normal,
                HasDataVisibility = hasData ? Visibility.Visible : Visibility.Hidden,
                ClickCommand = new RelayCommand(_ => ShowDetails(currentDate))
            };
            
            VisibleDays.Add(vm);
        }
    }
    
    private string FormatMeasurementsForTooltip(List<Measurement> measurements)
    {
        return string.Join("\n", measurements.Take(4).Select(m => $"{m.Type}: {m.Value}{m.Unit}"));
    }

    private void ShowDetails(DateTime date)
    {
        var measurements = _measurementRepository.GetMeasurementsByDate(date);
        
        if (measurements != null && measurements.Count > 0)
        {
            var detailsMessage = $"Measurements for {date.ToShortDateString()}:\n\n";
            detailsMessage += string.Join("\n", measurements.Select(m => $"{m.Type}: {m.Value}{m.Unit}"));
            MessageBox.Show(detailsMessage, "Measurement Details", MessageBoxButton.OK);
        }
        else
        {
            MessageBox.Show($"No measurements for {date.ToShortDateString()}", "No Data", MessageBoxButton.OK);
        }
    }
    
    public class DayCellViewModel : ObservableObject
    {
        public DateTime Date { get; set; }
        public string DayNumber { get; set; }
        public string TooltipText { get; set; }
        public Brush BackgroundColor { get; set; }
        public Brush DayNumberColor { get; set; }
        public FontWeight DayNumberWeight { get; set; }
        public Visibility HasDataVisibility { get; set; }
        public ICommand ClickCommand { get; set; }
    }
}