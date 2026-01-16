// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.EnergyRegenerationSystemV2
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class EnergyRegenerationSystemV2 : UpgradeItem
  {
    public static float Modifier = 0.15f;

    public EnergyRegenerationSystemV2()
    {
      this.Name = "regeneration system v2.0";
      this.Description = string.Format("Regenerates {0}% energy per second", (object) EnergyRegenerationSystemV2.Modifier);
      this.ShortDesc = string.Format("Regeneration {0}%", (object) EnergyRegenerationSystemV2.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_shieldRegenerationSystem2",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_shieldRegenerationSystem2",
        OnCopterTexture = "Hangar/Upgrade/shieldRegenerationSystem2"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.EnergyRegeneration += (float) ((double) player.MaxEnergy * (double) EnergyRegenerationSystemV2.Modifier / 100.0);
    }
  }
}
