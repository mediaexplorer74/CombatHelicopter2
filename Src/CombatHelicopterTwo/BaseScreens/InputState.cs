// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.InputState
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Helicopter.Model.Common;

#nullable disable
namespace Helicopter.BaseScreens
{
  public class InputState
  {
    public TouchCollection TouchState;
    public KeyboardState KeyboardState;
    public readonly List<GestureSample> Gestures = new List<GestureSample>();
    public MouseState MouseState;
    private bool _mouseWasDown;
    private Vector2 _prevMousePos;

    public InputState()
    {
      TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.Flick | GestureType.DragComplete;
    }

    public void Update()
    {
      KeyboardState = Keyboard.GetState();
      MouseState = Mouse.GetState();
      var rawTouches = TouchPanel.GetState();
      var adjusted = new List<TouchLocation>(rawTouches.Count + 1);
      for (int i = 0; i < rawTouches.Count; i++)
      {
        var t = rawTouches[i];
        var wp = t.Position;
        if (InputTransform.ViewportWidth > 0 && InputTransform.ViewportHeight > 0)
        {
          if (!InputTransform.IsInsideViewport(wp))
            continue;
          var gp = InputTransform.WindowToGame(wp);
          adjusted.Add(new TouchLocation(t.Id, t.State, gp));
        }
        else
        {
          adjusted.Add(t);
        }
      }
      var mp = new Vector2(MouseState.X, MouseState.Y);
      bool mouseIn = InputTransform.ViewportWidth > 0 ? InputTransform.IsInsideViewport(mp) : true;
      bool mouseDown = MouseState.LeftButton == ButtonState.Pressed;
      if (mouseIn)
      {
        var gp = InputTransform.ViewportWidth > 0 ? InputTransform.WindowToGame(mp) : mp;
        if (mouseDown)
        {
          var state = _mouseWasDown ? TouchLocationState.Moved : TouchLocationState.Pressed;
          adjusted.Add(new TouchLocation(-1, state, gp));
        }
        else if (_mouseWasDown)
        {
          adjusted.Add(new TouchLocation(-1, TouchLocationState.Released, gp));
        }
      }
      TouchState = new TouchCollection(adjusted.ToArray());
      _mouseWasDown = mouseDown;
      _prevMousePos = mp;
      Gestures.Clear();
      while (TouchPanel.IsGestureAvailable)
        Gestures.Add(TouchPanel.ReadGesture());
    }

    public bool IsBackButtonPressed => this.KeyboardState.IsKeyDown(Keys.Back);
  }
}
