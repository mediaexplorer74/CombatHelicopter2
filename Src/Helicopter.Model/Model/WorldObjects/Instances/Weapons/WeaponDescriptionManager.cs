// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.WeaponDescriptionManager
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class WeaponDescriptionManager
  {
    public static float SingleMachineGunDamage = 10f;
    public static float SingleMachineGunRate = 0.5f;
    public static float SingleMachineGunAcceleration = 50f;
    public static float SingleMachineGunStartSpeed = 2400f;
    public static float SingleMachineGunShootingTime = 1f;
    public static float SingleMachineGunReloadTime = -1f;
    public static WeaponDescription SingleMachineGunDescription = new WeaponDescription()
    {
      Damage = WeaponDescriptionManager.SingleMachineGunDamage,
      Acceleration = WeaponDescriptionManager.SingleMachineGunAcceleration,
      Rate = WeaponDescriptionManager.SingleMachineGunRate,
      StartSpeed = WeaponDescriptionManager.SingleMachineGunStartSpeed,
      ShootingTime = WeaponDescriptionManager.SingleMachineGunShootingTime,
      ReloadTime = WeaponDescriptionManager.SingleMachineGunReloadTime
    };
    public static float DualMachineGunDamage = 10f;
    public static float DualMachineGunRate = 0.3f;
    public static float DualMachineGunAcceleration = 50f;
    public static float DualMachineGunStartSpeed = 2400f;
    public static float DualMachineGunShootingTime = 1f;
    public static float DualMachineGunReloadTime = -1f;
    public static WeaponDescription DualMachineGunDescription = new WeaponDescription()
    {
      Damage = WeaponDescriptionManager.DualMachineGunDamage,
      Rate = WeaponDescriptionManager.DualMachineGunRate,
      Acceleration = WeaponDescriptionManager.DualMachineGunAcceleration,
      StartSpeed = WeaponDescriptionManager.DualMachineGunStartSpeed,
      ShootingTime = WeaponDescriptionManager.DualMachineGunShootingTime,
      ReloadTime = WeaponDescriptionManager.DualMachineGunReloadTime
    };
    public static float RocketLauncherDamage = 30f;
    public static float RocketLauncherRate = 2f;
    public static float RocketLauncherAcceleration = 2500f;
    public static float RocketLauncherStartSpeed = 100f;
    public static float RocketLauncherLaunchEngineTime = 0.1f;
    public static float RocketLauncherStartXAcceleration = 0.0f;
    public static float RocketLauncherStartYAcceleration = 100f;
    public static float RocketLauncherStartXSpeed = 0.0f;
    public static float RocketLauncherStartYSpeed = 100f;
    public static float RocketLauncherGunShootingTime = 1f;
    public static float RocketLauncherGunReloadTime = -1f;
    public static RocketWeaponDescription RocketLauncherDescription;
    public static float CopterSingleMachineGunDamage;
    public static float CopterSingleMachineGunRate;
    public static float CopterSingleMachineGunAcceleration;
    public static float CopterSingleMachineGunStartSpeed;
    public static float CopterSingleMachineGunGunShootingTime;
    public static float CopterSingleMachineGunGunReloadTime;
    public static WeaponDescription CopterSingleMachineGunDescription;
    public static float CopterDoubleMachineGunDamage;
    public static float CopterDoubleMachineGunRate;
    public static float CopterDoubleMachineGunAcceleration;
    public static float CopterDoubleMachineGunStartSpeed;
    public static float CopterDoubleMachineGunGunShootingTime;
    public static float CopterDoubleMachineGunGunReloadTime;
    public static WeaponDescription CopterDoubleMachineGunDescription;
    public static float BossCopterDoubleMachineGunDamage;
    public static float BossCopterDoubleMachineGunRate;
    public static float BossCopterDoubleMachineGunAcceleration;
    public static float BossCopterDoubleMachineGunStartSpeed;
    public static float BossCopterDoubleMachineGunGunShootingTime;
    public static float BossCopterDoubleMachineGunGunReloadTime;
    public static WeaponDescription BossDoubleMachineGunDescription;
    public static float BossCopterSingleMachineGunDamage;
    public static float BossCopterSingleMachineGunRate;
    public static float BossCopterSingleMachineGunAcceleration;
    public static float BossCopterSingleMachineGunStartSpeed;
    public static float BossCopterSingleMachineGunGunShootingTime;
    public static float BossCopterSingleMachineGunGunReloadTime;
    public static WeaponDescription BossSingleMachineGunDescription;
    public static float CopterRocketLauncherDamage;
    public static float CopterRocketLauncherRate;
    public static float CopterRocketLauncherAcceleration;
    public static float CopterRocketLauncherStartSpeed;
    public static float CopterRocketLauncherLaunchEngineTime;
    public static float CopterRocketLauncherStartXAcceleration;
    public static float CopterRocketLauncherStartYAcceleration;
    public static float CopterRocketLauncherStartXSpeed;
    public static float CopterRocketLauncherStartYSpeed;
    public static float CopterRocketLauncherGunShootingTime;
    public static float CopterRocketLauncherGunReloadTime;
    public static RocketWeaponDescription CopterRocketLauncherDescription;
    public static float BossCopterRocketLauncherDamage;
    public static float BossCopterRocketLauncherRate;
    public static float BossCopterRocketLauncherAcceleration;
    public static float BossCopterRocketLauncherStartSpeed;
    public static float BossCopterRocketLauncherLaunchEngineTime;
    public static float BossCopterRocketLauncherStartXAcceleration;
    public static float BossCopterRocketLauncherStartYAcceleration;
    public static float BossCopterRocketLauncherStartXSpeed;
    public static float BossCopterRocketLauncherStartYSpeed;
    public static float BossCopterRocketLauncherGunShootingTime;
    public static float BossCopterRocketLauncherGunReloadTime;
    public static RocketWeaponDescription BossRocketLauncherDescription;
    public static float DualRocketLauncherDamage;
    public static float DualRocketLauncherRate;
    public static float DualRocketLauncherAcceleration;
    public static float DualRocketLauncherStartSpeed;
    public static float DualRocketLauncherLaunchEngineTime;
    public static float DualRocketLauncherStartXAcceleration;
    public static float DualRocketLauncherStartYAcceleration;
    public static float DualRocketLauncherStartXSpeed;
    public static float DualRocketLauncherStartYSpeed;
    public static float DualRocketLauncherShootingTime;
    public static float DualRocketLauncherReloadTime;
    public static RocketWeaponDescription DualRocketLauncherDescription;
    public static float HomingRocketDamage;
    public static float HomingRocketRate;
    public static float HomingRocketAcceleration;
    public static float HomingRocketStartSpeed;
    public static float HomingRocketLaunchEngineTime;
    public static float HomingRocketStartXAcceleration;
    public static float HomingRocketStartYAcceleration;
    public static float HomingRocketStartXSpeed;
    public static float HomingRocketStartYSpeed;
    public static float HomingRocketShootingTime;
    public static float HomingRocketReloadTime;
    public static WeaponDescription HomingRocketDescription;
    public static float CasseteRocketDamage;
    public static float CasseteRocketRate;
    public static float CasseteRocketAcceleration;
    public static float CasseteRocketStartSpeed;
    public static float CasseteRocketStartAngle;
    public static float CasseteRocketEndAngle;
    public static float CasseteTimeToExplosion;
    public static int CasseteNumberOfBalls;
    public static int CasseteNumberOfBallsUpdated;
    public static float CasseteRocketGunShootingTime;
    public static float CasseteRocketGunReloadTime;
    public static CasseteBulletDescription CasseteRocketDescription;
    public static CasseteBulletDescription CopterCasseteRocketDescription;
    public static float ShieldDamage;
    public static float ShieldRate;
    public static float ShieldAcceleration;
    public static float ShieldStartSpeed;
    public static float ShieldRechargeSpeed;
    public static float ShieldDecreaseSpeed;
    public static float ShieldMaxEnergy;
    public static float ShieldStartEnergy;
    public static ShieldDescription ShieldDescription;
    public static ShieldDescription BossShieldDescription;
    public static float PlasmaGunDamage;
    public static float PlasmaGunRate;
    public static float PlasmaGunAcceleration;
    public static float PlasmaGunStartSpeed;
    public static float PlasmaGunRechargeSpeed;
    public static float PlasmaGunDecreaseSpeed;
    public static float PlasmaGunMaxEnergy;
    public static float PlasmaGunStartEnergy;
    public static float PlasmaGunTimeToBeam;
    public static float PlasmaGunGunShootingTime;
    public static float PlasmaGunGunReloadTime;
    public static WeaponDescription PlasmaGunDescription;
    public static float CopterPlasmaGunDamage;
    public static float CopterPlasmaGunRate;
    public static float CopterPlasmaGunAcceleration;
    public static float CopterPlasmaGunStartSpeed;
    public static float CopterPlasmaGunRechargeSpeed;
    public static float CopterPlasmaGunDecreaseSpeed;
    public static float CopterPlasmaGunMaxEnergy;
    public static float CopterPlasmaGunStartEnergy;
    public static float CopterPlasmaGunTimeToBeam;
    public static float CopterPlasmaGunGunShootingTime;
    public static float CopterPlasmaGunGunReloadTime;
    public static WeaponDescription CopterPlasmaGunDescription;
    public static float VulcanDamage;
    public static float VulcanRate;
    public static float VulcanAcceleration;
    public static float VulcanStartSpeed;
    public static float VulcanGunShootingTime;
    public static float VulcanGunReloadTime;
    public static float VulcanRechargeSpeed;
    public static float VulcanCurrentEnergy;
    public static float VulcanDecreaseEnergy;
    public static float VulcanMaxEnergy;
    public static WeaponDescription VulcanDescription;
    public static float CannonMachineGunDamage;
    public static float CannonMachineGunAcceleration;
    public static float CannonMachineGunRate;
    public static float CannonMachineGunStartSpeed;
    public static float CannonMachineGunShootingTime;
    public static float CannonMachineGunReloadTime;
    public static WeaponDescription CannonMachineGunDescription;
    public static float CannonRocketLauncherDamage;
    public static float CannonRocketLauncherRate;
    public static float CannonRocketLauncherAcceleration;
    public static float CannonRocketLauncherStartSpeed;
    public static float CannonRocketLauncherLaunchEngineTime;
    public static float CannonRocketLauncherStartXAcceleration;
    public static float CannonRocketLauncherStartYAcceleration;
    public static float CannonRocketLauncherStartXSpeed;
    public static float CannonRocketLauncherStartYSpeed;
    public static float CannonRocketLauncherShootingTime;
    public static float CannonRocketLauncherReloadTime;
    public static WeaponDescription CannonRocketLauncherDescription;

    public WeaponDescription GetDescriptionForType(WeaponType type)
    {
      switch (type)
      {
        case WeaponType.CopterSingleMachineGun:
          return WeaponDescriptionManager.CopterSingleMachineGunDescription;
        case WeaponType.CopterDualMachineGun:
          return WeaponDescriptionManager.CopterDoubleMachineGunDescription;
        case WeaponType.CopterRocketLauncher:
        case WeaponType.CopterSharkRocketLauncher:
          return (WeaponDescription) WeaponDescriptionManager.CopterRocketLauncherDescription;
        case WeaponType.CopterPlasmaGun:
          return WeaponDescriptionManager.CopterPlasmaGunDescription;
        case WeaponType.CopterShield:
          return (WeaponDescription) WeaponDescriptionManager.BossShieldDescription;
        case WeaponType.CopterCasseteRocket:
          return (WeaponDescription) WeaponDescriptionManager.CopterCasseteRocketDescription;
        case WeaponType.BossSingleMachineGun:
          return WeaponDescriptionManager.BossSingleMachineGunDescription;
        case WeaponType.BossDualMachineGun:
          return WeaponDescriptionManager.BossDoubleMachineGunDescription;
        case WeaponType.BossRocketLauncher:
          return (WeaponDescription) WeaponDescriptionManager.BossRocketLauncherDescription;
        case WeaponType.SingleMachineGun:
          return WeaponDescriptionManager.SingleMachineGunDescription;
        case WeaponType.DualMachineGun:
          return WeaponDescriptionManager.DualMachineGunDescription;
        case WeaponType.RocketLauncher:
          return (WeaponDescription) WeaponDescriptionManager.RocketLauncherDescription;
        case WeaponType.DualRocketLauncher:
          return (WeaponDescription) WeaponDescriptionManager.DualRocketLauncherDescription;
        case WeaponType.CasseteRocket:
        case WeaponType.CasseteRocketUpdated:
          return (WeaponDescription) WeaponDescriptionManager.CasseteRocketDescription;
        case WeaponType.Shield:
          return (WeaponDescription) WeaponDescriptionManager.ShieldDescription;
        case WeaponType.HomingRocket:
          return WeaponDescriptionManager.HomingRocketDescription;
        case WeaponType.PlasmaGun:
          return WeaponDescriptionManager.PlasmaGunDescription;
        case WeaponType.Vulcan:
          return WeaponDescriptionManager.VulcanDescription;
        case WeaponType.CannonMachineGun:
          return WeaponDescriptionManager.CannonMachineGunDescription;
        case WeaponType.CannonRocketLauncher:
          return WeaponDescriptionManager.CannonRocketLauncherDescription;
        case WeaponType.CannonDualMachineGun:
          return WeaponDescriptionManager.CannonRocketLauncherDescription;
        default:
          throw new ArgumentOutOfRangeException(nameof (type));
      }
    }

    static WeaponDescriptionManager()
    {
      RocketWeaponDescription weaponDescription1 = new RocketWeaponDescription();
      weaponDescription1.Acceleration = WeaponDescriptionManager.RocketLauncherAcceleration;
      weaponDescription1.Rate = WeaponDescriptionManager.RocketLauncherRate;
      weaponDescription1.Damage = WeaponDescriptionManager.RocketLauncherDamage;
      weaponDescription1.StartSpeed = WeaponDescriptionManager.RocketLauncherStartSpeed;
      weaponDescription1.LaunchEngineTime = WeaponDescriptionManager.RocketLauncherLaunchEngineTime;
      weaponDescription1.HasStartStage = true;
      weaponDescription1.StartAccelerationX = WeaponDescriptionManager.RocketLauncherStartXAcceleration;
      weaponDescription1.StartAccelerationY = WeaponDescriptionManager.RocketLauncherStartYAcceleration;
      weaponDescription1.StartSpeedX = WeaponDescriptionManager.RocketLauncherStartXSpeed;
      weaponDescription1.StartSpeedY = WeaponDescriptionManager.RocketLauncherStartYSpeed;
      weaponDescription1.ShootingTime = WeaponDescriptionManager.RocketLauncherGunShootingTime;
      weaponDescription1.ReloadTime = WeaponDescriptionManager.RocketLauncherGunReloadTime;
      WeaponDescriptionManager.RocketLauncherDescription = weaponDescription1;
      WeaponDescriptionManager.CopterSingleMachineGunDamage = 10f;
      WeaponDescriptionManager.CopterSingleMachineGunRate = 1f;
      WeaponDescriptionManager.CopterSingleMachineGunAcceleration = 50f;
      WeaponDescriptionManager.CopterSingleMachineGunStartSpeed = 800f;
      WeaponDescriptionManager.CopterSingleMachineGunGunShootingTime = 1f;
      WeaponDescriptionManager.CopterSingleMachineGunGunReloadTime = -1f;
      WeaponDescriptionManager.CopterSingleMachineGunDescription = new WeaponDescription()
      {
        Damage = WeaponDescriptionManager.CopterSingleMachineGunDamage,
        Acceleration = WeaponDescriptionManager.CopterSingleMachineGunAcceleration,
        Rate = WeaponDescriptionManager.CopterSingleMachineGunRate,
        StartSpeed = WeaponDescriptionManager.CopterSingleMachineGunStartSpeed,
        ShootingTime = WeaponDescriptionManager.CopterSingleMachineGunGunShootingTime,
        ReloadTime = WeaponDescriptionManager.CopterSingleMachineGunGunReloadTime
      };
      WeaponDescriptionManager.CopterDoubleMachineGunDamage = 10f;
      WeaponDescriptionManager.CopterDoubleMachineGunRate = 0.5f;
      WeaponDescriptionManager.CopterDoubleMachineGunAcceleration = 50f;
      WeaponDescriptionManager.CopterDoubleMachineGunStartSpeed = 800f;
      WeaponDescriptionManager.CopterDoubleMachineGunGunShootingTime = 1f;
      WeaponDescriptionManager.CopterDoubleMachineGunGunReloadTime = -1f;
      WeaponDescriptionManager.CopterDoubleMachineGunDescription = new WeaponDescription()
      {
        Damage = WeaponDescriptionManager.CopterDoubleMachineGunDamage,
        Rate = WeaponDescriptionManager.CopterDoubleMachineGunRate,
        Acceleration = WeaponDescriptionManager.CopterDoubleMachineGunAcceleration,
        StartSpeed = WeaponDescriptionManager.CopterDoubleMachineGunStartSpeed,
        ShootingTime = WeaponDescriptionManager.CopterDoubleMachineGunGunShootingTime,
        ReloadTime = WeaponDescriptionManager.CopterDoubleMachineGunGunReloadTime
      };
      WeaponDescriptionManager.BossCopterDoubleMachineGunDamage = 15f;
      WeaponDescriptionManager.BossCopterDoubleMachineGunRate = 0.5f;
      WeaponDescriptionManager.BossCopterDoubleMachineGunAcceleration = 50f;
      WeaponDescriptionManager.BossCopterDoubleMachineGunStartSpeed = 800f;
      WeaponDescriptionManager.BossCopterDoubleMachineGunGunShootingTime = 1f;
      WeaponDescriptionManager.BossCopterDoubleMachineGunGunReloadTime = -1f;
      WeaponDescriptionManager.BossDoubleMachineGunDescription = new WeaponDescription()
      {
        Damage = WeaponDescriptionManager.BossCopterDoubleMachineGunDamage,
        Rate = WeaponDescriptionManager.BossCopterDoubleMachineGunRate,
        Acceleration = WeaponDescriptionManager.BossCopterDoubleMachineGunAcceleration,
        StartSpeed = WeaponDescriptionManager.BossCopterDoubleMachineGunStartSpeed,
        ShootingTime = WeaponDescriptionManager.BossCopterDoubleMachineGunGunShootingTime,
        ReloadTime = WeaponDescriptionManager.BossCopterDoubleMachineGunGunReloadTime
      };
      WeaponDescriptionManager.BossCopterSingleMachineGunDamage = 15f;
      WeaponDescriptionManager.BossCopterSingleMachineGunRate = 0.5f;
      WeaponDescriptionManager.BossCopterSingleMachineGunAcceleration = 50f;
      WeaponDescriptionManager.BossCopterSingleMachineGunStartSpeed = 800f;
      WeaponDescriptionManager.BossCopterSingleMachineGunGunShootingTime = 1f;
      WeaponDescriptionManager.BossCopterSingleMachineGunGunReloadTime = -1f;
      WeaponDescriptionManager.BossSingleMachineGunDescription = new WeaponDescription()
      {
        Damage = WeaponDescriptionManager.BossCopterSingleMachineGunDamage,
        Rate = WeaponDescriptionManager.BossCopterSingleMachineGunRate,
        Acceleration = WeaponDescriptionManager.BossCopterSingleMachineGunAcceleration,
        StartSpeed = WeaponDescriptionManager.BossCopterSingleMachineGunStartSpeed,
        ShootingTime = WeaponDescriptionManager.BossCopterSingleMachineGunGunShootingTime,
        ReloadTime = WeaponDescriptionManager.BossCopterSingleMachineGunGunReloadTime
      };
      WeaponDescriptionManager.CopterRocketLauncherDamage = 50f;
      WeaponDescriptionManager.CopterRocketLauncherRate = 2f;
      WeaponDescriptionManager.CopterRocketLauncherAcceleration = 2500f;
      WeaponDescriptionManager.CopterRocketLauncherStartSpeed = 0.0f;
      WeaponDescriptionManager.CopterRocketLauncherLaunchEngineTime = 0.2f;
      WeaponDescriptionManager.CopterRocketLauncherStartXAcceleration = 0.0f;
      WeaponDescriptionManager.CopterRocketLauncherStartYAcceleration = 100f;
      WeaponDescriptionManager.CopterRocketLauncherStartXSpeed = 0.0f;
      WeaponDescriptionManager.CopterRocketLauncherStartYSpeed = 100f;
      WeaponDescriptionManager.CopterRocketLauncherGunShootingTime = 1f;
      WeaponDescriptionManager.CopterRocketLauncherGunReloadTime = -1f;
      RocketWeaponDescription weaponDescription2 = new RocketWeaponDescription();
      weaponDescription2.Acceleration = WeaponDescriptionManager.CopterRocketLauncherAcceleration;
      weaponDescription2.Rate = WeaponDescriptionManager.CopterRocketLauncherRate;
      weaponDescription2.Damage = WeaponDescriptionManager.CopterRocketLauncherDamage;
      weaponDescription2.StartSpeed = WeaponDescriptionManager.CopterRocketLauncherStartSpeed;
      weaponDescription2.LaunchEngineTime = WeaponDescriptionManager.CopterRocketLauncherLaunchEngineTime;
      weaponDescription2.HasStartStage = true;
      weaponDescription2.StartAccelerationX = WeaponDescriptionManager.CopterRocketLauncherStartXAcceleration;
      weaponDescription2.StartAccelerationY = WeaponDescriptionManager.CopterRocketLauncherStartYAcceleration;
      weaponDescription2.StartSpeedX = WeaponDescriptionManager.CopterRocketLauncherStartXSpeed;
      weaponDescription2.StartSpeedY = WeaponDescriptionManager.CopterRocketLauncherStartYSpeed;
      weaponDescription2.ShootingTime = WeaponDescriptionManager.CopterRocketLauncherGunShootingTime;
      weaponDescription2.ReloadTime = WeaponDescriptionManager.CopterRocketLauncherGunReloadTime;
      WeaponDescriptionManager.CopterRocketLauncherDescription = weaponDescription2;
      WeaponDescriptionManager.BossCopterRocketLauncherDamage = 50f;
      WeaponDescriptionManager.BossCopterRocketLauncherRate = 2f;
      WeaponDescriptionManager.BossCopterRocketLauncherAcceleration = 2500f;
      WeaponDescriptionManager.BossCopterRocketLauncherStartSpeed = 0.0f;
      WeaponDescriptionManager.BossCopterRocketLauncherLaunchEngineTime = 0.2f;
      WeaponDescriptionManager.BossCopterRocketLauncherStartXAcceleration = 0.0f;
      WeaponDescriptionManager.BossCopterRocketLauncherStartYAcceleration = 100f;
      WeaponDescriptionManager.BossCopterRocketLauncherStartXSpeed = 0.0f;
      WeaponDescriptionManager.BossCopterRocketLauncherStartYSpeed = 100f;
      WeaponDescriptionManager.BossCopterRocketLauncherGunShootingTime = 1f;
      WeaponDescriptionManager.BossCopterRocketLauncherGunReloadTime = -1f;
      RocketWeaponDescription weaponDescription3 = new RocketWeaponDescription();
      weaponDescription3.Acceleration = WeaponDescriptionManager.BossCopterRocketLauncherAcceleration;
      weaponDescription3.Rate = WeaponDescriptionManager.BossCopterRocketLauncherRate;
      weaponDescription3.Damage = WeaponDescriptionManager.BossCopterRocketLauncherDamage;
      weaponDescription3.StartSpeed = WeaponDescriptionManager.BossCopterRocketLauncherStartSpeed;
      weaponDescription3.LaunchEngineTime = WeaponDescriptionManager.BossCopterRocketLauncherLaunchEngineTime;
      weaponDescription3.HasStartStage = true;
      weaponDescription3.StartAccelerationX = WeaponDescriptionManager.BossCopterRocketLauncherStartXAcceleration;
      weaponDescription3.StartAccelerationY = WeaponDescriptionManager.BossCopterRocketLauncherStartYAcceleration;
      weaponDescription3.StartSpeedX = WeaponDescriptionManager.BossCopterRocketLauncherStartXSpeed;
      weaponDescription3.StartSpeedY = WeaponDescriptionManager.BossCopterRocketLauncherStartYSpeed;
      weaponDescription3.ShootingTime = WeaponDescriptionManager.BossCopterRocketLauncherGunShootingTime;
      weaponDescription3.ReloadTime = WeaponDescriptionManager.BossCopterRocketLauncherGunReloadTime;
      WeaponDescriptionManager.BossRocketLauncherDescription = weaponDescription3;
      WeaponDescriptionManager.DualRocketLauncherDamage = 30f;
      WeaponDescriptionManager.DualRocketLauncherRate = 1f;
      WeaponDescriptionManager.DualRocketLauncherAcceleration = 2500f;
      WeaponDescriptionManager.DualRocketLauncherStartSpeed = 0.0f;
      WeaponDescriptionManager.DualRocketLauncherLaunchEngineTime = 0.2f;
      WeaponDescriptionManager.DualRocketLauncherStartXAcceleration = 0.0f;
      WeaponDescriptionManager.DualRocketLauncherStartYAcceleration = 100f;
      WeaponDescriptionManager.DualRocketLauncherStartXSpeed = 0.0f;
      WeaponDescriptionManager.DualRocketLauncherStartYSpeed = 100f;
      WeaponDescriptionManager.DualRocketLauncherShootingTime = 1f;
      WeaponDescriptionManager.DualRocketLauncherReloadTime = -1f;
      RocketWeaponDescription weaponDescription4 = new RocketWeaponDescription();
      weaponDescription4.Damage = WeaponDescriptionManager.DualRocketLauncherDamage;
      weaponDescription4.Rate = WeaponDescriptionManager.DualRocketLauncherRate;
      weaponDescription4.Acceleration = WeaponDescriptionManager.DualRocketLauncherAcceleration;
      weaponDescription4.StartSpeed = WeaponDescriptionManager.DualRocketLauncherStartSpeed;
      weaponDescription4.HasStartStage = true;
      weaponDescription4.LaunchEngineTime = WeaponDescriptionManager.DualRocketLauncherLaunchEngineTime;
      weaponDescription4.StartAccelerationX = WeaponDescriptionManager.DualRocketLauncherStartXAcceleration;
      weaponDescription4.StartAccelerationY = WeaponDescriptionManager.DualRocketLauncherStartYAcceleration;
      weaponDescription4.StartSpeedX = WeaponDescriptionManager.DualRocketLauncherStartXSpeed;
      weaponDescription4.StartSpeedY = WeaponDescriptionManager.DualRocketLauncherStartYSpeed;
      weaponDescription4.ShootingTime = WeaponDescriptionManager.DualRocketLauncherShootingTime;
      weaponDescription4.ReloadTime = WeaponDescriptionManager.DualRocketLauncherReloadTime;
      WeaponDescriptionManager.DualRocketLauncherDescription = weaponDescription4;
      WeaponDescriptionManager.HomingRocketDamage = 30f;
      WeaponDescriptionManager.HomingRocketRate = 2f;
      WeaponDescriptionManager.HomingRocketAcceleration = 2500f;
      WeaponDescriptionManager.HomingRocketStartSpeed = 0.0f;
      WeaponDescriptionManager.HomingRocketLaunchEngineTime = 0.2f;
      WeaponDescriptionManager.HomingRocketStartXAcceleration = 0.0f;
      WeaponDescriptionManager.HomingRocketStartYAcceleration = 100f;
      WeaponDescriptionManager.HomingRocketStartXSpeed = 0.0f;
      WeaponDescriptionManager.HomingRocketStartYSpeed = 100f;
      WeaponDescriptionManager.HomingRocketShootingTime = 1f;
      WeaponDescriptionManager.HomingRocketReloadTime = -1f;
      RocketWeaponDescription weaponDescription5 = new RocketWeaponDescription();
      weaponDescription5.Damage = WeaponDescriptionManager.HomingRocketDamage;
      weaponDescription5.Rate = WeaponDescriptionManager.HomingRocketRate;
      weaponDescription5.Acceleration = WeaponDescriptionManager.HomingRocketAcceleration;
      weaponDescription5.StartSpeed = WeaponDescriptionManager.HomingRocketStartSpeed;
      weaponDescription5.HasStartStage = true;
      weaponDescription5.LaunchEngineTime = WeaponDescriptionManager.HomingRocketLaunchEngineTime;
      weaponDescription5.StartAccelerationX = WeaponDescriptionManager.HomingRocketStartXAcceleration;
      weaponDescription5.StartAccelerationY = WeaponDescriptionManager.HomingRocketStartYAcceleration;
      weaponDescription5.StartSpeedX = WeaponDescriptionManager.HomingRocketStartXSpeed;
      weaponDescription5.StartSpeedY = WeaponDescriptionManager.HomingRocketStartYSpeed;
      weaponDescription5.ShootingTime = WeaponDescriptionManager.HomingRocketShootingTime;
      weaponDescription5.ReloadTime = WeaponDescriptionManager.HomingRocketReloadTime;
      WeaponDescriptionManager.HomingRocketDescription = (WeaponDescription) weaponDescription5;
      WeaponDescriptionManager.CasseteRocketDamage = 20f;
      WeaponDescriptionManager.CasseteRocketRate = 2f;
      WeaponDescriptionManager.CasseteRocketAcceleration = 50f;
      WeaponDescriptionManager.CasseteRocketStartSpeed = 1600f;
      WeaponDescriptionManager.CasseteRocketStartAngle = -60f;
      WeaponDescriptionManager.CasseteRocketEndAngle = 60f;
      WeaponDescriptionManager.CasseteTimeToExplosion = 0.1f;
      WeaponDescriptionManager.CasseteNumberOfBalls = 8;
      WeaponDescriptionManager.CasseteNumberOfBallsUpdated = 10;
      WeaponDescriptionManager.CasseteRocketGunShootingTime = 1f;
      WeaponDescriptionManager.CasseteRocketGunReloadTime = -1f;
      CasseteBulletDescription bulletDescription1 = new CasseteBulletDescription();
      bulletDescription1.Damage = WeaponDescriptionManager.CasseteRocketDamage;
      bulletDescription1.Rate = WeaponDescriptionManager.CasseteRocketRate;
      bulletDescription1.Acceleration = WeaponDescriptionManager.CasseteRocketAcceleration;
      bulletDescription1.StartSpeed = WeaponDescriptionManager.CasseteRocketStartSpeed;
      bulletDescription1.StartAngle = WeaponDescriptionManager.CasseteRocketStartAngle;
      bulletDescription1.EndAngle = WeaponDescriptionManager.CasseteRocketEndAngle;
      bulletDescription1.NumberOfBalls = WeaponDescriptionManager.CasseteNumberOfBalls;
      bulletDescription1.NumberOfBallsUpdated = WeaponDescriptionManager.CasseteNumberOfBallsUpdated;
      bulletDescription1.TimeToExplosion = WeaponDescriptionManager.CasseteTimeToExplosion;
      bulletDescription1.ShootingTime = WeaponDescriptionManager.CasseteRocketGunShootingTime;
      bulletDescription1.ReloadTime = WeaponDescriptionManager.CasseteRocketGunReloadTime;
      bulletDescription1.ChildLinearSpeed = 700;
      bulletDescription1.ChildLinearAcceleration = 400;
      WeaponDescriptionManager.CasseteRocketDescription = bulletDescription1;
      CasseteBulletDescription bulletDescription2 = new CasseteBulletDescription();
      bulletDescription2.Damage = WeaponDescriptionManager.CasseteRocketDamage * 6f;
      bulletDescription2.Rate = WeaponDescriptionManager.CasseteRocketRate / 2f;
      bulletDescription2.Acceleration = WeaponDescriptionManager.CasseteRocketAcceleration;
      bulletDescription2.StartSpeed = 800f;
      bulletDescription2.StartAngle = 120f;
      bulletDescription2.EndAngle = 240f;
      bulletDescription2.NumberOfBalls = 4;
      bulletDescription2.NumberOfBallsUpdated = WeaponDescriptionManager.CasseteNumberOfBallsUpdated;
      bulletDescription2.TimeToExplosion = WeaponDescriptionManager.CasseteTimeToExplosion;
      bulletDescription2.ShootingTime = WeaponDescriptionManager.CasseteRocketGunShootingTime;
      bulletDescription2.ReloadTime = WeaponDescriptionManager.CasseteRocketGunReloadTime;
      bulletDescription2.ChildLinearSpeed = 350;
      bulletDescription2.ChildLinearAcceleration = 200;
      WeaponDescriptionManager.CopterCasseteRocketDescription = bulletDescription2;
      WeaponDescriptionManager.ShieldDamage = 0.0f;
      WeaponDescriptionManager.ShieldRate = 10000f;
      WeaponDescriptionManager.ShieldAcceleration = 500f;
      WeaponDescriptionManager.ShieldStartSpeed = 2000f;
      WeaponDescriptionManager.ShieldRechargeSpeed = 5f;
      WeaponDescriptionManager.ShieldDecreaseSpeed = 20f;
      WeaponDescriptionManager.ShieldMaxEnergy = 33f;
      WeaponDescriptionManager.ShieldStartEnergy = 16f;
      ShieldDescription shieldDescription1 = new ShieldDescription();
      shieldDescription1.Damage = WeaponDescriptionManager.ShieldDamage;
      shieldDescription1.Rate = WeaponDescriptionManager.ShieldRate;
      shieldDescription1.Acceleration = WeaponDescriptionManager.ShieldAcceleration;
      shieldDescription1.StartSpeed = WeaponDescriptionManager.ShieldStartSpeed;
      shieldDescription1.RechargeSpeed = WeaponDescriptionManager.ShieldRechargeSpeed;
      shieldDescription1.DecreaseSpeed = WeaponDescriptionManager.ShieldDecreaseSpeed;
      shieldDescription1.CurrentEnergy = WeaponDescriptionManager.ShieldStartEnergy;
      shieldDescription1.MaxEnergy = WeaponDescriptionManager.ShieldMaxEnergy;
      WeaponDescriptionManager.ShieldDescription = shieldDescription1;
      ShieldDescription shieldDescription2 = new ShieldDescription();
      shieldDescription2.Damage = WeaponDescriptionManager.ShieldDamage;
      shieldDescription2.Rate = WeaponDescriptionManager.ShieldRate;
      shieldDescription2.Acceleration = WeaponDescriptionManager.ShieldAcceleration;
      shieldDescription2.StartSpeed = WeaponDescriptionManager.ShieldStartSpeed;
      shieldDescription2.RechargeSpeed = WeaponDescriptionManager.ShieldRechargeSpeed;
      shieldDescription2.DecreaseSpeed = WeaponDescriptionManager.ShieldDecreaseSpeed;
      shieldDescription2.CurrentEnergy = 1000000f;
      shieldDescription2.MaxEnergy = 1000000f;
      WeaponDescriptionManager.BossShieldDescription = shieldDescription2;
      WeaponDescriptionManager.PlasmaGunDamage = 30f;
      WeaponDescriptionManager.PlasmaGunRate = 0.04f;
      WeaponDescriptionManager.PlasmaGunAcceleration = 50f;
      WeaponDescriptionManager.PlasmaGunStartSpeed = 1500f;
      WeaponDescriptionManager.PlasmaGunRechargeSpeed = 1f;
      WeaponDescriptionManager.PlasmaGunDecreaseSpeed = 0.0f;
      WeaponDescriptionManager.PlasmaGunMaxEnergy = 100f;
      WeaponDescriptionManager.PlasmaGunStartEnergy = 100f;
      WeaponDescriptionManager.PlasmaGunTimeToBeam = 0.1f;
      WeaponDescriptionManager.PlasmaGunGunShootingTime = 1f;
      WeaponDescriptionManager.PlasmaGunGunReloadTime = -1f;
      PlasmaGunWeaponDescription weaponDescription6 = new PlasmaGunWeaponDescription();
      weaponDescription6.Damage = WeaponDescriptionManager.PlasmaGunDamage;
      weaponDescription6.Rate = WeaponDescriptionManager.PlasmaGunRate;
      weaponDescription6.Acceleration = WeaponDescriptionManager.PlasmaGunAcceleration;
      weaponDescription6.StartSpeed = WeaponDescriptionManager.PlasmaGunStartSpeed;
      weaponDescription6.RechargeSpeed = WeaponDescriptionManager.PlasmaGunRechargeSpeed;
      weaponDescription6.DecreaseSpeed = WeaponDescriptionManager.PlasmaGunDecreaseSpeed;
      weaponDescription6.CurrentEnergy = WeaponDescriptionManager.PlasmaGunStartEnergy;
      weaponDescription6.MaxEnergy = WeaponDescriptionManager.PlasmaGunMaxEnergy;
      weaponDescription6.TimeToBeam = WeaponDescriptionManager.PlasmaGunTimeToBeam;
      weaponDescription6.ShootingTime = WeaponDescriptionManager.PlasmaGunGunShootingTime;
      weaponDescription6.ReloadTime = WeaponDescriptionManager.PlasmaGunGunReloadTime;
      WeaponDescriptionManager.PlasmaGunDescription = (WeaponDescription) weaponDescription6;
      WeaponDescriptionManager.CopterPlasmaGunDamage = 100f;
      WeaponDescriptionManager.CopterPlasmaGunRate = 0.04f;
      WeaponDescriptionManager.CopterPlasmaGunAcceleration = 50f;
      WeaponDescriptionManager.CopterPlasmaGunStartSpeed = 1500f;
      WeaponDescriptionManager.CopterPlasmaGunRechargeSpeed = 10f;
      WeaponDescriptionManager.CopterPlasmaGunDecreaseSpeed = 0.05f;
      WeaponDescriptionManager.CopterPlasmaGunMaxEnergy = 100f;
      WeaponDescriptionManager.CopterPlasmaGunStartEnergy = 50f;
      WeaponDescriptionManager.CopterPlasmaGunTimeToBeam = 0.1f;
      WeaponDescriptionManager.CopterPlasmaGunGunShootingTime = 1f;
      WeaponDescriptionManager.CopterPlasmaGunGunReloadTime = -1f;
      PlasmaGunWeaponDescription weaponDescription7 = new PlasmaGunWeaponDescription();
      weaponDescription7.Damage = WeaponDescriptionManager.CopterPlasmaGunDamage;
      weaponDescription7.Rate = WeaponDescriptionManager.CopterPlasmaGunRate;
      weaponDescription7.Acceleration = WeaponDescriptionManager.CopterPlasmaGunAcceleration;
      weaponDescription7.StartSpeed = WeaponDescriptionManager.CopterPlasmaGunStartSpeed;
      weaponDescription7.RechargeSpeed = WeaponDescriptionManager.CopterPlasmaGunRechargeSpeed;
      weaponDescription7.DecreaseSpeed = WeaponDescriptionManager.CopterPlasmaGunDecreaseSpeed;
      weaponDescription7.CurrentEnergy = WeaponDescriptionManager.CopterPlasmaGunStartEnergy;
      weaponDescription7.MaxEnergy = WeaponDescriptionManager.CopterPlasmaGunMaxEnergy;
      weaponDescription7.TimeToBeam = WeaponDescriptionManager.CopterPlasmaGunTimeToBeam;
      weaponDescription7.ShootingTime = WeaponDescriptionManager.CopterPlasmaGunGunShootingTime;
      weaponDescription7.ReloadTime = WeaponDescriptionManager.CopterPlasmaGunGunReloadTime;
      WeaponDescriptionManager.CopterPlasmaGunDescription = (WeaponDescription) weaponDescription7;
      WeaponDescriptionManager.VulcanDamage = 5f;
      WeaponDescriptionManager.VulcanRate = 0.07f;
      WeaponDescriptionManager.VulcanAcceleration = 50f;
      WeaponDescriptionManager.VulcanStartSpeed = 1600f;
      WeaponDescriptionManager.VulcanGunShootingTime = 1f;
      WeaponDescriptionManager.VulcanGunReloadTime = -1f;
      WeaponDescriptionManager.VulcanRechargeSpeed = 30f;
      WeaponDescriptionManager.VulcanCurrentEnergy = 90f;
      WeaponDescriptionManager.VulcanDecreaseEnergy = 45f;
      WeaponDescriptionManager.VulcanMaxEnergy = 90f;
      PlasmaGunWeaponDescription weaponDescription8 = new PlasmaGunWeaponDescription();
      weaponDescription8.Damage = WeaponDescriptionManager.VulcanDamage;
      weaponDescription8.Rate = WeaponDescriptionManager.VulcanRate;
      weaponDescription8.Acceleration = WeaponDescriptionManager.VulcanAcceleration;
      weaponDescription8.StartSpeed = WeaponDescriptionManager.VulcanStartSpeed;
      weaponDescription8.ShootingTime = WeaponDescriptionManager.VulcanGunShootingTime;
      weaponDescription8.ReloadTime = WeaponDescriptionManager.VulcanGunReloadTime;
      weaponDescription8.RechargeSpeed = WeaponDescriptionManager.VulcanRechargeSpeed;
      weaponDescription8.CurrentEnergy = WeaponDescriptionManager.VulcanCurrentEnergy;
      weaponDescription8.DecreaseSpeed = WeaponDescriptionManager.VulcanDecreaseEnergy;
      weaponDescription8.MaxEnergy = WeaponDescriptionManager.VulcanMaxEnergy;
      weaponDescription8.TimeToBeam = 0.0f;
      WeaponDescriptionManager.VulcanDescription = (WeaponDescription) weaponDescription8;
      WeaponDescriptionManager.CannonMachineGunDamage = 10f;
      WeaponDescriptionManager.CannonMachineGunAcceleration = 100f;
      WeaponDescriptionManager.CannonMachineGunRate = 0.6f;
      WeaponDescriptionManager.CannonMachineGunStartSpeed = 1000f;
      WeaponDescriptionManager.CannonMachineGunShootingTime = 1f;
      WeaponDescriptionManager.CannonMachineGunReloadTime = -1f;
      WeaponDescriptionManager.CannonMachineGunDescription = new WeaponDescription()
      {
        Damage = WeaponDescriptionManager.CannonMachineGunDamage,
        Acceleration = WeaponDescriptionManager.CannonMachineGunAcceleration,
        Rate = WeaponDescriptionManager.CannonMachineGunRate,
        StartSpeed = WeaponDescriptionManager.CannonMachineGunStartSpeed,
        ShootingTime = WeaponDescriptionManager.CannonMachineGunShootingTime,
        ReloadTime = WeaponDescriptionManager.CannonMachineGunReloadTime
      };
      WeaponDescriptionManager.CannonRocketLauncherDamage = 50f;
      WeaponDescriptionManager.CannonRocketLauncherRate = 2f;
      WeaponDescriptionManager.CannonRocketLauncherAcceleration = 2500f;
      WeaponDescriptionManager.CannonRocketLauncherStartSpeed = 0.0f;
      WeaponDescriptionManager.CannonRocketLauncherLaunchEngineTime = 0.2f;
      WeaponDescriptionManager.CannonRocketLauncherStartXAcceleration = 0.0f;
      WeaponDescriptionManager.CannonRocketLauncherStartYAcceleration = 100f;
      WeaponDescriptionManager.CannonRocketLauncherStartXSpeed = 0.0f;
      WeaponDescriptionManager.CannonRocketLauncherStartYSpeed = 100f;
      WeaponDescriptionManager.CannonRocketLauncherShootingTime = 1f;
      WeaponDescriptionManager.CannonRocketLauncherReloadTime = -1f;
      RocketWeaponDescription weaponDescription9 = new RocketWeaponDescription();
      weaponDescription9.Damage = WeaponDescriptionManager.CannonRocketLauncherDamage;
      weaponDescription9.Rate = WeaponDescriptionManager.CannonRocketLauncherRate;
      weaponDescription9.Acceleration = WeaponDescriptionManager.CannonRocketLauncherAcceleration;
      weaponDescription9.StartSpeed = WeaponDescriptionManager.CannonRocketLauncherStartSpeed;
      weaponDescription9.HasStartStage = false;
      weaponDescription9.LaunchEngineTime = WeaponDescriptionManager.CannonRocketLauncherLaunchEngineTime;
      weaponDescription9.StartAccelerationX = WeaponDescriptionManager.CannonRocketLauncherStartXAcceleration;
      weaponDescription9.StartAccelerationY = WeaponDescriptionManager.CannonRocketLauncherStartYAcceleration;
      weaponDescription9.StartSpeedX = WeaponDescriptionManager.CannonRocketLauncherStartXSpeed;
      weaponDescription9.StartSpeedY = WeaponDescriptionManager.CannonRocketLauncherStartYSpeed;
      weaponDescription9.ShootingTime = WeaponDescriptionManager.CannonRocketLauncherShootingTime;
      weaponDescription9.ReloadTime = WeaponDescriptionManager.CannonRocketLauncherReloadTime;
      WeaponDescriptionManager.CannonRocketLauncherDescription = (WeaponDescription) weaponDescription9;
    }
  }
}
