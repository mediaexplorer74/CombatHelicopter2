// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.CommonRandom
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Common
{
  public class CommonRandom
  {
    private static CommonRandom _instance;

    public Random Random { get; private set; }

    public static CommonRandom Instance
    {
      get => CommonRandom._instance ?? (CommonRandom._instance = new CommonRandom());
    }

    public CommonRandom() => this.Random = new Random();
  }
}
