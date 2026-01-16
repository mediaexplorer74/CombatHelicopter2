// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.Tween.Elastic
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public class Elastic
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      if ((double) t == 0.0)
        return b;
      if ((double) (t /= d) == 1.0)
        return b + c;
      float num1 = d * 0.3f;
      float num2 = num1 / 4f;
      return (float) -((double) c * Math.Pow(2.0, 10.0 * (double) --t) * Math.Sin(((double) t * (double) d - (double) num2) * (2.0 * Math.PI) / (double) num1)) + b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      if ((double) t == 0.0)
        return b;
      if ((double) (t /= d / 2f) == 2.0)
        return b + c;
      float num1 = d * 0.450000018f;
      float num2 = c;
      float num3 = num1 / 4f;
      return (double) t < 1.0 ? -0.5f * (float) ((double) num2 * Math.Pow(2.0, 10.0 * (double) --t) * Math.Sin(((double) t * (double) d - (double) num3) * (2.0 * Math.PI) / (double) num1)) + b : (float) ((double) num2 * Math.Pow(2.0, -10.0 * (double) --t) * Math.Sin(((double) t * (double) d - (double) num3) * (2.0 * Math.PI) / (double) num1) * 0.5) + c + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      if ((double) t == 0.0)
        return b;
      if ((double) (t /= d) == 1.0)
        return b + c;
      float num1 = d * 0.3f;
      float num2 = num1 / 4f;
      return (float) ((double) c * Math.Pow(2.0, -10.0 * (double) t) * Math.Sin(((double) t * (double) d - (double) num2) * (2.0 * Math.PI) / (double) num1)) + c + b;
    }
  }
}
