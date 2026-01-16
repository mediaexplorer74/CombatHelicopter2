// Modified by MediaExplorer (2026)
// Type: Helicopter.Analytics.ParametersFactory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;
using Helicopter.Items.Ammunition;
using Helicopter.Items.DeviceItems;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Helicopter.Analytics
{
  internal static class ParametersFactory
  {
    private const string Episode = "Episode";
    private const string Location = "Location";
    private const string Stars = "Stars";
    private const string None = "None";
    private const string Helicoper = "Helicoper";
    private const string FirstWeapon = "FirstWeapon";
    private const string SecondWeapon = "SecondWeapon";
    private const string UpgradeA = "UpgradeA";
    private const string UpgradeB = "UpgradeB";
    private const string HealthBonus = "HealthBonus";
    private const string DefenseBonus = "DefenseBonus";
    private const string DamageBonus = "DamageBonus";
    private const string Replay = "Replay";
    private const string Energy = "Energy";
    private const string BuyAmmunition = "BuyAmmunition";
    private const string BuyInApp = "BuyInApp";

    public static Parameter GetAmmunitionParam(AmmunitionItem item)
    {
      return new Parameter("BuyAmmunition", item.Name);
    }

    public static Parameter GetChallengeEpisodeParam(int episode)
    {
      return new Parameter("Episode", episode.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    public static Parameter GetEnergyParam(float energy)
    {
      int num = (int) energy;
      return new Parameter("Energy", (num - num % 100).ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    public static List<Parameter> GetGamerParams(Gamer gamer)
    {
      return new List<Parameter>()
      {
        new Parameter("Helicoper", gamer.CurrentHelicopter.Item.Name),
        new Parameter("FirstWeapon", ParametersFactory.GetSlotValue<WeaponItem>(gamer.FirstWeapon)),
        new Parameter("SecondWeapon", ParametersFactory.GetSlotValue<WeaponItem>(gamer.SecondWeapon)),
        new Parameter("UpgradeA", ParametersFactory.GetSlotValue<UpgradeItem>(gamer.UpgradeA)),
        new Parameter("UpgradeB", ParametersFactory.GetSlotValue<UpgradeItem>(gamer.UpgradeB)),
        new Parameter("HealthBonus", ParametersFactory.GetHealthBonusValue(gamer.HealthBonus)),
        new Parameter("DefenseBonus", ParametersFactory.GetSlotValue<AmmunitionItem>(gamer.DefenseBonus)),
        new Parameter("DamageBonus", ParametersFactory.GetSlotValue<AmmunitionItem>(gamer.DamageBonus))
      };
    }

    private static string GetHealthBonusValue(
      SlotDescription<HealthAmmunitionItem> slotDescription)
    {
      return !slotDescription.IsInstalled ? "None" : slotDescription.Item.Volume.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static Parameter GetInAppParam(int credits)
    {
      return new Parameter("BuyInApp", credits.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    public static Parameter GetLocationParam(WorldType worldType)
    {
      return new Parameter("Location", worldType.ToString());
    }

    public static Parameter GetReplayParam(bool replay)
    {
      return new Parameter("Replay", replay.ToString());
    }

    private static string GetSlotValue<T>(SlotDescription<T> slotDescription) where T : Item
    {
      return slotDescription.IsInstalled ? slotDescription.Item.Name : "None";
    }

    public static Parameter GetStarsParam(int stars)
    {
      return new Parameter("Stars", stars.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }

    public static Parameter GetStoryEpisodeParam(WorldType worldType, int episode)
    {
      int num = (int) (worldType - 1) * 3 + episode;
      return new Parameter("Episode", episode != 3 ? num.ToString((IFormatProvider) CultureInfo.InvariantCulture) : string.Format("{0} (Boss)", (object) num));
    }
  }
}
