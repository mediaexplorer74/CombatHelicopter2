// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.Tween.RectangleTweener
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public class RectangleTweener : Tweener
  {
    private Rectangle _fromRectangle;
    private Rectangle _toRectangle;

    public Rectangle CurrentPosition
    {
      get
      {
        return new Rectangle((int) ((double) this._fromRectangle.X + (double) (this._toRectangle.X - this._fromRectangle.X) * (double) this.Position), (int) ((double) this._fromRectangle.Y + (double) (this._toRectangle.Y - this._fromRectangle.Y) * (double) this.Position), (int) ((double) this._fromRectangle.Width + (double) (this._toRectangle.Width - this._fromRectangle.Width) * (double) this.Position), (int) ((double) this._fromRectangle.Height + (double) (this._toRectangle.Height - this._fromRectangle.Height) * (double) this.Position));
      }
    }

    public float Percent => this.Position;

    public RectangleTweener(
      Rectangle fromRectangle,
      Rectangle toRectangle,
      float time,
      TweeningFunction tweeningFunction)
      : base(0.0f, 1f, time, tweeningFunction)
    {
      this._fromRectangle = fromRectangle;
      this._toRectangle = toRectangle;
    }

    public void Init(
      Rectangle from,
      Rectangle to,
      float duration,
      TweeningFunction tweeningFunction)
    {
      this._fromRectangle = from;
      this._toRectangle = to;
      this._tweeningFunction = tweeningFunction;
      this._duration = duration;
      this.Reset();
    }
  }
}
