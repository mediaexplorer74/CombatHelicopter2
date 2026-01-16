// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.Popups.DailyBonusPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items.Ammunition;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace Helicopter.Screen.Popups
{
  internal class DailyBonusPopup : GameScreen
  {
    private readonly BasicControl _root = new BasicControl();
    private int _currentDay = 1;

    public DailyBonusPopup(int currentDay)
    {
      this.IsPopup = true;
      this._currentDay = currentDay;
    }

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

    public override void LoadContent()
    {
      base.LoadContent();
      this._root.Size = new Vector2(800f, 480f);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/dailyBonusPopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2 - 3), (float) (240 - sprite1.Bounds.Height / 2))));
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("PopUpWindow/butOk");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("PopUpWindow/butOkSelect");
      MenuControl child1 = new MenuControl(sprite2, sprite3, new Vector2((float) (400.0 - (double) sprite2.Bounds.Width / 2.0), 345f));
      child1.Clicked += new EventHandler<EventArgs>(this.OnOkClicked);
      this._root.AddChild((BasicControl) child1);
      SpriteFont resource1 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition14");
      SpriteFont resource2 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition10");
      Color color = new Color(254, 224, 23);
      this._root.AddChild((BasicControl) new TextControl("simply visiting the game daily", resource1, color, new Vector2(400f, 185f))
      {
        Origin = new Vector2(0.5f, 0.0f)
      });
      PanelControl child2 = new PanelControl();
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("PopUpWindow/creditsBoundle");
      float num1 = this._currentDay >= 1 ? 1f : 0.5f;
      TexturedControl child3 = new TexturedControl(sprite4, Vector2.Zero)
      {
        Color = Color.White * num1
      };
      child3.AddChild((BasicControl) new TextControl("&1000", resource1, color, new Vector2(36f, 75f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Color = (color * num1)
      });
      child3.AddChild((BasicControl) new TextControl("Day 1".ToLower(), resource2, color, new Vector2(36f, 95f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Color = (color * num1)
      });
      child2.AddChild((BasicControl) child3);
      float num2 = this._currentDay >= 2 ? 1f : 0.5f;
      TexturedControl child4 = new TexturedControl(sprite4, Vector2.Zero)
      {
        Color = Color.White * num2
      };
      child4.AddChild((BasicControl) new TextControl("&2000", resource1, color, new Vector2(36f, 75f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Color = (color * num2)
      });
      child4.AddChild((BasicControl) new TextControl("Day 2".ToLower(), resource2, color, new Vector2(36f, 95f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Color = (color * num2)
      });
      child2.AddChild((BasicControl) child4);
      float num3 = this._currentDay >= 3 ? 1f : 0.5f;
      TexturedControl child5 = new TexturedControl(ResourcesManager.Instance.GetSprite("Hangar/Items/Amunition color/itemHealth"), Vector2.Zero)
      {
        Color = Color.White * num3
      };
      child5.AddChild((BasicControl) new TextControl("BONUS".ToLower(), resource1, color, new Vector2(32f, 75f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Color = (color * num3)
      });
      child5.AddChild((BasicControl) new TextControl("Day 3".ToLower(), resource2, color, new Vector2(32f, 95f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Color = (color * num3)
      });
      child2.AddChild((BasicControl) child5);
      child2.LayoutRow(0.0f, 0.0f, 70f);
      child2.Position = new Vector2((float) (400.0 - (double) child2.Size.X / 2.0), 220f);
      this._root.AddChild((BasicControl) child2);
    }

    public override void OnBackButton() => this.Close();

    private void OnOkClicked(object sender, EventArgs e) => this.Close();

    private void Close()
    {
      this.ExitScreen();
      switch (this._currentDay)
      {
        case 1:
          Gamer.Instance.Money.AddMoney(1000f);
          break;
        case 2:
          Gamer.Instance.Money.AddMoney(2000f);
          break;
        case 3:
          this.GiftHealthPack();
          break;
      }
    }

    private void GiftHealthPack()
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
