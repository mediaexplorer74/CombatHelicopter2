// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.Tween.Bounce
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Bounce
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      return c - Bounce.EaseOut(d - t, 0.0f, c, d) + b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      return (double) t < (double) d / 2.0 ? Bounce.EaseIn(t * 2f, 0.0f, c, d) * 0.5f + b : (float) ((double) Bounce.EaseOut(t * 2f - d, 0.0f, c, d) * 0.5 + (double) c * 0.5) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      if ((double) (t /= d) < 4.0 / 11.0)
        return c * (121f / 16f * t * t) + b;
      if ((double) t < 8.0 / 11.0)
        return c * (float) (121.0 / 16.0 * (double) (t -= 0.545454562f) * (double) t + 0.75) + b;
      return (double) t < 10.0 / 11.0 ? c * (float) (121.0 / 16.0 * (double) (t -= 0.8181818f) * (double) t + 15.0 / 16.0) + b : c * (float) (121.0 / 16.0 * (double) (t -= 0.954545438f) * (double) t + 63.0 / 64.0) + b;
    }
  }
}
