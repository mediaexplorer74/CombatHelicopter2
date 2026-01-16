// Decompiled with JetBrains decompiler
// Type: Helicopter.Statistics
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Playing;
using System;
using System.Globalization;
using System.Xml.Linq;

#nullable disable
namespace Helicopter
{
  internal class Statistics
  {
    private int TotalUnitKilled { get; set; }

    private int TotalCopterKilled { get; set; }

    private int TotalCannonKilled { get; set; }

    private int TotalRammedEnemies { get; set; }

    public TimeSpan TotalFlyingTime { get; set; }

    public void AddCannonKilled(int killed)
    {
      this.TotalCannonKilled += killed;
      if (this.TotalCannonKilled <= 100)
        return;
      Gamer.Instance.AchievementManager.GrantAchievement("Air Force Cross");
    }

    public void AddCopterKilled(int killed)
    {
      this.TotalCopterKilled += killed;
      if (this.TotalCopterKilled <= 100)
        return;
      Gamer.Instance.AchievementManager.GrantAchievement("AirmanMedal");
    }

    public void AddTotalFlyTime(TimeSpan flyTime)
    {
      this.TotalFlyingTime += flyTime;
      if (this.TotalFlyingTime > TimeSpan.FromHours(5.0))
        Gamer.Instance.AchievementManager.GrantAchievement("LongServiceMedal");
      if (!(this.TotalFlyingTime > TimeSpan.FromHours(20.0)))
        return;
      Gamer.Instance.AchievementManager.GrantAchievement("MedalForLoyalty");
    }

    public void AddUnitKilled(int killed)
    {
      this.TotalUnitKilled += killed;
      if (this.TotalUnitKilled <= 1000)
        return;
      Gamer.Instance.AchievementManager.GrantAchievement("SoldierMedal");
    }

    public void Deserialize(XElement element)
    {
      this.TotalCannonKilled = int.Parse(element.Element((XName) "TotalUnitKilled").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      this.TotalCopterKilled = int.Parse(element.Element((XName) "TotalCopterKilled").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      this.TotalCannonKilled = int.Parse(element.Element((XName) "TotalCannonKilled").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      this.TotalRammedEnemies = int.Parse(element.Element((XName) "TotalKamikadzeKills").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      this.TotalFlyingTime = TimeSpan.Parse(element.Element((XName) "TotalFlyingTime").Value, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    public XElement Serialize()
    {
      XElement xelement = new XElement((XName) nameof (Statistics));
      xelement.Add((object) new XElement((XName) "TotalUnitKilled", (object) this.TotalUnitKilled.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "TotalCopterKilled", (object) this.TotalCopterKilled.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "TotalCannonKilled", (object) this.TotalCannonKilled.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "TotalKamikadzeKills", (object) this.TotalRammedEnemies.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "TotalFlyingTime", (object) this.TotalFlyingTime.ToString()));
      return xelement;
    }

    public void AddStatsData(StatisticsData statisticsData)
    {
      this.AddCannonKilled(statisticsData.CannonsKilled);
      this.AddCopterKilled(statisticsData.CoptersKilled);
      this.AddUnitKilled(statisticsData.UnitsKilled);
      this.AddTotalFlyTime(statisticsData.FlyTime);
    }

    public void AddRammedCopter(int killed)
    {
      this.TotalRammedEnemies += killed;
      if (this.TotalRammedEnemies <= 500)
        return;
      Gamer.Instance.AchievementManager.GrantAchievement("MedalOfHonor");
    }
  }
}
