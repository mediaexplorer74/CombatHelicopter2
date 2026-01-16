// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.Tween.Quadratic
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Quadratic
  {
    public static float EaseIn(float t, float b, float c, float d) => c * (t /= d) * t + b;

    public static float EaseInOut(float t, float b, float c, float d)
    {
      return (double) (t /= d / 2f) < 1.0 ? c / 2f * t * t + b : (float) (-(double) c / 2.0 * ((double) --t * ((double) t - 2.0) - 1.0)) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      return (float) (-(double) c * (double) (t /= d) * ((double) t - 2.0)) + b;
    }
  }
}
