// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.MountainType
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  [Flags]
  public enum MountainType
  {
    Normal = 1,
    Up1 = 2,
    Up2 = 4,
    Up3 = 8,
    Up = Up3 | Up2 | Up1, // 0x0000000E
    Down1 = 16, // 0x00000010
    Down2 = 32, // 0x00000020
    Down3 = 64, // 0x00000040
    Down = Down3 | Down2 | Down1, // 0x00000070
    Peak = 128, // 0x00000080
  }
}
