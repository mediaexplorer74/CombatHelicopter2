// Modified by MediaExplorer (2026)
// Type: Helicopter.Playing.StoryGameSession
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Analytics;
using Helicopter.BaseScreens;
using Helicopter.GamePlay;
using Helicopter.GamePlay.GameplayPopups;
using Helicopter.Items;
using Helicopter.Items.AchievementsSystem;
using Helicopter.Items.DeviceItems;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Screen;
using Helicopter.Utils.SoundManagers;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Playing
{
  public class StoryGameSession : GameSession
  {
    private int _episode = -1;
    private GameplayScreen _gameplayScreen;
    private Level _level;
    private WorldType _location;

    internal StoryGameSession(Gamer gamer)
      : base(gamer)
    {
    }

    private void OnChallengeFight(object sender, EventArgs e)
    {
      this.EndSession();
      this.ScreenManager.ExitAllScreens();
      GameProcess.Instance.ChallengeGameSession.ResumeSession();
    }

    private void OnDistanceChanged(object sender, DistanceEventArgs e)
    {
      this.Score += EncouragementManager.GetScoresFromDistance(e.Distance, 0);
      this._gameplayScreen.SetScoreCount((int) this.Score);
    }

    private void OnEnemyAnnihilation(object sender, EnemyAnnihilationEventArgs e)
    {
      this.Score += EncouragementManager.GetScoresFromUnit(e.Unit);
      this.ScoreForUnits += EncouragementManager.GetScoresFromUnit(e.Unit);
      this.CountEnemyKill(e.Unit.UnitType);
    }

    private void OnHangarBack(object sender, EventArgs args)
    {
      if (this._gameplayScreen != null)
        this.EndSession();
      this.ShowMap();
    }

    private void OnHangarFight(object sender, EventArgs args) => this.StartGame();

    private void OnMap(object sender, EventArgs e)
    {
      this.EndSession();
      this.ShowMap();
    }

    private void OnNext(object sender, EventArgs e)
    {
      this.EndSession();
      int episode = this._episode + 1;
      WorldType location;
      if (episode <= 2)
      {
        location = this._location;
      }
      else
      {
        episode = 1;
        switch (this._location)
        {
          case WorldType.Canyon:
            location = WorldType.Jungle;
            break;
          case WorldType.Jungle:
            location = WorldType.Ice;
            break;
          case WorldType.Ice:
            location = WorldType.Vulcan;
            break;
          case WorldType.Vulcan:
            location = WorldType.EnemyBase;
            break;
          case WorldType.EnemyBase:
            location = WorldType.EnemyBase;
            break;
          default:
            throw new ArgumentOutOfRangeException(string.Format("Location {0} has not next location", (object) this._location));
        }
      }
      if (this._location == WorldType.EnemyBase && this._episode == 2)
        episode = 3;
      this.StartSession(location, episode);
    }

    private void OnPause(object sender, EventArgs e)
    {
      PausePopup screen = new PausePopup();
      screen.Restart += new EventHandler(this.OnReplay);
      screen.MainMenu += new EventHandler(this.OnMap);
      screen.Resume += (EventHandler) ((x, y) =>
      {
        screen.Close();
        this._gameplayScreen.Play();
      });
      this.ScreenManager.AddScreen((GameScreen) screen);
      screen.Init(true);
    }

    private void OnPlayerLose(object sender, EventArgs e)
    {
      BackgroundSounds.Instance.StopTheme();
      this.NotifyFinishEpisode(false, 0);
      StoryLose screen = new StoryLose();
      screen.Map += new EventHandler(this.OnMap);
      screen.Replay += new EventHandler(this.OnReplay);
      screen.Back += new EventHandler(this.OnMap);
      this.ScreenManager.AddScreen((GameScreen) screen);
    }

    private void OnPlayerWin(object sender, EventArgs e)
    {
      BackgroundSounds.Instance.StopTheme();
      int stars = this.StarsForLevel();
      this.NotifyFinishEpisode(true, stars);
      if (this._location == WorldType.EnemyBase && this._episode == 3)
      {
        StoryEpicWin screen = new StoryEpicWin(stars);
        screen.Back += new EventHandler(this.OnPopupEpicWinBack);
        screen.ChallengeFight += new EventHandler(this.OnChallengeFight);
        screen.Menu += new EventHandler(this.OnPopupEpicWinBack);
        this.ScreenManager.AddScreen((GameScreen) screen);
      }
      else
      {
        StoryWin screen = new StoryWin((int) this._location, this._episode, stars);
        screen.Back += new EventHandler(this.OnPopupBack);
        screen.Replay += new EventHandler(this.OnReplay);
        screen.Map += new EventHandler(this.OnMap);
        screen.Next += new EventHandler(this.OnNext);
        Gamer.Instance.NewRank += new EventHandler(screen.OnNewRank);
        this.ScreenManager.AddScreen((GameScreen) screen);
      }
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(this._location, this._episode, stars);
      this.ShowNewCopters();
      this.ShowNewUpgrade();
      this.ShowNewWeapon();
      this.ShowNewAchievement();
      if (this._location != WorldType.Jungle || this._episode != 2 || SettingsGame.NeedAfter22Tutorial)
        return;
      SettingsGame.NeedAfter22Tutorial = true;
    }

    private void OnPopupEpicWinBack(object x, EventArgs y)
    {
      this.EndSession();
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu);
    }

    private int StarsForLevel()
    {
      if (this._location == WorldType.EnemyBase && this._episode == 3)
        return 0;
      float num = this.ScoreForUnits / this._level.TotalScoreForUnits;
      if ((double) num < 0.3)
        return 1;
      return (double) num >= 0.7 ? 3 : 2;
    }

    private void ShowNewAchievement()
    {
      if (this._episode == 2)
        this.GrantStroyAchievement(this._location);
      while (this.Gamer.AchievementManager.UnshownAchievement.Count > 0)
      {
        Achievement achievement = this.Gamer.AchievementManager.UnshownAchievement.First<Achievement>();
        NewAchievementPopup screen = new NewAchievementPopup();
        screen.Init(achievement);
        this.ScreenManager.AddScreen((GameScreen) screen);
        this.Gamer.AchievementManager.UnshownAchievement.RemoveAt(0);
      }
    }

    private void GrantStroyAchievement(WorldType location)
    {
      switch (location)
      {
        case WorldType.Canyon:
          this.Gamer.AchievementManager.GrantAchievement("DesertCoyote");
          break;
        case WorldType.Jungle:
          this.Gamer.AchievementManager.GrantAchievement("TropicThunder");
          break;
        case WorldType.Ice:
          this.Gamer.AchievementManager.GrantAchievement("IceFury");
          break;
        case WorldType.Vulcan:
          this.Gamer.AchievementManager.GrantAchievement("FireStorm");
          break;
        case WorldType.EnemyBase:
          this.Gamer.AchievementManager.GrantAchievement("Liberator");
          break;
        case WorldType.LandingZone:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (location));
      }
    }

    private void OnPopupBack(object sender, EventArgs e)
    {
      this.EndSession();
      this.ShowMap();
    }

    private void OnReplay(object sender, EventArgs e) => this.Replay();

    private void CorrectAmmunitionState()
    {
      this.Gamer.DefenseBonus.Clear();
      this.Gamer.DamageBonus.Clear();
      if (!this.Gamer.HealthBonus.IsInstalled || this._level.Player.ExternalSupply == null)
        return;
      this.Gamer.HealthBonus.Item.Volume = this._level.Player.ExternalSupply.Volume;
    }

    public override void EndSession()
    {
      base.EndSession();
      this.StatisticsData.FlyTime = this._gameplayScreen.FlyTime;
      this.FinishGame();
      this.NotifyFinishSession();
    }

    private void FinishGame()
    {
      this.IsGameStarted = false;
      this.NotifyCloseEpisode();
      this.CorrectAmmunitionState();
      this.SaveResults();
      if (this._level == null)
        return;
      this._level.DistanceChanged -= new EventHandler<DistanceEventArgs>(this.OnDistanceChanged);
      this._level.EnemyAnnihilation -= new EventHandler<EnemyAnnihilationEventArgs>(this.OnEnemyAnnihilation);
      this._level.ReleaseAll();
    }

    private void Replay()
    {
      this.EndSession();
      this.StartSession(this._location, this._episode);
    }

    private void ShowHangar()
    {
      HangerScreen screen = new HangerScreen();
      screen.Fight += new EventHandler(this.OnHangarFight);
      screen.Back += new EventHandler(this.OnHangarBack);
      this.ScreenManager.AddScreen((GameScreen) screen);
    }

    private void ShowMap() => GameProcess.Instance.Navigator.ShowScreen(ScreenType.Map);

    private void ShowNewCopters()
    {
      HelicopterItem helicopterItem = (HelicopterItem) null;
      if (this._location == WorldType.Canyon && this._episode == 2)
        helicopterItem = ItemCollection.Instance.GetHelicopter(HelicopterType.Harbinger);
      else if (this._location == WorldType.Ice && this._episode == 2)
        helicopterItem = ItemCollection.Instance.GetHelicopter(HelicopterType.Avenger);
      if (helicopterItem == null || helicopterItem.IsBought)
        return;
      helicopterItem.IsBought = true;
      this.ScreenManager.AddScreen((GameScreen) new NewCopterPopup(helicopterItem));
    }

    private void ShowNewUpgrade()
    {
      UpgradeType? nullable = new UpgradeType?();
      switch (this._location)
      {
        case WorldType.Canyon:
          if (this._episode == 2)
          {
            nullable = new UpgradeType?(UpgradeType.DamageControlSystemV1);
            break;
          }
          break;
        case WorldType.Jungle:
          switch (this._episode)
          {
            case 1:
              nullable = new UpgradeType?(UpgradeType.EnergyRegenerationSystemV1);
              break;
            case 2:
              nullable = new UpgradeType?(UpgradeType.BulletControlSystem);
              break;
          }
          break;
        case WorldType.Ice:
          switch (this._episode)
          {
            case 1:
              nullable = new UpgradeType?(UpgradeType.SystemCompensationCrush);
              break;
            case 2:
              nullable = new UpgradeType?(UpgradeType.DamageControlSystemV2);
              break;
          }
          break;
        case WorldType.Vulcan:
          switch (this._episode)
          {
            case 1:
              nullable = new UpgradeType?(UpgradeType.EnergyRegenerationSystemV2);
              break;
            case 2:
              nullable = new UpgradeType?(UpgradeType.PDUSystemV1);
              break;
          }
          break;
        case WorldType.EnemyBase:
          switch (this._episode)
          {
            case 1:
              nullable = new UpgradeType?(UpgradeType.UpgradedWarhead);
              break;
          }
          break;
      }
      if (!nullable.HasValue)
        return;
      UpgradeItem upgrade = ItemCollection.Instance.GetUpgrade(nullable.Value);
      if (upgrade.IsBought)
        return;
      upgrade.IsBought = true;
      NewAmunitionPopup screen = new NewAmunitionPopup();
      screen.InitNewUpgrade(upgrade);
      this.ScreenManager.AddScreen((GameScreen) screen);
    }

    private void ShowNewWeapon()
    {
      WeaponType? nullable = new WeaponType?();
      switch (this._location)
      {
        case WorldType.Canyon:
          switch (this._episode)
          {
            case 1:
              nullable = new WeaponType?(WeaponType.DualMachineGun);
              break;
            case 2:
              nullable = new WeaponType?(WeaponType.RocketLauncher);
              break;
          }
          break;
        case WorldType.Ice:
          if (this._episode == 1)
          {
            nullable = new WeaponType?(WeaponType.CasseteRocket);
            break;
          }
          break;
        case WorldType.Vulcan:
          switch (this._episode)
          {
            case 1:
              nullable = new WeaponType?(WeaponType.PlasmaGun);
              break;
            case 2:
              nullable = new WeaponType?(WeaponType.DualRocketLauncher);
              break;
          }
          break;
        case WorldType.EnemyBase:
          if (this._episode == 2)
          {
            nullable = new WeaponType?(WeaponType.Shield);
            break;
          }
          break;
      }
      if (!nullable.HasValue)
        return;
      WeaponItem weapon = ItemCollection.Instance.GetWeapon(nullable.Value);
      if (weapon.IsBought)
        return;
      weapon.IsBought = true;
      NewAmunitionPopup screen = new NewAmunitionPopup();
      screen.InitNewWeapon(weapon);
      this.ScreenManager.AddScreen((GameScreen) screen);
    }

    private void StartGame()
    {
      this._level = new Level();
      this._level.InitStoryLevel(this._location, this._episode);
      bool flag = this._location == WorldType.EnemyBase && this._episode == 3;
      this._gameplayScreen = new GameplayScreen()
      {
        Level = this._level,
        IsBossBattle = flag
      };
      this._gameplayScreen.PlayerWin += new EventHandler(this.OnPlayerWin);
      this._gameplayScreen.PlayerLose += new EventHandler(this.OnPlayerLose);
      this._gameplayScreen.Pause += new EventHandler(this.OnPause);
      this._level.DistanceChanged += new EventHandler<DistanceEventArgs>(this.OnDistanceChanged);
      this._level.EnemyAnnihilation += new EventHandler<EnemyAnnihilationEventArgs>(this.OnEnemyAnnihilation);
      this._gameplayScreen.Score = (int) this.Score;
      LoadingScreen screen = new LoadingScreen();
      screen.Init(this.ScreenManager, ((IEnumerable<GameScreen>) this.ScreenManager.GetScreens()).Last<GameScreen>(), (GameScreen) this._gameplayScreen);
      this.ScreenManager.AddScreen((GameScreen) screen);
      this.NotifyStartEpisode();
      Instance.ShowCredits = false;
      this.IsGameStarted = true;
    }

    protected new void StartSession() => base.StartSession();

    public void StartSession(WorldType location, int episode)
    {
      base.StartSession();
      this._location = location;
      this._episode = episode;
      this.NotifyStartSession();
      this.ShowHangar();
    }

    public void StartSessionWithoutHangar(WorldType location, int episode)
    {
      base.StartSession();
      this._location = location;
      this._episode = episode;
      this.NotifyStartSession();
      this.StartGame();
    }

    public override void Pause()
    {
      if (!this.IsGameStarted || this._gameplayScreen == null)
        return;
      this._gameplayScreen.PauseAtActivate = this._gameplayScreen.Level.State == LevelState.Play;
    }

    private void NotifyStartSession()
    {
      Api.LogEvent(FlurryEvents.StorySession, new List<Parameter>()
      {
        ParametersFactory.GetLocationParam(this._location),
        ParametersFactory.GetStoryEpisodeParam(this._location, this._episode)
      }, true);
    }

    private void NotifyFinishSession() => Api.EndTimedEvent(FlurryEvents.StorySession);

    private List<Parameter> GetFlurryParams()
    {
      List<Parameter> flurryParams = new List<Parameter>();
      flurryParams.Add(ParametersFactory.GetLocationParam(this._location));
      flurryParams.Add(ParametersFactory.GetStoryEpisodeParam(this._location, this._episode));
      EpisodeHistory episodeHistory = GameProcess.Instance.StoryModeHistory.GetEpisodeHistory(this._location, this._episode);
      flurryParams.Add(ParametersFactory.GetReplayParam(episodeHistory.Stars > 0));
      flurryParams.AddRange((IEnumerable<Parameter>) ParametersFactory.GetGamerParams(Gamer.Instance));
      flurryParams.Add(ParametersFactory.GetEnergyParam(Gamer.Instance.Energy));
      return flurryParams;
    }

    private void NotifyStartEpisode()
    {
      List<Parameter> flurryParams = this.GetFlurryParams();
      Api.LogEvent(FlurryEvents.StoryEpisode, flurryParams, true);
    }

    private void NotifyCloseEpisode() => Api.EndTimedEvent(FlurryEvents.StoryEpisode);

    private void NotifyFinishEpisode(bool win, int stars)
    {
      Api.EndTimedEvent(FlurryEvents.StoryEpisode);
      List<Parameter> flurryParams = this.GetFlurryParams();
      if (win)
      {
        flurryParams.Add(ParametersFactory.GetStarsParam(stars));
        Api.LogEvent(FlurryEvents.StoryEpisodeWin, flurryParams, true);
      }
      else
        Api.LogEvent(FlurryEvents.StoryEpisodeLose, flurryParams, true);
    }
  }
}
