using System.Numerics;
using DiNet.AvaTablet.CV.Trackers.Interfaces;
using OpenCvSharp;

namespace DiNet.AvaTablet.CV.Trackers;

public class BriskTracker : IPointerTracker
{
    private BRISK _brisk;

    public BriskTracker(BRISK brisk)
    {
        _brisk = brisk;
    }
    
    public Vector2[] TrackPoints(Mat image)
    {
        var keypoints = _brisk.Detect(image);

        var points = new Vector2[keypoints.Length];
        for (int i = 0; i < keypoints.Length; i++)
            points[i] = new(keypoints[i].Pt.X, keypoints[i].Pt.Y);

        return points;
    }
}