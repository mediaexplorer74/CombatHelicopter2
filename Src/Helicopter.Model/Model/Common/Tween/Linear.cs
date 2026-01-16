// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.Tween.Linear
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public static class Linear
  {
    public static float EaseIn(float t, float b, float c, float d) => c * t / d + b;

    public static float EaseInOut(float t, float b, float c, float d) => c * t / d + b;

    public static float EaseNone(float t, float b, float c, float d) => c * t / d + b;

    public static float EaseOut(float t, float b, float c, float d) => c * t / d + b;
  }
}
