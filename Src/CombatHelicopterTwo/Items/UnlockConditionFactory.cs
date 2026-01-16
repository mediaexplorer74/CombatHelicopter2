// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.UnlockConditionFactory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter.Items
{
  internal class UnlockConditionFactory
  {
    public UnlockCondition DualMachineGun()
    {
      return new UnlockCondition()
      {
        Price = 1000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (DualMachineGun)]
      };
    }

    public UnlockCondition PlasmaGun()
    {
      return new UnlockCondition()
      {
        Price = 7000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (PlasmaGun)]
      };
    }

    public UnlockCondition SingleMachineGun()
    {
      return new UnlockCondition()
      {
        Price = 0,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (SingleMachineGun)],
        IsBought = true
      };
    }

    public UnlockCondition Vulcan()
    {
      return new UnlockCondition()
      {
        Price = 12000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (Vulcan)]
      };
    }

    public UnlockCondition ClusterBomb()
    {
      return new UnlockCondition()
      {
        Price = 5000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (ClusterBomb)]
      };
    }

    public UnlockCondition DualRocketLauncher()
    {
      return new UnlockCondition()
      {
        Price = 9000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (DualRocketLauncher)]
      };
    }

    public UnlockCondition HomingRocket()
    {
      return new UnlockCondition()
      {
        Price = 15000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (HomingRocket)]
      };
    }

    public UnlockCondition RocketLauncher()
    {
      return new UnlockCondition()
      {
        Price = 1500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (RocketLauncher)]
      };
    }

    public UnlockCondition Shield()
    {
      return new UnlockCondition()
      {
        Price = 10000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (Shield)]
      };
    }

    public UnlockCondition BulletControlSystem()
    {
      return new UnlockCondition()
      {
        Price = 1000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (BulletControlSystem)]
      };
    }

    public UnlockCondition CriticalDamageSystemV1()
    {
      return new UnlockCondition()
      {
        Price = 2000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (CriticalDamageSystemV1)]
      };
    }

    public UnlockCondition CriticalDamageSystemV2()
    {
      return new UnlockCondition()
      {
        Price = 2000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (CriticalDamageSystemV2)]
      };
    }

    public UnlockCondition DamageControlSystemV1()
    {
      return new UnlockCondition()
      {
        Price = 500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (DamageControlSystemV1)]
      };
    }

    public UnlockCondition DamageControlSystemV2()
    {
      return new UnlockCondition()
      {
        Price = 3000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (DamageControlSystemV2)]
      };
    }

    public UnlockCondition DamageControlSystemV3()
    {
      return new UnlockCondition()
      {
        Price = 7000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (DamageControlSystemV3)]
      };
    }

    public UnlockCondition EnergyRegenerationSystemV1()
    {
      return new UnlockCondition()
      {
        Price = 500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (EnergyRegenerationSystemV1)]
      };
    }

    public UnlockCondition EnergyRegenerationSystemV2()
    {
      return new UnlockCondition()
      {
        Price = 2500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (EnergyRegenerationSystemV2)]
      };
    }

    public UnlockCondition EnergyRegenerationSystemV3()
    {
      return new UnlockCondition()
      {
        Price = 5000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (EnergyRegenerationSystemV3)]
      };
    }

    public UnlockCondition EnergyRegenerationSystemV4()
    {
      return new UnlockCondition()
      {
        Price = 10000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (EnergyRegenerationSystemV4)]
      };
    }

    public UnlockCondition EnhanchedRechargeSystem()
    {
      return new UnlockCondition()
      {
        Price = 10000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (EnhanchedRechargeSystem)]
      };
    }

    public UnlockCondition HarvestingSystem()
    {
      return new UnlockCondition()
      {
        Price = 2500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (HarvestingSystem)]
      };
    }

    public UnlockCondition HotPlasmaModule()
    {
      return new UnlockCondition()
      {
        Price = 2000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (HotPlasmaModule)]
      };
    }

    public UnlockCondition IncreasedCapacitySystem()
    {
      return new UnlockCondition()
      {
        Price = 2000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (IncreasedCapacitySystem)]
      };
    }

    public UnlockCondition PDUSystemV1()
    {
      return new UnlockCondition()
      {
        Price = 3000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (PDUSystemV1)]
      };
    }

    public UnlockCondition PDUSystemV2()
    {
      return new UnlockCondition()
      {
        Price = 5000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (PDUSystemV2)]
      };
    }

    public UnlockCondition SystemCompensationCrush()
    {
      return new UnlockCondition()
      {
        Price = 5000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (SystemCompensationCrush)]
      };
    }

    public UnlockCondition TargetAssistentSystem()
    {
      return new UnlockCondition()
      {
        Price = 2000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (TargetAssistentSystem)]
      };
    }

    public UnlockCondition UpgradedWarhead()
    {
      return new UnlockCondition()
      {
        Price = 5000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (UpgradedWarhead)]
      };
    }

    public UnlockCondition Avenger()
    {
      return new UnlockCondition()
      {
        Price = 10000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (Avenger)]
      };
    }

    public UnlockCondition GrimReaper()
    {
      return new UnlockCondition()
      {
        Price = 30000,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (GrimReaper)]
      };
    }

    public UnlockCondition Harbinger()
    {
      return new UnlockCondition()
      {
        Price = 3500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (Harbinger)]
      };
    }

    public UnlockCondition Viper()
    {
      return new UnlockCondition()
      {
        Price = 2500,
        UnlockAchievement = Gamer.Instance.AchievementManager.Achievements[nameof (Viper)],
        IsBought = true
      };
    }
  }
}
