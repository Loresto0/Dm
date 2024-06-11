using System;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DemoExam0306.Context;
using DemoExam0306.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Application = DemoExam0306.Models.Application;

namespace DemoExam0306;

public partial class AddApplication : Window
{
    public AddApplication()
    {
        InitializeComponent();
    }
    
    public AddApplication(int role)
    {
        InitializeComponent();
        if (role == 1)
        {
            DescriptionIspol.IsVisible = false;
            SaveApplic.Click += SaveApplicOnClick;
            LoadInfo(); 
        }
    }

    public Application application;
    public AddApplication(Application applic)
    {
        InitializeComponent();
        if (StaticInfo.User.Idrole == 3)
        {
            IspolApplic.IsVisible = true;
            IspolTextblock.IsVisible = true;
            
            application = applic;
            SaveApplic.Click += SaveRedaction;
            LoadInfo();
            NumApplic.Text = applic.Numberapplication;
            RentApplic.Text = applic.Rent;
            DescriptionApplic.Text = applic.Description;
            ClienApplic.SelectedIndex = applic.Client - 1;
            TypesApplication.SelectedIndex = applic.Type -1;
            StatusApplic.SelectedIndex = applic.Status -1;
            IspolApplic.SelectedIndex = applic.Ispol - 1;
            
            DescriptionIspol.IsEnabled =false;
            RentApplic.IsEnabled = false;
            NumApplic.IsEnabled = false;
            ClienApplic.IsEnabled = false;
            TypesApplication.IsEnabled = false;
            StatusApplic.IsEnabled = false;
        }
        
        if (StaticInfo.User.Idrole == 2)
        {
            IspolApplic.IsVisible = true;
            IspolTextblock.IsVisible = true;
            
            application = applic;
            SaveApplic.Click += SaveRedactionIspol;
            LoadInfo();
            NumApplic.Text = applic.Numberapplication;
            RentApplic.Text = applic.Rent;
            DescriptionApplic.Text = applic.Description;
            ClienApplic.SelectedIndex = applic.Client - 1;
            TypesApplication.SelectedIndex = applic.Type -1;
            StatusApplic.SelectedIndex = applic.Status-1;
            DescriptionIspol.Text = applic.Commentispol;
            IspolApplic.SelectedIndex = applic.Ispol - 1;
            RentApplic.IsEnabled = false;
            IspolApplic.IsEnabled = false;
            NumApplic.IsEnabled = false;
            ClienApplic.IsEnabled = false;
            TypesApplication.IsEnabled = false;
            DescriptionApplic.IsEnabled = false;
        }
    }

    private async void SaveRedactionIspol(object? sender, RoutedEventArgs e)
    {
        try
        {
            using (var context = new User304Context())
            {
                var applicRedaction = context.Applications.FirstOrDefault(x => x.Id == application.Id);
                if (string.IsNullOrEmpty(DescriptionIspol.Text))
                {
                    applicRedaction.Status = StatusApplic.SelectedIndex + 1;
                    await context.SaveChangesAsync();
                }
                else
                {
                    applicRedaction.Commentispol = DescriptionIspol.Text;
                    applicRedaction.Status = StatusApplic.SelectedIndex + 1;
                    await context.SaveChangesAsync();
                }

                if (StatusApplic.SelectedIndex + 1 == 1)
                {
                    Notification notification = new Notification()
                    {
                        Status = 1,
                        Nameemploye = StaticInfo.User.Iduser,
                        Numberapplication = applicRedaction.Id
                    };
                    context.Notifications.Add(notification);
                    await context.SaveChangesAsync();
                }
            }
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        catch (Exception exception)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    private async void SaveRedaction(object? sender, RoutedEventArgs e)
    {
        try
        {
            StringBuilder errors = new StringBuilder();
      
            if (string.IsNullOrEmpty(DescriptionApplic.Text))
                errors.AppendLine("Вы не ввели описание");
            if (IspolApplic.SelectedIndex == -1)
                errors.AppendLine("Вы не выбрали исполнителя");

            if (errors.Length > 0)
            {
                var message = MessageBoxManager.GetMessageBoxStandard("Уведомление", errors.ToString(), ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await message.ShowAsync();
                return;
            }

            using (var context = new User304Context())
            {
                var applicRedaction = context.Applications.FirstOrDefault(x => x.Id == application.Id);
                
                applicRedaction.Description = DescriptionApplic.Text;
                applicRedaction.Ispol = IspolApplic.SelectedIndex + 1;
                await context.SaveChangesAsync();
            }
        }
        catch (Exception exception)
        {
            
        }
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    public async void LoadInfo()
    {
        try
        {
            using (var context = new User304Context())
            {
                StatusApplic.ItemsSource = context.Statuses.OrderBy(x=>x.Idstatus).ToList();
                TypesApplication.ItemsSource = context.TypesApplics.OrderBy(x=>x.Idtype).ToList();
                ClienApplic.ItemsSource = context.Users.OrderBy(x=>x.Iduser).ToList();
                
                IspolApplic.ItemsSource = context.Users.OrderBy(x=>x.Iduser).ToList();
            }
        }
        catch (Exception e)
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Уведомление",
                "Произошла ошибка, проверьте подключение", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await message.ShowAsync();
        }
    }

    private async void SaveApplicOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(NumApplic.Text))
                errors.AppendLine("Вы не ввели номер заявки");
            if (string.IsNullOrEmpty(RentApplic.Text))
                errors.AppendLine("Вы не ввели оборудование");
            if (string.IsNullOrEmpty(DescriptionApplic.Text))
                errors.AppendLine("Вы не ввели описание");
            if (TypesApplication.SelectedIndex == -1)
                errors.AppendLine("Вы не ввели номер заявки");
            if (ClienApplic.SelectedIndex == -1)
                errors.AppendLine("Вы не выбрали клиента");
            if (IspolApplic.SelectedIndex == -1)
                errors.AppendLine("Вы не выбрали исполнителя");

            using (var context = new User304Context())
            {
                var checkNumber = context.Applications
                    .FirstOrDefault(x => NumApplic.Text != null && x.Numberapplication.Contains(NumApplic.Text));
                if(checkNumber != null)
                        errors.AppendLine("Заявка с таким номером уже существует");
            }

            if (errors.Length > 0)
            {
                var message = MessageBoxManager.GetMessageBoxStandard("Уведомление", errors.ToString(), ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await message.ShowAsync();
                return;
            }

            Application application = new Application();
            application.Numberapplication = NumApplic.Text;
            application.Dateaddapllication = DateTime.Now;
            application.Rent = RentApplic.Text;
            application.Type = TypesApplication.SelectedIndex + 1;
            application.Description = DescriptionApplic.Text;
            application.Client = ClienApplic.SelectedIndex + 1;
            application.Status = 3;
            application.Commentispol = "Нет коментариев";
            application.Ispol = IspolApplic.SelectedIndex + 1;

            using (var context = new User304Context())
            {
                context.Applications.Add(application);
                await context.SaveChangesAsync();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        catch (Exception exception)
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Уведомление",
                "Произошла ошибка, проверьте подключение", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await message.ShowAsync();
        }
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}