// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.GameplayPopups.NewAmunitionPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Items.DeviceItems;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class NewAmunitionPopup : BasePopup
  {
    private Item _item;
    private BasicControl _root;
    private Rectangle _infoRect = new Rectangle(171, 220, 457, 123);
    private string _bgTexturePath;
    private string _text;

    public NewAmunitionPopup()
    {
      this.IsPopup = true;
      this.Back += new EventHandler(this.OnBack);
    }

    public void InitNewWeapon(WeaponItem item)
    {
      this._item = (Item) item;
      this._bgTexturePath = "PopUpWindow/weaponPopUpBg";
      this._text = "NEW WEAPON AVALIABLE".ToLower();
      if (item.Slot == Slot.First)
        Gamer.Instance.HasNewFirstWeapon.Item = item;
      else
        Gamer.Instance.HasNewSecondWeapon.Item = item;
    }

    public void InitNewUpgrade(UpgradeItem item)
    {
      this._item = (Item) item;
      this._bgTexturePath = "PopUpWindow/upgradePopUpBg";
      this._text = "NEW Upgrade AVALIABLE".ToLower();
      Gamer.Instance.HasNewUpgrade.Item = item;
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
      Sprite sprite1 = ResourcesManager.Instance.GetSprite(this._bgTexturePath);
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2), (float) (245 - sprite1.Bounds.Height / 2))));
      BasicControl child1 = new BasicControl();
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butOk"), ResourcesManager.Instance.GetSprite("PopUpWindow/butOkSelect"), Vector2.Zero);
      child2.Clicked += new EventHandler<EventArgs>(this.OnOkButtonClicked);
      child1.AddChild((BasicControl) child2);
      child1.Position = new Vector2((float) (402.0 - (double) child1.Size.X / 2.0), 350f);
      this._root.AddChild(child1);
      if (this._item == null)
        return;
      Sprite sprite2 = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) this._item.HangarDesc).OnShopTexture);
      TexturedControl child3 = new TexturedControl(sprite2, Vector2.Zero);
      child3.ImageSize = child3.Size * 0.6f;
      child3.Origin = new Vector2((float) sprite2.Bounds.Width / 2f, (float) sprite2.Bounds.Height / 2f);
      child3.Position = new Vector2((float) this._infoRect.Center.X, (float) this._infoRect.Center.Y);
      this._root.AddChild((BasicControl) child3);
      TextControl child4 = new TextControl(this._text, ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition15"));
      Vector2 size1 = child4.ComputeSize();
      child4.Position = new Vector2((float) this._infoRect.X + (float) (((double) this._infoRect.Width - (double) size1.X) / 2.0), 228f);
      child4.Color = Color.Yellow;
      this._root.AddChild((BasicControl) child4);
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition11");
      TextControl child5 = new TextControl(this._item.Name.ToLower(), resource);
      Vector2 size2 = child5.ComputeSize();
      child5.Position = new Vector2((float) this._infoRect.X + (float) (((double) this._infoRect.Width - (double) size2.X) / 2.0), 324f);
      child5.Color = new Color(169, 162, 27);
      this._root.AddChild((BasicControl) child5);
    }
  }
}
