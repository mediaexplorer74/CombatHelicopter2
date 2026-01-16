// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.DeviceItems.HarvestingSystem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class HarvestingSystem : UpgradeItem
  {
    public static float Modifier = 20f;

    public HarvestingSystem()
    {
      this.Name = "Harvesting system";
      this.Description = string.Format("gives extra {0}% credits for killing an enemy", (object) HarvestingSystem.Modifier);
      this.ShortDesc = string.Format("{0}% credits", (object) HarvestingSystem.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_harvestingSystem",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_harvestingSystem",
        OnCopterTexture = "Hangar/Upgrade/harvestingSystem"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.MoneyModifier *= (float) ((100.0 + (double) HarvestingSystem.Modifier) / 100.0);
    }
  }
}
