// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.Control
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class Control : BasicControl
  {
    private const float MaxTimeWithoutTouch = 0.1f;
    public Rectangle ContactArea;
    private float _timeWithoutTouch;

    public event EventHandler<EventArgs> Down;

    public event EventHandler<EventArgs> Up;

    public Sprite Sprite { get; private set; }

    public bool IsPressed { get; private set; }

    public bool IsEnabled { get; set; }

    public virtual bool IsVisible { get; set; }

    public List<int> Touches { get; private set; }

    public Sprite PressedSprite { get; set; }

    public Control()
    {
      this.IsEnabled = true;
      this.IsVisible = true;
      this.Touches = new List<int>();
    }

    public Control(Sprite texture, Vector2 position)
      : this()
    {
      this.Sprite = texture;
      this.Position = position;
      this.ContactArea = new Rectangle((int) position.X, (int) position.Y, texture.Bounds.Width, texture.Bounds.Height);
    }

    public Control(Sprite texture, Vector2 position, Rectangle contactArea)
      : this(texture, position)
    {
      this.ContactArea = contactArea;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.IsVisible)
        return;
      if (!this.IsPressed || this.PressedSprite == null)
        this.Sprite.Draw(context.SpriteBatch, this.Position);
      else
        this.PressedSprite.Draw(context.SpriteBatch, this.Position);
    }

    public virtual void RaiseDownEvent()
    {
      if (!this.IsPressed && this.Down != null)
        this.Down((object) this, EventArgs.Empty);
      this.IsPressed = true;
    }

    public virtual void RaiseUpEvent()
    {
      if (this.IsPressed && this.Up != null)
        this.Up((object) this, EventArgs.Empty);
      this.IsPressed = false;
    }

    public override void HandleInput(InputState input)
    {
      foreach (TouchLocation touch in input.TouchState)
      {
        bool flag = false;
        if (this.ContactArea.Contains((int) touch.Position.X, (int) touch.Position.Y))
        {
          this._timeWithoutTouch = 0.0f;
          flag = true;
        }
        switch (touch.State)
        {
          case TouchLocationState.Invalid:
            continue;
          case TouchLocationState.Released:
            if (this.Touches.Contains(touch.Id))
            {
              this.UpControl();
              continue;
            }
            continue;
          case TouchLocationState.Pressed:
            if (this.ContactArea.Contains((int) touch.Position.X, (int) touch.Position.Y))
            {
              this.DownControl(touch);
              continue;
            }
            continue;
          case TouchLocationState.Moved:
            if (!this.IsPressed && flag && !this.Touches.Contains(touch.Id))
            {
              this.DownControl(touch);
              continue;
            }
            continue;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      base.HandleInput(input);
    }

    private void DownControl(TouchLocation touch)
    {
      if (!this.IsEnabled || !this.IsVisible)
        return;
      this.Touches.Add(touch.Id);
      Debug.WriteLine($"Control.Down id={touch.Id} pos={touch.Position.X:0},{touch.Position.Y:0} area=({this.ContactArea.X},{this.ContactArea.Y},{this.ContactArea.Width},{this.ContactArea.Height})");
      this.RaiseDownEvent();
    }

    private void UpControl()
    {
      this._timeWithoutTouch = 0.0f;
      this.Touches.Clear();
      Debug.WriteLine("Control.Up");
      this.RaiseUpEvent();
    }

    public override void Update(GameTime gametime)
    {
      base.Update(gametime);
      if (!this.IsPressed)
        return;
      this._timeWithoutTouch += (float) gametime.ElapsedGameTime.TotalSeconds;
      if ((double) this._timeWithoutTouch <= 0.10000000149011612)
        return;
      this.UpControl();
    }
  }
}
