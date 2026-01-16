// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.LeaderBoard.DummyReccordsProvider
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;
using Helicopter.Model.Common;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Screen.LeaderBoard
{
  internal static class DummyReccordsProvider
  {
    public static List<LeaderboardRecord> GetDummyRecords(int n, int currentNumber)
    {
      List<LeaderboardRecord> dummyRecords = new List<LeaderboardRecord>();
      for (int n1 = 0; n1 <= n; ++n1)
      {
        LeaderboardRecord dummyRecord = DummyReccordsProvider.GetDummyRecord(n1);
        if (n1 == currentNumber)
          dummyRecord.IsMyself = true;
        dummyRecords.Add(dummyRecord);
      }
      return dummyRecords;
    }

    private static LeaderboardRecord GetDummyRecord(int n)
    {
      return new LeaderboardRecord()
      {
        Number = n,
        Rank = (Rank) CommonRandom.Instance.Random.Next(6),
        Name = "Gamer " + (object) n,
        Scores = CommonRandom.Instance.Random.Next(1000000000)
      };
    }
  }
}
