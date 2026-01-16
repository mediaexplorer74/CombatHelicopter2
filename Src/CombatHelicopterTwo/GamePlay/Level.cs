// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.Level
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Descriptions;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Model.WorldObjects.Providers;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

#nullable disable
namespace Helicopter.GamePlay
{
  public class Level
  {
    private static int _preLastChallengeLocation = -1;
    private static int _lastChallengeLocation = -1;
    public string EpisodeDescriptionName;
    public GameMode Mode;
    private WorldDrawer _drawer;
    private GameWorld _episode;
    private int _episodeNumber;
    private bool _firstEpisodeInChallenge = true;
    private LandingLabel _landingLabel;
    private GameWorld _nextEpisode;
    private SmartPlayer _player;
    private BasicControl _root = new BasicControl();
    private LevelState _state;
    private float _timeFromDead;

    public event EventHandler<DistanceEventArgs> DistanceChanged;

    public event EventHandler<EnemyAnnihilationEventArgs> EnemyAnnihilation;

    public event EventHandler<EventArgs> EpisodeChanged;

    public event EventHandler Landing;

    public event EventHandler<StateChangeEventArgs<LevelState>> StateChanged;

    public event EventHandler<WeaponEventArgs> UnitFire;

    public event EventHandler<EnemyAnnihilationEventArgs> UnitAdded;

    public event EventHandler<EventArgs> UnitHitted;

    public int EpisodeNumber
    {
      get => this._episodeNumber;
      set
      {
        this._episodeNumber = value;
        if (this._episode == null || this.Mode != GameMode.Challenge)
          return;
        ((ChallengeProvider) this._episode.InstancesProvider).UpdateEpisodeNumber(this._episodeNumber);
      }
    }

    public float StartPlayerEnergy { get; set; }

    public LevelState State
    {
      get => this._state;
      set
      {
        if (this._state == value)
          return;
        LevelState state = this._state;
        this._state = value;
        if (this.StateChanged == null)
          return;
        this.StateChanged((object) this, new StateChangeEventArgs<LevelState>(state, this._state));
      }
    }

    public ScreenManager ScreenManager { get; set; }

    public SmartPlayer Player => this._episode == null ? (SmartPlayer) null : this._episode.Player;

    public WorldType WorldType => this._episode.WorldType;

    public float ProgressPercent
    {
      get
      {
        return MathHelper.Clamp((this._player.Position.X - 200f) / (float) this._episode.EpisodeLength, 0.0f, 1f);
      }
    }

    public int LandingZoneStart { get; private set; }

    public int LandingZoneEnd { get; private set; }

    public float TotalScoreForUnits => this._episode.TotalScoreForUnits;

    public float BossHealth
    {
      get
      {
        return this._episode == null || this._episode.Boss == null ? 1f : this._episode.Boss.Energy / this._episode.Boss.MaxEnergy;
      }
    }

    public Level() => this.State = LevelState.Preparation;

    public void Update(GameTime gameTime)
    {
      if (this.Mode == GameMode.Challenge)
      {
        bool flag1 = false;
        float num1 = this._episode.Player.Position.X + (float) this._episode.Player.Contour.Rectangle.Width;
        int num2 = 1600;
        int num3 = num2 + 36800;
        if (this._episode.ActiveArea.Left < num2)
        {
          this.LandingZoneStart = -this._episode.ActiveArea.Left;
          this.LandingZoneEnd = num2 + this.LandingZoneStart;
          flag1 = !this._firstEpisodeInChallenge && (double) num1 < (double) num2;
        }
        else if (this._episode.ActiveArea.Right > num3)
        {
          this.LandingZoneStart = num3 - this._episode.ActiveArea.Left;
          this.LandingZoneEnd = this.LandingZoneStart + 1600;
          bool flag2 = (double) this._episode.Player.Position.X > (double) num3;
          flag1 = (double) num1 > (double) num3;
          if (flag2 && (this.Player.DefenseModifier != null || this.Player.DamageModifier != null))
          {
            this.Player.DefenseModifier = (DefenseModifier) null;
            this.Player.DamageModifier = (DamageModifier) null;
          }
        }
        else
        {
          this.LandingZoneStart = -1;
          this.LandingZoneEnd = -1;
        }
        this._landingLabel.Visible = flag1;
        this._landingLabel.Update(gameTime);
        this._episode.EnableLandingZone = flag1;
        if (this._episode.ActiveArea.Right >= num3 + 1600)
          this.SelectNextEpisode();
      }
      if (this.State == LevelState.Play)
      {
        float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
        this._episode.Update(totalSeconds);
        this._drawer.Update(totalSeconds);
      }
      if (this.State != LevelState.Lose)
        return;
      this.LoseUpdate(gameTime);
    }

