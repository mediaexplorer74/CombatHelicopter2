// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.Tween.Sinusoidal
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Sinusoidal
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      return (float) (-(double) c * Math.Cos((double) t / (double) d * (Math.PI / 2.0))) + c + b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      return (float) (-(double) c / 2.0 * (Math.Cos(Math.PI * (double) t / (double) d) - 1.0)) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      return c * (float) Math.Sin((double) t / (double) d * (Math.PI / 2.0)) + b;
    }
  }
}
