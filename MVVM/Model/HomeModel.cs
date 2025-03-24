namespace BioStat.MVVM.Model;

public class CalendarHandler
{
    private DateTime measurementDate;
    private string measurementButtonClicked;
    private string measurementName;
    private string measurementDescription;
    
    public DateTime MeasurementDate
    {
        get => measurementDate;
        set => measurementDate = value;
    }

    public string MeasurementButtonClicked
    {
        get => measurementButtonClicked;
        set => measurementButtonClicked = value;
    }

    public string MeasurementName
    {
        get => measurementName;
        set => measurementName = value;
    }

    public string MeasurementDescription
    {
        get => measurementDescription;
        set => measurementDescription = value;
    }
    
}

public class HomeModel
{
    
}