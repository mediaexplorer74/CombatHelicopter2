// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.HelicopterItem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.WorldObjects.Patterns;

#nullable disable
namespace Helicopter.Items
{
  internal class HelicopterItem : Item
  {
    public bool HasFirstWeapon = true;
    public bool HasSecondWeapon;
    public bool HasUpgradeA;
    public bool HasUpgradeB;

    public HelicopterType HelicopterType { get; set; }

    public float Energy { get; private set; }

    public PlayerPattern Pattern { get; set; }

    public HelicopterItem(PlayerPattern pattern)
    {
      this.Pattern = pattern;
      this.Energy = pattern.Energy;
    }

    public override string Id => this.HelicopterType.ToString();
  }
}
