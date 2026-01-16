// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.ScrollingPanelControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class ScrollingPanelControl : PanelControl
  {
    private ScrollTracker scrollTracker = new ScrollTracker();

    public override void Update(GameTime gametime)
    {
      Vector2 size = this.ComputeSize();
      this.scrollTracker.CanvasRect.Width = (int) size.X;
      this.scrollTracker.CanvasRect.Height = (int) size.Y;
      this.scrollTracker.Update(gametime);
      base.Update(gametime);
    }

    public override void HandleInput(InputState input)
    {
      this.touchPositionOffset = new Vector2((float) this.ViewRect.X, (float) this.ViewRect.Y);
      this.Children.ForEach((Action<BasicControl>) (x => x.touchPositionOffset = this.touchPositionOffset));
      this.scrollTracker.HandleInput(input);
      base.HandleInput(input);
    }

    public override void Draw(DrawContext context)
    {
      context.DrawOffset.X = (float) -this.scrollTracker.ViewRect.X;
      context.DrawOffset.Y = (float) -this.scrollTracker.ViewRect.Y;
      base.Draw(context);
    }

    public Rectangle ViewRect
    {
      get => this.scrollTracker.ViewRect;
      set => this.scrollTracker.ViewRect = value;
    }

    public bool IsVertical
    {
      get => this.scrollTracker.IsVertical;
      set => this.scrollTracker.IsVertical = value;
    }

    public bool IsHorizontal
    {
      get => this.scrollTracker.IsHorizontal;
      set => this.scrollTracker.IsHorizontal = value;
    }

    public Vector2 Velocity
    {
      get => this.scrollTracker.Velocity;
      set => this.scrollTracker.Velocity = value;
    }
  }
}
