// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.MothershipCopter
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.Sounds;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Patterns;
using Helicopter.Model.WorldObjects.Providers;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  public class MothershipCopter : Copter
  {
    private const float PlayerSpeed = 300f;
    private const float AppearanceSpeed = 200f;
    private const float VerticalSpeed = 75f;
    private const int BacktrackSpeed = 400;
    private const float NewCoptersRate = 3f;
    private const float NewEggsRate = 1f;
    private const float OutOfScreenDuration = 15f;
    private static readonly ObjectPool<MothershipCopter> _pool = new ObjectPool<MothershipCopter>((ICreation<MothershipCopter>) new MothershipCopter.Creator());
    private UnitType _newCoptersUnitType;
    private Tweener _openningTweener;
    private Tweener _alightingCopterTweener;
    private bool _isDeliveryPosition;
    private Copter _newCopter;
    private Vector2 _childOffset = new Vector2(95f, 95f);
    private float _elapsedOutOfScreenTime;
    private float _elapsedNewCopterTime;
    private float _elapsedNewEggTime;
    private MothershipCopter.ShotState _shotState;
    private float _elapsedShotTime;
    private float _disableShieldTime;
    private float _breathTime;
    private float _shotTime;
    private float _enableShield;

    public static MothershipCopter GetInstance() => MothershipCopter._pool.GetObject();

    public override void Release() => MothershipCopter._pool.Release(this);

    public override void ResetState()
    {
      base.ResetState();
      this.BehaviorType = MothershipCopter.BossBehaviorType.Appearance;
    }

    public MothershipCopter.BossHealthPhase HealthPhase { get; set; }

    public MothershipCopter.BossBehaviorType BehaviorType { get; private set; }

    public int DoorOffset => (int) this._openningTweener.Position;

    public event EventHandler StartBreath;

    private void InvokeStartBreath(EventArgs e)
    {
      EventHandler startBreath = this.StartBreath;
      if (startBreath == null)
        return;
      startBreath((object) this, e);
    }

    public event EventHandler HealthPhaseChanged;

    public void InvokeHealthPhaseChanged(EventArgs e)
    {
      EventHandler healthPhaseChanged = this.HealthPhaseChanged;
      if (healthPhaseChanged == null)
        return;
      healthPhaseChanged((object) this, e);
    }

    protected MothershipCopter() => this.InitDefaultState();

    private void OnDoorOpened(object sender, EventArgs e)
    {
      if (this.State == 1 || (double) this._openningTweener.Position >= -0.001)
        return;
      Pattern pattern = ((BossBattleInstancesProvider) this.GameWorld.InstancesProvider).GetPattern(this._newCoptersUnitType);
      this._newCopter = Copter.GetInstance();
      this._newCopter.Init(pattern);
      this._newCopter.Position = new Vector2(this.Position.X + this._childOffset.X, this.Position.Y + this._childOffset.Y);
      this._newCopter.Team = this.Team;
      this._newCopter.ZIndex = this.ZIndex + 1f;
      this.GameWorld.AddInstance((Instance) this._newCopter);
      this._alightingCopterTweener.Init(0.0f, 100f, 1f, new TweeningFunction(Linear.EaseIn));
      this._alightingCopterTweener.Start();
    }

    private void OnAlightingCopterTweenerEnded(object sender, EventArgs e)
    {
      this._openningTweener.Reverse();
      this._openningTweener.Start();
      this.Speed.X = 400f;
      BossSounds.Instance.PlayDoor();
    }

    private void InitShootRates()
    {
      this._disableShieldTime = (float) (1.0 + CommonRandom.Instance.Random.NextDouble() * 1.5);
      this._breathTime = 0.0f + this._disableShieldTime;
      this._shotTime = this._breathTime + 1f;
      this._enableShield = this._shotTime + 1f;
    }

    protected override void SetEnergy(float newValue)
    {
      if (this.BehaviorType != MothershipCopter.BossBehaviorType.OutOfScreen)
        base.SetEnergy(newValue);
      float num = 100f * newValue / this.MaxEnergy;
      if ((double) num <= 70.001 && this.HealthPhase == MothershipCopter.BossHealthPhase.Phase100)
      {
        this.SelectNewHealthPhase(MothershipCopter.BossHealthPhase.Phase70);
        this.SelectNewBehavior(MothershipCopter.BossBehaviorType.BacktrackAndDelivery);
        this._newCoptersUnitType = UnitType.LightHelicopter;
      }
      else if ((double) num <= 40.001 && this.HealthPhase == MothershipCopter.BossHealthPhase.Phase70)
      {
        this.SelectNewHealthPhase(MothershipCopter.BossHealthPhase.Phase40);
        this.SelectNewBehavior(MothershipCopter.BossBehaviorType.BacktrackAndDelivery);
        this._newCoptersUnitType = UnitType.MediumHelicopter;
      }
      else
      {
        if ((double) num > 10.001 || this.HealthPhase != MothershipCopter.BossHealthPhase.Phase40)
          return;
        this.SelectNewHealthPhase(MothershipCopter.BossHealthPhase.Phase10);
        this.SelectNewBehavior(MothershipCopter.BossBehaviorType.BacktrackAndDelivery);
        this._newCoptersUnitType = UnitType.HeavyHelicopter;
      }
    }

    public override void Init(Pattern pattern)
    {
      base.Init(pattern);
      this.InitDefaultState();
      this.SelectNewBehavior(MothershipCopter.BossBehaviorType.Appearance);
    }

    private void InitDefaultState()
    {
      this.State = 0;
      this.EnableRotation = false;
      this.BehaviorType = MothershipCopter.BossBehaviorType.Appearance;
      this.HealthPhase = MothershipCopter.BossHealthPhase.Phase100;
      this._openningTweener = new Tweener(0.0f, -26f, 1f, new TweeningFunction(Linear.EaseIn));
      this._openningTweener.Ended += new EventHandler<EventArgs>(this.OnDoorOpened);
      this._openningTweener.Stop();
      this._alightingCopterTweener = new Tweener(0.0f, 100f, 1f, new TweeningFunction(Linear.EaseIn));
      this._alightingCopterTweener.Ended += new EventHandler<EventArgs>(this.OnAlightingCopterTweenerEnded);
      this._alightingCopterTweener.Stop();
      this._isDeliveryPosition = false;
    }

    protected override void UpdateState(float elapsedSeconds)
    {
      int right = this.GameWorld.ActiveArea.Right;
      switch (this.BehaviorType)
      {
        case MothershipCopter.BossBehaviorType.Appearance:
          if (this.Contour.Rectangle.Right < right - 20)
          {
            this.SelectNewBehavior(MothershipCopter.BossBehaviorType.Battle);
            break;
          }
          break;
        case MothershipCopter.BossBehaviorType.Battle:
          this.UpdateBattleState(elapsedSeconds);
          break;
        case MothershipCopter.BossBehaviorType.BacktrackAndDelivery:
          if (!this._isDeliveryPosition && (double) this.Position.Y < 150.0)
          {
            this._isDeliveryPosition = true;
            this._openningTweener.Init(0.0f, -26f, 1f, new TweeningFunction(Linear.EaseIn));
            this._openningTweener.Start();
            this.Speed.Y = 0.0f;
            BossSounds.Instance.PlayDoor();
          }
          this._openningTweener.Update(elapsedSeconds);
          this._alightingCopterTweener.Update(elapsedSeconds);
          if (this._alightingCopterTweener.Running && this._newCopter != null)
            this._newCopter.SetPosition(this.Position.X + this._childOffset.X, this.Position.Y + this._childOffset.Y + this._alightingCopterTweener.Position);
          if ((double) this.Position.X > (double) (right + 10))
          {
            this.SelectNewBehavior(MothershipCopter.BossBehaviorType.OutOfScreen);
            break;
          }
          break;
        case MothershipCopter.BossBehaviorType.OutOfScreen:
          this.UpdateOutOfScreenState(elapsedSeconds);
          break;
      }
      base.UpdateState(elapsedSeconds);
    }

    private void UpdateBattleState(float elapsedSeconds)
    {
      this._elapsedShotTime += elapsedSeconds;
      if (this._shotState == MothershipCopter.ShotState.Prepare && (double) this._elapsedShotTime > (double) this._disableShieldTime)
      {
        this._shotState = MothershipCopter.ShotState.DisableShield;
        this.Weapons[0].IsShooting = false;
      }
      else if (this._shotState == MothershipCopter.ShotState.DisableShield && (double) this._elapsedShotTime > (double) this._breathTime)
      {
        this._shotState = MothershipCopter.ShotState.Breath;
        this.InvokeStartBreath(EventArgs.Empty);
      }
      else if (this._shotState == MothershipCopter.ShotState.Breath && (double) this._elapsedShotTime > (double) this._shotTime)
      {
        this._shotState = MothershipCopter.ShotState.Shooting;
        this.Weapons[1].OneShoot();
      }
      else
      {
        if (this._shotState != MothershipCopter.ShotState.Shooting || (double) this._elapsedShotTime <= (double) this._enableShield)
          return;
        this._shotState = MothershipCopter.ShotState.Prepare;
        this._elapsedShotTime = 0.0f;
        this.Weapons[0].IsShooting = true;
        this.InitShootRates();
      }
    }

    private void UpdateOutOfScreenState(float elapsedSeconds)
    {
      int right = this.GameWorld.ActiveArea.Right;
      this._elapsedOutOfScreenTime += elapsedSeconds;
      if ((double) this._elapsedOutOfScreenTime > 15.0)
      {
        bool flag = true;
        int width = this.Contour.Rectangle.Width;
        foreach (Instance activeInstance in this.GameWorld.ActiveInstances)
        {
          if (activeInstance != this && activeInstance is Copter && right - activeInstance.Contour.Rectangle.Right < width)
            flag = false;
        }
        if (!flag)
          return;
        this.SelectNewBehavior(MothershipCopter.BossBehaviorType.Appearance);
        this._elapsedOutOfScreenTime = 0.0f;
      }
      else
      {
        this._elapsedNewCopterTime += elapsedSeconds;
        if ((double) this._elapsedNewCopterTime > 3.0)
        {
          this.AddNewCopter(this._newCoptersUnitType);
          this._elapsedNewCopterTime = 0.0f;
        }
        this._elapsedNewEggTime += elapsedSeconds;
        if ((double) this._elapsedNewEggTime <= 1.0)
          return;
        this.AddNewCopter(UnitType.Egg);
        this._elapsedNewEggTime = 0.0f;
      }
    }

    protected override void UpdateWeapons(float elapsedSeconds)
    {
      foreach (Weapon weapon in this.Weapons)
        weapon.Update(elapsedSeconds);
    }

    private void SelectNewBehavior(MothershipCopter.BossBehaviorType newState)
    {
      this.BehaviorType = newState;
      switch (this.BehaviorType)
      {
        case MothershipCopter.BossBehaviorType.Appearance:
          this.Speed.X = 200f;
          this.Speed.Y = 0.0f;
          this.SetPosition(this.Position.X, 180f);
          this.Weapons[0].IsShooting = true;
          this.Weapons[1].IsShooting = false;
          if (this.HealthPhase != MothershipCopter.BossHealthPhase.Phase40 && this.HealthPhase != MothershipCopter.BossHealthPhase.Phase10)
            break;
          BossSounds.Instance.PlaySparlkes();
          break;
        case MothershipCopter.BossBehaviorType.Battle:
          this.Speed.X = 300f;
          this.Speed.Y = 75f;
          this.Weapons[0].IsShooting = true;
          this.Weapons[1].IsShooting = false;
          this.InitShootRates();
          break;
        case MothershipCopter.BossBehaviorType.BacktrackAndDelivery:
          this.Speed.Y = -75f;
          this.Weapons[0].IsShooting = true;
          this.Weapons[1].IsShooting = false;
          this._isDeliveryPosition = false;
          break;
        case MothershipCopter.BossBehaviorType.OutOfScreen:
          BossSounds.Instance.StopSounds();
          this.Speed.X = 300f;
          this.Weapons[0].IsShooting = false;
          this.Weapons[1].IsShooting = false;
          this._elapsedOutOfScreenTime = 0.0f;
          this._elapsedNewCopterTime = 3f;
          this._openningTweener.Stop();
          this._openningTweener.Init(0.0f, -26f, 1f, new TweeningFunction(Linear.EaseIn));
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void SelectNewHealthPhase(MothershipCopter.BossHealthPhase phase)
    {
      this.HealthPhase = phase;
      this.InvokeHealthPhaseChanged(EventArgs.Empty);
    }

    private void AddNewCopter(UnitType unitType)
    {
      Pattern pattern = ((BossBattleInstancesProvider) this.GameWorld.InstancesProvider).GetPattern(unitType);
      Copter instance = Copter.GetInstance();
      instance.Init(pattern);
      instance.Position = new Vector2((float) this.GameWorld.ActiveArea.Right, (float) CommonRandom.Instance.Random.Next(100, 400));
      instance.Team = this.Team;
      this.GameWorld.AddInstance((Instance) instance);
    }

    protected new class Creator : ICreation<MothershipCopter>
    {
      public MothershipCopter Create() => new MothershipCopter();
    }

    public enum BossBehaviorType
    {
      Appearance,
      Battle,
      BacktrackAndDelivery,
      OutOfScreen,
    }

    public enum BossHealthPhase
    {
      Phase100,
      Phase70,
      Phase40,
      Phase10,
    }

    private enum ShotState
    {
      Prepare,
      Breath,
      DisableShield,
      Shooting,
    }
  }
}
