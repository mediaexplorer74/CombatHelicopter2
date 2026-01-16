// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.InputState
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.BaseScreens
{
  public class InputState
  {
    public TouchCollection TouchState;
    public KeyboardState KeyboardState;
    public readonly List<GestureSample> Gestures = new List<GestureSample>();

    public InputState()
    {
      TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.Flick | GestureType.DragComplete;
    }

    public void Update()
    {
      this.KeyboardState = Keyboard.GetState();
      this.TouchState = TouchPanel.GetState();
      this.Gestures.Clear();
      while (TouchPanel.IsGestureAvailable)
        this.Gestures.Add(TouchPanel.ReadGesture());
    }

    public bool IsBackButtonPressed => this.KeyboardState.IsKeyDown(Keys.Back);
  }
}
