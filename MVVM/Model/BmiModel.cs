﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BioStat.MVVM.Model;

public class BmiModel
{
    public void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    public double CalculateBmi(double heightBmi, double weightBmi)
    {
        heightBmi = heightBmi / 100;
        return weightBmi / Math.Pow(heightBmi, 2);
    }
}