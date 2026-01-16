// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.DamageControlSystemV2
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class DamageControlSystemV2 : UpgradeItem
  {
    public static float Modifier = 25f;

    public DamageControlSystemV2()
    {
      this.Name = "damage control system v2.0";
      this.Description = string.Format("reduces incoming damage by {0}%", (object) DamageControlSystemV2.Modifier);
      this.ShortDesc = string.Format("Armor {0}%", (object) DamageControlSystemV2.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnCopterTexture = "Hangar/Upgrade/damageControlSystem2",
        OnShopTexture = "Hangar/Items/Upgrades/Item_damageControlSystem2",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_damageControlSystem2"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.AmmoDefenseModifier = DamageControlSystemV2.Modifier / 100f;
    }
  }
}
