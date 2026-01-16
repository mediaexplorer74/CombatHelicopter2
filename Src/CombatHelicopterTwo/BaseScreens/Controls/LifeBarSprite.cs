// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.LifeBarSprite
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class LifeBarSprite : BasicControl
  {
    protected readonly Sprite _background;
    protected readonly Sprite _progressTexture;
    protected readonly Sprite _progressLine;
    protected Rectangle _elementArea;
    protected Rectangle _elementAreaDestination;
    protected double _full;
    protected Rectangle _textureSource;
    protected Rectangle _textureSourceCurrent;

    public virtual double Full
    {
      get => this._full;
      set
      {
        this._full = value > 1.0 ? 1.0 : value;
        this._full = this._full > 0.0 ? this._full : 0.0;
        this._elementAreaDestination.Width = (int) ((double) (this._elementArea.Width - 20) * this.Full);
        this._progressTexture.SourceRectangle.Width = (int) ((double) this._textureSourceCurrent.Width * this.Full);
      }
    }

    public LifeBarSprite(
      Rectangle destination,
      Sprite progressTexture,
      Sprite background,
      Sprite progressLine)
    {
      this._progressTexture = progressTexture;
      this._background = background;
      this._progressLine = progressLine;
      this._progressLine.Origin = this._progressLine.SourceSize / 2f;
      this._textureSourceCurrent = this._textureSource = this._progressTexture.SourceRectangle;
      this._textureSourceCurrent.X = 20;
      this._elementAreaDestination = this._elementArea = destination;
      this.Full = 1.0;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.Visible)
        return;
      this.DrawBar(context.SpriteBatch);
    }

    protected virtual void DrawBar(SpriteBatch spriteBatch)
    {
      Vector2 destination1 = new Vector2((float) this._elementArea.X, (float) this._elementArea.Y);
      Vector2 destination2 = new Vector2((float) this._elementAreaDestination.X, (float) this._elementAreaDestination.Y);
      this._background.Draw(spriteBatch, destination1);
      this._progressTexture.Draw(spriteBatch, destination2);
      if (this.Full <= 0.15000000596046448 || this.Full >= 0.949999988079071)
        return;
      this._progressLine.Draw(spriteBatch, new Vector2(destination2.X + (float) this._progressTexture.SourceRectangle.Width, (float) this._elementAreaDestination.Center.Y));
    }
  }
}
