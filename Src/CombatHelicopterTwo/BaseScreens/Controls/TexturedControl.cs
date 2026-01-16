// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.TexturedControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class TexturedControl : BasicControl
  {
    private Color _color;

    public TexturedControl(Sprite sprite, Vector2 position)
    {
      this.Sprite = sprite;
      this.Size = sprite.SourceSize;
      this.Position = position;
    }

    public virtual Sprite Sprite { get; set; }

    public Vector2 ImageSize
    {
      get => this.Sprite.ScaledSize;
      set => this.Sprite.Scale = value / this.Sprite.SourceSize;
    }

    public Color Color
    {
      get => this._color;
      set
      {
        this._color = value;
        this.Sprite.Color = value;
      }
    }

    public Vector2 Origin
    {
      get => this.Sprite.Origin;
      set => this.Sprite.Origin = value;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.Visible)
        return;
      this.Sprite.Draw(context.SpriteBatch, context.DrawOffset);
      base.Draw(context);
    }
  }
}
