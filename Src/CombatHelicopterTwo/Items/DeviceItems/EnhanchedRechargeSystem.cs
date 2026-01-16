// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.EnhanchedRechargeSystem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class EnhanchedRechargeSystem : UpgradeItem
  {
    public static float Modifier = 20f;

    public EnhanchedRechargeSystem()
    {
      this.Name = "enhanced recharge system";
      this.Description = string.Format("reduces the cooldown of heavy weapon by {0}%", (object) EnhanchedRechargeSystem.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_enhanchedRechargSystem",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_enhanchedRechargSystem",
        OnCopterTexture = "Hangar/Upgrade/enhanchedRechargSystem"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      if (player.Weapons[1] == null)
        return;
      player.Weapons[1].Rate *= (float) ((100.0 - (double) EnhanchedRechargeSystem.Modifier) / 100.0);
    }
  }
}
