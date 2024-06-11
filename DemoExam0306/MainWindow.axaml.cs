using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DemoExam0306.Context;
using DemoExam0306.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace DemoExam0306;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        if (StaticInfo.User.Idrole == 3)
        {
            NewApplication.IsVisible = true;
            CheckStats.IsVisible = true;
            Notifications.IsVisible = true;
            NotificationText.IsVisible = true;
        }
        
        if (StaticInfo.User.Idrole == 1)
        {
            ListToClient.IsVisible = true;
            ListApplication.IsVisible = false;
        }
        UpdateList();
    }

    public async void UpdateList()
    {
        try
        {
            using (var context = new User304Context())
            {
                List<Application> listApplication = context.Applications.Include(x => x.ClientNavigation)
                    .Include(x => x.TypeNavigation)
                    .Include(x => x.StatusNavigation)
                    .Include(x=>x.IspolNavigation).OrderBy(x=>x.Id).ToList();

                var notificate = context.Notifications.Include(x=>x.StatusNavigation)
                    .Include(x=>x.NumberapplicationNavigation)
                    .Include(x=>x.NameemployeNavigation)
                    .ToList();
                
                if (string.IsNullOrEmpty(SearchBox.Text) == false)
                {
                    List<Application> list = new();
                    var split = SearchBox.Text.Split(" ");
                    foreach (var a in split)
                    {
                        list.AddRange(context.Applications.Where(x=>x.Rent != null && (x.Description != null && (x.Numberapplication.ToLower().Contains(a.ToLower())
                                || x.Description.ToLower().Contains(a.ToLower()))
                            || x.Rent.ToLower().Contains(a.ToLower()))));
                    }

                    listApplication = list;
                }
                ListApplication.ItemsSource = listApplication;
                ListToClient.ItemsSource = listApplication;
                Notifications.ItemsSource = notificate;
            }
        }
        catch (Exception e)
        {
           
        }
    }

    private void NewApplication_OnClick(object? sender, RoutedEventArgs e)
    {
        AddApplication addApplication = new AddApplication(1);
        addApplication.Show();
        this.Close();
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Authorize mainWindow = new Authorize();
        mainWindow.Show();
        this.Close();
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            using (var context = new User304Context())
            {
                var idApplic = (int)(sender as Button).Tag;
                var applic = context.Applications.FirstOrDefault(x => x.Id == idApplic);
                AddApplication addApplication = new AddApplication(applic);
                addApplication.Show();
                this.Close();
            }
        }
        catch (Exception exception)
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Уведомление",
                "Проверь подключение", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await message.ShowAsync();
        }
    }

    private void InputElement_OnKeyUp(object? sender, KeyEventArgs e)
    {
       UpdateList();
    }

    private void CheckStats_OnClick(object? sender, RoutedEventArgs e)
    {
        Stats stats = new Stats();
        stats.Show();
        this.Close();
    }
}