    public void Draw(DrawContext drawContext)
    {
      this._drawer.Draw(drawContext.SpriteBatch);
      this._root.Draw(drawContext);
    }

    private void OnPlayerKilled(object sender, PlayerEventArgs e) => this.State = LevelState.Lose;

    public void Init()
    {
      this._episode.Update(0.0f);
      this._drawer.Update(0.0f);
    }

    public void LoadLevelContent()
    {
      this.InitLevelObjects();
      GameWorld gameWorld = LevelRestorer.RestoreWorld(ResourcesManager.Instance.GetResource<LevelDesc>(this.EpisodeDescriptionName));
      gameWorld.Init();
      if (this.Mode == GameMode.Challenge)
        ((ChallengeProvider) gameWorld.InstancesProvider).UpdateEpisodeNumber(this.EpisodeNumber);
      this.AddEventHandlers(gameWorld);
      this._episode = gameWorld;
      this._player = Gamer.Instance.GetPlayer(this._episode);
      if ((double) this.StartPlayerEnergy > 0.001)
        this._player.Energy = this.StartPlayerEnergy;
      this._player.Killed += new EventHandler<PlayerEventArgs>(this.OnPlayerKilled);
      this._episode.Player = this._player;
      if (this._episode.Mode == EpisodeMode.BossBattle || this._episode.Mode == EpisodeMode.Story)
        this._episode.Ended += (EventHandler) ((x, y) => this.State = LevelState.Win);
      this._drawer = new WorldDrawer();
      this._drawer.Init(this._episode);
      if (this.Mode == GameMode.Challenge)
        this.LoadNextEpisode();
      this._root = new BasicControl();
      this._landingLabel = new LandingLabel();
      this._landingLabel.Init();
      this._landingLabel.Position = new Vector2(106f, 140f);
      this._landingLabel.Visible = false;
      this._root.AddChild((BasicControl) this._landingLabel);
    }

    private void LoadNextEpisode()
    {
      this._nextEpisode = LevelRestorer.RestoreWorld(ResourcesManager.Instance.GetResource<LevelDesc>(this.GetChallengeEpisodeDescriptionPath()));
    }

    private void OnDistanceChanged(DistanceEventArgs e)
    {
      EventHandler<DistanceEventArgs> distanceChanged = this.DistanceChanged;
      if (distanceChanged == null)
        return;
      distanceChanged((object) this, e);
    }

    private void OnEnemyAnnihilation(EnemyAnnihilationEventArgs e)
    {
      EventHandler<EnemyAnnihilationEventArgs> enemyAnnihilation = this.EnemyAnnihilation;
      if (enemyAnnihilation == null)
        return;
      enemyAnnihilation((object) this, e);
    }

    public void OnEpisodeChanged(EventArgs e)
    {
      EventHandler<EventArgs> episodeChanged = this.EpisodeChanged;
      if (episodeChanged == null)
        return;
      episodeChanged((object) this, e);
    }

    private void OnGameWorldDistanceChanged(object sender, DistanceEventArgs e)
    {
      this.OnDistanceChanged(e);
    }

    private void OnGameWorldEnemyAnnihilation(object sender, EnemyAnnihilationEventArgs e)
    {
      this.OnEnemyAnnihilation(e);
      if (e.Unit.UnitType == UnitType.Egg || e.Unit.UnitType == UnitType.Droid || e.Unit.UnitType == UnitType.ArmedEgg)
        this.ScreenManager.AudioManager.GameplaySounds.PlayExplosionDroid();
      else
        this.ScreenManager.AudioManager.GameplaySounds.PlayExplosion();
    }

