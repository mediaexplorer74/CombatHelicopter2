// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.CriticalDamageSystemV2
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class CriticalDamageSystemV2 : UpgradeItem
  {
    public static float Modifier = 10f;

    public CriticalDamageSystemV2()
    {
      this.Name = "Critical damage system v2.0";
      this.Description = "Gives a 10% chance to deal x4 damage";
      this.ShortDesc = "4x critical";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_criticalDamageSystem2",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_criticalDamageSystem2",
        OnCopterTexture = "Hangar/Upgrade/criticalDamageSystem2"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      foreach (Weapon weapon in player.Weapons)
        weapon.X4DamageProbability = CriticalDamageSystemV2.Modifier / 100f;
    }
  }
}
