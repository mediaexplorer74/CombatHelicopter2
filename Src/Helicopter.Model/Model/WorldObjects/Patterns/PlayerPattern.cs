// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Patterns.PlayerPattern
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  public class PlayerPattern : Pattern
  {
    public float DownAcceleration = 350f;
    public float StartSpeed = 300f;
    public float UpAcceleration = -350f;
    public bool IsResetSpeed = true;
    public float ResetSpeedValue;
    public float ShortReboundSpeed = 150f;
    public float FarReboundSpeed = 200f;
    public float Energy = 100f;
    public float SpeedY = 100f;
    public float IndestrictableAfterCollisionTime = 0.7f;
    public float EnergyOnMountainCollision = 100f;
    public int RightPositionOnScreen = 266;
    public int UpRotationSpeed = 25;
    public int DownRotationSpeed = 15;
    public int MaxBackDegree = -2;
    public int MaxForwardDegree = 7;
    public Point[] WeaponPosition;
  }
}
