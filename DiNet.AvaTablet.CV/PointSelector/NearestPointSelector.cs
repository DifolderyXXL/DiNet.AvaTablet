using System.Numerics;
using DiNet.AvaTablet.CV.PointSelector.Interfaces;

namespace DiNet.AvaTablet.CV.PointSelector;

public class NearestPointSelector : IPointSelector
{
    public Vector2 SelectPoint(Vector2[] points, Vector2 previousPoint)
    {
        int selected = 0;
        float minDistance = float.MaxValue;

        for (int i = 0; i < points.Length; i++)
        {
            var dist = Vector2.DistanceSquared(points[i], previousPoint);
            if (dist < minDistance)
            {
                minDistance = dist;
                selected = i;
            }
        }

        return points[selected];
    }
}