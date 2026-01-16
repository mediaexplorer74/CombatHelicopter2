// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.FireControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class FireControl : Control
  {
    private Weapon _weapon;
    private Sprite _reloadTexture;
    private Sprite _textureWithoutBorder;
    private SpriteCircle _reloadCircle;

    public FireControl(
      Weapon weapon,
      Sprite texture,
      Sprite textureWithoutBorder,
      Sprite texturePressed,
      Sprite reloadTexture,
      Vector2 position,
      Rectangle contactArea)
      : base(texture, position, contactArea)
    {
      this._weapon = weapon;
      this._reloadTexture = reloadTexture;
      if (this._reloadTexture != null)
        this._reloadCircle = new SpriteCircle(this._reloadTexture.Texture.GraphicsDevice, this._reloadTexture, new Rectangle((int) this.Position.X - 1, (int) this.Position.Y - 1, reloadTexture.Bounds.Width, reloadTexture.Bounds.Height));
      if (textureWithoutBorder != null)
        this._textureWithoutBorder = textureWithoutBorder;
      this.PressedSprite = texturePressed;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.IsVisible)
        return;
      if (!this.IsPressed || this.PressedSprite == null)
      {
        if (this._textureWithoutBorder != null)
          this._textureWithoutBorder.Draw(context.SpriteBatch, this.Position);
        else
          this.Sprite.Draw(context.SpriteBatch, this.Position);
      }
      else
        this.PressedSprite.Draw(context.SpriteBatch, this.Position);
      if (this._reloadTexture == null)
        return;
      if ((double) this._weapon.WeaponReloadStatus >= 0.99900001287460327)
      {
        this._reloadTexture.Draw(context.SpriteBatch, this.Position);
      }
      else
      {
        context.SpriteBatch.End();
        this._reloadCircle.DrawCircle(this._weapon.WeaponReloadStatus);
        context.SpriteBatch.Begin();
      }
    }
  }
}
