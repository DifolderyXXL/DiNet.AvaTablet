using System.Numerics;
using OpenCvSharp;

namespace DiNet.AvaTablet.CV.Trackers.Interfaces;

public interface IPointerTracker
{
    public Vector2[] TrackPoints(Mat image);
}