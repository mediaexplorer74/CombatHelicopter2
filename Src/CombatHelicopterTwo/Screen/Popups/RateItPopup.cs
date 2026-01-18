// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.Popups.RateItPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items.Ammunition;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Windows.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace Helicopter.Screen.Popups
{
  internal class RateItPopup : GameScreen
  {
    private readonly BasicControl _root = new BasicControl();

    public RateItPopup() => this.IsPopup = true;

    public override void LoadContent()
    {
      base.LoadContent();
      this._root.Size = new Vector2(800f, 480f);
      Sprite sprite = ResourcesManager.Instance.GetSprite("PopUpWindow/rateItPopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite, new Vector2((float) (400 - sprite.Bounds.Width / 2), (float) (240 - sprite.Bounds.Height / 2))));
      MenuControl child1 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butRateIt"), ResourcesManager.Instance.GetSprite("PopUpWindow/butRateItSelect"), new Vector2(488f, 345f));
      child1.Clicked += new EventHandler<EventArgs>(this.OnRateItClicked);
      this._root.AddChild((BasicControl) child1);
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butLater"), ResourcesManager.Instance.GetSprite("PopUpWindow/butLaterSelect"), new Vector2(169f, 345f));
      child2.Clicked += new EventHandler<EventArgs>(this.OnLaterClicked);
      this._root.AddChild((BasicControl) child2);
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition14");
      Color color = new Color(254, 224, 23);
      this._root.AddChild((BasicControl) new TextControl("please, rate our game \n and get bonus", resource, color, new Vector2(400f, 189f))
      {
        Origin = new Vector2(0.5f, 0.0f)
      });
      PanelControl child3 = new PanelControl();
      TexturedControl child4 = new TexturedControl(ResourcesManager.Instance.GetSprite("PopUpWindow/creditsBoundle"), Vector2.Zero);
      child4.AddChild((BasicControl) new TextControl("&2000", resource, color, new Vector2(41f, 75f))
      {
        Origin = new Vector2(0.5f, 0.0f)
      });
      child3.AddChild((BasicControl) child4);
      TexturedControl child5 = new TexturedControl(ResourcesManager.Instance.GetSprite("Hangar/Items/Amunition color/itemHealth"), Vector2.Zero);
      child5.AddChild((BasicControl) new TextControl("BONUS".ToLower(), resource, color, new Vector2(37f, 75f))
      {
        Origin = new Vector2(0.5f, 0.0f)
      });
      child3.AddChild((BasicControl) child5);
      child3.LayoutRow(0.0f, 240f, 50f);
      child3.ComputeSize();
      child3.Position = new Vector2((float) (400.0 - (double) child3.Size.X / 2.0), child3.Position.Y);
      this._root.AddChild((BasicControl) child3);
    }

    public override void OnBackButton() => this.Close();

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
      this.TapOutPopup(input);
    }

    private void TapOutPopup(InputState input)
    {
      foreach (GestureSample gesture in input.Gestures)
      {
        if (((double) gesture.Position.X <= (double) this._root.Children[0].AbsolutePosition.X || (double) gesture.Position.X >= (double) this._root.Children[0].AbsolutePosition.X + (double) this._root.Children[0].Size.X || (double) gesture.Position.Y <= (double) this._root.Children[0].AbsolutePosition.Y || (double) gesture.Position.Y >= (double) this._root.Children[0].AbsolutePosition.Y + (double) this._root.Children[0].Size.Y) && gesture.GestureType == GestureType.Tap)
          this.Close();
      }
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

    private void OnLaterClicked(object sender, EventArgs e) => this.Close();

    private void OnRateItClicked(object sender, EventArgs e)
    {
      Gamer.Instance.Money.AddMoney(2000f);
      RateItPopup.GiftHealthPack();
      //Launcher.LaunchUriAsync(new Uri("ms-windows-store://home"));
      SettingsGame.IsRateIted = true;
      this.Close();
    }

    private void Close() => this.ExitScreen();

    public static void GiftHealthPack()
    {
      AmmunitionItem healthPack100 = AmmunitionFactory.GetHealthPack100();
      float num;
      switch (healthPack100.Type)
      {
        case AmunitionType.HealthPack50:
          num = 0.5f;
          break;
        case AmunitionType.HealthPack100:
          num = 1f;
          break;
        default:
          throw new Exception(string.Format("Unknown Health Amunition Type '{0}'.", (object) healthPack100.Type));
      }
      if (!Gamer.Instance.HealthBonus.IsInstalled)
        Gamer.Instance.HealthBonus.Item = new HealthAmmunitionItem();
      Gamer.Instance.HealthBonus.Item.Volume += num;
      Gamer.Instance.HealthBonus.Item.HangarDesc = healthPack100.HangarDesc;
    }
  }
}
