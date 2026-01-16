// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.RocketLauncherWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  internal class RocketLauncherWeapon : Weapon
  {
    protected float LaunchEngineTime = 0.2f;
    protected float StartXAcceleration;
    protected float StartYAcceleration = 300f;
    protected float StartXSpeed;
    protected float StartYSpeed = 300f;
    protected bool HasStartStage;

    public RocketLauncherWeapon(Instance owner)
      : base(owner)
    {
      this.Type = WeaponType.RocketLauncher;
      this.LaunchAngle = 0.0f;
    }

    public override Bullet GetBullet()
    {
      RocketBullet instance = RocketBullet.GetInstance();
      instance.Speed = new Vector2(this.StartXSpeed, this.StartYSpeed);
      instance.Acceleration = new Vector2(this.StartXAcceleration, this.StartYAcceleration);
      instance.LaunchEngineTime = this.LaunchEngineTime;
      instance.HasStartStage = this.HasStartStage;
      instance.DamageType = DamageType.Rocket;
      return (Bullet) instance;
    }

    public override void Configure(WeaponDescription weaponDesc)
    {
      base.Configure(weaponDesc);
      RocketWeaponDescription weaponDescription = weaponDesc is RocketWeaponDescription ? (RocketWeaponDescription) weaponDesc : throw new Exception("Invalid weapon description. It should be rocketweapondesc");
      this.LaunchEngineTime = weaponDescription.LaunchEngineTime;
      this.StartXAcceleration = weaponDescription.StartAccelerationX;
      this.StartYAcceleration = weaponDescription.StartAccelerationY;
      this.StartXSpeed = weaponDescription.StartSpeedX;
      this.StartYSpeed = weaponDescription.StartSpeedY;
      this.HasStartStage = weaponDescription.HasStartStage;
    }
  }
}
