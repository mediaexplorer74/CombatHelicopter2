// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.HotPlasmaModule
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class HotPlasmaModule : UpgradeItem
  {
    public static float Modifier = 1.3f;

    public HotPlasmaModule()
    {
      this.Name = "Hot plasma module";
      this.Description = "increases the damage of plasma gun by 30%";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_hotPlasmaModule",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_hotPlasmaModule",
        OnCopterTexture = "Hangar/Upgrade/hotPlasmaModule"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      if (player.Weapons[0].Type != WeaponType.PlasmaGun)
        return;
      player.Weapons[0].Damage = WeaponDescriptionManager.PlasmaGunDescription.Damage * HotPlasmaModule.Modifier;
    }
  }
}
