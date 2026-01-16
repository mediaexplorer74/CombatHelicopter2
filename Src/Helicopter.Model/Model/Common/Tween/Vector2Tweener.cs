// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Common.Tween.Vector2Tweener
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public class Vector2Tweener : Tweener
  {
    private Vector2 _fromVector2;
    private Vector2 _toVector2;

    public Vector2 CurrentPosition
    {
      get
      {
        return new Vector2(this._fromVector2.X + (this._toVector2.X - this._fromVector2.X) * this.Position, this._fromVector2.Y + (this._toVector2.Y - this._fromVector2.Y) * this.Position);
      }
    }

    public float Percent => this.Position;

    public Vector2Tweener(Vector2 from, Vector2 to, float time, TweeningFunction tweeningFunction)
      : base(0.0f, 1f, time, tweeningFunction)
    {
      this._fromVector2 = from;
      this._toVector2 = to;
    }

    public void Init(Vector2 from, Vector2 to, float duration, TweeningFunction tweeningFunction)
    {
      this._fromVector2 = from;
      this._toVector2 = to;
      this._tweeningFunction = tweeningFunction;
      this._duration = duration;
      this.Reset();
    }
  }
}
