// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.DamageControlSystemV1
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class DamageControlSystemV1 : UpgradeItem
  {
    public static float Modifier = 10f;

    public DamageControlSystemV1()
    {
      this.Name = "damage control system v1.0";
      this.Description = string.Format("reduces incoming damage by {0}%", (object) DamageControlSystemV1.Modifier);
      this.ShortDesc = string.Format("Armor {0}%", (object) DamageControlSystemV1.Modifier);
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnCopterTexture = "Hangar/Upgrade/damageControlSystem1",
        OnShopTexture = "Hangar/Items/Upgrades/Item_damageControlSystem1",
        OnShopBoughtTexture = "Hangar/ItemsGreen/Upgrades/Item_damageControlSystem1"
      };
    }

    public override void Apply(SmartPlayer player)
    {
      player.AmmoDefenseModifier = DamageControlSystemV1.Modifier / 100f;
    }
  }
}
