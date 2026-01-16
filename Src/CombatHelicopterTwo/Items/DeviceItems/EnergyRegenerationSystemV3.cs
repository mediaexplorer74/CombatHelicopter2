// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.EnergyRegenerationSystemV3
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class EnergyRegenerationSystemV3 : UpgradeItem
  {
    public static float Modifier = 0.22f;

    public EnergyRegenerationSystemV3()
    {
      this.Name = "regeneration system v3.0";
      this.Description = string.Format("Regenerates {0}% energy per second", (object) EnergyRegenerationSystemV3.Modifier);
      this.ShortDesc = string.Format("Regeneration {0}%", (object) EnergyRegenerationSystemV3.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_shieldRegenerationSystem3",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_shieldRegenerationSystem3",
        OnCopterTexture = "Hangar/Upgrade/shieldRegenerationSystem3"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.EnergyRegeneration += (float) ((double) player.MaxEnergy * (double) EnergyRegenerationSystemV3.Modifier / 100.0);
    }
  }
}
