// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.GameplayPopups.BasePopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class BasePopup : GameScreen
  {
    public event EventHandler Back;

    private void OnBack(EventArgs e)
    {
      EventHandler back = this.Back;
      if (back == null)
        return;
      back((object) this, e);
    }

    public override void OnBackButton() => this.OnBack(EventArgs.Empty);
  }
}
