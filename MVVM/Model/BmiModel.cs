using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BioStat.MVVM.Model;

public class BmiModel
{

    public double CalculateBmi(double heightBmi, double weightBmi)
    {
        if (heightBmi > 100)
        {
            heightBmi = heightBmi / 100;
            return weightBmi / Math.Pow(heightBmi, 2);
        }
            return weightBmi / Math.Pow(heightBmi, 2) * 703;
    }
    
    

    public string SetBmiRange(double resultBmi)
    {
        if (resultBmi < 18.5)
        {
            return "Underweight";
        }
        if(resultBmi >= 18.5 && resultBmi < 25)
        {
            return "Healthy Weight";
        }
        if (resultBmi >= 25 && resultBmi < 30)
        {
            return "Overweight";
        }
        if (resultBmi >= 30 && resultBmi < 35)
        {
            return "1st degree Obesity";
        }

        if (resultBmi >= 35 && resultBmi < 40)
        {
            return "2nd degree Obesity";
        }
        else {
            return "3rd degree Obesity";
        }
    }
    
    public string SetBmiRangeColour(string bmiRange)
    {
        switch (bmiRange)
        {
            case "Underweight":
                return "#5DADE2";
            
            case "Healthy Weight":
                return "#58D68D";
            
            case "Overweight":
                return "#F4D03F";
            
            case "1st degree Obesity":
                return "#F5B041";
            
            case "2nd degree Obesity":
                return "#EB984E";
            
            case "3rd degree Obesity":
                return "#EC7063";
            
            default:
                return "#333333";
        }
    }
}