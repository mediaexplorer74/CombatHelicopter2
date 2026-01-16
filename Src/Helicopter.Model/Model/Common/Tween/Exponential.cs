// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.Tween.Exponential
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Exponential
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      return (double) t != 0.0 ? c * (float) Math.Pow(2.0, 10.0 * ((double) t / (double) d - 1.0)) + b : b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      if ((double) t == 0.0)
        return b;
      if ((double) t == (double) d)
        return b + c;
      return (double) (t /= d / 2f) < 1.0 ? c / 2f * (float) Math.Pow(2.0, 10.0 * ((double) t - 1.0)) + b : c / 2f * (float) (-Math.Pow(2.0, -10.0 * (double) --t) + 2.0) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      return (double) t != (double) d ? c * (float) (-Math.Pow(2.0, -10.0 * (double) t / (double) d) + 1.0) + b : b + c;
    }
  }
}
