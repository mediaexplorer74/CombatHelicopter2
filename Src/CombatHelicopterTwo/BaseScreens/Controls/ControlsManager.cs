// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.ControlsManager
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class ControlsManager
  {
    public IList<Control> Controls { get; private set; }

    public ControlsManager() => this.Controls = (IList<Control>) new List<Control>();

    public void Draw(DrawContext context)
    {
      foreach (BasicControl control in (IEnumerable<Control>) this.Controls)
        control.Draw(context);
    }

    public void AddControl(Control control) => this.Controls.Add(control);

    public void HandleInput(InputState input)
    {
      foreach (TouchLocation touchLocation in input.TouchState)
      {
        switch (touchLocation.State)
        {
          case TouchLocationState.Released:
            using (IEnumerator<Control> enumerator = this.Controls.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Control current = enumerator.Current;
                if (current.Touches.Contains(touchLocation.Id))
                {
                  current.Touches.Remove(touchLocation.Id);
                  current.RaiseUpEvent();
                  break;
                }
              }
              continue;
            }
          case TouchLocationState.Pressed:
            using (IEnumerator<Control> enumerator = this.Controls.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Control current = enumerator.Current;
                if (current.ContactArea.Contains((int) touchLocation.Position.X, (int) touchLocation.Position.Y))
                {
                  if (current.IsEnabled)
                  {
                    if (current.IsVisible)
                    {
                      current.Touches.Add(touchLocation.Id);
                      current.RaiseDownEvent();
                      break;
                    }
                    break;
                  }
                  break;
                }
              }
              continue;
            }
          case TouchLocationState.Moved:
            using (IEnumerator<Control> enumerator = this.Controls.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Control current = enumerator.Current;
                if (current.ContactArea.Contains((int) touchLocation.Position.X, (int) touchLocation.Position.Y))
                {
                  if (!current.Touches.Contains(touchLocation.Id) && current.IsEnabled && current.IsVisible)
                  {
                    current.Touches.Add(touchLocation.Id);
                    current.RaiseDownEvent();
                  }
                }
                else if (current.Touches.Contains(touchLocation.Id) && current.IsEnabled && current.IsVisible)
                {
                  current.Touches.Remove(touchLocation.Id);
                  current.RaiseUpEvent();
                }
              }
              continue;
            }
          default:
            continue;
        }
      }
    }
  }
}
