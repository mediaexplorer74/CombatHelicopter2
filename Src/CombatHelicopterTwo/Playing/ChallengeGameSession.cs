// Decompiled with JetBrains decompiler
// Type: Helicopter.Playing.ChallengeGameSession
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using FlurryWP7SDK;
using FlurryWP7SDK.Models;
using Helicopter.Analytics;
using Helicopter.BaseScreens;
using Helicopter.GamePlay;
using Helicopter.GamePlay.GameplayPopups;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Screen;
using Helicopter.Utils.SoundManagers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.IsolatedStorage;

#nullable disable
namespace Helicopter.Playing
{
  public class ChallengeGameSession : GameSession
  {
    private float _coefficientOfHealth;
    private int _episodeNumber;
    private GameplayScreen _gameplayScreen;
    private Level _level;
    private long _lastFixedScores;

    public float Money { get; set; }

    public float EpisodeMoney { get; set; }

    public int EpisodeNumber => this._episodeNumber;

    internal ChallengeGameSession(Gamer gamer)
      : base(gamer)
    {
    }

    private void OnBuyNewCopter(object sender, EventArgs e)
    {
      this._coefficientOfHealth = 1f;
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      if (applicationSettings.Contains("PlayerHealth"))
        applicationSettings.Remove("PlayerHealth");
      applicationSettings.Save();
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
      this.EpisodeMoney += EncouragementManager.GetMoneyFromUnit(e.Unit) * this._level.Player.MoneyModifier;
      this.CountEnemyKill(e.Unit.UnitType);
    }

