// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.Damage50
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class Damage50 : UpgradeItem
  {
    public Damage50()
    {
      this.Name = "Upgraded warhead";
      this.Description = "Damage done by all rocket launchers increased by 20%";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_upgradedWarhead",
        OnCopterTexture = "Hangar/Upgrade/upgradedWarhead"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      foreach (Weapon weapon in player.Weapons)
      {
        if (weapon != null)
          weapon.Damage *= 1.5f;
      }
    }
  }
}
