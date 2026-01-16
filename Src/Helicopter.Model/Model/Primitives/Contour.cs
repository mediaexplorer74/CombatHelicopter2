// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Primitives.Contour
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.Primitives
{
  public class Contour : List<Point>
  {
    public Rectangle Rectangle;
    private Point _location = Point.Zero;

    public bool IntersectRectangleOnly { get; set; }

    public void CopyFrom(Contour contour)
    {
      this.Clear();
      this.AddRange((IEnumerable<Point>) contour);
      this._location = contour._location;
      this.Rectangle = contour.Rectangle;
      this.IntersectRectangleOnly = contour.IntersectRectangleOnly;
    }

    public void HorizontalFlip(int width)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        int x = width - 1 - this[index].X;
        this[index] = new Point(x, this[index].Y);
      }
    }

    public void Increase(int inc)
    {
      Point center = this.Rectangle.Center;
      for (int index = 0; index < this.Count; ++index)
      {
        Point point = this[index];
        float num1 = (float) (point.X - center.X);
        float num2 = (float) (point.Y - center.Y);
        float num3 = (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
        float num4 = num2 / num3;
        float num5 = num1 / num3;
        float num6 = (float) inc * num5;
        float num7 = (float) inc * num4;
        this[index] = new Point((int) ((double) point.X + (double) num6), (int) ((double) point.Y + (double) num7));
      }
      this.UpdateRectangle();
      this._location = this.Rectangle.Location;
    }

    public bool Intersects(Contour contour) => Contour.Intersects(this, contour);

    public static bool Intersects(Contour a, Contour b)
    {
      if (a.Count == 0 || b.Count == 0 || !a.Rectangle.Intersects(b.Rectangle))
        return false;
      if (a.IntersectRectangleOnly || b.IntersectRectangleOnly || Contour.IsLinesIntersect(a, b))
        return true;
      if (!a.Rectangle.Contains(b.Rectangle) && !b.Rectangle.Contains(a.Rectangle))
        return false;
      return a.InsideContour(b) || b.InsideContour(a);
    }

    private static bool IsLinesIntersect(Contour a, Contour b)
    {
      Point point1 = b[b.Count - 1];
      foreach (Point point2 in (List<Point>) b)
      {
        Point point3 = a[a.Count - 1];
        Point endB = point2;
        endB.X += b._location.X + b.Rectangle.X;
        endB.Y += b._location.Y + b.Rectangle.Y;
        foreach (Point point4 in (List<Point>) a)
        {
          Point startA = point3;
          startA.X += a._location.X + a.Rectangle.X;
          startA.Y += a._location.Y + a.Rectangle.Y;
          Point endA = point4;
          endA.X += a._location.X + a.Rectangle.X;
          endA.Y += a._location.Y + a.Rectangle.Y;
          Point startB = point1;
          startB.X += b._location.X + b.Rectangle.X;
          startB.Y += b._location.Y + b.Rectangle.Y;
          if (Contour.IsLinesCross(startA, endA, startB, endB))
            return true;
          point3 = point4;
        }
        point1 = point2;
      }
      return false;
    }

    private bool InsideContour(Contour contour)
    {
      Vector2 lineStart = new Vector2((float) this.Rectangle.Center.X, (float) this.Rectangle.Center.Y);
      Vector2 lineEnd = new Vector2((float) contour.Rectangle.Center.X, (float) contour.Rectangle.Center.Y);
      bool flag = Contour.IntersectionWithLine(this, lineStart, lineEnd).Count > 0;
      return Contour.IntersectionWithLine(contour, lineStart, lineEnd).Count <= 0 || !flag;
    }

    public static List<Vector2> IntersectionWithLine(
      Contour contour,
      Vector2 lineStart,
      Vector2 lineEnd)
    {
      if (contour.Count < 2)
        return new List<Vector2>();
      List<Vector2> vector2List = new List<Vector2>();
      int x1 = (int) lineStart.X;
      int y1 = (int) lineStart.Y;
      int x2 = (int) lineEnd.X;
      int y2 = (int) lineEnd.Y;
      int num1 = contour._location.X + contour.Rectangle.X + contour[contour.Count - 1].X;
      int num2 = contour._location.Y + contour.Rectangle.Y + contour[contour.Count - 1].Y;
      for (int index = 0; index < contour.Count; ++index)
      {
        int num3 = contour._location.X + contour.Rectangle.X + contour[index].X;
        int num4 = contour._location.Y + contour.Rectangle.Y + contour[index].Y;
        if (Contour.IsLinesCross(new Point(num1, num2), new Point(num3, num4), new Point(x1, y1), new Point(x2, y2)))
        {
          Vector2 vector2 = Contour.LineCrossPosition(num1, num2, num3, num4, x1, y1, x2, y2);
          if (!vector2List.Contains(vector2))
            vector2List.Add(vector2);
        }
        num1 = num3;
        num2 = num4;
      }
      return vector2List;
    }

    public static Vector2 ClosestIntersectionPosition(
      Contour contour,
      Vector2 lineStart,
      Vector2 lineEnd)
    {
      if (contour.Count < 2)
        return Vector2.Zero;
      List<Vector2> source = new List<Vector2>();
      int x1 = (int) lineStart.X;
      int y1 = (int) lineStart.Y;
      int x2 = (int) lineEnd.X;
      int y2 = (int) lineEnd.Y;
      int x11 = contour._location.X + contour.Rectangle.X + contour[contour.Count - 1].X;
      int y11 = contour._location.Y + contour.Rectangle.Y + contour[contour.Count - 1].Y;
      for (int index = 0; index < contour.Count; ++index)
      {
        int x12 = contour._location.X + contour.Rectangle.X + contour[index].X;
        int y12 = contour._location.Y + contour.Rectangle.Y + contour[index].Y;
        Vector2 vector2 = Contour.LineCrossPosition(x11, y11, x12, y12, x1, y1, x2, y2);
        if (!source.Contains(vector2))
          source.Add(vector2);
        x11 = x12;
        y11 = y12;
      }
      float num1 = float.MaxValue;
      Vector2 vector2_1 = source.FirstOrDefault<Vector2>();
      foreach (Vector2 vector2_2 in source)
      {
        float num2 = Math.Abs((vector2_2 - lineStart).Length());
        if ((double) num2 < (double) num1)
        {
          num1 = num2;
          vector2_1 = vector2_2;
        }
      }
      return vector2_1;
    }

    private static bool IsLinesCross(Point startA, Point endA, Point startB, Point endB)
    {
      return Contour.IsLinesCross(startA.X, startA.Y, endA.X, endA.Y, startB.X, startB.Y, endB.X, endB.Y) && Contour.IsLinesCross(startB.X, startB.Y, endB.X, endB.Y, startA.X, startA.Y, endA.X, endA.Y);
    }

    private static bool IsLinesCross(
      int x11,
      int y11,
      int x12,
      int y12,
      int x21,
      int y21,
      int x22,
      int y22)
    {
      int num1 = x11;
      int num2 = x12;
      int num3 = x21;
      int num4 = x22;
      int num5 = y11;
      int num6 = y12;
      int num7 = y21;
      int num8 = y22;
      float num9 = (float) ((num4 - num3) * (num5 - num7) - (num8 - num7) * (num1 - num3)) / (float) ((num8 - num7) * (num2 - num1) - (num4 - num3) * (num6 - num5));
      return (double) num9 <= 1.0 && (double) num9 >= 0.0;
    }

    private static Vector2 LineCrossPosition(Point startA, Point endA, Point startB, Point endB)
    {
      return Contour.LineCrossPosition(startA.X, startA.Y, endA.X, endA.Y, startB.X, startB.Y, endB.X, endB.Y);
    }

    private static Vector2 LineCrossPosition(
      int x11,
      int y11,
      int x12,
      int y12,
      int x21,
      int y21,
      int x22,
      int y22)
    {
      int num1 = x11;
      int num2 = x12;
      int num3 = x21;
      int num4 = x22;
      int num5 = y11;
      int num6 = y12;
      int num7 = y21;
      int num8 = y22;
      float num9 = (float) ((num4 - num3) * (num5 - num7) - (num8 - num7) * (num1 - num3)) / (float) ((num8 - num7) * (num2 - num1) - (num4 - num3) * (num6 - num5));
      return new Vector2()
      {
        X = (float) num1 + num9 * (float) (num2 - num1),
        Y = (float) num5 + num9 * (float) (num6 - num5)
      };
    }

    public void SetLocation(int x, int y)
    {
      this.UpdateRectangle();
      this.Rectangle.X = this._location.X + x;
      this.Rectangle.Y = this._location.Y + y;
    }

    public void UpdateRectangle()
    {
      if (this.Count == 0)
      {
        this.Rectangle = Rectangle.Empty;
      }
      else
      {
        int num1 = int.MaxValue;
        int num2 = int.MaxValue;
        int num3 = int.MinValue;
        int num4 = int.MinValue;
        foreach (Point point in (List<Point>) this)
        {
          num1 = (int) MathHelper.Min((float) num1, (float) point.X);
          num2 = (int) MathHelper.Min((float) num2, (float) point.Y);
          num3 = (int) MathHelper.Max((float) num3, (float) point.X);
          num4 = (int) MathHelper.Max((float) num4, (float) point.Y);
        }
        this.Rectangle.X = this._location.X + num1;
        this.Rectangle.Y = this._location.X + num2;
        this.Rectangle.Width = num3 - num1;
        this.Rectangle.Height = num4 - num2;
      }
    }

    public void VerticalFlip(int height)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        int y = height - 1 - this[index].Y;
        this[index] = new Point(this[index].X, y);
      }
    }

    public void SetPoints(params Point[] points)
    {
      this.Clear();
      this.AddRange((IEnumerable<Point>) points);
      this.UpdateRectangle();
    }
  }
}
