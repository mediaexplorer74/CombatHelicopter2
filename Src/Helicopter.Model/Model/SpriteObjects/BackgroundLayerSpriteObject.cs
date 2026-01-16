// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.BackgroundLayerSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class BackgroundLayerSpriteObject
  {
    private Rectangle _destRectLeftPart;
    private Rectangle _destRectRightPart;
    private BackgroundLayer _layer;
    private Point _offset;
    private Rectangle _sourceRectLeftPart;
    private Rectangle _sourceRectRightPart;

    public Sprite Sprite { get; set; }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Sprite.Texture, this._destRectLeftPart, new Rectangle?(this._sourceRectLeftPart), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
      spriteBatch.Draw(this.Sprite.Texture, this._destRectRightPart, new Rectangle?(this._sourceRectRightPart), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
    }

    public virtual void Update(Camera camera, float elapsedSeconds)
    {
      float left = (float) camera.Screen.Left;
      this._offset.Y = (int) ((double) this._layer.Position.Y - (double) camera.Screen.Y);
      this._offset.X = (int) ((double) this._layer.Position.X - (double) left * (1.0 - (double) this._layer.Remoteness)) % camera.Screen.Width;
      this._destRectLeftPart.X = 0;
      this._destRectLeftPart.Y = this._offset.Y;
      this._destRectLeftPart.Width = this.Sprite.Bounds.Width + this._offset.X;
      this._destRectLeftPart.Height = this.Sprite.Bounds.Height;
      this._sourceRectLeftPart.X = this.Sprite.SourceRectangle.X + Math.Abs(this._offset.X);
      this._sourceRectLeftPart.Width = this.Sprite.Bounds.Width - Math.Abs(this._offset.X);
      this._destRectRightPart.X = this.Sprite.Bounds.Width - Math.Abs(this._offset.X);
      this._destRectRightPart.Y = this._offset.Y;
      this._destRectRightPart.Width = Math.Abs(this._offset.X);
      this._destRectRightPart.Height = this.Sprite.Bounds.Height;
      this._sourceRectRightPart.Width = Math.Abs(this._offset.X);
    }

    public void Init(BackgroundLayer layer)
    {
      this._layer = layer;
      this.Sprite = ResourcesManager.Instance.GetSprite(layer.TexturePath);
      this._sourceRectLeftPart = this._sourceRectRightPart = this.Sprite.SourceRectangle;
      this._offset = new Point();
    }
  }
}
