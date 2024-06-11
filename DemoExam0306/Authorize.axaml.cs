using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DemoExam0306.Context;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace DemoExam0306;

public partial class Authorize : Window
{
    public Authorize()
    {
        InitializeComponent();
    }

    private async void Authorizetion_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            using (var context = new User304Context())
            {
                var user = context.Users.FirstOrDefault(x => Password.Text != null && Login.Text != null && x.Login.Contains(Login.Text) 
                                                             && x.Password.Contains(Password.Text));
                if (user != null)
                {
                    StaticInfo.User = user;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    var message = MessageBoxManager.GetMessageBoxStandard("Уведомление",
                        "Логин или пароль не верны", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                    await message.ShowAsync();
                }
                
            }
        }
        catch (Exception exception)
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Уведомление",
                "Проверь подключение", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await message.ShowAsync();
        }
    }
}