// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.BulletControlSystem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class BulletControlSystem : UpgradeItem
  {
    public static float Modifier = 30f;

    public BulletControlSystem()
    {
      this.Name = "Bullet control system";
      this.Description = string.Format("increases machine gun damage by {0}%", (object) BulletControlSystem.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_bulletControlSystem",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_bulletControlSystem",
        OnCopterTexture = "Hangar/Upgrade/bulletControlSystem"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      if (player.Weapons[0].Type != WeaponType.SingleMachineGun 
                && player.Weapons[0].Type != WeaponType.DualMachineGun && player.Weapons[0].Type != WeaponType.Vulcan)
        return;
      player.Weapons[0].Damage *= (float) ((100.0 + (double) BulletControlSystem.Modifier) / 100.0);
    }
  }
}
