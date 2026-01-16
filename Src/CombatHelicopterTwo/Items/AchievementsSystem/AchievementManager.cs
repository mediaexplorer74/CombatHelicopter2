// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.AchievementsSystem.AchievementManager
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
namespace Helicopter.Items.AchievementsSystem
{
  internal class AchievementManager
  {
    public List<Achievement> UnshownAchievement = new List<Achievement>();

    public Dictionary<string, Achievement> Achievements { get; set; }

    public AchievementManager()
    {
      this.Achievements = new Dictionary<string, Achievement>();
      this.InitAchievements();
    }

    private void InitAchievements()
    {
      this.Achievements["SingleMachineGun"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["DualMachineGun"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["Vulcan"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["PlasmaGun"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["RocketLauncher"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["DualRocketLauncher"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["ClusterBomb"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["Shield"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["HomingRocket"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["DamageControlSystemV1"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["DamageControlSystemV2"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["DamageControlSystemV3"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["SystemCompensationCrush"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["BulletControlSystem"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["EnergyRegenerationSystemV1"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["EnergyRegenerationSystemV2"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["EnergyRegenerationSystemV3"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["EnergyRegenerationSystemV4"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["TargetAssistentSystem"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["EnhanchedRechargeSystem"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["IncreasedCapacitySystem"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["HotPlasmaModule"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["CriticalDamageSystemV1"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["CriticalDamageSystemV2"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["PDUSystemV1"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["PDUSystemV2"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["UpgradedWarhead"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["HarvestingSystem"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["Viper"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["Harbinger"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["Avenger"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["GrimReaper"] = new Achievement()
      {
        Showable = false
      };
      this.Achievements["DesertCoyote"] = AchievementFactory.DesertCoyote();
      this.Achievements["TropicThunder"] = AchievementFactory.TropicThunder();
      this.Achievements["IceFury"] = AchievementFactory.IceFury();
      this.Achievements["FireStorm"] = AchievementFactory.FireStorm();
      this.Achievements["Liberator"] = AchievementFactory.Liberator();
      this.Achievements["Mercenary"] = AchievementFactory.Mercenary();
      this.Achievements["SoldierMedal"] = AchievementFactory.SoldierMedal();
      this.Achievements["AirmanMedal"] = AchievementFactory.AirManMedal();
      this.Achievements["Air Force Cross"] = AchievementFactory.AirForceCross();
      this.Achievements["MedalOfHonor"] = AchievementFactory.MedalOfHonor();
      this.Achievements["QuartermasterMedal"] = AchievementFactory.QuartermasterMedal();
      this.Achievements["Scout"] = AchievementFactory.ScoutMedal();
      this.Achievements["Sniper"] = AchievementFactory.SniperCross();
      this.Achievements["Ace"] = AchievementFactory.AceMedal();
      this.Achievements["LegionOfMerit"] = AchievementFactory.LegionOfMerit();
      this.Achievements["AirForceAchievementMedal"] = AchievementFactory.AirForceAchievementMedal();
      this.Achievements["DistinguishedService"] = AchievementFactory.DistinguishedService();
      this.Achievements["DistinguishedFlyingCross"] = AchievementFactory.DistinguishedFlyingCross();
      this.Achievements["LongServiceMedal"] = AchievementFactory.LongServiceMedal();
      this.Achievements["MedalForLoyalty"] = AchievementFactory.MedalForLoyalty();
      this.Achievements["MedalForMeritoriousService"] = AchievementFactory.MedalForMeritoriousService();
    }

    public string Serialize()
    {
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) new XElement((XName) nameof (AchievementManager))
      });
      XElement content = new XElement((XName) "Archived");
      foreach (KeyValuePair<string, Achievement> achievement in this.Achievements)
      {
        if (achievement.Value.Achieved)
          content.Add((object) new XElement((XName) "Achievement", (object) achievement.Key));
      }
      xdocument.Root.Add((object) content);
      return xdocument.ToString();
    }

    public void Deserialize(string value)
    {
      XElement xelement = XElement.Parse(value).Element((XName) "Archived");
      if (xelement == null)
        return;
      foreach (XElement element in xelement.Elements())
        this.Achievements[element.Value].Achieved = true;
    }

    public void GrantAchievement(string achievementName)
    {
      if (!this.Achievements.ContainsKey(achievementName))
        return;
      Achievement achievement = this.Achievements[achievementName];
      if (achievement.Achieved)
        return;
      achievement.Achieved = true;
      Gamer.Instance.Money.AddMoney((float) achievement.MoneyAward);
      this.UnshownAchievement.Add(achievement);
    }

    public bool IsAllItemsBought
    {
      get
      {
        return this.Achievements["SingleMachineGun"].Achieved && this.Achievements["DualMachineGun"].Achieved && this.Achievements["Vulcan"].Achieved && this.Achievements["PlasmaGun"].Achieved && this.Achievements["RocketLauncher"].Achieved && this.Achievements["DualRocketLauncher"].Achieved && this.Achievements["ClusterBomb"].Achieved && this.Achievements["Shield"].Achieved && this.Achievements["HomingRocket"].Achieved && this.Achievements["DamageControlSystemV1"].Achieved && this.Achievements["DamageControlSystemV2"].Achieved && this.Achievements["DamageControlSystemV3"].Achieved && this.Achievements["SystemCompensationCrush"].Achieved && this.Achievements["BulletControlSystem"].Achieved && this.Achievements["EnergyRegenerationSystemV1"].Achieved && this.Achievements["EnergyRegenerationSystemV2"].Achieved && this.Achievements["EnergyRegenerationSystemV3"].Achieved && this.Achievements["EnergyRegenerationSystemV4"].Achieved && this.Achievements["TargetAssistentSystem"].Achieved && this.Achievements["EnhanchedRechargeSystem"].Achieved && this.Achievements["IncreasedCapacitySystem"].Achieved && this.Achievements["HotPlasmaModule"].Achieved && this.Achievements["CriticalDamageSystemV1"].Achieved && this.Achievements["CriticalDamageSystemV2"].Achieved && this.Achievements["PDUSystemV1"].Achieved && this.Achievements["PDUSystemV2"].Achieved && this.Achievements["UpgradedWarhead"].Achieved && this.Achievements["HarvestingSystem"].Achieved && this.Achievements["Viper"].Achieved && this.Achievements["Harbinger"].Achieved && this.Achievements["Avenger"].Achieved && this.Achievements["GrimReaper"].Achieved;
      }
    }
  }
}
