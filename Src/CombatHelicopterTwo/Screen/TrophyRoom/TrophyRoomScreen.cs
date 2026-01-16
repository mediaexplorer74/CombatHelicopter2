// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.TrophyRoom.TrophyRoomScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Items.AchievementsSystem;
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
namespace Helicopter.Screen.TrophyRoom
{
  internal class TrophyRoomScreen : GameScreen
  {
    private BasicControl _root;

    public override void LoadContent()
    {
      base.LoadContent();
      this._root = (BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Achievement/bgAchieveScore"), Vector2.Zero);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/windowAchievement");
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400.0 - (double) sprite1.SourceSize.X / 2.0), (float) (240.0 - (double) sprite1.SourceSize.Y / 2.0))));
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
      this.CreateBottomShelf();
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/shelfsA");
      this._root.AddChild((BasicControl) new TexturedControl(sprite2, new Vector2((float) (400.0 - (double) sprite2.SourceSize.X / 2.0), 170f)));
      this.CreateTopShelf();
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/windowInnerShadow");
      this._root.AddChild((BasicControl) new TexturedControl(sprite3, new Vector2((float) (400.0 - (double) sprite3.SourceSize.X / 2.0), (float) (240.0 - (double) sprite3.SourceSize.Y / 2.0 + 13.0))));
    }

    private void CreateTopShelf()
    {
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/boxTopShelf");
      PanelControl child1 = new PanelControl();
      child1.Size = new Vector2(660f, 80f);
      BasicControl child2 = new BasicControl();
      child2.Size = new Vector2(165f, 110f);
      Sprite sprite2;
      switch (Gamer.Instance.Rank)
      {
        case Rank.Pilot:
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_pilot");
          break;
        case Rank.Sergeant:
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_sergeant");
          break;
        case Rank.Lieutenant:
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_lieutenant");
          break;
        case Rank.Captain:
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_pilot");
          break;
        case Rank.Major:
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_major");
          break;
        case Rank.Colonel:
          sprite2 = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_colonel");
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      TexturedControl child3 = new TexturedControl(sprite2, child2.Size / 2f - sprite2.SourceSize / 2f);
      TexturedControl child4 = new TexturedControl(sprite1, new Vector2((float) ((double) child2.Size.X / 2.0 - (double) sprite1.SourceSize.X / 2.0), (float) (110.0 - (double) sprite1.SourceSize.Y - 10.0)));
      child2.AddChild((BasicControl) child4);
      child2.AddChild((BasicControl) child3);
      child1.AddChild(child2);
      BasicControl child5 = new BasicControl();
      child5.Size = new Vector2(165f, 110f);
      Sprite sprite3 = ResourcesManager.Instance.GetSprite(Gamer.Instance.AchievementManager.Achievements["AirForceAchievementMedal"].Texture);
      TexturedControl child6 = new TexturedControl(sprite3, child5.Size / 2f - sprite3.SourceSize / 2f);
      child6.Visible = Gamer.Instance.AchievementManager.Achievements["AirForceAchievementMedal"].Achieved;
      child5.AddChild((BasicControl) child4);
      child5.AddChild((BasicControl) child6);
      child1.AddChild(child5);
      BasicControl child7 = new BasicControl();
      child7.Size = new Vector2(165f, 110f);
      Sprite sprite4 = ResourcesManager.Instance.GetSprite(Gamer.Instance.AchievementManager.Achievements["DistinguishedService"].Texture);
      TexturedControl child8 = new TexturedControl(sprite4, child7.Size / 2f - sprite4.SourceSize / 2f);
      child8.Visible = Gamer.Instance.AchievementManager.Achievements["DistinguishedService"].Achieved;
      child7.AddChild((BasicControl) child4);
      child7.AddChild((BasicControl) child8);
      child1.AddChild(child7);
      BasicControl child9 = new BasicControl();
      child9.Size = new Vector2(165f, 110f);
      Sprite sprite5 = ResourcesManager.Instance.GetSprite(Gamer.Instance.AchievementManager.Achievements["DistinguishedFlyingCross"].Texture);
      TexturedControl child10 = new TexturedControl(sprite5, child9.Size / 2f - sprite5.SourceSize / 2f);
      child10.Visible = Gamer.Instance.AchievementManager.Achievements["DistinguishedFlyingCross"].Achieved;
      child9.AddChild((BasicControl) child4);
      child9.AddChild((BasicControl) child10);
      child1.AddChild(child9);
      child1.LayoutRow(70f, 93f, 0.0f);
      this._root.AddChild((BasicControl) child1);
    }

    private void CreateBottomShelf()
    {
      FixedStepHorizontalScrollPanel child1 = new FixedStepHorizontalScrollPanel();
      child1.AreaOnScreen = new Rectangle(70, 215, 660, 200);
      int num = 0;
      PanelControl child2 = new PanelControl();
      int y1 = 112;
      List<Achievement> list = Gamer.Instance.AchievementManager.Achievements.Values.ToList<Achievement>();
      list.Sort((Comparison<Achievement>) ((x, y) =>
      {
        if (x.Achieved && !y.Achieved)
          return -1;
        return !x.Achieved && y.Achieved ? 1 : 0;
      }));
      foreach (Achievement achievement in list)
      {
        if (achievement.Showable)
        {
          if (num == 0)
            child2 = new PanelControl();
          if (!string.IsNullOrEmpty(achievement.Texture))
          {
            BasicControl achievedAchievement = this.CreateAchievedAchievement(achievement);
            child2.AddChild(achievedAchievement);
            ++num;
          }
          if (num == 2)
          {
            num = 0;
            child2.LayoutRow(15f, 0.0f, 0.0f);
            child2.Size = new Vector2(660f, child2.Size.Y);
            child2.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Achievement/shelfsB"), new Vector2(0.0f, (float) y1)), 0);
            child1.AddChild((BasicControl) child2);
          }
        }
      }
      if (num != 0)
      {
        child2.LayoutRow(15f, 0.0f, 0.0f);
        child2.Size = new Vector2(660f, child2.Size.Y);
        child2.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Achievement/shelfsB"), new Vector2(0.0f, (float) y1)), 0);
        child1.AddChild((BasicControl) child2);
      }
      child1.LayoutRow(0.0f, 0.0f, 0.0f);
      child1.Children[0].AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Achievement/shelfsB"), new Vector2(-660f, (float) y1)));
      child1.Children[child1.ChildCount - 1].AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/Achievement/shelfsB"), new Vector2(660f, (float) y1)));
      this._root.AddChild((BasicControl) child1);
    }

    private BasicControl CreateAchievedAchievement(Achievement achievement)
    {
      BasicControl achievedAchievement = new BasicControl();
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/tahoma12Bold");
      Sprite sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/boxWithTable");
      achievedAchievement.Size = new Vector2(sprite.SourceSize.X, 200f);
      TexturedControl child1 = new TexturedControl(sprite, new Vector2(0.0f, (float) ((double) achievedAchievement.Size.Y / 2.0 - (double) sprite.SourceSize.Y / 2.0)));
      TextControl child2 = new TextControl(achievement.Description, resource, Color.Black, new Vector2(236f, 80f))
      {
        Origin = new Vector2(0.5f, 0.5f),
        MaxSymbolsPerLine = 12,
        Centered = true
      };
      child2.RebuildLines();
      child1.AddChild((BasicControl) child2);
      if (achievement.Achieved)
      {
        TexturedControl child3 = new TexturedControl(ResourcesManager.Instance.GetSprite(achievement.Texture), new Vector2(38f, 16f));
        child1.AddChild((BasicControl) child3);
      }
      TextControl textControl = new TextControl(achievement.Name, resource, Color.Black);
      textControl.Origin = new Vector2(0.5f, 0.0f);
      textControl.Position = new Vector2(165f, 164f);
      textControl.MaxSymbolsPerLine = 10;
      TextControl child4 = textControl;
      child1.AddChild((BasicControl) child4);
      achievedAchievement.AddChild((BasicControl) child1);
      achievedAchievement.Size = new Vector2(330f, achievedAchievement.Size.Y);
      return achievedAchievement;
    }

    public override void Draw(DrawContext drawContext)
    {
      base.Draw(drawContext);
      this._root.Draw(drawContext);
    }

    public override void HandleInput(InputState input) => this._root.HandleInput(input);

    public override void OnBackButton()
    {
      GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu);
    }

    public override void Update(GameTime gameTime) => this._root.Update(gameTime);
  }
}
