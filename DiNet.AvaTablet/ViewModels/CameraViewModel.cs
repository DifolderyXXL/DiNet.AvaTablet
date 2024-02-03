using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using OpenCvSharp;

namespace DiNet.AvaTablet.ViewModels;
public partial class CameraViewModel : ViewModelBase
{
    [ObservableProperty]
    private Bitmap? _renderImage;

    public void SetSource(Mat view)
    {
        RenderImage = MatToBitmap(view);
    }

    private static Bitmap MatToBitmap(Mat mat)
    {
        using(var stream = mat.ToMemoryStream())
        {
            return new Bitmap(stream);
        }
    }
}
