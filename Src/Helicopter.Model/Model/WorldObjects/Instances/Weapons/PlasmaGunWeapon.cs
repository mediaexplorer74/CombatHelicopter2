// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.PlasmaGunWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class PlasmaGunWeapon : Weapon
  {
    public static int NotShooting;
    public static int StartShooting = 1;
    public static int Shooting = 2;
    public PlasmaBeam Beam;
    private float _currentEnergy;
    private float _decreaseSpeed;
    private float _elapsedTimeForShoot;
    private bool _isTimeFromStartInvoked;
    private float _maxEnergy;
    private float _rechargeSpeed;
    private float _timeForBeam;
    private float _timeFromStartFiring;

    public event EventHandler CaptureLost;

    public event EventHandler EnemyCaptured;

    public event EventHandler ShootingAfter125;

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
          if (this.Beam != null)
          {
            this.Beam.IsNeedRemove = true;
            this.Beam = (PlasmaBeam) null;
          }
          this.State = PlasmaGunWeapon.NotShooting;
          this._elapsedTimeForShoot = this._timeForBeam;
        }
        else if (this.IsRechargering)
          return;
        this._timeFromStartFiring = 0.0f;
        this._isTimeFromStartInvoked = false;
        base.IsShooting = value;
      }
    }

    public PlasmaGunWeapon(Instance owner)
      : base(owner)
    {
      this.Type = WeaponType.PlasmaGun;
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
      if (this.IsRechargering && (double) Math.Abs(this._currentEnergy - this._maxEnergy) < 1.0 / 1000.0)
        this.IsRechargering = false;
      if (!this.IsShooting)
        return;
      this._timeFromStartFiring += elapsedSeconds;
      if ((double) this._timeFromStartFiring > 0.5 && !this._isTimeFromStartInvoked)
      {
        this._isTimeFromStartInvoked = true;
        this.InvokeShootingAfter125();
      }
      this._currentEnergy -= this._decreaseSpeed * elapsedSeconds;
      if ((double) this._elapsedTimeForShoot > 0.0)
      {
        this.State = PlasmaGunWeapon.StartShooting;
        this._elapsedTimeForShoot -= elapsedSeconds;
      }
      else
      {
        this.State = PlasmaGunWeapon.Shooting;
        if (this.Beam != null)
          return;
        this.Beam = PlasmaBeam.GetInstance();
        this.Beam.Weapon = this;
        this.Beam.CaptureLost += new EventHandler(this.OnCaptureLost);
        this.Beam.EnemyCaptured += new EventHandler(this.OnEnemyCatptured);
        this.Beam.Owner = this.Owner;
        this.Beam.Damage = this.Damage;
        this.Beam.Angle = this.LaunchAngle;
        this.Owner.GameWorld.AddInstance((Instance) this.Beam);
        this.Beam.ZIndex = 100500f;
        this.Beam.Update(0.0f);
      }
    }

    public void InvokeCaptureLost(EventArgs e)
    {
      EventHandler captureLost = this.CaptureLost;
      if (captureLost == null)
        return;
      captureLost((object) this, e);
    }

    public void InvokeEnemyCaptured(EventArgs e)
    {
      EventHandler enemyCaptured = this.EnemyCaptured;
      if (enemyCaptured == null)
        return;
      enemyCaptured((object) this, e);
    }

    public void InvokeShootingAfter125()
    {
      EventHandler shootingAfter125 = this.ShootingAfter125;
      if (shootingAfter125 == null)
        return;
      shootingAfter125((object) this, EventArgs.Empty);
    }

    private void OnCaptureLost(object sender, EventArgs e)
    {
      this.InvokeCaptureLost(EventArgs.Empty);
    }

    private void OnEnemyCatptured(object sender, EventArgs e)
    {
      this.InvokeEnemyCaptured(EventArgs.Empty);
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

    public override Bullet GetBullet() => (Bullet) null;
  }
}
