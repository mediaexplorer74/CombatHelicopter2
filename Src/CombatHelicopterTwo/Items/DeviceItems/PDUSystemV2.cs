// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.DeviceItems.PDUSystemV2
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class PDUSystemV2 : UpgradeItem
  {
    public static float Modifier = 20f;

    public PDUSystemV2()
    {
      this.Name = "PDU system v2.0";
      this.Description = "increases maximum energy by 20%";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_PDUSystem2",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_PDUSystem2",
        OnCopterTexture = "Hangar/Upgrade/PDUSystem2"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.Energy = player.MaxEnergy = (float) ((double) player.MaxEnergy * (100.0 + (double) PDUSystemV2.Modifier) / 100.0);
    }
  }
}
