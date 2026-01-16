// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MainMenu.MoreGamePopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Windows.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Screen.MainMenu
{
  internal class MoreGamePopup : GameScreen
  {
    private const float BaseYOffset = 40f;
    private readonly Vector2Tweener _positionTweener = new Vector2Tweener(Vector2.Zero, Vector2.Zero, 0.0f, new TweeningFunction(Linear.EaseIn));
    private readonly BasicControl _root;
    private BasicControl _moveableControl;
    private MenuControl _moreGames;

    public MoreGamePopup()
    {
      this._root = new BasicControl();
      this.IsPopup = true;
      this._positionTweener.Stop();
      this._positionTweener.Ended += new EventHandler<EventArgs>(this.OnTweenerEnd);
    }

    private void OnTweenerEnd(object sender, EventArgs e)
    {
      this._moveableControl.Position = this._positionTweener.CurrentPosition;
      if ((double) this._moveableControl.Position.Y <= 445.0)
        return;
      this.ExitScreen();
      this._positionTweener.Stop();
    }

    public override void LoadContent()
    {
      MoreGamesSettings moreGamesSettings = MoreGamesSettings.Load();
      if (this._root.Children != null)
        this._root.Children.Clear();
      this._moveableControl = new BasicControl()
      {
        Position = new Vector2(0.0f, 450f)
      };
      this._root.AddChild(this._moveableControl);
      Sprite sprite = ResourcesManager.Instance.GetSprite("MainMenu/butMoreGames");
      this._moreGames = new MenuControl(sprite, sprite, Vector2.Zero);
      this._moreGames.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("MainMenu/coins"), new Vector2(484f, -17f)));
      this._moreGames.Clicked += new EventHandler<EventArgs>(this.OnMoreGamesClicked);
      this._moveableControl.AddChild((BasicControl) this._moreGames);
      TextControl child1 = new TextControl("Install our free games to receive credits!", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/Segoe28"));
      child1.Position = new Vector2((float) ((800.0 - (double) child1.Size.X) / 2.0), 60f);
      this._moveableControl.AddChild((BasicControl) child1);
      GameButton child2 = new GameButton();
      child2.Position = new Vector2(50f, 118f);
      child2.IconTexture = ResourcesManager.Instance.GetSprite("MoreGames/icon_JewelGod");
      child2.GameName = "Jewel God";
      child2.Bonus = 1000;
      child2.AllowBonus = moreGamesSettings.AllowJevelGodBonus;
      child2.GameType = PromoGame.JevelGod;
      child2.Init();
      child2.Clicked += new EventHandler<EventArgs>(this.OnGameClicked);
      this._moveableControl.AddChild((BasicControl) child2);
      GameButton child3 = new GameButton();
      child3.Position = new Vector2(230f, 118f);
      child3.IconTexture = ResourcesManager.Instance.GetSprite("MoreGames/icon_BubleBurst");
      child3.GameName = "Bubble Burst";
      child3.Bonus = 1000;
      child3.AllowBonus = moreGamesSettings.AllowBubbleBurstBonus;
      child3.GameType = PromoGame.BubbleBurst;
      child3.Init();
      child3.Clicked += new EventHandler<EventArgs>(this.OnGameClicked);
      this._moveableControl.AddChild((BasicControl) child3);
      GameButton child4 = new GameButton();
      child4.Position = new Vector2(410f, 118f);
      child4.IconTexture = ResourcesManager.Instance.GetSprite("MoreGames/icon_jumble");
      child4.GameName = "The Jumble";
      child4.Bonus = 1000;
      child4.AllowBonus = moreGamesSettings.AllowCombatHelicopterBonus;
      child4.GameType = PromoGame.TheJumble;
      child4.Init();
      child4.Clicked += new EventHandler<EventArgs>(this.OnGameClicked);
      this._moveableControl.AddChild((BasicControl) child4);
      GameButton child5 = new GameButton();
      child5.Position = new Vector2(590f, 118f);
      child5.IconTexture = ResourcesManager.Instance.GetSprite("MoreGames/icon_JewelLines");
      child5.GameName = "Jewel Lines";
      child5.Bonus = 1000;
      child5.AllowBonus = moreGamesSettings.AllowJevelLinesBonus;
      child5.GameType = PromoGame.JevelLines;
      child5.Init();
      child5.Clicked += new EventHandler<EventArgs>(this.OnGameClicked);
      this._moveableControl.AddChild((BasicControl) child5);
      base.LoadContent();
    }

    private void OnGameClicked(object sender, EventArgs e)
    {
      GameButton gameButton = (GameButton) sender;
      MoreGamesSettings moreGamesSettings = MoreGamesSettings.Load();
      string str;
      bool flag;
      switch (gameButton.GameType)
      {
        case PromoGame.JevelGod:
          str = "7ead781c-50f1-43bc-be2c-0c604da39c98";
          flag = moreGamesSettings.AllowJevelGodBonus;
          moreGamesSettings.AllowJevelGodBonus = false;
          break;
        case PromoGame.BubbleBurst:
          str = "7a11788d-36f3-4b84-9fd4-5a7a70edbd8b";
          flag = moreGamesSettings.AllowBubbleBurstBonus;
          moreGamesSettings.AllowBubbleBurstBonus = false;
          break;
        case PromoGame.TheJumble:
          str = "8c534770-7409-4427-a0ed-845375bc6747";
          flag = moreGamesSettings.AllowCombatHelicopterBonus;
          moreGamesSettings.AllowCombatHelicopterBonus = false;
          break;
        case PromoGame.JevelLines:
          str = "e46f492e-2f29-40de-ac0d-36580ab694f9";
          flag = moreGamesSettings.AllowJevelLinesBonus;
          moreGamesSettings.AllowJevelLinesBonus = false;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      if (flag)
      {
        Gamer.Instance.Money.AddMoney((float) gameButton.Bonus);
        moreGamesSettings.Save();
      }
      Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?ProductId=" + str));
    }

    public override void OnBackButton() => this.Close();

    public void AddControl(BasicControl control) => this._root.AddChild(control);

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
    }

    public override void Update(GameTime gameTime)
    {
      if (this._positionTweener.Running)
      {
        this._positionTweener.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
        this._moveableControl.Position = this._positionTweener.CurrentPosition;
      }
      base.Update(gameTime);
    }

    public override void Draw(DrawContext drawContext)
    {
      this._root.Draw(drawContext);
      base.Draw(drawContext);
    }

    private void OnMoreGamesClicked(object sender, EventArgs e) => this.Close();

    private void Close()
    {
      if (this._positionTweener.Running)
        return;
      this._positionTweener.Init(new Vector2(0.0f, 43f), new Vector2(0.0f, 450f), 0.6f, new TweeningFunction(Quadratic.EaseIn));
      this._positionTweener.Start();
    }

    public void Open()
    {
      if (this._positionTweener.Running)
        return;
      this._positionTweener.Init(new Vector2(0.0f, 450f), new Vector2(0.0f, 43f), 0.6f, new TweeningFunction(Quadratic.EaseIn));
      this._positionTweener.Start();
    }
  }
}
