// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Weapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public abstract class Weapon
  {
    private Vector2 _bulletSpawnPosition;
    private bool _isShooting;
    private bool _oneShoot;
    private int _shots;
    private Vector2 _weaponPosition;

    public event EventHandler<WeaponEventArgs> Fired;

    public event EventHandler ShootingState;

    public Vector2 BulletSpawnPosition
    {
      get
      {
        Vector2 bulletSpawnPosition = this._bulletSpawnPosition;
        float rotation = this.Owner.Rotation;
        bulletSpawnPosition.X = (float) ((double) this._bulletSpawnPosition.X * Math.Cos((double) rotation) - (double) this._bulletSpawnPosition.Y * Math.Sin((double) rotation));
        bulletSpawnPosition.Y = (float) ((double) this._bulletSpawnPosition.X * Math.Sin((double) rotation) + (double) this._bulletSpawnPosition.Y * Math.Cos((double) rotation));
        return bulletSpawnPosition;
      }
      set => this._bulletSpawnPosition = value;
    }

    public Vector2 WeaponPosition
    {
      get
      {
        Vector2 weaponPosition = this._weaponPosition;
        float rotation = this.Owner.Rotation;
        weaponPosition.X = (float) ((double) this._weaponPosition.X * Math.Cos((double) rotation) - (double) this._weaponPosition.Y * Math.Sin((double) rotation));
        weaponPosition.Y = (float) ((double) this._weaponPosition.X * Math.Sin((double) rotation) + (double) this._weaponPosition.Y * Math.Cos((double) rotation));
        return weaponPosition;
      }
      set => this._weaponPosition = value;
    }

    public float Damage { get; set; }

    public float Rate { get; set; }

    public float BulletSpeed { get; set; }

    public float BulletAcceleration { get; set; }

    public Instance Owner { get; set; }

    public float ElapsedTimeFromLastShoot { get; set; }

    public virtual bool IsShooting
    {
      get => this._isShooting;
      set
      {
        if (this._isShooting == value)
          return;
        this._isShooting = value;
        if (this.ShootingState == null)
          return;
        this.ShootingState((object) this, EventArgs.Empty);
      }
    }

    public float LaunchAngle { get; set; }

    public float X2DamageProbability { get; set; }

    public float X4DamageProbability { get; set; }

    public float DamageFactor { get; set; }

    public WeaponType Type { get; set; }

    public WeaponSlotDescription WeaponSlotDescription { get; set; }

    public float ReloadTime { get; set; }

    public float ShootingTime { get; set; }

    public virtual float WeaponReloadStatus
    {
      get => MathHelper.Clamp(this.ElapsedTimeFromLastShoot / this.Rate, 0.0f, 1f);
    }

    protected float ElapsedReloadTime { get; set; }

    protected float ElapsedShootingTime { get; set; }

    public Vector2 BaseWeaponPosition => this._weaponPosition;

    protected Weapon(Instance owner)
    {
      this.Owner = owner;
      this.ReloadTime = -1f;
      this.ShootingTime = 1f;
      this.DamageFactor = 1f;
    }

    public virtual void Update(float elapsedSeconds)
    {
      if ((double) this.ElapsedShootingTime > 0.0)
      {
        this.ElapsedShootingTime -= elapsedSeconds;
        if (this.IsShooting && (double) this.ElapsedTimeFromLastShoot > (double) this.Rate)
        {
          this.Shoot(this.Owner.Position);
          this.ElapsedTimeFromLastShoot = 0.0f;
          if (this._oneShoot)
            this.IsShooting = false;
        }
        this.ElapsedTimeFromLastShoot += elapsedSeconds;
        this.ElapsedReloadTime = this.ReloadTime;
      }
      else
      {
        this.ElapsedReloadTime -= elapsedSeconds;
        if ((double) this.ElapsedReloadTime < 0.0)
          this.ElapsedShootingTime = this.ShootingTime;
        else
          this.IsShooting = false;
      }
    }

    private void InvokeFire(WeaponEventArgs e)
    {
      EventHandler<WeaponEventArgs> fired = this.Fired;
      if (fired == null)
        return;
      fired((object) this, e);
    }

    protected virtual void AdditionalInitialization(Bullet bullet)
    {
    }

    protected void BaseInitialization(Vector2 initialPosition, Bullet bullet)
    {
      bullet.Owner = this.Owner;
      bullet.Position = initialPosition;
      bullet.Position.X += this.WeaponPosition.X + this.BulletSpawnPosition.X;
      bullet.Position.Y += this.WeaponPosition.Y + this.BulletSpawnPosition.Y;
      bullet.SetPosition(bullet.Position);
      bullet.LinearSpeed = this.BulletSpeed;
      bullet.LinearAcceleration = this.BulletAcceleration;
      bullet.Damage = this.Damage;
      bullet.Angle = this.LaunchAngle;
      bullet.GameWorld = this.Owner.GameWorld;
      bullet.Team = ((IUnit) this.Owner).Team;
      bullet.X2DamageProbability = this.X2DamageProbability;
      bullet.X4DamageProbability = this.X4DamageProbability;
      bullet.DamageFactor = this.DamageFactor;
    }

    public virtual void Configure(WeaponDescription weaponDesc)
    {
      this.BulletSpeed = weaponDesc.StartSpeed;
      this.BulletAcceleration = weaponDesc.Acceleration;
      this.Damage = weaponDesc.Damage;
      this.ElapsedTimeFromLastShoot = this.Rate = weaponDesc.Rate;
      this.ShootingTime = weaponDesc.ShootingTime;
      this.ReloadTime = weaponDesc.ReloadTime;
    }

    public abstract Bullet GetBullet();

    public void Shoot(Vector2 initialPosition)
    {
      ++this._shots;
      this.InvokeFire(WeaponEventArgs.Create(this.Type, this._shots));
      Bullet bullet = this.GetBullet();
      if (bullet == null)
        return;
      this.BaseInitialization(initialPosition, bullet);
      this.Owner.GameWorld.AddInstance((Instance) bullet);
      this.AdditionalInitialization(bullet);
    }

    public void OneShoot()
    {
      this._oneShoot = true;
      this.IsShooting = true;
    }
  }
}
