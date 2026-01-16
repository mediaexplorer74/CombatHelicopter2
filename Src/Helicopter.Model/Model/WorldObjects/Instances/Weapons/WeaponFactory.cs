// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.WeaponFactory
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class WeaponFactory
  {
    public static Weapon GetCannonWeapon(Cannon owner, WeaponType type)
    {
      VerticalAlignment alignment = owner.CannonPattern.Alignment;
      Weapon weapon;
      switch (type)
      {
        case WeaponType.CannonMachineGun:
          int y1 = alignment == VerticalAlignment.Top ? 17 : 0;
          SingleMachineGunWeapon machineGunWeapon = new SingleMachineGunWeapon((Instance) owner);
          machineGunWeapon.BulletSpawnPosition = new Vector2(0.0f, (float) y1);
          weapon = (Weapon) machineGunWeapon;
          break;
        case WeaponType.CannonRocketLauncher:
          int y2 = alignment == VerticalAlignment.Top ? 24 : 0;
          CannonRocketLauncherWeapon rocketLauncherWeapon1 = new CannonRocketLauncherWeapon((Instance) owner);
          rocketLauncherWeapon1.BulletSpawnPosition = new Vector2(0.0f, (float) y2);
          weapon = (Weapon) rocketLauncherWeapon1;
          break;
        case WeaponType.CannonDualMachineGun:
          int y3 = alignment == VerticalAlignment.Top ? 24 : 0;
          CannonRocketLauncherWeapon rocketLauncherWeapon2 = new CannonRocketLauncherWeapon((Instance) owner);
          rocketLauncherWeapon2.BulletSpawnPosition = new Vector2(0.0f, (float) y3);
          weapon = (Weapon) rocketLauncherWeapon2;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      return WeaponFactory.InitWeapon(type, weapon);
    }

    public static Weapon GetWeapon(Instance owner, WeaponType type)
    {
      Weapon weapon;
      switch (type)
      {
        case WeaponType.CopterSingleMachineGun:
          SingleMachineGunWeapon machineGunWeapon1 = new SingleMachineGunWeapon(owner);
          machineGunWeapon1.BulletSpawnPosition = new Vector2(3f, 4f);
          weapon = (Weapon) machineGunWeapon1;
          break;
        case WeaponType.CopterDualMachineGun:
          DualMachineGunWeapon machineGunWeapon2 = new DualMachineGunWeapon(owner);
          machineGunWeapon2.BulletSpawnPosition = new Vector2(3f, 4f);
          weapon = (Weapon) machineGunWeapon2;
          break;
        case WeaponType.CopterRocketLauncher:
          RocketLauncherWeapon rocketLauncherWeapon1 = new RocketLauncherWeapon(owner);
          rocketLauncherWeapon1.BulletSpawnPosition = new Vector2(0.0f, 4f);
          weapon = (Weapon) rocketLauncherWeapon1;
          break;
        case WeaponType.CopterPlasmaGun:
          PlasmaGunWeapon plasmaGunWeapon1 = new PlasmaGunWeapon(owner);
          plasmaGunWeapon1.BulletSpawnPosition = new Vector2(10f, 7f);
          weapon = (Weapon) plasmaGunWeapon1;
          break;
        case WeaponType.CopterSharkRocketLauncher:
          RocketLauncherWeapon rocketLauncherWeapon2 = new RocketLauncherWeapon(owner);
          rocketLauncherWeapon2.BulletSpawnPosition = new Vector2(0.0f, 3f);
          weapon = (Weapon) rocketLauncherWeapon2;
          break;
        case WeaponType.CopterShield:
          ShieldWeapon shieldWeapon1 = new ShieldWeapon(owner);
          shieldWeapon1.BulletSpawnPosition = new Vector2(0.0f, -35f);
          weapon = (Weapon) shieldWeapon1;
          break;
        case WeaponType.CopterCasseteRocket:
          CassetteWeapon cassetteWeapon1 = new CassetteWeapon(owner);
          cassetteWeapon1.BulletSpawnPosition = new Vector2(0.0f, 4f);
          weapon = (Weapon) cassetteWeapon1;
          break;
        case WeaponType.BossSingleMachineGun:
          SingleMachineGunWeapon machineGunWeapon3 = new SingleMachineGunWeapon(owner);
          machineGunWeapon3.BulletSpawnPosition = new Vector2(3f, 8f);
          weapon = (Weapon) machineGunWeapon3;
          break;
        case WeaponType.BossDualMachineGun:
          DualMachineGunWeapon machineGunWeapon4 = new DualMachineGunWeapon(owner);
          machineGunWeapon4.BulletSpawnPosition = new Vector2(3f, 8f);
          weapon = (Weapon) machineGunWeapon4;
          break;
        case WeaponType.BossRocketLauncher:
          RocketLauncherWeapon rocketLauncherWeapon3 = new RocketLauncherWeapon(owner);
          rocketLauncherWeapon3.BulletSpawnPosition = new Vector2(65f, 7f);
          weapon = (Weapon) rocketLauncherWeapon3;
          break;
        case WeaponType.SingleMachineGun:
          SingleMachineGunWeapon machineGunWeapon5 = new SingleMachineGunWeapon(owner);
          machineGunWeapon5.BulletSpawnPosition = new Vector2(20f, 3f);
          weapon = (Weapon) machineGunWeapon5;
          break;
        case WeaponType.DualMachineGun:
          DualMachineGunWeapon machineGunWeapon6 = new DualMachineGunWeapon(owner);
          machineGunWeapon6.BulletSpawnPosition = new Vector2(20f, 3f);
          weapon = (Weapon) machineGunWeapon6;
          break;
        case WeaponType.RocketLauncher:
          RocketLauncherWeapon rocketLauncherWeapon4 = new RocketLauncherWeapon(owner);
          rocketLauncherWeapon4.BulletSpawnPosition = new Vector2(40f, 4f);
          weapon = (Weapon) rocketLauncherWeapon4;
          break;
        case WeaponType.DualRocketLauncher:
          DualRocketLauncherWeapon rocketLauncherWeapon5 = new DualRocketLauncherWeapon(owner);
          rocketLauncherWeapon5.BulletSpawnPosition = new Vector2(40f, 4f);
          weapon = (Weapon) rocketLauncherWeapon5;
          break;
        case WeaponType.CasseteRocket:
          CassetteWeapon cassetteWeapon2 = new CassetteWeapon(owner);
          cassetteWeapon2.BulletSpawnPosition = new Vector2(40f, 4f);
          weapon = (Weapon) cassetteWeapon2;
          break;
        case WeaponType.CasseteRocketUpdated:
          CassetteWeapon cassetteWeapon3 = new CassetteWeapon(owner);
          cassetteWeapon3.IsUpgraded = true;
          cassetteWeapon3.BulletSpawnPosition = new Vector2(40f, 4f);
          weapon = (Weapon) cassetteWeapon3;
          break;
        case WeaponType.Shield:
          ShieldWeapon shieldWeapon2 = new ShieldWeapon(owner);
          shieldWeapon2.BulletSpawnPosition = new Vector2(40f, 4f);
          weapon = (Weapon) shieldWeapon2;
          break;
        case WeaponType.HomingRocket:
          HomingRocketWeapon homingRocketWeapon = new HomingRocketWeapon(owner);
          homingRocketWeapon.BulletSpawnPosition = new Vector2(40f, 4f);
          weapon = (Weapon) homingRocketWeapon;
          break;
        case WeaponType.PlasmaGun:
          PlasmaGunWeapon plasmaGunWeapon2 = new PlasmaGunWeapon(owner);
          plasmaGunWeapon2.BulletSpawnPosition = new Vector2(20f, 7f);
          weapon = (Weapon) plasmaGunWeapon2;
          break;
        case WeaponType.Vulcan:
          VulcanWeapon vulcanWeapon = new VulcanWeapon(owner);
          vulcanWeapon.BulletSpawnPosition = new Vector2(20f, 3f);
          weapon = (Weapon) vulcanWeapon;
          break;
        case WeaponType.CannonMachineGun:
          SingleMachineGunWeapon machineGunWeapon7 = new SingleMachineGunWeapon(owner);
          machineGunWeapon7.BulletSpawnPosition = new Vector2(0.0f, 0.0f);
          weapon = (Weapon) machineGunWeapon7;
          break;
        case WeaponType.CannonRocketLauncher:
          CannonRocketLauncherWeapon rocketLauncherWeapon6 = new CannonRocketLauncherWeapon(owner);
          rocketLauncherWeapon6.BulletSpawnPosition = new Vector2(0.0f, 0.0f);
          weapon = (Weapon) rocketLauncherWeapon6;
          break;
        case WeaponType.CannonDualMachineGun:
          CannonRocketLauncherWeapon rocketLauncherWeapon7 = new CannonRocketLauncherWeapon(owner);
          rocketLauncherWeapon7.BulletSpawnPosition = new Vector2(0.0f, 0.0f);
          weapon = (Weapon) rocketLauncherWeapon7;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (type));
      }
      return WeaponFactory.InitWeapon(type, weapon);
    }

    private static Weapon InitWeapon(WeaponType type, Weapon weapon)
    {
      WeaponDescription descriptionForType = new WeaponDescriptionManager().GetDescriptionForType(type);
      weapon.Configure(descriptionForType);
      weapon.Type = type;
      return weapon;
    }
  }
}
