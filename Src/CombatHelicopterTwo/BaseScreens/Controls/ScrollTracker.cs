// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.ScrollTracker
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class ScrollTracker
  {
    public const GestureType GesturesNeeded = GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.Flick | GestureType.DragComplete;
    private const float SpringMaxDrag = 400f;
    private const float SpringMaxOffset = 133.333328f;
    private const float SpringReturnRate = 0.1f;
    private const float SpringReturnMin = 2f;
    private const float Deceleration = 500f;
    private const float MaxVelocity = 2000f;
    private bool _exceededEventWasSend;
    public Rectangle CanvasRect;
    public Rectangle ViewRect;
    public Vector2 Velocity;
    private Vector2 ViewOrigin;
    private Vector2 UnclampedViewOrigin;

    public event EventHandler TopExceeded;

    private void OnTopExceeded(EventArgs e)
    {
      EventHandler topExceeded = this.TopExceeded;
      if (topExceeded == null)
        return;
      topExceeded((object) this, e);
    }

    public event EventHandler BottomExceeded;

    private void OnBottomExceeded(EventArgs e)
    {
      EventHandler bottomExceeded = this.BottomExceeded;
      if (bottomExceeded == null)
        return;
      bottomExceeded((object) this, e);
    }

    public Rectangle FullCanvasRect
    {
      get
      {
        Rectangle canvasRect = this.CanvasRect;
        if (canvasRect.Width < this.ViewRect.Width)
          canvasRect.Width = this.ViewRect.Width;
        if (canvasRect.Height < this.ViewRect.Height)
          canvasRect.Height = this.ViewRect.Height;
        return canvasRect;
      }
    }

    public bool IsTracking { get; private set; }

    public bool IsMoving
    {
      get
      {
        return this.IsTracking || (double) this.Velocity.X != 0.0 || (double) this.Velocity.Y != 0.0 || !this.FullCanvasRect.Contains(this.ViewRect);
      }
    }

    public ScrollTracker()
    {
      this.ViewRect = new Rectangle()
      {
        Width = TouchPanel.DisplayWidth,
        Height = TouchPanel.DisplayHeight
      };
      this.CanvasRect = this.ViewRect;
    }

    public void Update(GameTime gametime)
    {
      float totalSeconds = (float) gametime.ElapsedGameTime.TotalSeconds;
      Vector2 vector2_1 = new Vector2()
      {
        X = 0.0f,
        Y = 0.0f
      };
      Vector2 vector2_2 = new Vector2()
      {
        X = (float) (this.CanvasRect.Width - this.ViewRect.Width),
        Y = (float) (this.CanvasRect.Height - this.ViewRect.Height)
      };
      vector2_2.X = Math.Max(vector2_1.X, vector2_2.X);
      vector2_2.Y = Math.Max(vector2_1.Y, vector2_2.Y);
      if (this.IsTracking)
      {
        this.ViewOrigin.X = this.SoftClamp(this.UnclampedViewOrigin.X, vector2_1.X, vector2_2.X);
        this.ViewOrigin.Y = this.SoftClamp(this.UnclampedViewOrigin.Y, vector2_1.Y, vector2_2.Y);
      }
      else
      {
        this.ApplyVelocity(totalSeconds, ref this.ViewOrigin.X, ref this.Velocity.X, vector2_1.X, vector2_2.X);
        this.ApplyVelocity(totalSeconds, ref this.ViewOrigin.Y, ref this.Velocity.Y, vector2_1.Y, vector2_2.Y);
      }
      this.ViewRect.X = (int) this.ViewOrigin.X;
      this.ViewRect.Y = (int) this.ViewOrigin.Y;
    }

    public void HandleInput(InputState input)
    {
      if (!this.IsTracking)
      {
        for (int index = 0; index < input.TouchState.Count; ++index)
        {
          if (input.TouchState[index].State == TouchLocationState.Pressed)
          {
            this.UnclampedViewOrigin = this.ViewOrigin;
            break;
          }
        }
      }
      foreach (GestureSample gesture in input.Gestures)
      {
        switch (gesture.GestureType)
        {
          case GestureType.HorizontalDrag:
            if (this.IsHorizontal)
            {
              this.UnclampedViewOrigin.X -= gesture.Delta.X;
              this.IsTracking = true;
              continue;
            }
            continue;
          case GestureType.VerticalDrag:
            if (this.IsVertical)
            {
              this.UnclampedViewOrigin.Y -= gesture.Delta.Y;
              this.IsTracking = true;
              continue;
            }
            continue;
          case GestureType.FreeDrag:
            if (this.IsHorizontal && this.IsVertical)
            {
              this.UnclampedViewOrigin -= gesture.Delta;
              this.IsTracking = true;
              continue;
            }
            continue;
          case GestureType.Flick:
            if (this.IsHorizontal && (double) Math.Abs(gesture.Delta.X) > (double) Math.Abs(gesture.Delta.Y))
            {
              this.IsTracking = false;
              this.Velocity = -gesture.Delta;
              this.Velocity.Y = 0.0f;
            }
            if (this.IsVertical && (double) Math.Abs(gesture.Delta.X) < (double) Math.Abs(gesture.Delta.Y))
            {
              this.IsTracking = false;
              this.Velocity = -gesture.Delta;
              this.Velocity.X = 0.0f;
            }
            if (this.IsHorizontal && this.IsVertical)
            {
              this.IsTracking = false;
              this.Velocity = -gesture.Delta;
              continue;
            }
            continue;
          case GestureType.DragComplete:
            this.IsTracking = false;
            continue;
          default:
            continue;
        }
      }
    }

    public bool IsVertical { get; set; }

    public bool IsHorizontal { get; set; }

    private float SoftClamp(float x, float min, float max)
    {
      if ((double) x < (double) min)
      {
        if (!this._exceededEventWasSend)
        {
          this.OnTopExceeded(EventArgs.Empty);
          this._exceededEventWasSend = true;
        }
        return (float) ((double) Math.Max(x - min, -400f) * 133.33332824707031 / 400.0) + min;
      }
      if ((double) x > (double) max)
      {
        if (!this._exceededEventWasSend)
        {
          this.OnBottomExceeded(EventArgs.Empty);
          this._exceededEventWasSend = true;
        }
        return (float) ((double) Math.Min(x - max, 400f) * 133.33332824707031 / 400.0) + max;
      }
      this._exceededEventWasSend = false;
      return x;
    }

    private void ApplyVelocity(float dt, ref float x, ref float v, float min, float max)
    {
      x += v * dt;
      v = MathHelper.Clamp(v, -2000f, 2000f);
      v = Math.Max(Math.Abs(v) - dt * 500f, 0.0f) * (float) Math.Sign(v);
      if ((double) x < (double) min)
      {
        x = Math.Min((float) ((double) x + ((double) min - (double) x) * 0.10000000149011612 + 2.0), min);
        v = 0.0f;
      }
      if ((double) x <= (double) max)
        return;
      x = Math.Max((float) ((double) x - ((double) x - (double) max) * 0.10000000149011612 - 2.0), max);
      v = 0.0f;
    }

    public void ScrollToPostion(int yPos)
    {
      this.ViewRect.Y = yPos;
      this.ViewOrigin.Y = (float) yPos;
    }
  }
}
