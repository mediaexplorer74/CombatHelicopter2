// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MainMenu.MainMenuScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Items.DeviceItems;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Playing;
using Helicopter.Screen.MapScreen.Tutorial_Popups;
using Helicopter.Screen.Popups;
using Helicopter.Utils;
using Windows.System;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Helicopter.Screen.MainMenu
{
  internal class MainMenuScreen : GameScreen
  {
    private readonly MoreGamePopup moreGamePopup = new MoreGamePopup();
    private BasicControl _root;
    private float _timeFromStart;

    public override void Draw(DrawContext drawContext) => this._root.Draw(drawContext);

    public override void HandleInput(InputState input) => this._root.HandleInput(input);

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this._root.Update(gameTime);
    }

    public override void LoadContent()
    {
      this._root = (BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/bgMainMenu"), Vector2.Zero);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/butCampaign");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/butCampaignPressed");
      MenuControl child1 = new MenuControl(sprite1, sprite2, new Vector2((float) (400 - sprite1.Bounds.Width / 2), 290f));
      child1.Clicked += new EventHandler<EventArgs>(this.OnStoryButtonClick);
      this._root.AddChild((BasicControl) child1);
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("MainMenu/butSurvival");
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("MainMenu/butSurvivalPressed");
      MenuControl child2 = new MenuControl(sprite3, sprite4, new Vector2((float) (400 - sprite4.Bounds.Width / 2), 380f));
      child2.Clicked += new EventHandler<EventArgs>(this.OnChallengeButtonClick);
      this._root.AddChild((BasicControl) child2);
      int x = 25;
      int num = 25;
      Sprite sprite5 = ResourcesManager.Instance.GetSprite("MainMenu/butAchievement");
      Sprite sprite6 = ResourcesManager.Instance.GetSprite("MainMenu/butAchievementSelect");
      MenuControl child3 = new MenuControl(sprite5, sprite6, new Vector2((float) x, (float) (480 - sprite6.Bounds.Height - num)));
      child3.Clicked += new EventHandler<EventArgs>(this.OnAchievementButtonClick);
      this._root.AddChild((BasicControl) child3);
      Sprite sprite7 = ResourcesManager.Instance.GetSprite("MainMenu/butLeaderboard");
      Sprite sprite8 = ResourcesManager.Instance.GetSprite("MainMenu/butLeaderboardSelect");
      MenuControl child4 = new MenuControl(sprite7, sprite8, new Vector2((float) (800 - sprite7.Bounds.Width - x), (float) (480 - sprite7.Bounds.Height - num)));
      child4.Clicked += new EventHandler<EventArgs>(this.OnScoreboardButtonClick);
      this._root.AddChild((BasicControl) child4);
      MenuControl menuControl1 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/facebook"), ResourcesManager.Instance.GetSprite("MainMenu/facebookSelect"), new Vector2(665f, 0.0f));
      menuControl1.Clicked += new EventHandler<EventArgs>(MainMenuScreen.OnFacebookButtonClick);
      this.moreGamePopup.AddControl((BasicControl) menuControl1);
      this._root.AddChild((BasicControl) menuControl1);
      MenuControl menuControl2 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/twitter"), ResourcesManager.Instance.GetSprite("MainMenu/twitterSelect"), new Vector2(735f, 0.0f));
      menuControl2.Clicked += new EventHandler<EventArgs>(MainMenuScreen.OnFacebookButtonClick);
      this.moreGamePopup.AddControl((BasicControl) menuControl2);
      this._root.AddChild((BasicControl) menuControl2);
      MenuControl stateOff = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/soundOn"), ResourcesManager.Instance.GetSprite("MainMenu/soundOnSelect"), Vector2.Zero);
      RadioButton radioButton = new RadioButton(new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/soundOff"), ResourcesManager.Instance.GetSprite("MainMenu/soundOffSelect"), Vector2.Zero), stateOff, SettingsGame.Sound);
      radioButton.Position = new Vector2(15f, 0.0f);
      radioButton.StateChanged += new EventHandler<BooleanEventArgs>(this.ScreenManager.AudioManager.SoundStateChanged);
      this.moreGamePopup.AddControl((BasicControl) radioButton);
      this._root.AddChild((BasicControl) radioButton);
      MenuControl child5 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/Help"), ResourcesManager.Instance.GetSprite("MainMenu/HelpSelect"), new Vector2((float) (15.0 + (double) radioButton.Size.X + 15.0), 0.0f));
      child5.Clicked += new EventHandler<EventArgs>(this.OnHelpClicked);
      this._root.AddChild((BasicControl) child5);
      MenuControl child6 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/butMoreGamesA"), ResourcesManager.Instance.GetSprite("MainMenu/butMoreGamesB"), new Vector2(0.0f, 450f));
      child6.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/coins"), new Vector2(484f, -17f)));
      child6.Clicked += new EventHandler<EventArgs>(this.OnMoreGamesClicked);
      this._root.AddChild((BasicControl) child6);
      this.ScreenManager.AudioManager.BackgroundSounds.PlayMenuTheme();
      this._timeFromStart = 0.0f;
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = false
      });
    }

    private void OnMagicButtonClick(object sender, EventArgs e)
    {
      Gamer.Instance.CurrentHelicopter.Item = ItemCollection.Instance.GetHelicopter(HelicopterType.Avenger);
      Gamer.Instance.FirstWeapon.Item = ItemCollection.Instance.GetWeapon(WeaponType.PlasmaGun);
      Gamer.Instance.SecondWeapon.Item = ItemCollection.Instance.GetWeapon(WeaponType.CasseteRocket);
      Gamer.Instance.UpgradeA.Item = ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV2);
      Gamer.Instance.UpgradeB.Item = (UpgradeItem) null;
      Gamer.Instance.Money.SetDebugMoney(1000000f);
      ItemCollection.Instance.GetHelicopter(HelicopterType.Viper).IsBought = true;
      ItemCollection.Instance.GetHelicopter(HelicopterType.Harbinger).IsBought = true;
      ItemCollection.Instance.GetHelicopter(HelicopterType.Avenger).IsBought = true;
      ItemCollection.Instance.GetHelicopter(HelicopterType.GrimReaper).IsBought = false;
      ItemCollection.Instance.GetWeapon(WeaponType.SingleMachineGun).IsBought = true;
      ItemCollection.Instance.GetWeapon(WeaponType.DualMachineGun).IsBought = true;
      ItemCollection.Instance.GetWeapon(WeaponType.Vulcan).IsBought = false;
      ItemCollection.Instance.GetWeapon(WeaponType.PlasmaGun).IsBought = true;
      ItemCollection.Instance.GetWeapon(WeaponType.RocketLauncher).IsBought = true;
      ItemCollection.Instance.GetWeapon(WeaponType.DualRocketLauncher).IsBought = false;
      ItemCollection.Instance.GetWeapon(WeaponType.HomingRocket).IsBought = false;
      ItemCollection.Instance.GetWeapon(WeaponType.CasseteRocket).IsBought = true;
      ItemCollection.Instance.GetWeapon(WeaponType.Shield).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV1).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV2).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV3).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.SystemCompensationCrush).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.BulletControlSystem).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV1).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV2).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV3).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV4).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.TargetAssistentSystem).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.EnhanchedRechargeSystem).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.IncreasedCapacitySystem).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.HotPlasmaModule).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.CriticalDamageSystemV1).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.CriticalDamageSystemV2).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.PDUSystemV1).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.PDUSystemV2).IsBought = false;
      ItemCollection.Instance.GetUpgrade(UpgradeType.UpgradedWarhead).IsBought = true;
      ItemCollection.Instance.GetUpgrade(UpgradeType.HarvestingSystem).IsBought = false;
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Canyon, 1, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Canyon, 2, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Canyon, 3, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Jungle, 1, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Jungle, 2, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Jungle, 3, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Vulcan, 1, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Vulcan, 2, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Vulcan, 3, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Ice, 1, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Ice, 2, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Ice, 3, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.EnemyBase, 1, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.EnemyBase, 2, 3);
      GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.EnemyBase, 3, 3);
    }

    private void OnAchievementButtonClick(object sender, EventArgs e)
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.TrophyRoom);
    }

    public override void OnBackButton()
    {
      this.ScreenManager.AddScreen((GameScreen) new ExitPopup());
    }

    private void OnChallengeButtonClick(object sender, EventArgs e)
    {
      GameProcess.Instance.ChallengeGameSession.StartSession();
      if (!SettingsGame.NeedChallengeTutorial)
        return;
      this.ScreenManager.AddScreen((GameScreen) new TutorialPopup(TutorialPopup.Tutorial.Challenge));
    }

    private void OnHelpClicked(object x, EventArgs y)
    {
      MainMenuTutorialPopup screen = new MainMenuTutorialPopup();
      screen.Init(this._root.Children[0], this._root.Children[1]);
      this.ScreenManager.AddScreen((GameScreen) screen);
    }

    private void OnMoreGamesClicked(object sender, EventArgs e)
    {
        // Default behavior is disabled

        //this.ScreenManager.AddScreen((GameScreen) this.moreGamePopup);
        //this.moreGamePopup.Open();

        // Magic action :)

        Gamer.Instance.CurrentHelicopter.Item = ItemCollection.Instance.GetHelicopter(HelicopterType.Avenger);
        Gamer.Instance.FirstWeapon.Item = ItemCollection.Instance.GetWeapon(WeaponType.PlasmaGun);
        Gamer.Instance.SecondWeapon.Item = ItemCollection.Instance.GetWeapon(WeaponType.CasseteRocket);
        Gamer.Instance.UpgradeA.Item = ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV2);
        Gamer.Instance.UpgradeB.Item = (UpgradeItem)null;
        Gamer.Instance.Money.SetDebugMoney(3000f);
        ItemCollection.Instance.GetHelicopter(HelicopterType.Viper).IsBought = true;
        ItemCollection.Instance.GetHelicopter(HelicopterType.Harbinger).IsBought = true;
        ItemCollection.Instance.GetHelicopter(HelicopterType.Avenger).IsBought = true;
        ItemCollection.Instance.GetHelicopter(HelicopterType.GrimReaper).IsBought = false;
        ItemCollection.Instance.GetWeapon(WeaponType.SingleMachineGun).IsBought = true;
        ItemCollection.Instance.GetWeapon(WeaponType.DualMachineGun).IsBought = true;
        ItemCollection.Instance.GetWeapon(WeaponType.Vulcan).IsBought = false;
        ItemCollection.Instance.GetWeapon(WeaponType.PlasmaGun).IsBought = true;
        ItemCollection.Instance.GetWeapon(WeaponType.RocketLauncher).IsBought = true;
        ItemCollection.Instance.GetWeapon(WeaponType.DualRocketLauncher).IsBought = false;
        ItemCollection.Instance.GetWeapon(WeaponType.HomingRocket).IsBought = false;
        ItemCollection.Instance.GetWeapon(WeaponType.CasseteRocket).IsBought = true;
        ItemCollection.Instance.GetWeapon(WeaponType.Shield).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV1).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV2).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.DamageControlSystemV3).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.SystemCompensationCrush).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.BulletControlSystem).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV1).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV2).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV3).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.EnergyRegenerationSystemV4).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.TargetAssistentSystem).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.EnhanchedRechargeSystem).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.IncreasedCapacitySystem).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.HotPlasmaModule).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.CriticalDamageSystemV1).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.CriticalDamageSystemV2).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.PDUSystemV1).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.PDUSystemV2).IsBought = false;
        ItemCollection.Instance.GetUpgrade(UpgradeType.UpgradedWarhead).IsBought = true;
        ItemCollection.Instance.GetUpgrade(UpgradeType.HarvestingSystem).IsBought = false;
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Canyon, 1, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Canyon, 2, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Canyon, 3, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Jungle, 1, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Jungle, 2, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Jungle, 3, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Vulcan, 1, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Vulcan, 2, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Vulcan, 3, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Ice, 1, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Ice, 2, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.Ice, 3, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.EnemyBase, 1, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.EnemyBase, 2, 3);
        GameProcess.Instance.StoryModeHistory.CompleteEpisode(WorldType.EnemyBase, 3, 3);
    }

    private void OnScoreboardButtonClick(object sender, EventArgs e)
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.Leaderboard);
    }

    private void OnStoryButtonClick(object sender, EventArgs e)
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.Map);
      if (!SettingsGame.NeedMapTutorial)
        return;
      this.ScreenManager.AddScreen((GameScreen) new TutorialPopup(TutorialPopup.Tutorial.Story));
    }

    public static void OnFacebookButtonClick(object sender, EventArgs e)
    {
      Launcher.LaunchUriAsync(MainMenuScreen.CreateLink());
    }

    public static Guid ApplicationId()
    {
      using (Stream stream = TitleContainer.OpenStream("WMAppManifest.xml"))
      {
        string g = XElement.Load(stream).Descendants((XName) "App").Select<XElement, string>((Func<XElement, string>) (app => app.Attribute((XName) "ProductID")?.Value)).FirstOrDefault<string>();
        return string.IsNullOrEmpty(g) ? Guid.Empty : new Guid(g);
      }
    }

    private static string CreateFacebookMessage()
    {
      string format = "I've just reached {0} score in Combat Helicopter 2 - new, mindblowing action game! {1} {2}";
      string str = "Player";
      return string.Format(format, (object) Scoreboard.Instance.GetHighScores(), (object) Gamer.Instance.Rank, (object) str);
    }

    private static Uri CreateLink()
    {
      return new Uri("https://www.microsoft.com/store/apps");
    }

    private void RequestTime()
    {
      NtpClient ntpClient = new NtpClient();
      ntpClient.TimeReceived += (EventHandler<NtpClient.TimeReceivedEventArgs>) ((x, y) =>
      {
        SettingsGame.CurrentTime = y.CurrentTime.ToUniversalTime();
        this.ShowDailyPopup();
      });
      ntpClient.RequestTime();
    }

    private void ShowDailyPopup()
    {
      TimeSpan timeSpan = SettingsGame.CurrentTime - SettingsGame.LastDailyBonusDate;
      if (timeSpan >= TimeSpan.FromDays(2.0) || SettingsGame.LaunchNumber == 1)
        SettingsGame.LastDailyBonus = 1;
      if (!(timeSpan >= TimeSpan.FromDays(1.0)))
        return;
      this.ScreenManager.AddScreen((GameScreen) new DailyBonusPopup(SettingsGame.LastDailyBonus));
      ++SettingsGame.LastDailyBonus;
      if (SettingsGame.LastDailyBonus == 4)
        SettingsGame.LastDailyBonus = 1;
      SettingsGame.LastDailyBonusDate = SettingsGame.CurrentTime;
    }

    public void ShowPopup()
    {
      if (this.ShowRateIt())
        return;
      this.RequestTime();
    }

    private bool ShowRateIt()
    {
      if (SettingsGame.IsRateIted || SettingsGame.LaunchNumber % 3 != 0 || SettingsGame.LaunchNumber == 0)
        return false;
      this.ScreenManager.AddScreen((GameScreen) new RateItPopup());
      return true;
    }
  }
}
