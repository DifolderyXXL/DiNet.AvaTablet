using System.Numerics;
using DiNet.AvaTablet.CV.PointSelector.Interfaces;
using DiNet.AvaTablet.CV.Trackers.Interfaces;
using OpenCvSharp;

namespace DiNet.AvaTablet.CV;

public class CamCursorTracker : IDisposable
{
    public event Action<Mat> OnRendered = null!;

    private bool _isTracking = false;
    
    private CamImageProvider? _camProvider;
    private IPointerTracker? _tracker;
    private IPointSelector? _selector;

    private Mat? _projection = null;
    private Vector2 _cursorPosition = default;

    public Vector2 CursorPosition => _cursorPosition;

    public CamCursorTracker(){}
    public CamCursorTracker(CamImageProvider cameraProvider, IPointerTracker tracker, IPointSelector selector)
    {
        _camProvider = cameraProvider;
        _tracker = tracker;
        _selector = selector;
    }

    public void SetPointerTracker(IPointerTracker tracker)
        => _tracker = tracker;
    
    public void SetPointSelector(IPointSelector selector)
        => _selector = selector;

    public void SetProjectionMatrix(Mat projection)
        => _projection = projection;

    public async Task StartTracking(CancellationToken ctx = default)
    {
        if (_isTracking)
            return;
        
        _isTracking = true;

        await Task.Run(() =>
        {
            while (!ctx.IsCancellationRequested)
                _cursorPosition = GetCursorPosition();
        }, ctx);

        _isTracking = false;
    }
    
    private Vector2 GetCursorPosition()
    {
        var image = _camProvider!.Provide(_projection);
        OnRendered?.Invoke(image);
        
        var points = _tracker!.TrackPoints(image);
        if( points.Length != 0 )
        _cursorPosition = _selector!.SelectPoint(points, _cursorPosition);
        return _cursorPosition;
    }

    public void Dispose()
    {
        _camProvider?.Dispose();
        _projection?.Dispose();
    }
}