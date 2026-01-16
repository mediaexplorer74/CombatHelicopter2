// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Copter
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances.Behaviour;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  public class Copter : MoveableInstance, IUnit
  {
    public const int Normal = 0;
    public const int Death = 1;
    private static readonly ObjectPool<Copter> _pool = new ObjectPool<Copter>((ICreation<Copter>) new Copter.Creator());
    public float HeightCompensation;
    public float HitShotCorridor;
    public float ObstaclesReboundYSpeed;
    public float PatrolingSpeed;
    public float PursuitAcceleration;
    public float PursuitMaxYSpeed;
    public float PursuitXSpeed;
    public int StartPursuitDistance;
    private bool _canShoot;
    private int _downRotationSpeed = 15;
    private float _energy;
    private int _maxBackDegree = -5;
    private int _maxForwardDegree = 2;
    private float _shootDelay;
    private int _upRotationSpeed = 25;

    public static Copter GetInstance() => Copter._pool.GetObject();

    public override void Release() => Copter._pool.Release(this);

    public override void ResetState()
    {
      this.Weapons.ForEach((Action<Weapon>) (x => x.IsShooting = false));
      base.ResetState();
      this.MountainDefenseModifier = 0.0f;
      this.AmmoDefenseModifier = 0.0f;
      this._shootDelay = 1f;
      this._canShoot = false;
      this.State = 0;
      this.DamageFactor = 1f;
    }

    protected bool EnableRotation { get; set; }

    public IBehaviour Behaviour { get; set; }

    public List<Weapon> Weapons { get; set; }

    public bool IsBoss { get; set; }

    protected bool IsFlyUp => (double) this.Speed.Y < 0.0;

    public float DamageFactor { get; set; }

    protected Copter()
    {
      this.MountainDefenseModifier = 0.0f;
      this.AmmoDefenseModifier = 0.0f;
      this._shootDelay = 1f;
      this._canShoot = false;
      this.EnableRotation = true;
      this.DamageFactor = 1f;
    }

    public float Price { get; set; }

    public float Score { get; set; }

    public UnitType UnitType => ((HelicopterPattern) this.Pattern).UnitType;

    public float Energy
    {
      get => this._energy;
      set => this.SetEnergy(value);
    }

    protected virtual void SetEnergy(float newValue)
    {
      if (this.State == 1)
        return;
      this._energy = newValue;
      if ((double) newValue > 0.0 || this.GameWorld == null)
        return;
      this.State = 1;
    }

    public float MaxEnergy { get; set; }

    public float EnergyRegeneration { get; set; }

    public float CollisionDamage { get; set; }

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

    public void InvokeWeaponFire(WeaponEventArgs e)
    {
      EventHandler<WeaponEventArgs> weaponFired = this.WeaponFired;
      if (weaponFired == null)
        return;
      weaponFired((object) this, e);
    }

    public void InvokeHitted(PlayerEventArgs e)
    {
      EventHandler<PlayerEventArgs> damaged = this.Damaged;
      if (damaged == null)
        return;
      damaged((object) this, e);
    }

    public void HandleCollisionDamage(float damage)
    {
      this.Energy -= damage * (1f - this.MountainDefenseModifier);
    }

    public void AwayFromObstacles(Instance obstacle) => this.Behaviour.AwayFromObstacles(obstacle);

    private Vector2 GetInitialSpeed()
    {
      return new Vector2()
      {
        X = (float) (CommonRandom.Instance.Random.NextDouble() - 0.5) * this.PatrolingSpeed,
        Y = CommonRandom.Instance.Random.Next(2) != 1 ? -this.PatrolingSpeed : this.PatrolingSpeed
      };
    }

    public override void Init(Pattern pattern)
    {
      HelicopterPattern helicopterPattern = pattern is HelicopterPattern ? (HelicopterPattern) pattern : throw new Exception("Not correct pattern type");
      base.Init((Pattern) helicopterPattern);
      this.Speed = this.GetInitialSpeed();
      this.Reaction = (Reaction) new CopterReaction((Instance) this);
      this.Weapons = new List<Weapon>();
      foreach (WeaponSlotDescription weaponSlot in helicopterPattern.WeaponSlots)
      {
        Weapon weapon = WeaponFactory.GetWeapon((Instance) this, weaponSlot.WeaponType);
        weapon.WeaponPosition = new Vector2((float) weaponSlot.Offset.X, (float) weaponSlot.Offset.Y);
        weapon.LaunchAngle = MathHelper.ToRadians(180f);
        weapon.WeaponSlotDescription = weaponSlot;
        weapon.DamageFactor = this.DamageFactor;
        this.AddWeapon(weapon);
      }
      if (this.Weapons != null)
      {
        if (this.Weapons.Count > 0 && this.Weapons[0] != null)
        {
          this.Weapons[0].Rate = (double) helicopterPattern.FirstWeaponRate > 0.0 ? helicopterPattern.FirstWeaponRate : this.Weapons[0].Rate;
          this.Weapons[0].ShootingTime = (double) helicopterPattern.FirstWeaponShootingTime > 0.0 ? helicopterPattern.FirstWeaponShootingTime : this.Weapons[0].ShootingTime;
          this.Weapons[0].ReloadTime = (double) helicopterPattern.FirstWeaponReloadTime > 0.0 ? helicopterPattern.FirstWeaponReloadTime : this.Weapons[0].ReloadTime;
        }
        if (this.Weapons.Count > 1 && this.Weapons[1] != null)
        {
          this.Weapons[1].Rate = (double) helicopterPattern.SecondWeaponRate > 0.0 ? helicopterPattern.SecondWeaponRate : this.Weapons[1].Rate;
          this.Weapons[1].ShootingTime = (double) helicopterPattern.SecondWeaponShootingTime > 0.0 ? helicopterPattern.SecondWeaponShootingTime : this.Weapons[1].ShootingTime;
          this.Weapons[1].ReloadTime = (double) helicopterPattern.SecondWeaponReloadTime > 0.0 ? helicopterPattern.SecondWeaponReloadTime : this.Weapons[1].ReloadTime;
        }
      }
      this.PursuitAcceleration = 198f;
      this.PursuitMaxYSpeed = helicopterPattern.PursuitMaxYSpeed;
      this.PursuitXSpeed = helicopterPattern.PursuitXSpeed;
      this.HitShotCorridor = helicopterPattern.HitShotCorridor;
      this.HeightCompensation = helicopterPattern.HeightCompensation;
      this.StartPursuitDistance = helicopterPattern.StartPursuitDistance;
      this.PatrolingSpeed = helicopterPattern.PatrolingSpeed;
      this.CollisionDamage = helicopterPattern.CollisionDamage;
      this.ObstaclesReboundYSpeed = helicopterPattern.ObstaclesReboundYSpeed;
      this._shootDelay = helicopterPattern.ShootDelay;
      this.Speed = new Vector2(this.PursuitXSpeed, 0.0f);
      this.Price = helicopterPattern.Price;
      this.Score = helicopterPattern.Price;
      this.MaxEnergy = this.Energy = helicopterPattern.Energy;
      switch (helicopterPattern.MotionType)
      {
        case HelicopterMotionType.Static:
          this.Behaviour = (IBehaviour) new FixedPositionBehaviour(this);
          break;
        case HelicopterMotionType.Prosecution:
          this.Behaviour = (IBehaviour) new PursuitBehaviour(this);
          break;
        case HelicopterMotionType.Attack:
          this.Behaviour = (IBehaviour) new AttackBehaviour(this);
          break;
        case HelicopterMotionType.Stupid:
          this.Behaviour = (IBehaviour) new StupidBehaviour(this);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.StateChanged -= new EventHandler<StateChangeEventArgs<int>>(this.OnStateChanged);
      this.StateChanged += new EventHandler<StateChangeEventArgs<int>>(this.OnStateChanged);
    }

    private void AddWeapon(Weapon weapon)
    {
      weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFire);
      weapon.Fired += new EventHandler<WeaponEventArgs>(this.OnWeaponFire);
      this.Weapons.Add(weapon);
    }

    private void OnWeaponFire(object sender, WeaponEventArgs e) => this.InvokeWeaponFire(e);

    protected override void UpdateState(float elapsedSeconds)
    {
      if (this.State != 0)
        return;
      this.Energy += this.EnergyRegeneration * elapsedSeconds;
      this.Behaviour.Update(elapsedSeconds);
      if (this.EnableRotation)
        this.CopterRotationChange(elapsedSeconds);
      this.UpdateWeapons(elapsedSeconds);
    }

    protected virtual void UpdateWeapons(float elapsedSeconds)
    {
      if (!this._canShoot)
      {
        this._shootDelay -= elapsedSeconds;
        if ((double) this._shootDelay < 0.0)
          this._canShoot = true;
      }
      if (!this._canShoot)
        return;
      foreach (Weapon weapon in this.Weapons)
        weapon.Update(elapsedSeconds);
      if ((double) Math.Abs(this.Contour.Rectangle.Center.Y - this.GameWorld.Player.Contour.Rectangle.Center.Y) < (double) this.HitShotCorridor)
      {
        foreach (Weapon weapon in this.Weapons)
          weapon.IsShooting = true;
      }
      else
      {
        foreach (Weapon weapon in this.Weapons)
          weapon.IsShooting = false;
      }
    }

    protected virtual void CopterRotationChange(float elapsedSeconds)
    {
      if (this.IsFlyUp)
        this.Rotation -= MathHelper.ToRadians((float) this._upRotationSpeed * elapsedSeconds);
      else
        this.Rotation += MathHelper.ToRadians((float) this._downRotationSpeed * elapsedSeconds);
      this.Rotation = MathHelper.Clamp(this.Rotation, MathHelper.ToRadians((float) this._maxBackDegree), MathHelper.ToRadians((float) this._maxForwardDegree));
    }

    protected override void OnStateChanged(object sender, StateChangeEventArgs<int> e)
    {
      if (e.NextState != 1)
        return;
      this.Weapons.ForEach((Action<Weapon>) (x => x.IsShooting = false));
      this.GameWorld.InvokeEnemyAnnihilation(new EnemyAnnihilationEventArgs()
      {
        Unit = (IUnit) this
      });
      this.Speed = Vector2.Zero;
      this.Acceleration = Vector2.Zero;
    }

    protected class Creator : ICreation<Copter>
    {
      public Copter Create() => new Copter();
    }
  }
}
