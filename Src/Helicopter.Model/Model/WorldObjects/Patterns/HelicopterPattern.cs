// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Patterns.HelicopterPattern
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  public class HelicopterPattern : Pattern
  {
    public int DroidPatternId;
    public float Energy;
    public float FirstWeaponRate;
    public float FirstWeaponReloadTime;
    public float FirstWeaponShootingTime;
    public float HeightCompensation;
    public float HitShotCorridor;
    public HelicopterMotionType MotionType;
    public float ObstaclesReboundYSpeed;
    public float PatrolingSpeed;
    public float Price;
    public float PursuitAcceleration;
    public float PursuitMaxYSpeed;
    public float PursuitXSpeed;
    public float SecondWeaponRate;
    public float SecondWeaponReloadTime;
    public float SecondWeaponShootingTime;
    public int StartPursuitDistance;
    public List<WeaponSlotDescription> WeaponSlots;
    public float CollisionDamage;
    public float ShootDelay;

    public UnitType UnitType { get; set; }
  }
}
