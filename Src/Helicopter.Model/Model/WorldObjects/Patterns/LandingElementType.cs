// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Patterns.LandingElementType
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  [Flags]
  public enum LandingElementType
  {
    StartBlock = 1,
    MediumBlock = 2,
    EndBlock = 4,
    StartShield = 8,
    MediumShield = 16, // 0x00000010
    EndShield = 32, // 0x00000020
    Label = 64, // 0x00000040
    Background = Label | EndShield | MediumShield | StartShield, // 0x00000078
    Block = EndBlock | MediumBlock | StartBlock, // 0x00000007
  }
}
