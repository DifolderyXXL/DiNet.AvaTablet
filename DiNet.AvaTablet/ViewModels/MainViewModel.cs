using CommunityToolkit.Mvvm.ComponentModel;
using DiNet.AvaTablet.CV;
using DiNet.AvaTablet.CV.PointSelector;
using DiNet.AvaTablet.CV.Trackers;
using DiNet.AvaTablet.Model;
using OpenCvSharp;
using System.Threading;

namespace DiNet.AvaTablet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private CameraViewModel _cameraViewModel;
    [ObservableProperty] private BriskViewModel _briskViewModel;

    private CamCursorTracker _tracker;
    private CancellationTokenSource _trackerCts;

    public MainViewModel()
    {
        _cameraViewModel = new();
        _briskViewModel = new();

        var camImg = new CamImageProvider(VideoCapture.FromCamera(0, VideoCaptureAPIs.ANY));
        _tracker = new(camImg, new BriskTracker(BRISK.Create(75, 1, 1)), new NearestPointSelector());

        _briskViewModel.BriskSettingsChanged += BriskChanged;
        _tracker.OnRendered += _cameraViewModel.SetSource;

        _trackerCts = new();
        _tracker.StartTracking(_trackerCts.Token);
    }

    private void BriskChanged(BriskSettings obj)
    {
        _tracker.SetPointerTracker(new BriskTracker(BRISK.Create(obj.Thresh, obj.Octaves, obj.Thresh)));
    }
}
