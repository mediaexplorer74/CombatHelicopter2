// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.DeviceItems.SystemCompensationCrush
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class SystemCompensationCrush : UpgradeItem
  {
    public static float Modifier = 30f;

    public SystemCompensationCrush()
    {
      this.Name = "crash compensation system";
      this.Description = "Reduces collision damage by 30%";
      this.ShortDesc = string.Format("Collision Armor {0}%", (object) SystemCompensationCrush.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Upgrades/Item_crushCompensationSystem",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_crushCompensationSystem",
        OnCopterTexture = "Hangar/Upgrade/crushCompensationSystem"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.MountainDefenseModifier = (float) ((100.0 - (double) SystemCompensationCrush.Modifier) / 100.0);
    }
  }
}
