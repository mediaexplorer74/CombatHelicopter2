// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.ShieldWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class ShieldWeapon : Weapon
  {
    public Shield Bullet;
    public float CurrentEnergy;
    public float DecreaseSpeed;
    public float MaxEnergy;
    public float RechargeSpeed;

    public event EventHandler FireAttemptWhileRecharging;

    public bool IsRechargering { get; set; }

    public override bool IsShooting
    {
      get => base.IsShooting;
      set
      {
        if (base.IsShooting == value)
          return;
        if (value && ((double) this.CurrentEnergy <= 0.0099999997764825821 || this.IsRechargering))
        {
          base.IsShooting = false;
          this.InvokeFireAttemptWhileRecharging();
        }
        else
        {
          base.IsShooting = value;
          if (base.IsShooting)
          {
            if (!(this.Owner is SmartPlayer))
              return;
            this.Owner.State = 2;
          }
          else
          {
            if (this.Owner is SmartPlayer)
              this.Owner.State = 0;
            if (this.Bullet == null)
              return;
            this.Bullet.IsNeedRemove = true;
            this.Bullet = (Shield) null;
          }
        }
      }
    }

    public override float WeaponReloadStatus => this.CurrentEnergy / this.MaxEnergy;

    public ShieldWeapon(Instance owner)
      : base(owner)
    {
      this.Type = WeaponType.Shield;
    }

    public override void Update(float elapsedSeconds)
    {
      if (this.IsRechargering || !this.IsShooting)
        this.CurrentEnergy += this.RechargeSpeed * elapsedSeconds;
      if (this.IsShooting)
      {
        this.CurrentEnergy -= this.DecreaseSpeed * elapsedSeconds;
        if (this.Bullet == null)
          this.CreateBullet();
      }
      if ((double) this.CurrentEnergy <= 0.0099999997764825821)
      {
        this.IsRechargering = true;
        this.IsShooting = false;
      }
      this.CurrentEnergy = MathHelper.Clamp(this.CurrentEnergy, 0.0f, this.MaxEnergy);
      if (!this.IsRechargering || (double) this.CurrentEnergy <= (double) this.MaxEnergy * 0.05000000074505806)
        return;
      this.IsRechargering = false;
    }

    public void InvokeFireAttemptWhileRecharging()
    {
      EventHandler attemptWhileRecharging = this.FireAttemptWhileRecharging;
      if (attemptWhileRecharging == null)
        return;
      attemptWhileRecharging((object) this, EventArgs.Empty);
    }

    public override void Configure(WeaponDescription weaponDesc)
    {
      ShieldDescription shieldDescription = (ShieldDescription) weaponDesc;
      this.CurrentEnergy = shieldDescription.CurrentEnergy;
      this.MaxEnergy = shieldDescription.MaxEnergy;
      this.RechargeSpeed = shieldDescription.RechargeSpeed;
      this.DecreaseSpeed = shieldDescription.DecreaseSpeed;
      base.Configure(weaponDesc);
    }

    private void CreateBullet()
    {
      this.Bullet = Shield.GetInstance();
      this.Bullet.Owner = this.Owner;
      this.Owner.GameWorld.AddInstance((Instance) this.Bullet);
      if (this.Owner is SmartPlayer)
      {
        this.Bullet.SpawnPosition = (Shield.ShieldPosition) (x => new Vector2((float) this.Owner.Contour.Rectangle.Center.X, (float) this.Owner.Contour.Rectangle.Center.Y - (float) x.Contour.Rectangle.Height / 2f));
      }
      else
      {
        this.Bullet.SpawnPosition = (Shield.ShieldPosition) (x => this.Owner.Position + this.WeaponPosition + this.BulletSpawnPosition - new Vector2((float) x.Contour.Rectangle.Width, (float) x.Contour.Rectangle.Height / 2f));
        this.Bullet.Contour.HorizontalFlip(this.Bullet.Contour.Rectangle.Width);
      }
      this.Bullet.ZIndex = 100500f;
      this.Bullet.Update(0.0f);
    }

    public override Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.Bullet GetBullet()
    {
      return (Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.Bullet) null;
    }
  }
}
