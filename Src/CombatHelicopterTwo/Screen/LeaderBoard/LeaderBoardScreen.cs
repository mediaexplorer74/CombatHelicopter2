// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.LeaderBoard.LeaderBoardScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Playing;
using Helicopter.Screen.MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Screen.LeaderBoard
{
  internal class LeaderBoardScreen : GameScreen
  {
    private readonly RecordsList _allRecords = new RecordsList();
    private readonly Color _lineColor = new Color(232, 185, 0);
    private Texture2D _blankTexture;
    private LeaderBoardScreen.PartOfList _currentPart;
    private int _lastTopRecordNumber = -1;
    private TextControl _loadingControl;
    private TextControl _loadingErrorControl;
    private LeaderBoardScreen.LoadingMode _loadingMode;
    private Dictionary<Rank, string> _rankTexts;
    private Dictionary<Rank, Sprite> _rankTextures;
    private SpriteFont _recordsFont;
    private BasicControl _root;
    private VerticalScrollingPanel _scoresList;
    private SpriteFont _titleFont;

    public override void LoadContent()
    {
      base.LoadContent();
      this._blankTexture = ResourcesManager.BlankSprite.Texture;
      this._root = (BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/bgMainMenu"), Vector2.Zero);
      this._root.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/windowLeader2"), new Vector2(57f, 43f)));
      MenuControl child1 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/facebook"), ResourcesManager.Instance.GetSprite("MainMenu/facebookSelect"), new Vector2(665f, 0.0f));
      child1.Clicked += new EventHandler<EventArgs>(MainMenuScreen.OnFacebookButtonClick);
      this._root.AddChild((BasicControl) child1);
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/twitter"), ResourcesManager.Instance.GetSprite("MainMenu/twitterSelect"), new Vector2(735f, 0.0f));
      child2.Clicked += new EventHandler<EventArgs>(MainMenuScreen.OnFacebookButtonClick);
      this._root.AddChild((BasicControl) child2);
      MenuControl stateOff = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/soundOn"), ResourcesManager.Instance.GetSprite("MainMenu/soundOnSelect"), Vector2.Zero);
      RadioButton child3 = new RadioButton(new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/soundOff"), ResourcesManager.Instance.GetSprite("MainMenu/soundOffSelect"), Vector2.Zero), stateOff, SettingsGame.Sound);
      child3.Position = new Vector2(15f, 0.0f);
      child3.StateChanged += new EventHandler<BooleanEventArgs>(this.ScreenManager.AudioManager.SoundStateChanged);
      this._root.AddChild((BasicControl) child3);
      MenuControl child4 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/butCurrent"), ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/butCurrentSelect"), new Vector2(732f, 230f));
      child4.Clicked += new EventHandler<EventArgs>(this.OnCurrentButtonClicked);
      this._root.AddChild((BasicControl) child4);
      MenuControl child5 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/butBottom"), ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/butBottomSelect"), new Vector2(732f, 357f));
      child5.Clicked += new EventHandler<EventArgs>(this.OnDownButtomClicked);
      this._root.AddChild((BasicControl) child5);
      MenuControl child6 = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/butUp"), ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/butUpSelect"), new Vector2(732f, 110f));
      child6.Clicked += new EventHandler<EventArgs>(this.OnUpClicked);
      this._root.AddChild((BasicControl) child6);
      this._titleFont = ResourcesManager.Instance.GetResource<SpriteFont>("Fonts/coalition24");
      this._recordsFont = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition20");
      TextControl child7 = new TextControl("N".ToLower(), this._titleFont);
      child7.Position = new Vector2(160f, 110f);
      child7.Color = this._lineColor;
      child7.Scale = 0.5f;
      this._root.AddChild((BasicControl) child7);
      TextControl child8 = new TextControl("RANK".ToLower(), this._titleFont);
      child8.Position = new Vector2(207f, 110f);
      child8.Color = this._lineColor;
      child8.Scale = 0.5f;
      this._root.AddChild((BasicControl) child8);
      TextControl child9 = new TextControl("NAME".ToLower(), this._titleFont);
      child9.Position = new Vector2(355f, 110f);
      child9.Color = this._lineColor;
      child9.Scale = 0.5f;
      this._root.AddChild((BasicControl) child9);
      TextControl child10 = new TextControl("SCORE".ToLower(), this._titleFont);
      child10.Position = new Vector2(570f, 110f);
      child10.Color = this._lineColor;
      child10.Scale = 0.5f;
      this._root.AddChild((BasicControl) child10);
      TextControl child11 = new TextControl("Tap on your nickname to change it".ToLower(), this._titleFont)
      {
        Color = this._lineColor,
        Scale = 0.5f
      };
      child11.Position = new Vector2((float) ((800.0 - (double) child11.Size.X) / 2.0), 445f);
      this._root.AddChild((BasicControl) child11);
      this._scoresList = new VerticalScrollingPanel();
      this._scoresList.Position = new Vector2(106f, 133f);
      this._scoresList.ViewRect = new Rectangle(0, 0, 586, 269);
      this._root.AddChild((BasicControl) this._scoresList);
      Sprite instance = Sprite.GetInstance();
      instance.Init(this._blankTexture);
      this._root.AddChild((BasicControl) new TexturedControl(instance, new Vector2(108f, 132f))
      {
        ImageSize = new Vector2(582f, 1f),
        Color = this._lineColor
      });
      this._rankTextures = new Dictionary<Rank, Sprite>()
      {
        {
          Rank.Pilot,
          ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/Rank/piot")
        },
        {
          Rank.Sergeant,
          ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/Rank/rank_sergeant")
        },
        {
          Rank.Lieutenant,
          ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/Rank/rank_lieutenant")
        },
        {
          Rank.Captain,
          ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/Rank/rank_captain")
        },
        {
          Rank.Major,
          ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/Rank/rank_major")
        },
        {
          Rank.Colonel,
          ResourcesManager.Instance.GetSprite("MainMenu/LeaderBoard/Rank/rank_colonel")
        }
      };
      this._rankTexts = new Dictionary<Rank, string>()
      {
        {
          Rank.Pilot,
          "Pilot"
        },
        {
          Rank.Sergeant,
          "Sergeant"
        },
        {
          Rank.Lieutenant,
          "Lieutenant"
        },
        {
          Rank.Captain,
          "Captain"
        },
        {
          Rank.Major,
          "Major"
        },
        {
          Rank.Colonel,
          "Colonel"
        }
      };
      this._scoresList.TopExceeded += new EventHandler(this.OnTopExceeded);
      this._scoresList.BottomExceeded += new EventHandler(this.OnBottomExceeded);
      this._loadingControl = new TextControl("Loading".ToLower(), this._titleFont);
      this._loadingControl.Color = this._lineColor;
      this._loadingControl.Scale = 0.8f;
      this._loadingControl.Position = new Vector2((float) ((800.0 - (double) this._loadingControl.Size.X) / 2.0), 240f);
      this._loadingErrorControl = new TextControl("Internet connection error, please try again".ToLower(), this._titleFont);
      this._loadingErrorControl.CenteredX = true;
      this._loadingErrorControl.MaxSymbolsPerLine = 26;
      this._loadingErrorControl.RebuildLines();
      this._loadingErrorControl.Color = this._lineColor;
      this._loadingErrorControl.Scale = 0.8f;
      this._loadingErrorControl.Position = new Vector2(200f, 240f);
      this.AddTestData();
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
    }

    public override void Draw(DrawContext drawContext)
    {
      base.Draw(drawContext);
      this._root.Draw(drawContext);
    }

    public override void HandleInput(InputState input) => this._root.HandleInput(input);

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this._root.Update(gameTime);
    }

    public override void OnBackButton()
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu);
    }

    private void OnBottomExceeded(object sender, EventArgs e)
    {
      this._lastTopRecordNumber = this.GetTopVisibleRank();
      this._loadingMode = LeaderBoardScreen.LoadingMode.AdditionalLoadingBottom;
    }

    private void OnCurrentButtonClicked(object sender, EventArgs e)
    {
      this.HideLoading();
      this.AddTestData();
    }

    private void OnDownButtomClicked(object sender, EventArgs e)
    {
      this.HideLoading();
      this.LoadBottomRecords();
    }

    private void OnNicknameChanged(object sender, UserNameEventArgs e)
    {
      foreach (LeaderboardRecordControl child in this._scoresList.Children)
      {
        if (child.Record.IsMyself)
        {
          child.Record.Name = e.NewName;
          child.UpdateLayout();
          break;
        }
      }
    }

    private void OnTapUserRecord(object sender, EventArgs e)
    {
      LeaderboardRecordControl leaderboardRecordControl = (LeaderboardRecordControl) sender;
      ProfileInfoPopup screen = new ProfileInfoPopup();
      screen.Nickname = leaderboardRecordControl.Record.Name;
      screen.NicknameChanged += new EventHandler<UserNameEventArgs>(this.OnNicknameChanged);
      this.ScreenManager.AddScreen((GameScreen) screen);
    }

    private void OnTopExceeded(object sender, EventArgs e)
    {
      this._lastTopRecordNumber = this.GetTopVisibleRank();
      this._loadingMode = LeaderBoardScreen.LoadingMode.AdditionalLoadingTop;
    }

    private void OnUpClicked(object sender, EventArgs e)
    {
      this.HideLoading();
      this.LoadTopRecords();
    }

    private void AddTestData()
    {
      this.ShowScoresList((IEnumerable<LeaderboardRecord>) DummyReccordsProvider.GetDummyRecords(100, 25));
    }

    private void ClearData()
    {
      this._scoresList.Children.Clear();
      this._allRecords.Clear();
    }

    private RecordsList GetCurrentList() => this._allRecords;

    private int GetTopVisibleRank()
    {
      return this._scoresList.TopVisibleRecord < 0 || this._scoresList.TopVisibleRecord >= this._scoresList.Children.Count ? -1 : ((LeaderboardRecordControl) this._scoresList.Children[this._scoresList.TopVisibleRecord]).Record.Number;
    }

    private void HideLoading()
    {
      if (this._root.Children.Contains((BasicControl) this._loadingErrorControl))
        this._root.Children.Remove((BasicControl) this._loadingErrorControl);
      if (!this._root.Children.Contains((BasicControl) this._loadingControl))
        return;
      this._root.Children.Remove((BasicControl) this._loadingControl);
    }

    private void LoadBottomRecords()
    {
      this.ClearData();
      this._currentPart = LeaderBoardScreen.PartOfList.Bottom;
      this._loadingMode = LeaderBoardScreen.LoadingMode.ShowBottom;
      this._allRecords.AddRange(DummyReccordsProvider.GetDummyRecords(100, 75));
      this.ShowLoadedData();
    }

    private void LoadTopRecords()
    {
      this.ClearData();
      this._currentPart = LeaderBoardScreen.PartOfList.Top;
      this._loadingMode = LeaderBoardScreen.LoadingMode.ShowTop;
      this._allRecords.AddRange(DummyReccordsProvider.GetDummyRecords(100, 0));
      this.ShowLoadedData();
    }

    public void LoadUserRecords()
    {
      this.ClearData();
      this._currentPart = LeaderBoardScreen.PartOfList.Center;
      this._loadingMode = LeaderBoardScreen.LoadingMode.ShowPlayer;
      this._allRecords.AddRange(DummyReccordsProvider.GetDummyRecords(100, 25));
      this.ShowLoadedData();
    }

    private LeaderboardRecord PrepareScore(LeaderboardRecord record) => record;

    private void ScoresContoller_RequestFailed(object sender, EventArgs e) { }

    private void ScoresContoller_ScoresLoaded(object sender, EventArgs e) { }

    private void ShowLoadedData()
    {
      this.ShowScoresList((IEnumerable<LeaderboardRecord>) this.GetCurrentList().Records);
      switch (this._loadingMode)
      {
        case LeaderBoardScreen.LoadingMode.ShowTop:
          this._scoresList.ShowTop();
          break;
        case LeaderBoardScreen.LoadingMode.ShowPlayer:
          this.ShowPlayer();
          break;
        case LeaderBoardScreen.LoadingMode.ShowBottom:
          this._scoresList.ShowBottom();
          break;
        case LeaderBoardScreen.LoadingMode.AdditionalLoadingTop:
          this._scoresList.ShowBottom();
          break;
        case LeaderBoardScreen.LoadingMode.AdditionalLoadingBottom:
          this._scoresList.ShowTop();
          break;
      }
    }

    private void ShowLoading()
    {
      this._scoresList.Children.Clear();
      if (this._root.Children.Contains((BasicControl) this._loadingErrorControl))
        this._root.Children.Remove((BasicControl) this._loadingErrorControl);
      if (this._root.Children.Contains((BasicControl) this._loadingControl))
        return;
      this._root.Children.Add((BasicControl) this._loadingControl);
    }

    private void ShowLoadingError()
    {
      this._scoresList.Children.Clear();
      if (this._root.Children.Contains((BasicControl) this._loadingControl))
        this._root.Children.Remove((BasicControl) this._loadingControl);
      if (this._root.Children.Contains((BasicControl) this._loadingErrorControl))
        return;
      this._root.Children.Add((BasicControl) this._loadingErrorControl);
    }

    private void ShowPlayer()
    {
      for (int index = 0; index < this._scoresList.Children.Count; ++index)
      {
        if (((LeaderboardRecordControl) this._scoresList.Children[index]).Record.IsMyself)
        {
          this._scoresList.ShowRecord(index, true);
          break;
        }
      }
    }

    private void ShowScoresList(IEnumerable<LeaderboardRecord> loadedRecords)
    {
      if (this._scoresList.Children != null)
        this._scoresList.Children.Clear();
      int num = 0;
      foreach (LeaderboardRecord loadedRecord in loadedRecords)
      {
        LeaderboardRecordControl child = new LeaderboardRecordControl();
        child.Font = this._recordsFont;
        Sprite instance = Sprite.GetInstance();
        instance.Init(this._blankTexture);
        child.BlankTexture = instance;
        child.RankTextures = this._rankTextures;
        child.RankTexts = this._rankTexts;
        child.Record = loadedRecord;
        child.LightBackground = num % 2 == 1;
        this._scoresList.AddChild((BasicControl) child);
        ++num;
        if (loadedRecord.IsMyself)
          child.Tap += new EventHandler(this.OnTapUserRecord);
      }
      this._scoresList.LayoutColumn(0.0f, 0.0f, 0.0f);
    }

    private enum LoadingMode
    {
      ShowTop,
      ShowPlayer,
      ShowBottom,
      AdditionalLoadingTop,
      AdditionalLoadingBottom,
    }

    private enum PartOfList
    {
      Top,
      Center,
      Bottom,
    }
  }
}
