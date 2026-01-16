// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.LevelsFactory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter.GamePlay
{
  internal static class LevelsFactory
  {
    public static Level GetChallengeLevelBlank()
    {
      Level challengeLevelBlank = new Level();
      challengeLevelBlank.InitChallengeLevel();
      return challengeLevelBlank;
    }
  }
}