    private void OnGameWorldUnitFire(object sender, WeaponEventArgs e) => this.OnUnitFire(e);

    private void OnGameWorldUnitHitted(object sender, EventArgs e) => this.InvokeUnitHitted(e);

    private void OnLanding(object sender, EventArgs e)
    {
      this.SaveSessionSetting();
      this.OnNeedShowHangar(EventArgs.Empty);
    }

    public void OnNeedShowHangar(EventArgs e)
    {
      EventHandler landing = this.Landing;
      if (landing == null)
        return;
      landing((object) this, e);
    }

    public void OnUnitFire(WeaponEventArgs e)
    {
      EventHandler<WeaponEventArgs> unitFire = this.UnitFire;
      if (unitFire == null)
        return;
      unitFire((object) this, e);
    }

    public void InvokeUnitHitted(EventArgs e)
    {
      EventHandler<EventArgs> unitHitted = this.UnitHitted;
      if (unitHitted == null)
        return;
      unitHitted((object) this, e);
    }

    public void InvokeUnitAdded(EnemyAnnihilationEventArgs e)
    {
      EventHandler<EnemyAnnihilationEventArgs> unitAdded = this.UnitAdded;
      if (unitAdded == null)
        return;
      unitAdded((object) this, e);
    }

    private void AddEventHandlers(GameWorld gameWorld)
    {
      gameWorld.Landing += new EventHandler(this.OnLanding);
      gameWorld.DistanceChanged += new EventHandler<DistanceEventArgs>(this.OnGameWorldDistanceChanged);
      gameWorld.EnemyAnnihilation += new EventHandler<EnemyAnnihilationEventArgs>(this.OnGameWorldEnemyAnnihilation);
      gameWorld.UnitHitted += new EventHandler<PlayerEventArgs>(this.OnGameWorldUnitHitted);
      gameWorld.UnitFire += new EventHandler<WeaponEventArgs>(this.OnGameWorldUnitFire);
      gameWorld.UnitAdded += new EventHandler<EnemyAnnihilationEventArgs>(this.OnGameWorldUnitAdded);
    }

    private void RemoveEventHandlers(GameWorld gameWorld)
    {
      gameWorld.Landing -= new EventHandler(this.OnLanding);
      gameWorld.DistanceChanged -= new EventHandler<DistanceEventArgs>(this.OnGameWorldDistanceChanged);
      gameWorld.EnemyAnnihilation -= new EventHandler<EnemyAnnihilationEventArgs>(this.OnGameWorldEnemyAnnihilation);
      gameWorld.UnitHitted -= new EventHandler<PlayerEventArgs>(this.OnGameWorldUnitHitted);
      gameWorld.UnitFire -= new EventHandler<WeaponEventArgs>(this.OnGameWorldUnitFire);
      gameWorld.UnitAdded -= new EventHandler<EnemyAnnihilationEventArgs>(this.OnGameWorldUnitAdded);
    }

    private void OnGameWorldUnitAdded(object sender, EnemyAnnihilationEventArgs e)
    {
      this.InvokeUnitAdded(e);
    }

    public void FireStart(int weapon) => this._player.StartShootWeapon(weapon);

    public void FireStop(int weapon) => this._player.EndShootWeapon(weapon);

    private string GetChallengeEpisodeDescriptionPath()
    {
      int num = CommonRandom.Instance.Random.Next(1, 6);
      while (Level._lastChallengeLocation == num || Level._preLastChallengeLocation == num)
        num = CommonRandom.Instance.Random.Next(1, 6);
      Level._preLastChallengeLocation = Level._lastChallengeLocation;
      Level._lastChallengeLocation = num;
      return string.Format("GameWorld/Levels/{0}/{1}", (object) num.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) "Challenge");
    }

    public void InitChallengeLevel()
    {
      this.Mode = GameMode.Challenge;
      this.EpisodeDescriptionName = this.GetChallengeEpisodeDescriptionPath();
      this.LandingZoneStart = 0;
      this.LandingZoneEnd = 1600;
    }

    private void InitLevelObjects()
    {
    }

