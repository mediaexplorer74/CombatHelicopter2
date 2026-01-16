// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.DeviceItems.UpgradedWarhead
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class UpgradedWarhead : UpgradeItem
  {
    public static float Modifier = 20f;

    public UpgradedWarhead()
    {
      this.Name = "Upgraded warhead";
      this.Description = string.Format("increases all rocket damage by {0}%", (object) UpgradedWarhead.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_upgradedWarhead",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_upgradedWarhead",
        OnCopterTexture = "Hangar/Upgrade/upgradedWarhead"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      if (player.Weapons[1].Type != WeaponType.DualRocketLauncher && player.Weapons[1].Type != WeaponType.RocketLauncher && player.Weapons[1].Type != WeaponType.HomingRocket)
        return;
      player.Weapons[1].Damage *= (float) ((100.0 + (double) UpgradedWarhead.Modifier) / 100.0);
    }
  }
}
