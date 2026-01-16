// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.StateChangeEventArgs`1
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  public class StateChangeEventArgs<T> : EventArgs
  {
    public T PreviousState { get; set; }

    public T NextState { get; set; }

    public StateChangeEventArgs(T previous, T next)
    {
      this.PreviousState = previous;
      this.NextState = next;
    }
  }
}
