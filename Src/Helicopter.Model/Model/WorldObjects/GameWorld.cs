// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.GameWorld
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Behaviour;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Providers;
using Helicopter.Model.WorldObjects.Scripts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  public class GameWorld
  {
    public const int PlayerPositionOnScreen = 210;
    private readonly CollisionAnalyzer _collisionAnalyzer;
    private readonly InstanceComparer _comparer = new InstanceComparer();
    internal readonly IList<Instance> RemoveItems = (IList<Instance>) new List<Instance>();
    public Rectangle ActiveArea;
    public Rectangle IncreasedActiveArea;
    private Script _currentScript;
    private float _lastPlayerPosition;

    public event EventHandler<DistanceEventArgs> DistanceChanged;

    public event EventHandler Ended;

    public event EventHandler<EnemyAnnihilationEventArgs> EnemyAnnihilation;

    public event EventHandler Landing;

    public event EventHandler<WeaponEventArgs> UnitFire;

    public event EventHandler<PlayerEventArgs> UnitHitted;

    public event EventHandler<EnemyAnnihilationEventArgs> UnitAdded;

    public int Length { get; set; }

    public int EpisodeLength { get; set; }

    public WorldType WorldType { get; set; }

    public EpisodeMode Mode { get; set; }

    public Helicopter.Model.WorldObjects.Background.Background Background { get; set; }

    public bool EnableLandingZone { get; set; }

    public SmartPlayer Player { get; set; }

    public MothershipCopter Boss { get; set; }

    public List<Instance> Instances { get; set; }

    public List<Instance> ActiveInstances { get; private set; }

    public IInstancesProvider InstancesProvider { get; set; }

    protected bool IsScriptRunning { get; set; }

    public float TotalScoreForUnits { get; set; }

    public GameWorld()
    {
      this.ActiveArea = new Rectangle(0, 0, 800, 480);
      this.IncreasedActiveArea = new Rectangle(0, 0, 815, 480);
      this._collisionAnalyzer = new CollisionAnalyzer();
      this.Instances = new List<Instance>();
    }

    public void Update(float elapsedSeconds)
    {
      if (this.IsScriptRunning)
        this._currentScript.Update(elapsedSeconds);
      else
        this.UsualUpdate(elapsedSeconds);
    }

    internal void UsualUpdate(float elapsedSeconds)
    {
      this.UpdateActiveInstances();
      this._collisionAnalyzer.Analyze((IList<Instance>) this.ActiveInstances);
      this.UpdateInstances(elapsedSeconds);
      this.UpdateActiveArea();
      this.UpdateDistance();
    }

    public void UpdateActiveInstances()
    {
      int left = this.IncreasedActiveArea.Left;
      int right = this.IncreasedActiveArea.Right;
      if (right > this.InstancesProvider.Seek)
      {
        foreach (Instance nextInstance in (IEnumerable<Instance>) this.InstancesProvider.GetNextInstances(this.InstancesProvider.RecommendedLengthForNextInstances))
          this.AddInstance(nextInstance);
      }
      this.Instances.Sort((IComparer<Instance>) this._comparer);
      int index1 = -1;
      int count = -1;
      int num = 0;
      for (int index2 = 0; index2 < this.Instances.Count; ++index2)
      {
        Instance instance = this.Instances[index2];
        if (index1 == -1 && instance.Contour.Rectangle.Right >= left)
        {
          index1 = num;
          count = 0;
        }
        if (count >= 0)
        {
          if (instance.Contour.Rectangle.Left < right)
            ++count;
          else
            break;
        }
        ++num;
        if (instance.IsNeedRemove || instance.IsTemporary && !this.IncreasedActiveArea.Intersects(instance.Contour.Rectangle))
          this.RemoveItems.Add(instance);
      }
      this.ActiveInstances = this.Instances.GetRange(index1, count);
      if (index1 > 1)
      {
        foreach (Instance instance in this.Instances.GetRange(0, index1 - 1))
          this.RemoveItems.Add(instance);
      }
      foreach (Instance removeItem in (IEnumerable<Instance>) this.RemoveItems)
        this.RemoveInstance(removeItem);
    }

    public void UpdateInstances(float elapsedSeconds)
    {
      this.RemoveItems.Clear();
      for (int index = 0; index < this.ActiveInstances.Count; ++index)
        this.ActiveInstances[index].Update(elapsedSeconds);
    }

    private void UpdateActiveArea()
    {
      if (this.IsScriptRunning)
        return;
      this.MoveActiveArea(this.Player.Contour.Rectangle.Right - 210);
      if (this.Mode != EpisodeMode.Story || this.ActiveArea.X < this.Length - this.ActiveArea.Width)
        return;
      this.MoveActiveArea(this.Length - this.ActiveArea.Width);
      this.StartFinishScript();
    }

    private void UpdateDistance()
    {
      if ((double) this._lastPlayerPosition < 0.001)
        this._lastPlayerPosition = this.Player.Position.X;
      if ((double) this.Player.Position.X - (double) this._lastPlayerPosition < 50.0)
        return;
      float num = this.Player.Position.X - this._lastPlayerPosition;
      this._lastPlayerPosition = this.Player.Position.X;
      this.InvokeDistanceChanged(new DistanceEventArgs()
      {
        Distance = num
      });
    }

    private void OnUnitDamaged(object sender, PlayerEventArgs e) => this.InvokeUnitHitted(e);

    private void InvokeDistanceChanged(DistanceEventArgs e)
    {
      EventHandler<DistanceEventArgs> distanceChanged = this.DistanceChanged;
      if (distanceChanged == null)
        return;
      distanceChanged((object) this, e);
    }

    private void InvokeEnd()
    {
      if (this.Ended == null)
        return;
      this.Ended((object) this, EventArgs.Empty);
    }

    public void InvokeEnemyAnnihilation(EnemyAnnihilationEventArgs e)
    {
      EventHandler<EnemyAnnihilationEventArgs> enemyAnnihilation = this.EnemyAnnihilation;
      if (enemyAnnihilation == null)
        return;
      enemyAnnihilation((object) this, e);
    }

    public void InvokeLanding()
    {
      EventHandler landing = this.Landing;
      if (landing == null)
        return;
      landing((object) this, EventArgs.Empty);
    }

    public void InvokeUnitHitted(PlayerEventArgs e)
    {
      EventHandler<PlayerEventArgs> unitHitted = this.UnitHitted;
      if (unitHitted == null)
        return;
      unitHitted((object) this, e);
    }

    public void InvokeUnitAdded(IUnit unit)
    {
      EventHandler<EnemyAnnihilationEventArgs> unitAdded = this.UnitAdded;
      if (unitAdded == null)
        return;
      unitAdded((object) this, new EnemyAnnihilationEventArgs()
      {
        Unit = unit
      });
    }

    public void InvokeUnitWeaponFire(WeaponEventArgs e)
    {
      EventHandler<WeaponEventArgs> unitFire = this.UnitFire;
      if (unitFire == null)
        return;
      unitFire((object) this, e);
    }

    private void OnUnitWeaponFire(object sender, WeaponEventArgs e) => this.InvokeUnitWeaponFire(e);

    public void AddInstance(Instance instance)
    {
      instance.GameWorld = this;
      if (instance is IUnit)
      {
        IUnit unit = (IUnit) instance;
        unit.WeaponFired -= new EventHandler<WeaponEventArgs>(this.OnUnitWeaponFire);
        unit.WeaponFired += new EventHandler<WeaponEventArgs>(this.OnUnitWeaponFire);
        unit.Damaged += new EventHandler<PlayerEventArgs>(this.OnUnitDamaged);
      }
      if (instance is MothershipCopter)
      {
        this.Boss = (MothershipCopter) instance;
        this.Boss.Behaviour = (IBehaviour) new BossStupidBehaviour((Copter) this.Boss);
        this.Boss.StateChanged += (EventHandler<StateChangeEventArgs<int>>) delegate
        {
          this.StartFinishScript();
        };
      }
      if (instance is IUnit)
        this.InvokeUnitAdded(instance as IUnit);
      this.Instances.Add(instance);
    }

    public void Init() => this.Instances.ForEach((Action<Instance>) (x => x.GameWorld = this));

    public void RemoveInstance(Instance instance)
    {
      if (this.Instances.Contains(instance))
        this.Instances.Remove(instance);
      if (!this.ActiveInstances.Contains(instance))
        return;
      this.ActiveInstances.Remove(instance);
    }

    public void MoveActiveArea(int xPos)
    {
      this.ActiveArea.X = xPos;
      this.IncreasedActiveArea.X = xPos;
    }

    private void StartFinishScript()
    {
      this.IsScriptRunning = true;
      this._currentScript = this.Mode != EpisodeMode.Story ? (Script) new FinishBossBattle(this) : (Script) new StraightPlayerFly(this);
      this._currentScript.Ended += (EventHandler) ((x, y) =>
      {
        this.InvokeEnd();
        this.IsScriptRunning = false;
      });
    }
  }
}
