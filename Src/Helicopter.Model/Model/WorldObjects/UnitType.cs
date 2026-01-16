// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.UnitType
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  [Flags]
  public enum UnitType
  {
    None = 1,
    LightHelicopter = 2,
    MediumHelicopter = 4,
    HeavyHelicopter = 8,
    LightTurret = 16, // 0x00000010
    MediumTurret = 32, // 0x00000020
    HeavyTurret = 64, // 0x00000040
    Droid = 128, // 0x00000080
    Egg = 256, // 0x00000100
    ArmedEgg = 512, // 0x00000200
    Boss1 = 1024, // 0x00000400
    Boss2 = 2048, // 0x00000800
    Boss3 = 4096, // 0x00001000
    Boss4 = 8192, // 0x00002000
    Boss5 = 16384, // 0x00004000
    Player = 32768, // 0x00008000
    Boss = Boss5 | Boss4 | Boss3 | Boss2 | Boss1, // 0x00007C00
  }
}
