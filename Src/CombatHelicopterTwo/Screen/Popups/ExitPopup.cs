// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.Popups.ExitPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Windows.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace Helicopter.Screen.Popups
{
  internal class ExitPopup : GameScreen
  {
    private readonly BasicControl _root = new BasicControl();

    public ExitPopup() => this.IsPopup = true;

    public override void LoadContent()
    {
      this._root.Size = new Vector2(800f, 480f);
      TexturedControl child1 = new TexturedControl(ResourcesManager.Instance.GetSprite("PopUpWindow/quitGamePopUpBg"), Vector2.Zero);
      child1.Position = new Vector2(400f, 240f) - child1.Size / 2f;
      this._root.AddChild((BasicControl) child1);
      string lower = "Do you really \nwant to surrender?".ToLower();
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition15");
      Color color = new Color(254, 224, 23);
      TextControl child2 = new TextControl(lower, resource, color);
      child2.Origin = new Vector2(0.5f, 0.5f);
      child2.Position = new Vector2(402f, 291f);
      child2.Centered = true;
      this._root.AddChild((BasicControl) child2);
      PanelControl child3 = new PanelControl();
      BasicControl child4 = new BasicControl()
      {
        Size = new Vector2(149f, 45f)
      };
      TextControl child5 = new TextControl("press\nback", resource, color, new Vector2(74.5f, 32.5f))
      {
        Origin = new Vector2(0.5f, 0.5f),
        Centered = true
      };
      child4.AddChild((BasicControl) child5);
      child3.AddChild(child4);
      MenuControl child6 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butRateIt"), ResourcesManager.Instance.GetSprite("PopUpWindow/butRateItSelect"), Vector2.Zero);
      child6.Clicked += new EventHandler<EventArgs>(this.OnRateItClicked);
      child3.AddChild((BasicControl) child6);
      MenuControl child7 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butNo"), ResourcesManager.Instance.GetSprite("PopUpWindow/butNoSelect"), Vector2.Zero);
      child7.Clicked += new EventHandler<EventArgs>(this.OnNoClicked);
      child3.AddChild((BasicControl) child7);
      child3.LayoutRow(0.0f, 0.0f, 12f);
      child3.Position = new Vector2((float) (400.0 - (double) child3.Size.X / 2.0), 345f);
      this._root.AddChild((BasicControl) child3);
      base.LoadContent();
      this.ScreenManager.Game.IsQuitCan = true;
    }

    public override void OnBackButton() => this.Close();

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
      this.TapOutPopup(input);
    }

    public override void Update(GameTime gameTime)
    {
      this._root.Update(gameTime);
      base.Update(gameTime);
    }

    public override void Draw(DrawContext drawContext)
    {
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    private void OnNoClicked(object x, EventArgs y) => this.Close();

    private void OnRateItClicked(object sender, EventArgs e)
    {
      Launcher.LaunchUriAsync(new Uri("ms-windows-store://home"));
      this.Close();
      RateItPopup.GiftHealthPack();
    }

    private void Close()
    {
      this.ScreenManager.Game.IsQuitCan = false;
      this.ExitScreen();
    }

    private void TapOutPopup(InputState input)
    {
      foreach (GestureSample gesture in input.Gestures)
      {
        if (((double) gesture.Position.X <= (double) this._root.Children[0].AbsolutePosition.X || (double) gesture.Position.X >= (double) this._root.Children[0].AbsolutePosition.X + (double) this._root.Children[0].Size.X || (double) gesture.Position.Y <= (double) this._root.Children[0].AbsolutePosition.Y || (double) gesture.Position.Y >= (double) this._root.Children[0].AbsolutePosition.Y + (double) this._root.Children[0].Size.Y) && gesture.GestureType == GestureType.Tap)
          this.Close();
      }
    }
  }
}
