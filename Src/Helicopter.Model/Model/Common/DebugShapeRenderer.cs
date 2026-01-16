// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.DebugShapeRenderer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Helicopter.Model.Common
{
  public static class DebugShapeRenderer
  {
    private const int sphereResolution = 30;
    private const int sphereLineCount = 93;
    private static readonly List<DebugShapeRenderer.DebugShape> cachedShapes = new List<DebugShapeRenderer.DebugShape>();
    private static readonly List<DebugShapeRenderer.DebugShape> activeShapes = new List<DebugShapeRenderer.DebugShape>();
    private static VertexPositionColor[] verts = new VertexPositionColor[64];
    private static GraphicsDevice graphics;
    private static BasicEffect effect;
    private static Vector3[] corners = new Vector3[8];
    private static Vector3[] unitSphere;
    private static bool _initialized;

    [Conditional("DEBUG")]
    public static void Initialize(GraphicsDevice graphicsDevice)
    {
      if (DebugShapeRenderer._initialized)
        return;
      DebugShapeRenderer._initialized = true;
      DebugShapeRenderer.graphics = graphicsDevice;
      DebugShapeRenderer.effect = new BasicEffect(graphicsDevice);
      DebugShapeRenderer.effect.VertexColorEnabled = true;
      DebugShapeRenderer.effect.TextureEnabled = false;
      DebugShapeRenderer.effect.DiffuseColor = Vector3.One;
      DebugShapeRenderer.effect.World = Matrix.Identity;
      DebugShapeRenderer.InitializeSphere();
    }

    [Conditional("DEBUG")]
    public static void AddLine(Vector3 a, Vector3 b, Color color)
    {
    }

    [Conditional("DEBUG")]
    public static void AddLine(Vector3 a, Vector3 b, Color color, float life)
    {
      DebugShapeRenderer.DebugShape shapeForLines = DebugShapeRenderer.GetShapeForLines(1, life);
      shapeForLines.Vertices[0] = new VertexPositionColor(a, color);
      shapeForLines.Vertices[1] = new VertexPositionColor(b, color);
    }

    [Conditional("DEBUG")]
    public static void AddTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
    }

    [Conditional("DEBUG")]
    public static void AddTriangle(Vector3 a, Vector3 b, Vector3 c, Color color, float life)
    {
      DebugShapeRenderer.DebugShape shapeForLines = DebugShapeRenderer.GetShapeForLines(3, life);
      shapeForLines.Vertices[0] = new VertexPositionColor(a, color);
      shapeForLines.Vertices[1] = new VertexPositionColor(b, color);
      shapeForLines.Vertices[2] = new VertexPositionColor(b, color);
      shapeForLines.Vertices[3] = new VertexPositionColor(c, color);
      shapeForLines.Vertices[4] = new VertexPositionColor(c, color);
      shapeForLines.Vertices[5] = new VertexPositionColor(a, color);
    }

    [Conditional("DEBUG")]
    public static void AddBoundingFrustum(BoundingFrustum frustum, Color color)
    {
    }

    [Conditional("DEBUG")]
    public static void AddBoundingFrustum(BoundingFrustum frustum, Color color, float life)
    {
      DebugShapeRenderer.DebugShape shapeForLines = DebugShapeRenderer.GetShapeForLines(12, life);
      frustum.GetCorners(DebugShapeRenderer.corners);
      shapeForLines.Vertices[0] = new VertexPositionColor(DebugShapeRenderer.corners[0], color);
      shapeForLines.Vertices[1] = new VertexPositionColor(DebugShapeRenderer.corners[1], color);
      shapeForLines.Vertices[2] = new VertexPositionColor(DebugShapeRenderer.corners[1], color);
      shapeForLines.Vertices[3] = new VertexPositionColor(DebugShapeRenderer.corners[2], color);
      shapeForLines.Vertices[4] = new VertexPositionColor(DebugShapeRenderer.corners[2], color);
      shapeForLines.Vertices[5] = new VertexPositionColor(DebugShapeRenderer.corners[3], color);
      shapeForLines.Vertices[6] = new VertexPositionColor(DebugShapeRenderer.corners[3], color);
      shapeForLines.Vertices[7] = new VertexPositionColor(DebugShapeRenderer.corners[0], color);
      shapeForLines.Vertices[8] = new VertexPositionColor(DebugShapeRenderer.corners[4], color);
      shapeForLines.Vertices[9] = new VertexPositionColor(DebugShapeRenderer.corners[5], color);
      shapeForLines.Vertices[10] = new VertexPositionColor(DebugShapeRenderer.corners[5], color);
      shapeForLines.Vertices[11] = new VertexPositionColor(DebugShapeRenderer.corners[6], color);
      shapeForLines.Vertices[12] = new VertexPositionColor(DebugShapeRenderer.corners[6], color);
      shapeForLines.Vertices[13] = new VertexPositionColor(DebugShapeRenderer.corners[7], color);
      shapeForLines.Vertices[14] = new VertexPositionColor(DebugShapeRenderer.corners[7], color);
      shapeForLines.Vertices[15] = new VertexPositionColor(DebugShapeRenderer.corners[4], color);
      shapeForLines.Vertices[16] = new VertexPositionColor(DebugShapeRenderer.corners[0], color);
      shapeForLines.Vertices[17] = new VertexPositionColor(DebugShapeRenderer.corners[4], color);
      shapeForLines.Vertices[18] = new VertexPositionColor(DebugShapeRenderer.corners[1], color);
      shapeForLines.Vertices[19] = new VertexPositionColor(DebugShapeRenderer.corners[5], color);
      shapeForLines.Vertices[20] = new VertexPositionColor(DebugShapeRenderer.corners[2], color);
      shapeForLines.Vertices[21] = new VertexPositionColor(DebugShapeRenderer.corners[6], color);
      shapeForLines.Vertices[22] = new VertexPositionColor(DebugShapeRenderer.corners[3], color);
      shapeForLines.Vertices[23] = new VertexPositionColor(DebugShapeRenderer.corners[7], color);
    }

    [Conditional("DEBUG")]
    public static void AddBoundingBox(BoundingBox box, Color color)
    {
    }

    [Conditional("DEBUG")]
    public static void AddBoundingBox(BoundingBox box, Color color, float life)
    {
      DebugShapeRenderer.DebugShape shapeForLines = DebugShapeRenderer.GetShapeForLines(12, life);
      box.GetCorners(DebugShapeRenderer.corners);
      shapeForLines.Vertices[0] = new VertexPositionColor(DebugShapeRenderer.corners[0], color);
      shapeForLines.Vertices[1] = new VertexPositionColor(DebugShapeRenderer.corners[1], color);
      shapeForLines.Vertices[2] = new VertexPositionColor(DebugShapeRenderer.corners[1], color);
      shapeForLines.Vertices[3] = new VertexPositionColor(DebugShapeRenderer.corners[2], color);
      shapeForLines.Vertices[4] = new VertexPositionColor(DebugShapeRenderer.corners[2], color);
      shapeForLines.Vertices[5] = new VertexPositionColor(DebugShapeRenderer.corners[3], color);
      shapeForLines.Vertices[6] = new VertexPositionColor(DebugShapeRenderer.corners[3], color);
      shapeForLines.Vertices[7] = new VertexPositionColor(DebugShapeRenderer.corners[0], color);
      shapeForLines.Vertices[8] = new VertexPositionColor(DebugShapeRenderer.corners[4], color);
      shapeForLines.Vertices[9] = new VertexPositionColor(DebugShapeRenderer.corners[5], color);
      shapeForLines.Vertices[10] = new VertexPositionColor(DebugShapeRenderer.corners[5], color);
      shapeForLines.Vertices[11] = new VertexPositionColor(DebugShapeRenderer.corners[6], color);
      shapeForLines.Vertices[12] = new VertexPositionColor(DebugShapeRenderer.corners[6], color);
      shapeForLines.Vertices[13] = new VertexPositionColor(DebugShapeRenderer.corners[7], color);
      shapeForLines.Vertices[14] = new VertexPositionColor(DebugShapeRenderer.corners[7], color);
      shapeForLines.Vertices[15] = new VertexPositionColor(DebugShapeRenderer.corners[4], color);
      shapeForLines.Vertices[16] = new VertexPositionColor(DebugShapeRenderer.corners[0], color);
      shapeForLines.Vertices[17] = new VertexPositionColor(DebugShapeRenderer.corners[4], color);
      shapeForLines.Vertices[18] = new VertexPositionColor(DebugShapeRenderer.corners[1], color);
      shapeForLines.Vertices[19] = new VertexPositionColor(DebugShapeRenderer.corners[5], color);
      shapeForLines.Vertices[20] = new VertexPositionColor(DebugShapeRenderer.corners[2], color);
      shapeForLines.Vertices[21] = new VertexPositionColor(DebugShapeRenderer.corners[6], color);
      shapeForLines.Vertices[22] = new VertexPositionColor(DebugShapeRenderer.corners[3], color);
      shapeForLines.Vertices[23] = new VertexPositionColor(DebugShapeRenderer.corners[7], color);
    }

    [Conditional("DEBUG")]
    public static void AddBoundingSphere(BoundingSphere sphere, Color color)
    {
    }

    [Conditional("DEBUG")]
    public static void AddBoundingSphere(BoundingSphere sphere, Color color, float life)
    {
      DebugShapeRenderer.DebugShape shapeForLines = DebugShapeRenderer.GetShapeForLines(93, life);
      for (int index = 0; index < DebugShapeRenderer.unitSphere.Length; ++index)
      {
        Vector3 position = DebugShapeRenderer.unitSphere[index] * sphere.Radius + sphere.Center;
        shapeForLines.Vertices[index] = new VertexPositionColor(position, color);
      }
    }

    [Conditional("DEBUG")]
    public static void Draw(float elapsed, Matrix view, Matrix projection)
    {
      DebugShapeRenderer.effect.View = view;
      DebugShapeRenderer.effect.Projection = projection;
      int num1 = 0;
      foreach (DebugShapeRenderer.DebugShape activeShape in DebugShapeRenderer.activeShapes)
        num1 += activeShape.LineCount * 2;
      if (num1 > 0)
      {
        if (DebugShapeRenderer.verts.Length < num1)
          DebugShapeRenderer.verts = new VertexPositionColor[num1 * 2];
        int val1 = 0;
        int num2 = 0;
        foreach (DebugShapeRenderer.DebugShape activeShape in DebugShapeRenderer.activeShapes)
        {
          val1 += activeShape.LineCount;
          int num3 = activeShape.LineCount * 2;
          for (int index = 0; index < num3; ++index)
            DebugShapeRenderer.verts[num2++] = activeShape.Vertices[index];
        }
        DebugShapeRenderer.effect.CurrentTechnique.Passes[0].Apply();
        int vertexOffset = 0;
        int primitiveCount;
        for (; val1 > 0; val1 -= primitiveCount)
        {
          primitiveCount = Math.Min(val1, (int) ushort.MaxValue);
          DebugShapeRenderer.graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, DebugShapeRenderer.verts, vertexOffset, primitiveCount);
          vertexOffset += primitiveCount * 2;
        }
      }
      bool flag = false;
      for (int index = DebugShapeRenderer.activeShapes.Count - 1; index >= 0; --index)
      {
        DebugShapeRenderer.DebugShape activeShape = DebugShapeRenderer.activeShapes[index];
        activeShape.Lifetime -= elapsed;
        if ((double) activeShape.Lifetime <= 0.0)
        {
          DebugShapeRenderer.cachedShapes.Add(activeShape);
          DebugShapeRenderer.activeShapes.RemoveAt(index);
          flag = true;
        }
      }
      if (!flag)
        return;
      DebugShapeRenderer.cachedShapes.Sort(new Comparison<DebugShapeRenderer.DebugShape>(DebugShapeRenderer.CachedShapesSort));
    }

    private static void InitializeSphere()
    {
      DebugShapeRenderer.unitSphere = new Vector3[186];
      float num1 = 0.209439516f;
      int num2 = 0;
      for (float num3 = 0.0f; (double) num3 < 6.2831854820251465; num3 += num1)
      {
        Vector3[] unitSphere1 = DebugShapeRenderer.unitSphere;
        int index1 = num2;
        int num4 = index1 + 1;
        unitSphere1[index1] = new Vector3((float) Math.Cos((double) num3), (float) Math.Sin((double) num3), 0.0f);
        Vector3[] unitSphere2 = DebugShapeRenderer.unitSphere;
        int index2 = num4;
        num2 = index2 + 1;
        unitSphere2[index2] = new Vector3((float) Math.Cos((double) num3 + (double) num1), (float) Math.Sin((double) num3 + (double) num1), 0.0f);
      }
      for (float num5 = 0.0f; (double) num5 < 6.2831854820251465; num5 += num1)
      {
        Vector3[] unitSphere3 = DebugShapeRenderer.unitSphere;
        int index3 = num2;
        int num6 = index3 + 1;
        unitSphere3[index3] = new Vector3((float) Math.Cos((double) num5), 0.0f, (float) Math.Sin((double) num5));
        Vector3[] unitSphere4 = DebugShapeRenderer.unitSphere;
        int index4 = num6;
        num2 = index4 + 1;
        unitSphere4[index4] = new Vector3((float) Math.Cos((double) num5 + (double) num1), 0.0f, (float) Math.Sin((double) num5 + (double) num1));
      }
      for (float num7 = 0.0f; (double) num7 < 6.2831854820251465; num7 += num1)
      {
        Vector3[] unitSphere5 = DebugShapeRenderer.unitSphere;
        int index5 = num2;
        int num8 = index5 + 1;
        unitSphere5[index5] = new Vector3(0.0f, (float) Math.Cos((double) num7), (float) Math.Sin((double) num7));
        Vector3[] unitSphere6 = DebugShapeRenderer.unitSphere;
        int index6 = num8;
        num2 = index6 + 1;
        unitSphere6[index6] = new Vector3(0.0f, (float) Math.Cos((double) num7 + (double) num1), (float) Math.Sin((double) num7 + (double) num1));
      }
    }

    private static int CachedShapesSort(
      DebugShapeRenderer.DebugShape s1,
      DebugShapeRenderer.DebugShape s2)
    {
      return s1.Vertices.Length.CompareTo(s2.Vertices.Length);
    }

    private static DebugShapeRenderer.DebugShape GetShapeForLines(int lineCount, float life)
    {
      DebugShapeRenderer.DebugShape shapeForLines = (DebugShapeRenderer.DebugShape) null;
      int length = lineCount * 2;
      for (int index = 0; index < DebugShapeRenderer.cachedShapes.Count; ++index)
      {
        if (DebugShapeRenderer.cachedShapes[index].Vertices.Length >= length)
        {
          shapeForLines = DebugShapeRenderer.cachedShapes[index];
          DebugShapeRenderer.cachedShapes.RemoveAt(index);
          DebugShapeRenderer.activeShapes.Add(shapeForLines);
          break;
        }
      }
      if (shapeForLines == null)
      {
        shapeForLines = new DebugShapeRenderer.DebugShape()
        {
          Vertices = new VertexPositionColor[length]
        };
        DebugShapeRenderer.activeShapes.Add(shapeForLines);
      }
      shapeForLines.LineCount = lineCount;
      shapeForLines.Lifetime = life;
      return shapeForLines;
    }

    private class DebugShape
    {
      public VertexPositionColor[] Vertices;
      public int LineCount;
      public float Lifetime;
    }
  }
}