    public void InitStoryLevel(WorldType worldType, int episode)
    {
      this.Mode = GameMode.Story;
      string str = string.Empty;
      switch (worldType)
      {
        case WorldType.Canyon:
          str = "1";
          break;
        case WorldType.Jungle:
          str = "2";
          break;
        case WorldType.Ice:
          str = "3";
          break;
        case WorldType.Vulcan:
          str = "4";
          break;
        case WorldType.EnemyBase:
          str = "5";
          break;
      }
      this.EpisodeDescriptionName = string.Format("GameWorld/Levels/{0}/{1}", (object) str, (object) episode);
      if (worldType != WorldType.Canyon || episode != 3)
        return;
      this.EpisodeDescriptionName = "GameWorld/Levels/1/test";
    }

    private void LoseUpdate(GameTime gameTime)
    {
      float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
      this._timeFromDead += totalSeconds;
      if ((double) this._timeFromDead > 1.1000000238418579)
        return;
      if (this.Mode == GameMode.Story)
      {
        this._episode.IncreasedActiveArea.X = (int) MathHelper.Clamp((float) this._episode.IncreasedActiveArea.X + totalSeconds * this._player.Speed.X, 0.0f, (float) (this._episode.Length - this._episode.IncreasedActiveArea.Width));
        this._episode.ActiveArea.X = (int) MathHelper.Clamp((float) this._episode.ActiveArea.X + totalSeconds * this._player.Speed.X, 0.0f, (float) (this._episode.Length - this._episode.ActiveArea.Width));
      }
      else
      {
        this._episode.IncreasedActiveArea.X = (int) ((double) this._episode.IncreasedActiveArea.X + (double) totalSeconds * (double) this._player.Speed.X);
        this._episode.ActiveArea.X = (int) ((double) this._episode.ActiveArea.X + (double) totalSeconds * (double) this._player.Speed.X);
      }
      this._episode.UpdateActiveInstances();
      this._episode.UpdateInstances(totalSeconds);
      this._drawer.Update(totalSeconds);
    }

    private void MoveUnits(GameWorld lastWorld, GameWorld newWorld)
    {
      int num = lastWorld.ActiveArea.X - newWorld.ActiveArea.X;
      newWorld.Player = this._player;
      newWorld.AddInstance((Instance) this._player);
      newWorld.Player.SetPosition(this._player.Position.X - (float) num, this._player.Position.Y);
      foreach (Instance activeInstance in lastWorld.ActiveInstances)
      {
        if (activeInstance is Copter || activeInstance is Bullet)
        {
          activeInstance.SetPosition(activeInstance.Position.X - (float) num, activeInstance.Position.Y);
          newWorld.AddInstance(activeInstance);
        }
      }
    }

    public void Pause() => this.State = LevelState.Pause;

    public void Play() => this.State = LevelState.Play;

    public void ReleaseAll()
    {
        //RnD: Clean up level resources
        try
        {
            this._episode.ActiveInstances.ForEach(x => this._drawer.Remove(x));
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Level - ReleaseAll exception: " + ex.Message);
        }

        foreach (SpriteObject spriteObject in this._drawer.SpriteObjects.Values)
          spriteObject.Release();

        this._drawer.SpriteObjects.Clear();
        this._drawer.SpriteObjectsList.Clear();
        this.RemoveEventHandlers(this._episode);


        //RnD: Clean up instances
        try
        {
            this._episode.Instances.ForEach(x => x.Release());
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] Level - ReleaseAll exception: " + ex.Message);
        }

    }

    private void SaveSessionSetting()
    {
    }

    private void SelectNextEpisode()
    {
      this.RemoveEventHandlers(this._episode);
      GameWorld nextEpisode = this._nextEpisode;
      nextEpisode.Init();
      this.MoveUnits(this._episode, nextEpisode);
      this.AddEventHandlers(nextEpisode);
      this._drawer = new WorldDrawer();
      this._drawer.Init(nextEpisode);
      this._episode = nextEpisode;
      ++this.EpisodeNumber;
      this.OnEpisodeChanged(EventArgs.Empty);
      Task.Run(() => this.LoadNextEpisode());
      this._firstEpisodeInChallenge = false;
    }

    public void StartUpPlayer() => this._player.StartUp();

    public void StopUpPlayer() => this._player.StopUp();
  }
}
