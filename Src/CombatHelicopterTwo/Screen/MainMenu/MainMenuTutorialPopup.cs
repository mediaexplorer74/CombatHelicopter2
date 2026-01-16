// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.MainMenu.MainMenuTutorialPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

#nullable disable
namespace Helicopter.Screen.MainMenu
{
  internal class MainMenuTutorialPopup : GameScreen
  {
    private BasicControl _root;

    public MainMenuTutorialPopup() => this.IsPopup = true;

    public override void Draw(DrawContext drawContext)
    {
      base.Draw(drawContext);
      this._root.Draw(drawContext);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this._root.Update(gameTime);
    }

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      this._root.HandleInput(input);
      foreach (GestureSample gesture in input.Gestures)
      {
        if (gesture.GestureType == GestureType.Tap)
          this.Exit();
      }
    }

    public override void OnBackButton() => this.Exit();

    private void Exit() => this.ExitScreen();

    public void Init(BasicControl storyButton, BasicControl challengeButon)
    {
      this._root = new BasicControl();
      this._root.AddChild(storyButton);
      this._root.AddChild(challengeButon);
      this._root.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("Tutorial/tutorialMenuBg2"), Vector2.Zero));
      TextControl child1 = new TextControl("", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition12"), Color.Black, new Vector2(125f, 363f))
      {
        Centered = true,
        MaxSymbolsPerLine = 15,
        Origin = new Vector2(0.5f, 0.0f)
      };
      child1.Text = "Complete each mission, upgrade your rank and ammunition!".ToLowerInvariant();
      this._root.AddChild((BasicControl) child1);
      TextControl child2 = new TextControl("", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition12"), Color.Black, new Vector2(675f, 363f))
      {
        Centered = true,
        MaxSymbolsPerLine = 15,
        Origin = new Vector2(0.5f, 0.0f)
      };
      child2.Text = "Compete against other pilots on Leaderboard, earn credits and upgrade ammunition!".ToLowerInvariant();
      this._root.AddChild((BasicControl) child2);
    }
  }
}
