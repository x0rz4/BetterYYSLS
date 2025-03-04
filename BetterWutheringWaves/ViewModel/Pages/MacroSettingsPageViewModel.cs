﻿using YYSLS.Core.Config;
using YYSLS.Service.Interface;
using YYSLS.View.Pages;
using YYSLS.View.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace YYSLS.ViewModel.Pages;

public partial class MacroSettingsPageViewModel : ObservableObject, INavigationAware, IViewModel
{
    public AllConfig Config { get; set; }

    private readonly INavigationService _navigationService;

    public MacroSettingsPageViewModel(IConfigService configService, INavigationService navigationService)
    {
        Config = configService.Get();
        _navigationService = navigationService;
    }

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    public void OnGoToHotKeyPage()
    {
        _navigationService.Navigate(typeof(HotKeyPage));
    }

    [RelayCommand]
    public void OnEditAvatarMacro()
    {
        JsonMonoDialog.Show(@"User\avatar_macro.json");
    }

    [RelayCommand]
    public void OnGoToOneKeyMacroUrl()
    {
        Process.Start(new ProcessStartInfo("https://bgi.huiyadan.com/feats/onem.html") { UseShellExecute = true });
    }
}
