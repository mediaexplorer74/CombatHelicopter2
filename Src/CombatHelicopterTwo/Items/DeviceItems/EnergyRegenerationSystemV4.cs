// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.DeviceItems.EnergyRegenerationSystemV4
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class EnergyRegenerationSystemV4 : UpgradeItem
  {
    public static float Modifier = 0.3f;

    public EnergyRegenerationSystemV4()
    {
      this.Name = "regeneration system v4.0";
      this.Description = string.Format("Regenerates {0}% energy per second", (object) EnergyRegenerationSystemV4.Modifier);
      this.ShortDesc = string.Format("Regeneration {0}%", (object) EnergyRegenerationSystemV4.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_shieldRegenerationSystem4",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_shieldRegenerationSystem4",
        OnCopterTexture = "Hangar/Upgrade/shieldRegenerationSystem4"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.EnergyRegeneration += (float) ((double) player.MaxEnergy * (double) EnergyRegenerationSystemV4.Modifier / 100.0);
    }
  }
}
