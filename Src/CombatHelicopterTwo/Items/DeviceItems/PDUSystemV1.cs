// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.PDUSystemV1
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class PDUSystemV1 : UpgradeItem
  {
    public static float Modifier = 10f;

    public PDUSystemV1()
    {
      this.Name = "PDU system v1.0";
      this.Description = "increases maximum energy by 10%";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_PDUSystem1",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_PDUSystem1",
        OnCopterTexture = "Hangar/Upgrade/PDUSystem1"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.Energy = player.MaxEnergy = (float) ((double) player.MaxEnergy * (100.0 + (double) PDUSystemV1.Modifier) / 100.0);
    }
  }
}
