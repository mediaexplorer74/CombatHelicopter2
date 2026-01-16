// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.SmartPlayer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.DeviceBonus;
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
  public class SmartPlayer : MoveableInstance, IUnit
  {
    public const int Normal = 0;
    public const int Death = 1;
    public const int UnderShield = 2;
    public const int MountainShield = 3;
    public int BulletCount;
    public int DamagedCount;
    public int Hits;
    private float _downAcceleration;
    private int _downRotationSpeed = 15;
    private float _energy;
    private float _farReboundSpeed = 200f;
    private bool _isResetSpeed;
    private int _maxBackDegree = 1;
    private int _maxForwardDegree = 7;
    private float _resetSpeedValue;
    private float _shortReboundSpeed = 150f;
    private float _startSpeed;
    private float _timeToGetNormal = 1f;
    private float _upAcceleration;
    private int _upRotationSpeed = 25;
    private Point[] _weaponPosition;

    public event EventHandler<PlayerEventArgs> Killed;

    public event EventHandler<PlayerEventArgs> MountainCollided;

    public event EventHandler<PlayerEventArgs> Rammed;

    public int RightPositionOnScreen { get; set; }

    public float EnergyOnMountainCollision { get; set; }

    public float MoneyModifier { get; set; }

    public Money Money { get; set; }

    public Weapon[] Weapons { get; set; }

    protected List<GameItem> GameItems { get; set; }

    public bool IsFlyUp => (double) this.Speed.Y < 0.0;

    public float IndestrictableAfterCollisionTime { get; set; }

    public float EnergyPercent => this.Energy / this.MaxEnergy;

    public ExternalSupply ExternalSupply { get; set; }

    public DefenseModifier DefenseModifier { get; set; }

    public DamageModifier DamageModifier { get; set; }

    public Vector2 DeathPosition { get; set; }

    public SmartPlayer() => this.MoneyModifier = 1f;

    public event EventHandler<PlayerEventArgs> Damaged;

    public float Price { get; set; }

    public float Score { get; set; }

    public UnitType UnitType => UnitType.Player;

    public float CollisionDamage { get; set; }

    public float Energy
    {
      get => this._energy;
      set
      {
        this._energy = value;
        if ((double) this._energy < 0.05 && this.ExternalSupply != null && (double) this.ExternalSupply.Volume > 0.001)
        {
          float num = (double) this.ExternalSupply.Volume <= 0.999 ? ((double) this.ExternalSupply.Volume <= 0.499 ? this.ExternalSupply.Volume : 0.5f) : 1f;
          this._energy += num * this.MaxEnergy;
          this.ExternalSupply.Volume -= num;
          this.ExternalSupply.InvokeUsed(EventArgs.Empty);
        }
        this._energy = MathHelper.Clamp(this._energy, 0.0f, this.MaxEnergy);
        if ((double) this._energy >= 1.0 / 1000.0 || this.State == 1)
          return;
        this.State = 1;
        this.DeathPosition = this.Position + new Vector2((float) this.Contour.Rectangle.Width / 2f, (float) this.Contour.Rectangle.Height / 2f);
        if (this.Killed == null)
          return;
        this.Killed((object) this, new PlayerEventArgs());
      }
    }

    public float MaxEnergy { get; set; }

    public float EnergyRegeneration { get; set; }

    public int Team { get; set; }

    public float AmmoDefenseModifier { get; set; }

    public float MountainDefenseModifier { get; set; }

    public void HandleDamage(float damage, DamageType damageType)
    {
      ++this.DamagedCount;
      if (this.State == 3 || this.State == 2)
        return;
      float ammoDefenseModifier = this.AmmoDefenseModifier;
      float num = 0.0f;
      if (this.DefenseModifier != null)
        num = this.DefenseModifier.GetDefenseCoef(damageType);
      this.Energy -= (float) ((double) damage * (1.0 - (double) ammoDefenseModifier) * (1.0 - (double) num));
      if (this.DefenseModifier != null && (double) num > 0.001)
        this.DefenseModifier.InvokeUsed(EventArgs.Empty);
      if (this.State == 1)
        return;
      this.InvokeHitted(PlayerEventArgs.Create(damageType));
    }

    public event EventHandler<WeaponEventArgs> WeaponFired;

    public void HandleCollisionDamage(float damage)
    {
      ++this.DamagedCount;
      if (this.State == 3)
        return;
      float mountainDefenseModifier = this.MountainDefenseModifier;
      float num = 0.0f;
      if (this.DefenseModifier != null)
        num = this.DefenseModifier.GetDefenseCoef(DamageType.Collision);
      if (this.State == 2)
        this.Weapons[1].IsShooting = false;
      this.State = 3;
      this.Energy -= (float) ((double) damage * (1.0 - (double) mountainDefenseModifier) * (1.0 - (double) num));
      if (this.DefenseModifier != null && (double) num > 0.001)
        this.DefenseModifier.InvokeUsed(EventArgs.Empty);
      if (this.MountainCollided == null)
        return;
      this.MountainCollided((object) this, PlayerEventArgs.Create(DamageType.Collision));
    }

    public void InvokeEnemyRammed()
    {
      if (this.Rammed == null)
        return;
      this.Rammed((object) this, new PlayerEventArgs());
    }

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

    public void OnBulletHit() => ++this.Hits;

    private void OnWeaponFired(object sender, WeaponEventArgs e)
    {
      ++this.BulletCount;
      this.InvokeWeaponFired(e);
    }

    public void AddGameItem(GameItem gameItem) => this.GameItems.Add(gameItem);

    public void AddWeapon(int slot, Weapon weapon)
    {
      weapon.WeaponPosition = new Vector2((float) this._weaponPosition[slot].X, (float) this._weaponPosition[slot].Y);
      this.Weapons[slot] = weapon;
      this.Weapons[slot].Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapons[slot].Fired += new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
    }

        public void EndShootWeapon(int weapon)
        {
            // RnD : this.Weapons[1] is always null?? IS THIS BUG?
            if (this.Weapons[weapon] != null)
              this.Weapons[weapon].IsShooting = false;
        }

        public void Init(PlayerPattern pattern)
    {
      this.Init((Pattern) pattern);
      this._startSpeed = pattern.StartSpeed;
      this._downAcceleration = pattern.DownAcceleration;
      this._upAcceleration = pattern.UpAcceleration;
      this._isResetSpeed = pattern.IsResetSpeed;
      this._resetSpeedValue = pattern.ResetSpeedValue;
      this._farReboundSpeed = pattern.FarReboundSpeed;
      this._shortReboundSpeed = pattern.ShortReboundSpeed;
      this.IndestrictableAfterCollisionTime = pattern.IndestrictableAfterCollisionTime;
      this.EnergyOnMountainCollision = pattern.EnergyOnMountainCollision;
      this.RightPositionOnScreen = pattern.RightPositionOnScreen;
      this._upRotationSpeed = pattern.UpRotationSpeed;
      this._downRotationSpeed = pattern.DownRotationSpeed;
      this._maxBackDegree = pattern.MaxBackDegree;
      this._maxForwardDegree = pattern.MaxForwardDegree;
      this._weaponPosition = pattern.WeaponPosition;
      this.Speed = new Vector2(this._startSpeed, 0.0f);
      this.Acceleration = new Vector2(0.0f, this._downAcceleration);
      this.Reaction = (Reaction) new PlayerReaction(this);
      this.GameItems = new List<GameItem>();
    }

    public void ReboundDown(bool shortRebound)
    {
      this.Speed.Y = shortRebound ? this._shortReboundSpeed : this._farReboundSpeed;
    }

    public void ReboundUp(bool shortRebound)
    {
      this.Speed.Y = shortRebound ? -this._shortReboundSpeed : -this._farReboundSpeed;
    }

    public void StartShootWeapon(int weapon)
    {
      //RnD : this.Weapons[1] is always null.. IS THIS BUG?
      if (this.Weapons[weapon] != null)
        this.Weapons[weapon].IsShooting = true;
    }

    public void StartUp()
    {
      this.Acceleration = new Vector2(0.0f, this._upAcceleration);
      if (!this._isResetSpeed || (double) this.Speed.Y <= (double) this._resetSpeedValue)
        return;
      this.Speed.Y = this._resetSpeedValue;
    }

    public void StopUp()
    {
      this.Acceleration = new Vector2(0.0f, this._downAcceleration);
      if (!this._isResetSpeed || (double) this.Speed.Y >= -(double) this._resetSpeedValue)
        return;
      this.Speed.Y = -this._resetSpeedValue;
    }

    protected override void UpdateState(float elapsedSeconds)
    {
      if (this.State == 1)
        return;
      this.Energy += this.EnergyRegeneration * elapsedSeconds;
      foreach (Weapon weapon in this.Weapons)
        weapon?.Update(elapsedSeconds);
      if (this.IsFlyUp)
        this.Rotation += MathHelper.ToRadians((float) this._upRotationSpeed * elapsedSeconds);
      else
        this.Rotation -= MathHelper.ToRadians((float) this._downRotationSpeed * elapsedSeconds);
      this.Rotation = MathHelper.Clamp(this.Rotation, MathHelper.ToRadians((float) this._maxBackDegree), MathHelper.ToRadians((float) this._maxForwardDegree));
      if (this.State != 3)
        return;
      this._timeToGetNormal -= elapsedSeconds;
      if ((double) this._timeToGetNormal >= 0.0)
        return;
      this.State = 0;
      this._timeToGetNormal = this.IndestrictableAfterCollisionTime;
    }
  }
}
