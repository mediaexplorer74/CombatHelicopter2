// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.LeaderBoard.RecordsList
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Screen.LeaderBoard
{
  internal class RecordsList
  {
    private readonly Dictionary<int, LeaderboardRecord> _records;

    public int TopNumber { get; private set; }

    public int BottomNumber { get; private set; }

    public int Count => this._records.Count;

    public List<LeaderboardRecord> Records
    {
      get
      {
        List<LeaderboardRecord> records = new List<LeaderboardRecord>((IEnumerable<LeaderboardRecord>) this._records.Values);
        records.Sort(new Comparison<LeaderboardRecord>(this.CombareByNumber));
        return records;
      }
    }

    public RecordsList()
    {
      this._records = new Dictionary<int, LeaderboardRecord>();
      this.TopNumber = -1;
      this.BottomNumber = -1;
    }

    public void Add(LeaderboardRecord record)
    {
      int key = -1;
      foreach (LeaderboardRecord leaderboardRecord in this._records.Values)
      {
        if (leaderboardRecord.Id == record.Id)
        {
          if (record.Number >= leaderboardRecord.Number)
            return;
          key = leaderboardRecord.Number;
          break;
        }
      }
      if (key >= 0 && this._records.ContainsKey(key))
        this._records.Remove(key);
      this._records[record.Number] = record;
      if (this.TopNumber == -1)
        this.TopNumber = record.Number;
      else if (this.TopNumber > record.Number)
        this.TopNumber = record.Number;
      if (this.BottomNumber == -1)
      {
        this.BottomNumber = record.Number;
      }
      else
      {
        if (this.BottomNumber >= record.Number)
          return;
        this.BottomNumber = record.Number;
      }
    }

    public void Clear()
    {
      this._records.Clear();
      this.TopNumber = -1;
      this.BottomNumber = -1;
    }

    private int CombareByNumber(LeaderboardRecord a, LeaderboardRecord b)
    {
      return a.Number.CompareTo(b.Number);
    }

    public void AddRange(IEnumerable<LeaderboardRecord> range)
    {
      foreach (LeaderboardRecord record in range)
        this.Add(record);
    }
  }
}
