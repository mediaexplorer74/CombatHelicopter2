// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.AchievementsSystem.AchievementFactory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter.Items.AchievementsSystem
{
  internal class AchievementFactory
  {
    public static Achievement AceMedal()
    {
      return new Achievement()
      {
        Name = "Ace Medal",
        Description = "Complete a mission in Survival mode without receiving any damage",
        Texture = "MainMenu/Achievement/achieve_AceMedal",
        MoneyAward = 5000
      };
    }

    public static Achievement AirForceAchievementMedal()
    {
      return new Achievement()
      {
        Name = "Air Force Achievement Medal",
        Description = "Third place in Leaderboard",
        Texture = "MainMenu/Achievement/achieve_AirForceAchievementMedal",
        Showable = false
      };
    }

    public static Achievement AirForceCross()
    {
      return new Achievement()
      {
        Name = "Air Force Cross",
        Description = "Destroy 100 enemy gun towers",
        Texture = "MainMenu/Achievement/achieve_AirForceCross",
        MoneyAward = 1000
      };
    }

    public static Achievement AirManMedal()
    {
      return new Achievement()
      {
        Name = "Airman's Medal",
        Description = "Destroy 100 enemy helicopters",
        Texture = "MainMenu/Achievement/achieve_AirmansMedal",
        MoneyAward = 1000
      };
    }

    public static Achievement DesertCoyote()
    {
      return new Achievement()
      {
        Name = "Desert Coyote",
        Description = "Complete all Desert missions",
        Texture = "MainMenu/Achievement/achieve_DesertCoyote"
      };
    }

    public static Achievement DistinguishedFlyingCross()
    {
      return new Achievement()
      {
        Name = "Distinguished Flying Cross",
        Description = "First place in Leaderboard",
        Texture = "MainMenu/Achievement/achieve_DistinguishedFlyingCross",
        Showable = false
      };
    }

    public static Achievement DistinguishedService()
    {
      return new Achievement()
      {
        Name = "Distinguished Service Medal",
        Description = "Second place in Leaderboard",
        Texture = "MainMenu/Achievement/achieve_DistinguishedServiceMedal",
        Showable = false
      };
    }

    public static Achievement FireStorm()
    {
      return new Achievement()
      {
        Name = "Fire Storm",
        Description = "Complete all Volcano missions",
        Texture = "MainMenu/Achievement/achieve_FireStorm"
      };
    }

    public static Achievement IceFury()
    {
      return new Achievement()
      {
        Name = "Ice Fury",
        Description = "Complete all Arctic missions",
        Texture = "MainMenu/Achievement/achieve_IceFury"
      };
    }

    public static Achievement LegionOfMerit()
    {
      return new Achievement()
      {
        Name = "Legion of Merit",
        Description = "For for reaching top 10 in Leaderboard",
        Texture = "MainMenu/Achievement/achieve_LegionOfMerit",
        Showable = false
      };
    }

    public static Achievement Liberator()
    {
      return new Achievement()
      {
        Name = nameof (Liberator),
        Description = "Complete Campaign",
        Texture = "MainMenu/Achievement/achieve_liberator"
      };
    }

    public static Achievement LongServiceMedal()
    {
      return new Achievement()
      {
        Name = "Long Service Medal",
        Description = "For 5 cumulative hours in the air",
        Texture = "MainMenu/Achievement/achieve_LongServiceMedal"
      };
    }

    public static Achievement MedalForLoyalty()
    {
      return new Achievement()
      {
        Name = "Medal for Loyalty",
        Description = "For 20 cumulative hours in the air",
        Texture = "MainMenu/Achievement/achieve_MedalForloyalty"
      };
    }

    public static Achievement MedalForMeritoriousService()
    {
      return new Achievement()
      {
        Name = "Medal for Meritorious Service",
        Description = "Destroy all enemies in a single mission",
        Texture = "MainMenu/Achievement/achieve_MedalForMeritoriousService"
      };
    }

    public static Achievement MedalOfHonor()
    {
      return new Achievement()
      {
        Name = "Medal of Honor",
        Description = "Ram 500 enemies",
        Texture = "MainMenu/Achievement/achieve_MedalOfHonor",
        MoneyAward = 5000
      };
    }

    public static Achievement Mercenary()
    {
      return new Achievement()
      {
        Name = nameof (Mercenary),
        Description = "Complete first mission in Survival Mode",
        Texture = "MainMenu/Achievement/achieve_Mercenary"
      };
    }

    public static Achievement QuartermasterMedal()
    {
      return new Achievement()
      {
        Name = "Quartermaster Medal",
        Description = "Collect all items in the hangar",
        Texture = "MainMenu/Achievement/achieve_QuartermasterMedal"
      };
    }

    public static Achievement ScoutMedal()
    {
      return new Achievement()
      {
        Name = "Order of The Scout",
        Description = "Complete a mission in Survival Mode without firing a single shot",
        Texture = "MainMenu/Achievement/achieve_OrderOfTheScout",
        MoneyAward = 5000
      };
    }

    public static Achievement SniperCross()
    {
      return new Achievement()
      {
        Name = "Sniper Cross",
        Description = "Complete a mission in Survival mode without any misses",
        Texture = "MainMenu/Achievement/achieve_SniperCross",
        MoneyAward = 5000
      };
    }

    public static Achievement SoldierMedal()
    {
      return new Achievement()
      {
        Name = "Soldier's Medal",
        Description = "Destroy 1,000 enemies",
        Texture = "MainMenu/Achievement/achieve_SoldiersMedal",
        MoneyAward = 500
      };
    }

    public static Achievement TropicThunder()
    {
      return new Achievement()
      {
        Name = "Tropic Thunder",
        Description = "Complete all Jungle missions",
        Texture = "MainMenu/Achievement/achieve_TropicThunder"
      };
    }
  }
}
