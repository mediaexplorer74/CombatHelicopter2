// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.DeviceItems.TargetAssistentSystem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class TargetAssistentSystem : UpgradeItem
  {
    public TargetAssistentSystem()
    {
      this.Name = "target assistant system";
      this.Description = "reduces the cooldown of homing rocket";
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_trgetAssistentSystem",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_trgetAssistentSystem",
        OnCopterTexture = "Hangar/Upgrade/trgetAssistentSystem"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      if (player.Weapons[1].Type != WeaponType.HomingRocket)
        return;
      player.Weapons[1].Rate = new WeaponDescriptionManager().GetDescriptionForType(WeaponType.RocketLauncher).Rate;
    }
  }
}
