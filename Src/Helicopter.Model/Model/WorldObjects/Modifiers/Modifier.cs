// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Modifiers.Modifier
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Modifiers
{
  public class Modifier
  {
    public event EventHandler Used;

    public void InvokeUsed(EventArgs e)
    {
      EventHandler used = this.Used;
      if (used == null)
        return;
      used((object) this, e);
    }
  }
}