    private void OnHangarBack(object sender, EventArgs args)
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu);
    }

    private void OnHangarFight(object sender, EventArgs args) => this.StartGame();

    private void OnLanding(object sender, EventArgs e)
    {
      if ((double) this._level.Player.Position.X < 1600.0)
        this.NotifyCloseEpisode();
      else
        this.NotifyFinishEpisode(true);
      this._lastFixedScores = (long) this.Score;
      this.Money += this.EpisodeMoney;
      Gamer.Instance.Money.AddMoney(this.EpisodeMoney);
      this.EpisodeMoney = 0.0f;
      this.CorrectAmmunitionState();
      this.PauseSession();
      this.ShowHangar();
    }

    private void OnLeaderboard(object sender, EventArgs e)
    {
      this.FinishGame();
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.Leaderboard);
    }

    public void OnMenu(object sender, EventArgs e)
    {
      this.FinishGame();
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu);
    }

    private void OnPause(object sender, EventArgs e)
    {
      this.SaveChallengeProgress();
      PausePopup screen = new PausePopup();
      screen.Restart += new EventHandler(this.OnReplay);
      screen.MainMenu += new EventHandler(this.OnMenu);
      screen.Resume += (EventHandler) ((x, y) =>
      {
        screen.Close();
        this._gameplayScreen.Play();
      });
      this.ScreenManager.AddScreen((GameScreen) screen);
      screen.Init(false);
    }

    private void OnPlayerLose(object sender, EventArgs e)
    {
      BackgroundSounds.Instance.StopTheme();
      ChallengeGameOverPopup screen = new ChallengeGameOverPopup();
      screen.Replay += new EventHandler(this.OnReplay);
      screen.Back += new EventHandler(this.OnMenu);
      screen.Menu += new EventHandler(this.OnMenu);
      screen.Leaderboard += new EventHandler(this.OnLeaderboard);
      screen.InitScore((int) this.Score, (int) this.Money);
      this.ScreenManager.AddScreen((GameScreen) screen);
      this.NotifyFinishEpisode(false);
      this.EndSession();
    }

    private void TryGrantAchievement()
    {
      SmartPlayer player = this._gameplayScreen.Level.Player;
      if (player.Hits == player.BulletCount && player.BulletCount > 0)
        this.Gamer.AchievementManager.GrantAchievement("Sniper");
      if (player.DamagedCount == 0)
        this.Gamer.AchievementManager.GrantAchievement("Ace");
      if (player.BulletCount != 0)
        return;
      this.Gamer.AchievementManager.GrantAchievement("Scout");
    }

    private void OnReplay(object sender, EventArgs e) => this.Replay();

    private void CorrectAmmunitionState()
    {
      this.Gamer.DefenseBonus.Clear();
      this.Gamer.DamageBonus.Clear();
      if (this._level == null || !this.Gamer.HealthBonus.IsInstalled || this._level.Player.ExternalSupply == null)
        return;
      this.Gamer.HealthBonus.Item.Volume = this._level.Player.ExternalSupply.Volume;
    }

    public override void EndSession()
    {
      base.EndSession();
      this.FinishGame();
      Scoreboard.Instance.SendScore(this.Score, this.Gamer.Rank);
      this.NotifyFinishSession();
      this._episodeNumber = 0;
      this.Score = 0.0f;
      this._lastFixedScores = 0L;
      Gamer.Instance.Money.AddMoney(this.EpisodeMoney);
      this.EpisodeMoney = this.Money = 0.0f;
      this.SaveChallengeProgress();
    }

    private void FinishGame()
    {
      this.IsGameStarted = false;
      this.NotifyCloseEpisode();
      this.StatisticsData.FlyTime = this._gameplayScreen.FlyTime;
      this.SaveResults();
      this._gameplayScreen.Landing -= new EventHandler(this.OnLanding);
      this._gameplayScreen.PlayerLose -= new EventHandler(this.OnPlayerLose);
      this._level.DistanceChanged -= new EventHandler<DistanceEventArgs>(this.OnDistanceChanged);
      this._level.EnemyAnnihilation -= new EventHandler<EnemyAnnihilationEventArgs>(this.OnEnemyAnnihilation);
      this._level.EpisodeChanged -= new EventHandler<EventArgs>(this.OnEpisodeChanged);
    }

    private void PauseSession()
    {
      this._coefficientOfHealth = this._level.Player.Energy / this.Gamer.CurrentHelicopter.Item.Pattern.Energy;
      this._episodeNumber = (double) this._level.Player.Position.X <= 1600.0 ? this._level.EpisodeNumber : this._level.EpisodeNumber + 1;
      this.SaveChallengeProgress();
      this.FinishGame();
      Scoreboard.Instance.SendScore(this.Score, this.Gamer.Rank);
    }

    private void Replay()
    {
      this.EndSession();
      this.StartSession();
    }

    public void ResumeSession()
    {
      Instance.ShowCredits = true;
      this.Load();
      this.StartGame();
    }

    private void ShowHangar()
    {
      this.ScreenManager.ExitAllScreens();
      HangerScreen screen = new HangerScreen();
      screen.Back += new EventHandler(this.OnHangarBack);
      screen.Fight += new EventHandler(this.OnHangarFight);
      screen.BuyNewCopter += new EventHandler(this.OnBuyNewCopter);
      if ((double) this._coefficientOfHealth < 1.0)
      {
        screen.HealthPercent = this._coefficientOfHealth;
        IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
        applicationSettings["PlayerHealth"] = (object) this._coefficientOfHealth.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        applicationSettings.Save();
      }
      this.ScreenManager.AddScreen((GameScreen) screen);
      screen.AddHelpButton();
    }

    private void StartGame()
    {
      this._level = LevelsFactory.GetChallengeLevelBlank();
      this._level.StartPlayerEnergy = this._coefficientOfHealth * this.Gamer.CurrentHelicopter.Item.Pattern.Energy;
      this._level.EpisodeNumber = this._episodeNumber;
      this._gameplayScreen = new GameplayScreen()
      {
        Level = this._level
      };
      this._gameplayScreen.Landing += new EventHandler(this.OnLanding);
      this._gameplayScreen.PlayerLose += new EventHandler(this.OnPlayerLose);
      this._gameplayScreen.Pause += new EventHandler(this.OnPause);
      this._gameplayScreen.Score = (int) this.Score;
      this._level.DistanceChanged += new EventHandler<DistanceEventArgs>(this.OnDistanceChanged);
      this._level.EnemyAnnihilation += new EventHandler<EnemyAnnihilationEventArgs>(this.OnEnemyAnnihilation);
      this._level.EpisodeChanged += new EventHandler<EventArgs>(this.OnEpisodeChanged);
      this.NotifyStartEpisode();
      LoadingScreen screen = new LoadingScreen();
      GameScreen[] screens = this.ScreenManager.GetScreens();
      GameScreen from = (GameScreen) null;
      if (screens.Length > 0)
        from = screens[screens.Length - 1];
      screen.Init(this.ScreenManager, from, (GameScreen) this._gameplayScreen);
      this.ScreenManager.AddScreen((GameScreen) screen);
      this.IsGameStarted = true;
    }

    private void OnEpisodeChanged(object sender, EventArgs e)
    {
      this.NotifyFinishEpisode(true);
      this._episodeNumber = this._level.EpisodeNumber;
      this._lastFixedScores = (long) this.Score;
      this.SaveChallengeProgress();
      this.Gamer.AchievementManager.GrantAchievement("Mercenary");
      this.CorrectAmmunitionState();
      this.TryGrantAchievement();
      Scoreboard.Instance.SendScore(this.Score, this.Gamer.Rank);
      this.NotifyStartEpisode();
    }

    public override void StartSession()
    {
      base.StartSession();
      this._lastFixedScores = 0L;
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      this._coefficientOfHealth = !applicationSettings.Contains("PlayerHealth") ? 1f : float.Parse(applicationSettings["PlayerHealth"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      this.EpisodeMoney = 0.0f;
      this.ShowHangar();
      this.Load();
      this.NotifyStartSession();
      Instance.ShowCredits = true;
    }

    public void StartSessionWithoutHangar()
    {
      base.StartSession();
      this._lastFixedScores = 0L;
      this._coefficientOfHealth = 1f;
      this.EpisodeMoney = 0.0f;
      this.Load();
      this.NotifyStartSession();
      Instance.ShowCredits = true;
      this.StartGame();
    }

    public void SaveChallengeProgress()
    {
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      applicationSettings[SerializationIDs.ChallengeEpisodeNumber] = (object) this._episodeNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      applicationSettings[SerializationIDs.ChallengeLastScores] = (object) this._lastFixedScores.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      applicationSettings["Money"] = (object) this.Money.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      applicationSettings.Save();
    }

    public void Load()
    {
      try
      {
        IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
        if (applicationSettings.Contains(SerializationIDs.ChallengeEpisodeNumber))
          this._episodeNumber = int.Parse((string) applicationSettings[SerializationIDs.ChallengeEpisodeNumber], (IFormatProvider) CultureInfo.InvariantCulture);
        if (applicationSettings.Contains(SerializationIDs.ChallengeLastScores))
        {
          this._lastFixedScores = long.Parse((string) applicationSettings[SerializationIDs.ChallengeLastScores], (IFormatProvider) CultureInfo.InvariantCulture);
          this.Score = (float) this._lastFixedScores;
        }
        if (!applicationSettings.Contains("Money"))
          return;
        this.Money = float.Parse((string) applicationSettings["Money"], (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (Exception ex)
      {
        this._episodeNumber = 0;
        this.Score = 0.0f;
        this._lastFixedScores = 0L;
        this.Money = this.EpisodeMoney = 0.0f;
        this.SaveChallengeProgress();
      }
    }

    public override void Pause()
    {
      if (!this.IsGameStarted || this._gameplayScreen == null)
        return;
      this._gameplayScreen.PauseAtActivate = this._gameplayScreen.Level.State == LevelState.Play;
    }

    private void NotifyStartSession() => Api.LogEvent(FlurryEvents.ChallengeSession, true);

    private void NotifyFinishSession() => Api.EndTimedEvent(FlurryEvents.ChallengeSession);

    private List<Parameter> GetFlurryParams()
    {
      List<Parameter> flurryParams = new List<Parameter>();
      flurryParams.Add(ParametersFactory.GetChallengeEpisodeParam(this._episodeNumber));
      flurryParams.AddRange((IEnumerable<Parameter>) ParametersFactory.GetGamerParams(Gamer.Instance));
      float energy = this._level.Player != null ? this._level.Player.Energy : Gamer.Instance.Energy;
      flurryParams.Add(ParametersFactory.GetEnergyParam(energy));
      return flurryParams;
    }

    private void NotifyStartEpisode()
    {
      List<Parameter> flurryParams = this.GetFlurryParams();
      Api.LogEvent(FlurryEvents.ChallengeEpisode, flurryParams, true);
    }

    private void NotifyCloseEpisode() => Api.EndTimedEvent(FlurryEvents.ChallengeEpisode);

    private void NotifyFinishEpisode(bool win)
    {
      Api.EndTimedEvent(FlurryEvents.ChallengeEpisode);
      List<Parameter> flurryParams = this.GetFlurryParams();
      Api.LogEvent(win ? FlurryEvents.ChallengeEpisodeCompleated : FlurryEvents.ChallengeEpisodeLose, flurryParams, true);
    }
  }
}
