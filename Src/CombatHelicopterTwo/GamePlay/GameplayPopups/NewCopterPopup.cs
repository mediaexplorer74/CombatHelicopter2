// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.GameplayPopups.NewCopterPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class NewCopterPopup : BasePopup
  {
    private readonly HelicopterItem _item;
    private BasicControl _root;
    private Rectangle _infoRect = new Rectangle(171, 220, 457, 123);

    public NewCopterPopup(HelicopterItem item)
    {
      this.IsPopup = true;
      this._item = item;
      Gamer.Instance.HasNewCopter.Item = item;
      Gamer.Instance.CurrentHelicopter.Item = item;
      this.Back += new EventHandler(this.OnBack);
    }

    public override void Draw(DrawContext drawContext)
    {
      if (!this.OnTop)
        return;
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    private void OnBack(object sender, EventArgs e) => this.ExitScreen();

    private void OnOkButtonClicked(object sender, EventArgs e) => this.ExitScreen();

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      this._root.HandleInput(input);
    }

    public override void LoadContent()
    {
      this._root = new BasicControl();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/NewCopter/helicopterPopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2), (float) (245 - sprite1.Bounds.Height / 2))));
      BasicControl child1 = new BasicControl();
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butOk"), ResourcesManager.Instance.GetSprite("PopUpWindow/butOkSelect"), Vector2.Zero);
      child2.Clicked += new EventHandler<EventArgs>(this.OnOkButtonClicked);
      child1.AddChild((BasicControl) child2);
      child1.Position = new Vector2((float) (402.0 - (double) child1.Size.X / 2.0), 350f);
      this._root.AddChild(child1);
      if (this._item == null)
        return;
      string texturePath;
      switch (this._item.HelicopterType)
      {
        case HelicopterType.Viper:
          texturePath = "PopUpWindow/NewCopter/copter_1";
          break;
        case HelicopterType.Harbinger:
          texturePath = "PopUpWindow/NewCopter/copter_2";
          break;
        case HelicopterType.Avenger:
          texturePath = "PopUpWindow/NewCopter/copter_3";
          break;
        case HelicopterType.GrimReaper:
          texturePath = "PopUpWindow/NewCopter/copter_4";
          break;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Unknown helicopter type '{0}'", (object) this._item.HelicopterType));
      }
      Sprite sprite2 = ResourcesManager.Instance.GetSprite(texturePath);
      TexturedControl child3 = new TexturedControl(sprite2, Vector2.Zero);
      child3.Origin = sprite2.SourceSize / 2f;
      child3.Position = new Vector2((float) this._infoRect.Center.X, (float) (this._infoRect.Center.Y - 6));
      this._root.AddChild((BasicControl) child3);
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition11");
      TextControl child4 = new TextControl(this._item.Name.ToLower(), resource);
      Vector2 size = child4.ComputeSize();
      child4.Position = new Vector2((float) this._infoRect.X + (float) (((double) this._infoRect.Width - (double) size.X) / 2.0), 324f);
      child4.Color = new Color(169, 162, 27);
      this._root.AddChild((BasicControl) child4);
    }
  }
}
