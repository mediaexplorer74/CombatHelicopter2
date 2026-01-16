// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Cannon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  public class Cannon : MoveableInstance, IUnit
  {
    public const int Normal = 0;
    public const int Death = 1;
    private static readonly ObjectPool<Cannon> _pool = new ObjectPool<Cannon>((ICreation<Cannon>) new Cannon.Creator());
    public Weapon Weapon;
    private Vector2 _basePosition;
    private float _energy;
    private float _hitCorridor = 50f;
    private float? _hitPosition;
    private float _launchAngleCos;
    private float _launchAngleSin;
    private float _maxYPosition;
    private float _minYPosition;
    private float _moutionPath;
    private float _moutionRange = 20f;
    private float _moutionSpeed = 100f;

    public static Cannon GetInstance() => Cannon._pool.GetObject();

    public override void Release() => Cannon._pool.Release(this);

    public override void ResetState()
    {
      base.ResetState();
      this.State = 0;
      this.CannonPattern = (CannonPattern) null;
      this.Weapon = (Weapon) null;
      this._basePosition = Vector2.Zero;
      this.WeaponFired = (EventHandler<WeaponEventArgs>) null;
    }

    public CannonPattern CannonPattern
    {
      get => (CannonPattern) this.Pattern;
      protected set => this.Pattern = (Pattern) value;
    }

    protected Cannon()
    {
      this.AmmoDefenseModifier = 0.0f;
      this.MountainDefenseModifier = 0.0f;
    }

    public float Price { get; set; }

    public float Score { get; set; }

    public UnitType UnitType => ((CannonPattern) this.Pattern).UnitType;

    public float Energy
    {
      get => this._energy;
      set
      {
        if (this.State == 1)
          return;
        this._energy = value;
        if ((double) this._energy > 0.0)
          return;
        this.State = 1;
      }
    }

    public float MaxEnergy { get; set; }

    public float EnergyRegeneration { get; set; }

    public int Team { get; set; }

    public float AmmoDefenseModifier { get; set; }

    public float MountainDefenseModifier { get; set; }

    public void HandleDamage(float damage, DamageType damageType)
    {
      this.Energy -= damage * (1f - this.AmmoDefenseModifier);
      if (this.State == 1)
        return;
      this.InvokeHitted(PlayerEventArgs.Create(damageType));
    }

    public event EventHandler<PlayerEventArgs> Damaged;

    public event EventHandler<WeaponEventArgs> WeaponFired;

    public void HandleCollisionDamage(float damage)
    {
    }

    public float CollisionDamage { get; set; }

    public void InvokeHitted(PlayerEventArgs e)
    {
      EventHandler<PlayerEventArgs> damaged = this.Damaged;
      if (damaged == null)
        return;
      damaged((object) this, e);
    }

    public void InvokeWeaponFired(WeaponEventArgs e)
    {
      EventHandler<WeaponEventArgs> weaponFired = this.WeaponFired;
      if (weaponFired == null)
        return;
      weaponFired((object) this, e);
    }

    protected override void OnStateChanged(object sender, StateChangeEventArgs<int> e)
    {
      if (e.NextState != 1)
        return;
      this.GameWorld.InvokeEnemyAnnihilation(new EnemyAnnihilationEventArgs()
      {
        Unit = (IUnit) this
      });
      this.Speed = Vector2.Zero;
      this.Acceleration = Vector2.Zero;
    }

    private void OnWeaponFired(object sender, WeaponEventArgs e) => this.InvokeWeaponFired(e);

    private float? GetHitPoint()
    {
      Vector2 vector2_1 = new Vector2(this.Weapon.BulletSpeed * this._launchAngleCos, this.Weapon.BulletSpeed * this._launchAngleSin);
      Vector2 vector2_2 = new Vector2(this.Weapon.BulletAcceleration * this._launchAngleCos, this.Weapon.BulletAcceleration * this._launchAngleSin);
      float num1 = this.SolveQuadraticEquation(Math.Abs(vector2_2.X), this.GameWorld.Player.Speed.X + Math.Abs(vector2_1.X), (float) this.GameWorld.Player.Contour.Rectangle.Right - (this.Position.X + this.Weapon.BulletSpawnPosition.X));
      if ((double) num1 <= 0.0)
        return new float?();
      float num2 = (float) this.GameWorld.Player.Contour.Rectangle.Center.Y - (float) ((double) vector2_1.Y * (double) num1 + (double) vector2_2.Y * (double) num1 * (double) num1);
      return new float?(this.CannonPattern.Alignment != VerticalAlignment.Top ? num2 + this.Weapon.WeaponPosition.Y : num2 - this.Weapon.WeaponPosition.Y);
    }

    public override void Init(Pattern pattern)
    {
      CannonPattern cannonPattern = pattern is CannonPattern ? (CannonPattern) pattern : throw new Exception("Not correct pattern type");
      base.Init((Pattern) cannonPattern);
      this.Weapon = WeaponFactory.GetCannonWeapon(this, cannonPattern.WeaponSlotDesc.WeaponType);
      this.Weapon.WeaponPosition = new Vector2((float) cannonPattern.WeaponSlotDesc.Offset.X, (float) cannonPattern.WeaponSlotDesc.Offset.Y);
      this.Weapon.LaunchAngle = MathHelper.ToRadians(cannonPattern.Alignment == VerticalAlignment.Top ? 150f : 210f);
      this.Weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapon.Fired += new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapon.WeaponSlotDescription = cannonPattern.WeaponSlotDesc;
      this._launchAngleSin = (float) Math.Sin((double) this.Weapon.LaunchAngle);
      this._launchAngleCos = (float) Math.Cos((double) this.Weapon.LaunchAngle);
      this._moutionSpeed = cannonPattern.MoutonSpeed;
      this._moutionPath = (float) (this.CannonPattern.Contour.Rectangle.Height - 60);
      this._hitCorridor = cannonPattern.HitCorridor;
      this._moutionRange = cannonPattern.MoutionRange;
      this.Price = cannonPattern.Price;
      this.Score = cannonPattern.Price;
      this.Energy = cannonPattern.Energy;
      this.CollisionDamage = cannonPattern.CollisionDamage;
      this.Reaction = (Reaction) new CannonReaction((Instance) this);
    }

    public float SolveQuadraticEquation(float a, float b, float c)
    {
      float num1 = (float) Math.Sqrt((double) b * (double) b - 4.0 * (double) a * (double) c);
      float num2 = (float) ((-(double) b + (double) num1) / (2.0 * (double) a));
      float num3 = (float) ((-(double) b - (double) num1) / (2.0 * (double) a));
      if ((double) num2 > 0.0)
        return num2;
      return (double) num3 > 0.0 ? num3 : -1f;
    }

    protected override void UpdateState(float elapsedSeconds)
    {
      if (this.State == 1)
        return;
      if (this._basePosition == Vector2.Zero)
      {
        this._basePosition = this.Position;
        if (this.CannonPattern.Alignment == VerticalAlignment.Top)
        {
          this._minYPosition = this._basePosition.Y;
          this._maxYPosition = this._basePosition.Y;
        }
        else
        {
          this._minYPosition = this._basePosition.Y - this._moutionPath;
          this._maxYPosition = this._basePosition.Y;
        }
      }
      this.Energy += this.EnergyRegeneration * elapsedSeconds;
      this.UpdateTarget();
      this.Weapon.Update(elapsedSeconds);
      this._hitPosition = this.GetHitPoint();
      if (this._hitPosition.HasValue)
      {
        this.UpdateTarget();
        this.Weapon.IsShooting = (double) Math.Abs(this.Position.Y + this.Weapon.BulletSpawnPosition.Y - this._hitPosition.Value) < (double) this._hitCorridor;
      }
      if ((double) this.Position.Y <= (double) this._minYPosition && (double) this.Speed.Y < 0.0)
        this.Speed.Y = 0.0f;
      if ((double) this.Position.Y < (double) this._maxYPosition || (double) this.Speed.Y <= 0.0)
        return;
      this.Speed.Y = 0.0f;
    }

    private void UpdateTarget()
    {
      if (this.State == 1)
        return;
      float? hitPosition1 = this._hitPosition;
      float num1 = this.Position.Y - this._moutionRange;
      if (((double) hitPosition1.GetValueOrDefault() >= (double) num1 ? 0 : (hitPosition1.HasValue ? 1 : 0)) != 0)
      {
        this.Speed.Y = -this._moutionSpeed;
      }
      else
      {
        float? hitPosition2 = this._hitPosition;
        float num2 = this.Position.Y + this._moutionRange;
        if (((double) hitPosition2.GetValueOrDefault() <= (double) num2 ? 0 : (hitPosition2.HasValue ? 1 : 0)) != 0)
          this.Speed.Y = this._moutionSpeed;
        else
          this.Speed.Y = 0.0f;
      }
    }

    protected class Creator : ICreation<Cannon>
    {
      public Cannon Create() => new Cannon();
    }
  }
}
