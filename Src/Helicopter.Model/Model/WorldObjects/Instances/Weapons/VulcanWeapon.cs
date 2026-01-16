// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.VulcanWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class VulcanWeapon : Weapon
  {
    public static int NotShooting;
    public static int StartShooting = 1;
    public static int Shooting = 2;
    private PlasmaBeam _beam;
    private float _currentEnergy;
    private float _decreaseSpeed;
    private float _elapsedTimeForShoot;
    private float _maxEnergy;
    private float _rechargeSpeed;
    private float _timeForBeam;

    public int State { get; set; }

    public bool IsRechargering { get; set; }

    public override float WeaponReloadStatus => this._currentEnergy / this._maxEnergy;

    public override bool IsShooting
    {
      get => base.IsShooting;
      set
      {
        if (base.IsShooting == value)
          return;
        if (!value)
        {
          if (this._beam != null)
          {
            this._beam.IsNeedRemove = true;
            this._beam = (PlasmaBeam) null;
          }
          this.State = VulcanWeapon.NotShooting;
          this._elapsedTimeForShoot = this._timeForBeam;
        }
        else if (this.IsRechargering)
        {
          this.InvokeFireAttemptWhileRecharging();
          return;
        }
        base.IsShooting = value;
      }
    }

    public event EventHandler FireAttemptWhileRecharging;

    public void InvokeFireAttemptWhileRecharging()
    {
      EventHandler attemptWhileRecharging = this.FireAttemptWhileRecharging;
      if (attemptWhileRecharging == null)
        return;
      attemptWhileRecharging((object) this, EventArgs.Empty);
    }

    public VulcanWeapon(Instance owner)
      : base(owner)
    {
      this.Type = WeaponType.Vulcan;
    }

    public override void Update(float elapsedSeconds)
    {
      base.Update(elapsedSeconds);
      if (this.IsRechargering || !this.IsShooting)
        this._currentEnergy += this._rechargeSpeed * elapsedSeconds;
      if ((double) this._currentEnergy <= 0.0099999997764825821)
      {
        this.IsRechargering = true;
        this.IsShooting = false;
      }
      this._currentEnergy = MathHelper.Clamp(this._currentEnergy, 0.0f, this._maxEnergy);
      if (this.IsRechargering && (double) this._currentEnergy > (double) this._maxEnergy * 0.05000000074505806)
        this.IsRechargering = false;
      if (!this.IsShooting)
        return;
      this._currentEnergy -= this._decreaseSpeed * elapsedSeconds;
      if ((double) this._elapsedTimeForShoot > 0.0)
      {
        this.State = VulcanWeapon.StartShooting;
        this._elapsedTimeForShoot -= elapsedSeconds;
      }
      else
        this.State = VulcanWeapon.Shooting;
    }

    public override void Configure(WeaponDescription weaponDesc)
    {
      PlasmaGunWeaponDescription weaponDescription = (PlasmaGunWeaponDescription) weaponDesc;
      this._currentEnergy = weaponDescription.CurrentEnergy;
      this._maxEnergy = weaponDescription.MaxEnergy;
      this._rechargeSpeed = weaponDescription.RechargeSpeed;
      this._decreaseSpeed = weaponDescription.DecreaseSpeed;
      this._elapsedTimeForShoot = this._timeForBeam = weaponDescription.TimeToBeam;
      base.Configure(weaponDesc);
    }

    public override Bullet GetBullet()
    {
      MachineGunBullet instance = MachineGunBullet.GetInstance();
      instance.DamageType = DamageType.Bullet;
      return (Bullet) instance;
    }

    protected override void AdditionalInitialization(Bullet bullet)
    {
      base.AdditionalInitialization(bullet);
      bullet.Angle += (float) (CommonRandom.Instance.Random.NextDouble() * 3.1415927410125732 / 18.0 - 0.08726646502812703);
    }
  }
}
