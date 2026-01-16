// Decompiled with JetBrains decompiler
// Type: Helicopter.Playing.GameSession
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.Model.WorldObjects;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Playing
{
  public abstract class GameSession
  {
    protected StatisticsData StatisticsData = new StatisticsData();
    public readonly Dictionary<UnitType, int> UnitsByType;

    public ScreenManager ScreenManager { get; set; }

    public bool IsGameStarted { get; set; }

    public int UnitsCount { get; set; }

    public float Score { get; set; }

    public float ScoreForUnits { get; set; }

    internal Gamer Gamer { get; private set; }

    internal GameSession(Gamer gamer)
    {
      this.Gamer = gamer;
      this.UnitsByType = new Dictionary<UnitType, int>();
    }

    public virtual void StartSession()
    {
      this.UnitsByType.Clear();
      this.UnitsCount = 0;
      this.Score = 0.0f;
      this.ScoreForUnits = 0.0f;
    }

    public virtual void EndSession()
    {
    }

    public abstract void Pause();

    protected void SaveResults() => this.Gamer.Statistics.AddStatsData(this.StatisticsData);

    protected void CountEnemyKill(UnitType type)
    {
      switch (type)
      {
        case UnitType.LightHelicopter:
        case UnitType.MediumHelicopter:
        case UnitType.HeavyHelicopter:
          ++this.StatisticsData.UnitsKilled;
          ++this.StatisticsData.CoptersKilled;
          break;
        case UnitType.LightTurret:
        case UnitType.MediumTurret:
        case UnitType.HeavyTurret:
          ++this.StatisticsData.UnitsKilled;
          ++this.StatisticsData.CannonsKilled;
          break;
        case UnitType.Droid:
        case UnitType.Egg:
        case UnitType.ArmedEgg:
        case UnitType.Boss1:
        case UnitType.Boss2:
        case UnitType.Boss3:
        case UnitType.Boss4:
        case UnitType.Boss5:
        case UnitType.Boss:
          ++this.StatisticsData.UnitsKilled;
          break;
      }
    }
  }
}
