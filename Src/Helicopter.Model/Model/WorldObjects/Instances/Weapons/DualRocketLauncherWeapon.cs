// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.DualRocketLauncherWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  internal class DualRocketLauncherWeapon : RocketLauncherWeapon
  {
    public DualRocketLauncherWeapon(Instance owner)
      : base(owner)
    {
      this.Type = WeaponType.DualRocketLauncher;
      this.LaunchAngle = 0.0f;
    }

    public override Bullet GetBullet()
    {
      DoubleRocketBullet instance = DoubleRocketBullet.GetInstance();
      instance.Speed = new Vector2(this.StartXSpeed, this.StartYSpeed);
      instance.Acceleration = new Vector2(this.StartXAcceleration, this.StartYAcceleration);
      instance.LaunchEngineTime = this.LaunchEngineTime;
      instance.HasStartStage = this.HasStartStage;
      instance.DamageType = DamageType.Rocket;
      return (Bullet) instance;
    }
  }
}
