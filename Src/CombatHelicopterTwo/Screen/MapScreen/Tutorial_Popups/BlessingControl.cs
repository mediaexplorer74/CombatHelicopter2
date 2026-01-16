// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Tutorial_Popups.BlessingControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Screen.MapScreen.Tutorial_Popups
{
  internal class BlessingControl : TexturedControl
  {
    private readonly Tweener _alphaTweener;

    public BlessingControl(Sprite sprite, Vector2 position)
      : base(sprite, position)
    {
      this._alphaTweener = new Tweener(0.0f, 3.14159274f, 2f, new TweeningFunction(Linear.EaseIn));
      this._alphaTweener.Ended += (EventHandler<EventArgs>) ((x, y) =>
      {
        this._alphaTweener.Reset();
        this._alphaTweener.Start();
      });
    }

    public override void Update(GameTime gametime)
    {
      base.Update(gametime);
      this._alphaTweener.Update((float) gametime.ElapsedGameTime.TotalSeconds);
      this.Color = Color.White * (float) Math.Sin((double) this._alphaTweener.Position);
    }
  }
}
