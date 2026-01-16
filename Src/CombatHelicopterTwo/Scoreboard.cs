// Decompiled with JetBrains decompiler
// Type: Helicopter.Scoreboard
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;
using NotificationScheduledAgent;
using Scoreloop.CoreSocial.API;
using Scoreloop.CoreSocial.API.Model;
using System;
using System.Globalization;
using System.IO.IsolatedStorage;

#nullable disable
namespace Helicopter
{
  internal class Scoreboard
  {
    public const string HighScoresKey = "HeightScores";
    private const int ChallengeScoreMode = 0;
    private static Scoreboard _instance;
    private readonly IRankingController _rankingController;
    private readonly IScoreController _scoreController;
    private readonly ScoreloopClient _scoreloopClient;
    private readonly IScoresController _scoresContoller;
    private readonly IUserController _userController;
    private readonly IUsersController _usersController;

    public static Scoreboard Instance
    {
      get => Scoreboard._instance ?? (Scoreboard._instance = new Scoreboard());
    }

    public IScoreController ScoreController => this._scoreController;

    public IScoresController ScoresContoller => this._scoresContoller;

    public IUserController UserController => this._userController;

    public IUsersController UsersController => this._usersController;

    public Game Game => this._scoreloopClient.Game;

    public IRankingController RankingController => this._rankingController;

    public Scoreboard()
    {
      this._scoreloopClient = new ScoreloopClient(new Version(1, 2), "a38a547a-8179-44a4-9795-638c5ca56a65", "7PpF9HKVMNUZm4365YrDokXA17H36WpbgxlxMWc3G+OA/n9nmI4I7w==", "CTU");
      this._scoreController = this._scoreloopClient.CreateScoreController();
      this._scoreController.Submit(this._scoreController.CreateScore(0.0, 0.0, 0));
      this._scoreController.ScoreSubmitted += new EventHandler<RequestControllerEventArgs<IScoreController>>(this.OnScoreSubmitted);
      this._scoresContoller = this._scoreloopClient.CreateScoresController();
      this._scoresContoller.ScoresLoaded += new EventHandler<RequestControllerEventArgs<IScoresController>>(this._scoresContoller_ScoresLoaded);
      this._userController = this._scoreloopClient.CreateUserController();
      this._userController.Load(this._userController.User, UserLoadScope.Detailed);
      this._usersController = this._scoreloopClient.CreateUsersController();
      this._rankingController = this._scoreloopClient.CreateRankingController();
      this._rankingController.RankingLoaded += new EventHandler<RequestControllerEventArgs<IRankingController>>(this.OnRankLoaded);
    }

    private void OnRankLoaded(object sender, RequestControllerEventArgs<IRankingController> e)
    {
      switch (e.Controller.Rank)
      {
        case 1:
          Gamer.Instance.AchievementManager.GrantAchievement("DistinguishedFlyingCross");
          break;
        case 2:
          Gamer.Instance.AchievementManager.GrantAchievement("DistinguishedService");
          break;
        case 3:
          Gamer.Instance.AchievementManager.GrantAchievement("AirForceAchievementMedal");
          break;
      }
    }

    private void OnScoreSubmitted(object sender, RequestControllerEventArgs<IScoreController> e)
    {
      ScoreSearchList scoreSearchList = this._scoresContoller.CreateScoreSearchList(ScoreSearchListTimeScope.Global);
      this._scoresContoller.LoadScores(scoreSearchList, this._scoresContoller.User, 75);
      this._rankingController.LoadRanking(scoreSearchList, 0);
    }

    public int GetHighScores()
    {
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      if (!applicationSettings.Contains("HeightScores"))
        return 0;
      int result;
      if (!int.TryParse(applicationSettings["HeightScores"].ToString(), out result))
        result = 0;
      return result;
    }

    public void SendScore(float points, Rank rank)
    {
      this.SetHighScores((int) points);
      this._scoreController.Submit(this._scoreController.CreateScore(Math.Ceiling((double) points), (double) rank, 0));
    }

    public void SetHighScores(int scores)
    {
      if (this.GetHighScores() >= scores)
        return;
      IsolatedStorageSettings.ApplicationSettings["HeightScores"] = (object) scores.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    private void _scoresContoller_ScoresLoaded(
      object sender,
      RequestControllerEventArgs<IScoresController> e)
    {
      foreach (Score score in e.Controller.Scores)
      {
        if (score.User.ID == e.Controller.User.ID)
        {
          ulong rank = score.Rank;
          NotificationSettings notificationSettings = NotificationSettings.Load();
          if (rank >= notificationSettings.LastRankInLeaderboard)
            break;
          notificationSettings.LastRankInLeaderboard = rank;
          notificationSettings.Save();
          break;
        }
      }
    }
  }
}
