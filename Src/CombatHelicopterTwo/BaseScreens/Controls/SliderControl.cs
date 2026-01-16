// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.SliderControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class SliderControl : BasicControl
  {
    private readonly TexturedControl _background;
    private readonly MenuControl _slider;
    private bool _isDragging;
    private bool _isOn;

    public event EventHandler<BooleanEventArgs> StateChanged;

    public bool IsOn
    {
      get => this._isOn;
      set
      {
        bool isOn = this._isOn;
        this._isOn = value;
        this._slider.Position = this._isOn ? new Vector2(this._background.Size.X - this._slider.Size.X, this._slider.Position.Y) : new Vector2(-1f, this._slider.Position.Y);
        if (isOn == this._isOn)
          return;
        this.OnStateChanged(new BooleanEventArgs()
        {
          State = this._isOn
        });
      }
    }

    public SliderControl(Sprite background, Sprite slider, Sprite sliderActive, Vector2 position)
    {
      this.Position = position;
      this._background = new TexturedControl(background, Vector2.Zero);
      this._slider = new MenuControl(slider, sliderActive, new Vector2(-1f, (float) ((double) background.Bounds.Height / 2.0 - (double) sliderActive.Bounds.Height / 2.0)));
      this._background.AddChild((BasicControl) this._slider);
      this.AddChild((BasicControl) this._background);
      this.IsOn = false;
    }

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      foreach (GestureSample gesture in input.Gestures)
      {
        if ((double) gesture.Position.X > (double) this.AbsolutePosition.X && (double) gesture.Position.X < (double) this.AbsolutePosition.X + (double) this.Size.X && (double) gesture.Position.Y > (double) this.AbsolutePosition.Y && (double) gesture.Position.Y < (double) this.AbsolutePosition.Y + (double) this.Size.Y)
        {
          switch (gesture.GestureType)
          {
            case GestureType.Tap:
              this.IsOn = !this.IsOn;
              break;
            case GestureType.HorizontalDrag:
              MenuControl slider = this._slider;
              slider.Position = slider.Position + new Vector2(gesture.Delta.X, 0.0f);
              this._slider.Position = new Vector2(MathHelper.Clamp(this._slider.Position.X, 0.0f, this._background.Size.X - this._slider.Size.X), this._slider.Position.Y);
              this._isDragging = true;
              break;
          }
        }
        if (gesture.GestureType == GestureType.DragComplete && this._isDragging)
        {
          this._isDragging = false;
          this.IsOn = (double) this._slider.Position.X > (double) this._background.Size.X * 0.30000001192092896;
        }
      }
    }

    public void OnStateChanged(BooleanEventArgs e)
    {
      EventHandler<BooleanEventArgs> stateChanged = this.StateChanged;
      if (stateChanged == null)
        return;
      stateChanged((object) this, e);
    }
  }
}
