// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Tutorial_Popups.AnimDesc
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common.Tween;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Screen.MapScreen.Tutorial_Popups
{
  internal class AnimDesc
  {
    public BasicControl Control;
    public Action EndAction;
    public Vector2 From;
    public TweeningFunction Function;
    public float Time;
    public Vector2 To;
  }
}
