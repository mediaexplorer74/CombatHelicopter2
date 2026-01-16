// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.SingleMachineGunWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class SingleMachineGunWeapon : Weapon
  {
    public SingleMachineGunWeapon(Instance owner)
      : base(owner)
    {
      this.ElapsedTimeFromLastShoot = this.Rate;
      this.Type = WeaponType.SingleMachineGun;
    }

    protected override void AdditionalInitialization(Bullet bullet)
    {
      bullet.Position.Y += this.BulletSpawnPosition.Y + (float) CommonRandom.Instance.Random.Next(-8, 8);
    }

    public override Bullet GetBullet()
    {
      MachineGunBullet instance = MachineGunBullet.GetInstance();
      instance.DamageType = DamageType.Bullet;
      return (Bullet) instance;
    }
  }
}
