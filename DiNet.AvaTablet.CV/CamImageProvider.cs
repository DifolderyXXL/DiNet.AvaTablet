using OpenCvSharp;

namespace DiNet.AvaTablet.CV;

public class CamImageProvider : IDisposable
{
    private VideoCapture _capture;
    private Mat _renderImage;
    
    public CamImageProvider(VideoCapture capture)
    {
        _capture = capture;
        
        _renderImage = new();
    }
    
    public Mat Provide(Mat? perspectiveMat = null)
    {
        _capture.Read(_renderImage);
        if (_renderImage.Empty()) 
            throw new Exception("Render image is empty.");

        if (perspectiveMat != null)
            return _renderImage.WarpPerspective(perspectiveMat, _renderImage.Size());
        
        return _renderImage;
    }

    public void Dispose()
    {
        _renderImage.Dispose();
    }
}