using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DemoExam0306.Context;

namespace DemoExam0306;

public partial class Stats : Window
{
    public Stats()
    {
        InitializeComponent();
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        try
        {
            using (var context =new User304Context())
            {
                CountApplic.Text = "Количество выполненных заявок: " +
                                   context.Applications.Count(x => x.Status == 1).ToString();
                
                TypeAppliStat.Text = "Перегрев: " +
                                   context.Applications.Count(x => x.Type == 3).ToString() + " заявки";
                
                TypeAppliStatbattery.Text = "Не заряжается: " +
                                     context.Applications.Count(x => x.Type == 3).ToString() + " заявки";
                
                TypeAppliStatdontopen.Text = "Не включается: " +
                                     context.Applications.Count(x => x.Type == 3).ToString() + " заявки";
            }
        }
        catch (Exception e)
        {
           
        }
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}