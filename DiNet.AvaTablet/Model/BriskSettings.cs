using CommunityToolkit.Mvvm.ComponentModel;

namespace DiNet.AvaTablet.Model;

public partial class BriskSettings : ObservableObject
{
    [ObservableProperty] private int _thresh;
    [ObservableProperty] private int _octaves;
    [ObservableProperty] private int _patternScale;
}