// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.Ammunition.AmunitionType
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;

#nullable disable
namespace Helicopter.Items.Ammunition
{
  [Flags]
  public enum AmunitionType
  {
    HealthPack50 = 1,
    HealthPack100 = 2,
    Shield25 = 4,
    Shield50 = 8,
    MisslesShieldUnit = 16, // 0x00000010
    BallisticShieldModule = 32, // 0x00000020
    CollisionsModule = 64, // 0x00000040
    DamageIncrease50 = 128, // 0x00000080
    Health = HealthPack100 | HealthPack50, // 0x00000003
    Damage = DamageIncrease50, // 0x00000080
    Defense = CollisionsModule | BallisticShieldModule | MisslesShieldUnit | Shield50 | Shield25, // 0x0000007C
  }
}
