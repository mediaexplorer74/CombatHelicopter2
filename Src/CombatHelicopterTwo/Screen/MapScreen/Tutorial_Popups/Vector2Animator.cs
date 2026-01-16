// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Tutorial_Popups.Vector2Animator
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common.Tween;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Screen.MapScreen.Tutorial_Popups
{
  internal class Vector2Animator
  {
    private readonly List<BasicControl> _controls = new List<BasicControl>();
    private readonly List<Vector2Tweener> _tweeners = new List<Vector2Tweener>();

    public Vector2Animator(params AnimDesc[] desc)
    {
      foreach (AnimDesc animDesc in desc)
        this.AddAnim(animDesc);
    }

    public Vector2Animator()
    {
    }

    public void Update(float elapsed)
    {
      for (int index = 0; index < this._tweeners.Count; ++index)
      {
        Vector2Tweener tweener = this._tweeners[index];
        tweener.Update(elapsed);
        this._controls[index].Position = tweener.CurrentPosition;
      }
    }

    public void AddAnim(AnimDesc animDesc)
    {
      Vector2Tweener vector2Tweener = new Vector2Tweener(animDesc.From, animDesc.To, animDesc.Time, animDesc.Function);
      if (animDesc.EndAction != null)
        vector2Tweener.Ended += (EventHandler<EventArgs>) ((x, y) => animDesc.EndAction());
      this._tweeners.Add(vector2Tweener);
      this._controls.Add(animDesc.Control);
    }

    public void Clear()
    {
      this._tweeners.Clear();
      this._controls.Clear();
    }

    public void Remove(BasicControl control)
    {
      if (!this._controls.Contains(control))
        return;
      this.RemoveAt(this._controls.IndexOf(control));
    }

    private void RemoveAt(int i)
    {
      this._tweeners.RemoveAt(i);
      this._controls.RemoveAt(i);
    }
  }
}
