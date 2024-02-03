using System.Numerics;

namespace DiNet.AvaTablet.CV.PointSelector.Interfaces;

public interface IPointSelector
{
    public Vector2 SelectPoint(Vector2[] points, Vector2 previousPoint);
}