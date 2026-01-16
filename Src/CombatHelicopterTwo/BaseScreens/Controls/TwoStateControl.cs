// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.TwoStateControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class TwoStateControl : BasicControl
  {
    private readonly Texture2D _firstState;
    private readonly Texture2D _secondState;

    public bool IsOn { get; set; }

    public TwoStateControl(Texture2D first, Texture2D second, Vector2 position, bool isOn)
    {
      this._firstState = first;
      this._secondState = second;
      this.IsOn = isOn;
      this.Position = position;
    }

    public override void Draw(DrawContext context)
    {
      context.SpriteBatch.Draw(this.IsOn ? this._secondState : this._firstState, context.DrawOffset, Color.White);
      base.Draw(context);
    }
  }
}
