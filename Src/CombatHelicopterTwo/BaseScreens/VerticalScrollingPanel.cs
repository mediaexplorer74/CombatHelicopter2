// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.VerticalScrollingPanel
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.BaseScreens
{
  internal class VerticalScrollingPanel : PanelControl
  {
    private readonly ScrollTracker _scrollTracker = new ScrollTracker();

    public int TopVisibleRecord { get; private set; }

    public int BottomVisibleRecord { get; private set; }

    public event EventHandler TopExceeded
    {
      add => this._scrollTracker.TopExceeded += value;
      remove => this._scrollTracker.TopExceeded -= value;
    }

    public event EventHandler BottomExceeded
    {
      add => this._scrollTracker.BottomExceeded += value;
      remove => this._scrollTracker.BottomExceeded -= value;
    }

    public VerticalScrollingPanel()
    {
      this._scrollTracker.IsVertical = true;
      if (this.Children != null)
        return;
      this.Children = new List<BasicControl>();
    }

    public override void Update(GameTime gametime)
    {
      Vector2 size = this.ComputeSize();
      this._scrollTracker.CanvasRect.X = (int) this.Position.X;
      this._scrollTracker.CanvasRect.Y = (int) this.Position.Y;
      this._scrollTracker.CanvasRect.Width = (int) size.X;
      this._scrollTracker.CanvasRect.Height = (int) size.Y;
      this._scrollTracker.Update(gametime);
      base.Update(gametime);
    }

    public override void HandleInput(InputState input)
    {
      this.touchPositionOffset = new Vector2((float) this.ViewRect.X, (float) this.ViewRect.Y);
      if (this.Children != null)
        this.Children.ForEach((Action<BasicControl>) (x => x.touchPositionOffset = this.touchPositionOffset));
      this._scrollTracker.HandleInput(input);
      base.HandleInput(input);
    }

    public override void Draw(DrawContext context)
    {
      context.DrawOffset.X -= (float) this._scrollTracker.ViewRect.X;
      context.DrawOffset.Y -= (float) this._scrollTracker.ViewRect.Y;
      Vector2 drawOffset = context.DrawOffset;
      float y = this.Position.Y;
      float num = this.Position.Y + (float) this.ViewRect.Height;
      this.TopVisibleRecord = -1;
      this.BottomVisibleRecord = -1;
      for (int index = 0; index < this.ChildCount; ++index)
      {
        BasicControl child = this.Children[index];
        context.DrawOffset = drawOffset + child.Position;
        Rectangle rectangle = new Rectangle((int) context.DrawOffset.X, (int) context.DrawOffset.Y, (int) child.Size.X, (int) child.Size.Y);
        if (child.Visible && (double) rectangle.Y >= (double) y && (double) rectangle.Bottom < (double) num + 5.0)
        {
          child.Draw(context);
          if (this.TopVisibleRecord == -1)
            this.TopVisibleRecord = index;
          this.BottomVisibleRecord = index;
        }
      }
    }

    public Rectangle ViewRect
    {
      get => this._scrollTracker.ViewRect;
      set => this._scrollTracker.ViewRect = value;
    }

    public Vector2 Velocity
    {
      get => this._scrollTracker.Velocity;
      set => this._scrollTracker.Velocity = value;
    }

    public void ShowTop() => this._scrollTracker.ScrollToPostion(0);

    public void ShowRecord(int recordNumber, bool center)
    {
      if (this.Children.Count < recordNumber)
        return;
      BasicControl child = this.Children[recordNumber];
      float num = center ? (float) (((double) this._scrollTracker.ViewRect.Height - (double) child.Size.Y) / 2.0) : 0.0f;
      float yPos = child.Position.Y - num;
      if ((double) this.Children.Count * (double) child.Size.Y < (double) this._scrollTracker.ViewRect.Height)
        yPos = 0.0f;
      this._scrollTracker.ScrollToPostion((int) yPos);
    }

    public void ShowBottom()
    {
      this._scrollTracker.ScrollToPostion((int) Math.Max(this.Size.Y - (float) this._scrollTracker.ViewRect.Height, 0.0f));
    }
  }
}
