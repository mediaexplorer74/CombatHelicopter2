// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.DualMachineGunWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  internal class DualMachineGunWeapon : Weapon
  {
    private bool IsOdd { get; set; }

    public DualMachineGunWeapon(Instance owner)
      : base(owner)
    {
      this.ElapsedTimeFromLastShoot = this.Rate;
      this.Type = WeaponType.DualMachineGun;
    }

    protected override void AdditionalInitialization(Bullet bullet)
    {
      bullet.Position.Y += this.BulletSpawnPosition.Y;
      if (this.IsOdd)
        bullet.Position.Y -= 5f;
      this.IsOdd = !this.IsOdd;
    }

    public override Bullet GetBullet()
    {
      MachineGunBullet instance = MachineGunBullet.GetInstance();
      instance.DamageType = DamageType.Bullet;
      return (Bullet) instance;
    }
  }
}
