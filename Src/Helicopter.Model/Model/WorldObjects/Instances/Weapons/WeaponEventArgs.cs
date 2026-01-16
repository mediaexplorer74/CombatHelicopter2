// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.WeaponEventArgs
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class WeaponEventArgs : EventArgs
  {
    public WeaponType WeaponType { get; set; }

    public int ShotNumber { get; set; }

    public static WeaponEventArgs Create(WeaponType type, int shots)
    {
      return new WeaponEventArgs()
      {
        WeaponType = type,
        ShotNumber = shots
      };
    }
  }
}
