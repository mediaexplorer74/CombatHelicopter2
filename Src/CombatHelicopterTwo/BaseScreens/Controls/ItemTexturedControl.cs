// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.ItemTexturedControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.Ammunition;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class ItemTexturedControl : TexturedControl
  {
    public Helicopter.Items.Item Item;
    private Sprite _installed;
    private Sprite _locked;

    public Sprite Installed
    {
      get => this._installed;
      set
      {
        this._installed = value;
        if (this._installed == null)
          return;
        this._installed.Origin = this._installed.SourceSize / 2f;
      }
    }

    public Sprite Locked
    {
      get => this._locked;
      set
      {
        this._locked = value;
        if (this._locked == null)
          return;
        this._locked.Origin = this._locked.SourceSize / 2f;
      }
    }

    public ItemTexturedControl(Helicopter.Items.Item item, Sprite texture, Vector2 position)
      : base(texture, position)
    {
      this.Item = item;
    }

    public override void Draw(DrawContext context)
    {
      base.Draw(context);
      if (this.IsInstalled(this.Item) && this.Installed != null)
      {
        this.Installed.Draw(context.SpriteBatch, context.DrawOffset + this.Size / 2f);
        this.Color = Color.White * 0.5f;
      }
      else if (this.Locked != null && !this.Item.IsBought && !(this.Item is AmmunitionItem))
      {
        this.Locked.Draw(context.SpriteBatch, context.DrawOffset + this.Size / 2f);
        this.Color = Color.White * 0.0f;
      }
      else
        this.Color = Color.White;
    }

    protected bool IsInstalled(Helicopter.Items.Item item)
    {
      return item == Gamer.Instance.FirstWeapon.Item || item == Gamer.Instance.SecondWeapon.Item || item == Gamer.Instance.UpgradeA.Item || item == Gamer.Instance.UpgradeB.Item;
    }
  }
}
