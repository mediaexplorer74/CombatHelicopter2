// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.RadioButton
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class RadioButton : BasicControl
  {
    private MenuControl _stateOn;
    private MenuControl _stateOff;
    private bool _state;

    public event EventHandler<BooleanEventArgs> StateChanged;

    public bool State
    {
      get => this._state;
      set
      {
        this._state = value;
        this._stateOn.Visible = this._state;
        this._stateOff.Visible = !this._state;
        if (this.StateChanged == null)
          return;
        this.StateChanged((object) this, new BooleanEventArgs()
        {
          State = this._state
        });
      }
    }

    public RadioButton(MenuControl stateOn, MenuControl stateOff, bool state)
    {
      this._stateOn = stateOn;
      this._stateOff = stateOff;
      this.AddChild((BasicControl) this._stateOn);
      this.AddChild((BasicControl) this._stateOff);
      this._stateOn.Clicked += new EventHandler<EventArgs>(this.ChangeState);
      this._stateOff.Clicked += new EventHandler<EventArgs>(this.ChangeState);
      this.State = state;
    }

    private void ChangeState(object sender, EventArgs e) => this.State = !this.State;
  }
}
