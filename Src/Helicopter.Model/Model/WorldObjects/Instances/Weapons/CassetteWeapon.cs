// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.CassetteWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  internal class CassetteWeapon : Weapon
  {
    public float EndAngle = 60f;
    public int NumberOfBalls = 8;
    public float StartAngle = -60f;
    public float TimeToExplosion = 0.5f;
    private int _childLinearSpeed;
    private int _childLinearAcceleration;

    public bool IsUpgraded { get; set; }

    public CassetteWeapon(Instance owner)
      : base(owner)
    {
      this.Type = WeaponType.CasseteRocket;
    }

    public override void Configure(WeaponDescription weaponDesc)
    {
      CasseteBulletDescription bulletDescription = weaponDesc is CasseteBulletDescription ? (CasseteBulletDescription) weaponDesc : throw new ArgumentOutOfRangeException();
      this.NumberOfBalls = this.IsUpgraded ? bulletDescription.NumberOfBallsUpdated : bulletDescription.NumberOfBalls;
      this.StartAngle = bulletDescription.StartAngle;
      this.EndAngle = bulletDescription.EndAngle;
      this.TimeToExplosion = bulletDescription.TimeToExplosion;
      this._childLinearSpeed = bulletDescription.ChildLinearSpeed;
      this._childLinearAcceleration = bulletDescription.ChildLinearAcceleration;
      base.Configure(weaponDesc);
    }

    public override Bullet GetBullet()
    {
      CassetteBullet instance = CassetteBullet.GetInstance();
      instance.NumberOfBalls = this.NumberOfBalls;
      instance.StartAngle = this.StartAngle;
      instance.EndAngle = this.EndAngle;
      instance.TimeToExplosion = this.TimeToExplosion;
      instance.DamageType = DamageType.Bullet;
      instance.ChildLinearSpeed = this._childLinearSpeed;
      instance.ChildLinearAcceleration = this._childLinearAcceleration;
      return (Bullet) instance;
    }
  }
}
