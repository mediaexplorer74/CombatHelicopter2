// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.LeaderBoard.LeaderboardRecord
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;

#nullable disable
namespace Helicopter.Screen.LeaderBoard
{
  internal class LeaderboardRecord
  {
    public string Id { get; set; }

    public bool IsMyself { get; set; }

    public int Number { get; set; }

    public Rank Rank { get; set; }

    public string Name { get; set; }

    public int Scores { get; set; }
  }
}
