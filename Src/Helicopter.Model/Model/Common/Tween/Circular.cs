// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.Tween.Circular
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Circular
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      return (float) (-(double) c * (Math.Sqrt(1.0 - (double) (t /= d) * (double) t) - 1.0)) + b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      return (double) (t /= d / 2f) < 1.0 ? (float) (-(double) c / 2.0 * (Math.Sqrt(1.0 - (double) t * (double) t) - 1.0)) + b : (float) ((double) c / 2.0 * (Math.Sqrt(1.0 - (double) (t -= 2f) * (double) t) + 1.0)) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      return c * (float) Math.Sqrt(1.0 - (double) (t = (float) ((double) t / (double) d - 1.0)) * (double) t) + b;
    }
  }
}
