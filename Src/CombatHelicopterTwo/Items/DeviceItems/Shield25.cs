// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.DeviceItems.Shield25
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.WorldObjects.Instances;

#nullable disable
namespace Helicopter.Items.DeviceItems
{
  internal class Shield25 : UpgradeItem
  {
    public override void Apply(SmartPlayer player) => player.AmmoDefenseModifier = 0.25f;
  }
}
