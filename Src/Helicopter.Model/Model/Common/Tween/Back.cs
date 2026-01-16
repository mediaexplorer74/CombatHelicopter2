// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.Tween.Back
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Back
  {
    public static float EaseIn(float t, float b, float c, float d)
    {
      return (float) ((double) c * (double) (t /= d) * (double) t * (2.7015800476074219 * (double) t - 1.7015800476074219)) + b;
    }

    public static float EaseInOut(float t, float b, float c, float d)
    {
      float num1 = 1.70158f;
      float num2;
      float num3;
      return (double) (t /= d / 2f) < 1.0 ? (float) ((double) c / 2.0 * ((double) t * (double) t * (((double) (num2 = num1 * 1.525f) + 1.0) * (double) t - (double) num2))) + b : (float) ((double) c / 2.0 * ((double) (t -= 2f) * (double) t * (((double) (num3 = num1 * 1.525f) + 1.0) * (double) t + (double) num3) + 2.0)) + b;
    }

    public static float EaseOut(float t, float b, float c, float d)
    {
      return c * (float) ((double) (t = (float) ((double) t / (double) d - 1.0)) * (double) t * (2.7015800476074219 * (double) t + 1.7015800476074219) + 1.0) + b;
    }
  }
}
