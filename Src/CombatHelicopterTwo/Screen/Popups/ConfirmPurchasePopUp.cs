// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.Popups.ConfirmPurchasePopUp
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Screen.Popups
{
  internal class ConfirmPurchasePopUp : GameScreen
  {
    private readonly BasicControl _root = new BasicControl();

    public event EventHandler Confitm;

    public void OnConfitm(EventArgs e)
    {
      EventHandler confitm = this.Confitm;
      if (confitm == null)
        return;
      confitm((object) this, e);
    }

    public ConfirmPurchasePopUp() => this.IsPopup = true;

    public override void LoadContent()
    {
      this._root.Size = new Vector2(800f, 480f);
      TexturedControl child1 = new TexturedControl(ResourcesManager.Instance.GetSprite("PopUpWindow/confirmPurchasePopUp"), Vector2.Zero);
      child1.Position = new Vector2(400f, 240f) - child1.Size / 2f;
      this._root.AddChild((BasicControl) child1);
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butYes"), ResourcesManager.Instance.GetSprite("PopUpWindow/butYesSelect"), new Vector2(484f, 345f));
      child2.Clicked += new EventHandler<EventArgs>(this.OnYesClicked);
      this._root.AddChild((BasicControl) child2);
      this._root.AddChild((BasicControl) new TextControl("are you sure you want \n to buy credits?", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition14"), new Color(254, 224, 23), new Vector2(400f, 260f))
      {
        Origin = new Vector2(0.5f, 0.0f)
      });
      MenuControl child3 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butNo"), ResourcesManager.Instance.GetSprite("PopUpWindow/butNoSelect"), new Vector2(166f, 345f));
      child3.Clicked += new EventHandler<EventArgs>(this.OnNoClicked);
      this._root.AddChild((BasicControl) child3);
      base.LoadContent();
    }

    public override void OnBackButton() => this.ExitScreen();

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
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

    private void OnNoClicked(object x, EventArgs y) => this.ExitScreen();

    private void OnYesClicked(object x, EventArgs y)
    {
      this.OnConfitm(EventArgs.Empty);
      this.ExitScreen();
    }
  }
}
