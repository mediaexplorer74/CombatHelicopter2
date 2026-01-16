// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.MapScreen.ChoseLevelMapScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Playing;
using Helicopter.Screen.MapScreen.Buttons;
using Helicopter.Screen.MapScreen.Tutorial_Popups;
using Microsoft.Xna.Framework;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter.Screen.MapScreen
{
  internal class ChoseLevelMapScreen : GameScreen
  {
    private Sprite _background;
    private FlyingCloudControl _cloud1;
    private FlyingCloudControl _cloud2;
    private InteractiveLevelButtonControl _level1;
    private Sprite _level1Disabled;
    private InteractiveLevelButtonControl _level2;
    private Sprite _level2Disabled;
    private InteractiveLevelButtonControl _level3;
    private Sprite _level3Disabled;
    private InteractiveLevelButtonControl _level4;
    private Sprite _level4Disabled;
    private InteractiveLevelButtonControl _level5;
    private Sprite _level5Disabled;
    private FourTexturePack _levelButtonFive;
    private FourTexturePack _levelButtonFour;
    private FourTexturePack _levelButtonHugeSelected;
    private FourTexturePack _levelButtonMediumSelected;
    private FourTexturePack _levelButtonSmallSelected;
    private FourTexturePack _levelButtonOne;
    private FourTexturePack _levelButtonThree;
    private FourTexturePack _levelButtonTwo;
    private Sprite _line1To2;
    private TexturedControl _line1To2Control;
    private Sprite _line2To3;
    private TexturedControl _line2To3Control;
    private Sprite _line3To4;
    private TexturedControl _line3To4Control;
    private Sprite _line4To5;
    private TexturedControl _line4To5Control;
    private FourTexturePack _numberOne;
    private FourTexturePack _numberOneSmall;
    private FourTexturePack _numberThree;
    private FourTexturePack _numberTwo;
    private FourTexturePack _numberTwoSmall;
    private BasicControl _root;
    private Sprite _smallStarTexture;
    private Sprite _starTexture;

    public override void Draw(DrawContext drawContext) => this._root.Draw(drawContext);

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this._root.Update(gameTime);
      if (this.IsFullyLoaded)
        return;
      this.IsFullyLoaded = true;
      if (!SettingsGame.NeedAfter22Tutorial)
        return;
      this.ScreenManager.AddScreen((GameScreen) new TutorialPopup(TutorialPopup.Tutorial.After22));
    }

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
    }

    public override void LoadContent()
    {
      this._background = ResourcesManager.Instance.GetSprite("MainMenu/Map/bgMap");
      this._root = (BasicControl) new TexturedControl(this._background, Vector2.Zero);
      this._starTexture = ResourcesManager.Instance.GetSprite("MainMenu/Map/Level1star");
      this._smallStarTexture = ResourcesManager.Instance.GetSprite("MainMenu/Map/Level2star");
      this._numberOneSmall = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_1"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_1S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_2NoActive"),
        StateTwoSelected = this._smallStarTexture
      };
      this._numberTwoSmall = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_2"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_2S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_2NoActive"),
        StateTwoSelected = this._smallStarTexture
      };
      FourTexturePack fourTexturePack = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_Boss"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_BossS"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2_BossNoActive"),
        StateTwoSelected = this._smallStarTexture
      };
      this._numberOne = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_1"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_1S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_2NoActive"),
        StateTwoSelected = this._starTexture
      };
      this._level1Disabled = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1Lock");
      this._numberTwo = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_2"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_2S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_2NoActive"),
        StateTwoSelected = this._starTexture
      };
      this._level2Disabled = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2SLock");
      this._numberThree = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_Boss"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_BossS"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1_BossNoActive"),
        StateTwoSelected = this._starTexture
      };
      this._level3Disabled = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3Lock");
      this._levelButtonOne = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1Curent"),
        StateTwoSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1CurentS")
      };
      this._levelButtonTwo = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2Curent"),
        StateTwoSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2SelectS")
      };
      this._levelButtonThree = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3Curent"),
        StateTwoSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3SelectS")
      };
      this._levelButtonFour = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel4"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel4S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2Curent"),
        StateTwoSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2SelectS")
      };
      this._level4Disabled = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel4SLock");
      this._levelButtonFive = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel5"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel5S"),
        StateTwo = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1Curent"),
        StateTwoSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1SelectS")
      };
      this._level5Disabled = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel5SLock");
      this._levelButtonHugeSelected = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1Select"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel1SelectS")
      };
      this._levelButtonMediumSelected = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2Select"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel2Select")
      };
      this._levelButtonSmallSelected = new FourTexturePack()
      {
        StateOne = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3Select"),
        StateOneSelected = ResourcesManager.Instance.GetSprite("MainMenu/Map/butLevel3SelectS")
      };
      this._line1To2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/lineLevel1to2");
      this._line2To3 = ResourcesManager.Instance.GetSprite("MainMenu/Map/lineLevel2to3");
      this._line3To4 = ResourcesManager.Instance.GetSprite("MainMenu/Map/lineLevel3to4");
      this._line4To5 = ResourcesManager.Instance.GetSprite("MainMenu/Map/lineLevel4to5");
      float angle = 1.30899692f;
      this._cloud1 = new FlyingCloudControl();
      this._cloud1.Init(ResourcesManager.Instance.GetSprite("MainMenu/Map/cloud1"), new Rectangle(520, 20, 180, 100), angle, 35f, 105f, 1f, 5f);
      this._cloud2 = new FlyingCloudControl();
      this._cloud2.Init(ResourcesManager.Instance.GetSprite("MainMenu/Map/cloud2"), new Rectangle(720, 20, 180, 100), angle, 35f, 105f, 1f, 5f);
      this.Init(GameProcess.Instance.StoryModeHistory);
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = false
      });
    }

    public override void OnBackButton()
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu);
    }

    private void OnClosed(object sender, EventArgs e)
    {
    }

    private void OnNextEpisodeUnlock(object sender, EventArgs e)
    {
      Gamer.Instance.Money.AddMoney(100000f);
      StoryModeHistory storyModeHistory = GameProcess.Instance.StoryModeHistory;
      if (storyModeHistory.LocationHistories[4].ThirdEpisode.IsCompleted)
      {
        foreach (LocationHistory locationHistory in storyModeHistory.LocationHistories)
        {
          locationHistory.FirstEpisode.IsAvailiable = false;
          locationHistory.FirstEpisode.IsCompleted = false;
          locationHistory.FirstEpisode.Stars = 0;
          locationHistory.SecondEpisode.IsAvailiable = false;
          locationHistory.SecondEpisode.IsCompleted = false;
          locationHistory.SecondEpisode.Stars = 0;
          locationHistory.ThirdEpisode.IsAvailiable = false;
          locationHistory.ThirdEpisode.IsCompleted = false;
          locationHistory.ThirdEpisode.Stars = 0;
        }
        storyModeHistory.LocationHistories[0].FirstEpisode.IsAvailiable = true;
      }
      else
      {
        for (int index = 0; index < storyModeHistory.LocationHistories.Length; ++index)
        {
          LocationHistory locationHistory = storyModeHistory.LocationHistories[index];
          if (!locationHistory.IsFullCompleted)
          {
            if (locationHistory.FirstEpisode.IsAvailiable && !locationHistory.FirstEpisode.IsCompleted)
            {
              locationHistory.FirstEpisode.IsCompleted = true;
              locationHistory.FirstEpisode.Stars = CommonRandom.Instance.Random.Next(1, 4);
              locationHistory.SecondEpisode.IsAvailiable = true;
              break;
            }
            if (locationHistory.SecondEpisode.IsAvailiable && !locationHistory.SecondEpisode.IsCompleted)
            {
              locationHistory.SecondEpisode.IsCompleted = true;
              locationHistory.SecondEpisode.Stars = CommonRandom.Instance.Random.Next(1, 4);
              locationHistory.ThirdEpisode.IsCompleted = true;
              if (index != 4)
              {
                storyModeHistory.LocationHistories[index + 1].FirstEpisode.IsAvailiable = true;
                break;
              }
              storyModeHistory.LocationHistories[index].ThirdEpisode.IsAvailiable = true;
              break;
            }
            break;
          }
        }
      }
      this._root.RemoveAllChilds();
      this.Init(storyModeHistory);
    }

    private void OnOpened(object sender, EventArgs e)
    {
      InteractiveLevelButtonControl[] levelButtonControlArray = new InteractiveLevelButtonControl[5]
      {
        this._level1,
        this._level2,
        this._level3,
        this._level4,
        this._level5
      };
      foreach (InteractiveLevelButtonControl levelButtonControl in levelButtonControlArray)
      {
        if (levelButtonControl != sender && levelButtonControl != null)
          levelButtonControl.Close();
      }
    }

    private BasicControl CreateTotalStars(StoryModeHistory storyModeHistory)
    {
      int num1 = 15;
      BasicControl totalStars = new BasicControl();
      foreach (int num2 in storyModeHistory.TotalStars.ToString((IFormatProvider) CultureInfo.InvariantCulture))
      {
        Sprite sprite = ResourcesManager.Instance.GetSprite(string.Format("MainMenu/Map/Rank/{0}", (object) (char) num2));
        totalStars.AddChild((BasicControl) new TexturedControl(sprite, new Vector2(totalStars.Size.X * 0.85f, 0.0f)));
      }
      TexturedControl child1 = new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/x"), new Vector2(totalStars.Size.X + (float) num1, 0.0f));
      totalStars.AddChild((BasicControl) child1);
      TexturedControl child2 = new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Map/star"), new Vector2(totalStars.Size.X + (float) num1, -15f));
      totalStars.AddChild((BasicControl) child2);
      totalStars.Position = new Vector2(800f - totalStars.Size.X - (float) num1, 433f);
      return totalStars;
    }

    private BasicControl CreateUserRank()
    {
      MenuControl userRank = new MenuControl(Rectangle.Empty);
      int x1 = 15;
      Sprite sprite1;
      Sprite sprite2;
      switch (Gamer.Instance.Rank)
      {
        case Rank.Pilot:
          sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/piot");
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/pilot");
          break;
        case Rank.Sergeant:
          sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/rank_sergeant");
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/sergeant");
          break;
        case Rank.Lieutenant:
          sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/rank_lieutenant");
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/lieutenant");
          break;
        case Rank.Captain:
          sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/rank_captain");
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/captain");
          break;
        case Rank.Major:
          sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/rank_major");
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/major");
          break;
        case Rank.Colonel:
          sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/rank_colonel");
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Map/Rank/colonel");
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      userRank._passive.Sprite = userRank._active.Sprite = sprite1;
      this._root.AddChild((BasicControl) new TexturedControl(sprite2, new Vector2(65f, 433f)));
      userRank.Position = new Vector2((float) x1, (float) (433 - sprite1.Bounds.Height / 2));
      userRank.Size = new Vector2(200f, 100f);
      userRank.Clicked += (EventHandler<EventArgs>) ((x, y) => GameProcess.Instance.Navigator.ShowScreen(ScreenType.TrophyRoom));
      return (BasicControl) userRank;
    }

    private void GoLevel(WorldType location, int episode)
    {
      if (episode == 3 && location != WorldType.Canyon && location != WorldType.EnemyBase)
        throw new ArgumentOutOfRangeException();
      GameProcess.Instance.StoryGameSession.StartSession(location, episode);
    }

    public void Init(StoryModeHistory storyModeHistory)
    {
      this._root.RemoveAllChilds();
      this._root.AddChild(this.CreateUserRank());
      this._root.AddChild(this.CreateTotalStars(storyModeHistory));
      this._root.AddChild((BasicControl) this._cloud1);
      this._root.AddChild((BasicControl) this._cloud2);
      this._line2To3Control = new TexturedControl(this._line2To3, new Vector2(241f, 135f));
      this._root.AddChild((BasicControl) this._line2To3Control);
      this._level2 = new InteractiveLevelButtonControl(WorldType.Jungle);
      this._level2.Init(this._levelButtonTwo, this._levelButtonMediumSelected, new Vector2(215f, 141f), this._numberOneSmall, new Vector2(146f, 102f), this._numberTwoSmall, new Vector2(103f, 177f), (FourTexturePack) null, new Vector2(178f, 213f), this._level2Disabled, this._smallStarTexture);
      this._level2.ButtonClicked += new ButtonClickHandler(this.GoLevel);
      this._level2.Opened += new EventHandler<EventArgs>(this.OnOpened);
      this._level2.Closed += new EventHandler<EventArgs>(this.OnClosed);
      this._root.AddChild((BasicControl) this._level2);
      this._level2.Init(storyModeHistory.LocationHistories[1], storyModeHistory.IsUnlocked(2) && !storyModeHistory.IsUnlocked(3), false);
      this._line1To2Control = new TexturedControl(this._line1To2, new Vector2(240f, 205f));
      this._root.AddChild((BasicControl) this._line1To2Control);
      this._level1 = new InteractiveLevelButtonControl(WorldType.Canyon);
      this._level1.Init(this._levelButtonOne, this._levelButtonHugeSelected, new Vector2(261f, 237f), this._numberOne, new Vector2(169f, 292f), this._numberTwo, new Vector2(252f, 339f), (FourTexturePack) null, new Vector2(331f, 292f), this._level1Disabled, this._starTexture);
      this._level1.ButtonClicked += new ButtonClickHandler(this.GoLevel);
      this._level1.Opened += new EventHandler<EventArgs>(this.OnOpened);
      this._level1.Closed += new EventHandler<EventArgs>(this.OnClosed);
      this._root.AddChild((BasicControl) this._level1);
      this._level1.Init(storyModeHistory.LocationHistories[0], !storyModeHistory.IsUnlocked(2), false);
      if (!storyModeHistory.LocationHistories[1].IsUnlocked)
        this._level1.Open();
      this._line3To4Control = new TexturedControl(this._line3To4, new Vector2(401f, 135f));
      this._root.AddChild((BasicControl) this._line3To4Control);
      this._level3 = new InteractiveLevelButtonControl(WorldType.Ice);
      this._level3.Init(this._levelButtonThree, this._levelButtonSmallSelected, new Vector2(375f, 87f), this._numberOneSmall, new Vector2(299f, 36f), this._numberTwoSmall, new Vector2(370f, 9f), (FourTexturePack) null, new Vector2(439f, 36f), this._level3Disabled, this._smallStarTexture);
      this._level3.ButtonClicked += new ButtonClickHandler(this.GoLevel);
      this._level3.Opened += new EventHandler<EventArgs>(this.OnOpened);
      this._level3.Closed += new EventHandler<EventArgs>(this.OnClosed);
      this._level3.Init(storyModeHistory.LocationHistories[2], storyModeHistory.IsUnlocked(3) && !storyModeHistory.IsUnlocked(4), false);
      this._root.AddChild((BasicControl) this._level3);
      this._level4 = new InteractiveLevelButtonControl(WorldType.Vulcan);
      this._level4.Init(this._levelButtonFour, this._levelButtonMediumSelected, new Vector2(534f, 141f), this._numberOneSmall, new Vector2(591f, 102f), this._numberTwoSmall, new Vector2(635f, 177f), (FourTexturePack) null, new Vector2(559f, 213f), this._level4Disabled, this._smallStarTexture);
      this._level4.ButtonClicked += new ButtonClickHandler(this.GoLevel);
      this._level4.Opened += new EventHandler<EventArgs>(this.OnOpened);
      this._level4.Closed += new EventHandler<EventArgs>(this.OnClosed);
      this._root.AddChild((BasicControl) this._level4);
      this._level4.Init(storyModeHistory.LocationHistories[3], storyModeHistory.IsUnlocked(4) && !storyModeHistory.IsUnlocked(5), false);
      this._line4To5Control = new TexturedControl(this._line4To5, new Vector2(508f, 204f));
      this._level5 = new InteractiveLevelButtonControl(WorldType.EnemyBase);
      this._level5.Init(this._levelButtonFive, this._levelButtonHugeSelected, new Vector2(476f, 237f), this._numberOne, new Vector2(384f, 292f), this._numberTwo, new Vector2(468f, 339f), this._numberThree, new Vector2(547f, 292f), this._level5Disabled, this._starTexture);
      this._level5.ButtonClicked += new ButtonClickHandler(this.GoLevel);
      this._level5.Opened += new EventHandler<EventArgs>(this.OnOpened);
      this._level5.Closed += new EventHandler<EventArgs>(this.OnClosed);
      this._root.AddChild((BasicControl) this._line4To5Control);
      this._root.AddChild((BasicControl) this._level5);
      this._level5.Init(storyModeHistory.LocationHistories[4], storyModeHistory.IsUnlocked(5), true);
      MenuControl child = new MenuControl(ResourcesManager.Instance.GetSprite("MainMenu/Map/butTutorialMap"), ResourcesManager.Instance.GetSprite("MainMenu/Map/butTutorialMapSelect"), new Vector2(15f, 7f));
      child.EntryPosition = new Rectangle(0, 0, 200, 50);
      child.Clicked += (EventHandler<EventArgs>) ((x, y) => this.ScreenManager.AddScreen((GameScreen) new TutorialPopup(TutorialPopup.Tutorial.Story)));
      this._root.AddChild((BasicControl) child);
    }
  }
}
