// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.Ammunition.AmmunitionItem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter.Items.Ammunition
{
  internal class AmmunitionItem : Item
  {
    public AmunitionType Type { get; set; }

    public override string Id => this.Type.ToString();

    public AmmunitionItem() => this.UnlockCondition = new UnlockCondition();
  }
}
