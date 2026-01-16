// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.Tween.Quintic
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Quintic
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      return c * (t /= d) * t * t * t * t + b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      return (double) (t /= d / 2f) < 1.0 ? c / 2f * t * t * t * t * t + b : (float) ((double) c / 2.0 * ((double) (t -= 2f) * (double) t * (double) t * (double) t * (double) t + 2.0)) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      return c * (float) ((double) (t = (float) ((double) t / (double) d - 1.0)) * (double) t * (double) t * (double) t * (double) t + 1.0) + b;
    }
  }
}
