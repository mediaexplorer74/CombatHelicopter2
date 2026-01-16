// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.IncreasedCapacitySystem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class IncreasedCapacitySystem : UpgradeItem
  {
    public IncreasedCapacitySystem()
    {
      this.Name = "High capacity system";
      this.Description = "increases the capacity of cluster bomb";
      this.ShortDesc = "Cluster capacity";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_increasedCapacitySystem)",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_increasedCapacitySystem",
        OnCopterTexture = "Hangar/Upgrade/increasedCapacitySystem)"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      if (player.Weapons[1] == null || player.Weapons[1].Type != WeaponType.CasseteRocket)
        return;
      player.AddWeapon(1, WeaponFactory.GetWeapon((Instance) player, WeaponType.CasseteRocketUpdated));
    }
  }
}
