// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Scripts.Script
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Scripts
{
  public class Script
  {
    public event EventHandler Ended;

    public virtual void Update(float elapsedSeconds)
    {
    }

    protected void OnEnd()
    {
      if (this.Ended == null)
        return;
      this.Ended((object) this, EventArgs.Empty);
    }
  }
}
