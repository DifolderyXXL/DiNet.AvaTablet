using CommunityToolkit.Mvvm.ComponentModel;
using DiNet.AvaTablet.Model;
using System;
using System.ComponentModel;

namespace DiNet.AvaTablet.ViewModels;
public partial class BriskViewModel : ObservableObject
{
    public event Action<BriskSettings> BriskSettingsChanged = null!;

    [ObservableProperty] private BriskSettings _briskSettings;

    public BriskViewModel(BriskSettings? settings = null)
    {
        _briskSettings = settings ?? new();

        this.BriskSettings.PropertyChanged += BriskSettingsPropertyChanged;
    }

    private void BriskSettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        BriskSettingsChanged?.Invoke(BriskSettings);
    }
}