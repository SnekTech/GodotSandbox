using System.Collections.Generic;
using Godot;

namespace Sandbox;

public static class BezierCurve
{
    private static readonly float[] Factorial =
    [
        1.0f,
        1.0f,
        2.0f,
        6.0f,
        24.0f,
        120.0f,
        720.0f,
        5040.0f,
        40320.0f,
        362880.0f,
        3628800.0f,
        39916800.0f,
        479001600.0f,
        6227020800.0f,
        87178291200.0f,
        1307674368000.0f,
        20922789888000.0f,
        355687428096000.0f,
        6402373705728000.0f
    ];

    private static readonly int MaxN = Factorial.Length;

    private static float Binomial(int n, int i)
    {
        var a1 = Factorial[n];
        var a2 = Factorial[i];
        var a3 = Factorial[n - i];
        var ni = a1 / (a2 * a3);
        return ni;
    }

    private static float Bernstein(int n, int i, float t)
    {
        var ti = Mathf.Pow(t, i);
        var tnMinusI = Mathf.Pow(1 - t, n - i);
        var basis = Binomial(n, i) * ti * tnMinusI;
        return basis;
    }

    public static Vector3 Point3(float t, List<Vector3> controlPoints)
    {
        var trimmedControlPoints = GetTrimmed3(controlPoints);
        var n = trimmedControlPoints.Count - 1;

        if (t <= 0)
            return trimmedControlPoints[0];
        if (t >= 1)
            return trimmedControlPoints[^1];

        var point = new Vector3();

        for (var i = 0; i < trimmedControlPoints.Count; i++)
        {
            var bn = Bernstein(n, i, t) * trimmedControlPoints[i];
            point += bn;
        }

        return point;
    }

    public static List<Vector3> PointList3(List<Vector3> controlPoints, float interval = 0.01f)
    {
        var trimmedControlPoints = GetTrimmed3(controlPoints);
        var n = trimmedControlPoints.Count - 1;

        var points = new List<Vector3>();
        for (var t = 0f; t <= 1 + interval - 0.0001f; t += interval)
        {
            var p = new Vector3();
            for (var i = 0; i < trimmedControlPoints.Count; i++)
            {
                var bn = Bernstein(n, i, t) * trimmedControlPoints[i];
                p += bn;
            }

            points.Add(p);
        }

        return points;
    }
    
    public static Vector2 Point2(float t, List<Vector2> controlPoints)
    {
        var trimmedControlPoints = GetTrimmed2(controlPoints);
        var n = trimmedControlPoints.Count - 1;

        if (t <= 0)
            return trimmedControlPoints[0];
        if (t >= 1)
            return trimmedControlPoints[^1];

        var point = new Vector2();

        for (var i = 0; i < trimmedControlPoints.Count; i++)
        {
            var bn = Bernstein(n, i, t) * trimmedControlPoints[i];
            point += bn;
        }

        return point;
    }

    public static List<Vector2> PointList2(List<Vector2> controlPoints, float interval = 0.01f)
    {
        var trimmedControlPoints = GetTrimmed2(controlPoints);
        var n = trimmedControlPoints.Count - 1;

        var points = new List<Vector2>();
        for (var t = 0f; t <= 1 + interval - 0.0001f; t += interval)
        {
            var p = new Vector2();
            for (var i = 0; i < trimmedControlPoints.Count; i++)
            {
                var bn = Bernstein(n, i, t) * trimmedControlPoints[i];
                p += bn;
            }

            points.Add(p);
        }

        return points;
    }

    private static List<Vector3> GetTrimmed3(List<Vector3> controlPointsRaw)
    {
        var trimmedPoints = new List<Vector3>(controlPointsRaw);
        var n = controlPointsRaw.Count - 1;

        if (n <= MaxN) return trimmedPoints;

        GD.PushWarning($"You have used more than {MaxN} control points");
        trimmedPoints.RemoveRange(MaxN, controlPointsRaw.Count - MaxN);

        return trimmedPoints;
    }

    private static List<Vector2> GetTrimmed2(List<Vector2> controlPointsRaw)
    {
        var trimmedPoints = new List<Vector2>(controlPointsRaw);
        var n = controlPointsRaw.Count - 1;

        if (n <= MaxN) return trimmedPoints;

        GD.PushWarning($"You have used more than {MaxN} control points");
        trimmedPoints.RemoveRange(MaxN, controlPointsRaw.Count - MaxN);

        return trimmedPoints;
    }
}