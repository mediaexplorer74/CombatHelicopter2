// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.Hangar.InAppPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Analytics;
using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Screen.Popups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Screen.Hangar
{
  internal class InAppPopup : GameScreen
  {
    private readonly Color _colorGreen = new Color(174, 248, 69);
    private readonly Color _colorYellow = new Color(254, 224, 23);
    private SpriteFont _font;
    private BasicControl _root;
    private Sprite _buyTexture;
    private Sprite _buyTextureSelect;

    public InAppPopup() => this.IsPopup = true;

    public override void LoadContent()
    {
      base.LoadContent();
      this._font = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition15");
      this._root = new BasicControl();
      this._root.Size = new Vector2(800f, 480f);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/buyCreditsPopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2), (float) (240 - sprite1.Bounds.Height / 2))));
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("PopUpWindow/butOk");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("PopUpWindow/butOkSelect");
      MenuControl child1 = new MenuControl(sprite2, sprite3, new Vector2((float) (400.0 - (double) sprite2.Bounds.Width / 2.0), 345f));
      child1.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnBackButton());
      this._buyTexture = ResourcesManager.Instance.GetSprite("PopUpWindow/butBuy");
      this._buyTextureSelect = ResourcesManager.Instance.GetSprite("PopUpWindow/butBuySelect");
      TextControl child2 = new TextControl("you can also earn credits \nin survival mode", this._font, this._colorYellow * 0.5f)
      {
        Centered = true,
        Origin = new Vector2(0.5f, 0.0f),
        Scale = 0.8f
      };
      child2.Position = new Vector2(400f, 190f);
      this._root.AddChild((BasicControl) child2);
      this.CreateInAppList();
      this._root.AddChild((BasicControl) child1);
    }

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

    public override void OnBackButton() => this.Close();

    private void Close() => this.ExitScreen();

    private BasicControl CreateInApp(int creditNumber, float price, string productID)
    {
      PanelControl inApp = new PanelControl();
      TextControl child1 = new TextControl(string.Format("& {0,-5} = $ {1,5:N}", (object) creditNumber, (object) price), this._font, this._colorGreen)
      {
        Origin = new Vector2(0.5f, 0.0f),
        CenteredX = true
      };
      child1.Position = new Vector2(190f, 0.0f);
      inApp.AddChild((BasicControl) child1);
      MenuControl child2 = new MenuControl(this._buyTexture, this._buyTextureSelect, Vector2.Zero);
      child2.Clicked += (EventHandler<EventArgs>) ((x, y) =>
      {
        ConfirmPurchasePopUp screen = new ConfirmPurchasePopUp();
        screen.Confitm += (EventHandler) delegate
        {
          this.NotifyBuyInApp(creditNumber);
          this.ScreenManager.Game.InvokeShopNeeded(productID);
        };
        this.ScreenManager.AddScreen((GameScreen) screen);
      });
      child2.Position = new Vector2(320f, 0.0f);
      inApp.AddChild((BasicControl) child2);
      child1.Position = new Vector2(child1.Position.X, child1.Position.Y + 8f);
      return (BasicControl) inApp;
    }

    private void CreateInAppList()
    {
      PanelControl child = new PanelControl();
      BasicControl inApp = this.CreateInApp(5000, 1f, "1040654");
      child.AddChild(inApp);
      child.AddChild(this.CreateInApp(12000, 1.99f, "1040655"));
      child.AddChild(this.CreateInApp(25000, 2.99f, "1040656"));
      child.AddChild(this.CreateInApp(45000, 3.99f, "1040657"));
      child.LayoutColumn(200f, 205f, 2f);
      inApp.Children[0].Position = new Vector2(inApp.Children[0].Position.X + 7f, inApp.Children[0].Position.Y);
      this._root.AddChild((BasicControl) child);
    }

    private void NotifyBuyInApp(int creditNumber)
    {
      Api.LogEvent(FlurryEvents.BuyInApp, new List<Parameter>()
      {
        ParametersFactory.GetInAppParam(creditNumber)
      });
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
