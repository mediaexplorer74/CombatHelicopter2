// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.Item
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;

#nullable disable
namespace Helicopter.Items
{
  internal class Item
  {
    public HangarLayoutDescription HangarDesc;

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual string Id { get; protected set; }

    public UnlockCondition UnlockCondition { get; set; }

    public int Price => this.UnlockCondition.Price;

    public static Item Empty => new Item();

    public bool IsBought
    {
      get => this.UnlockCondition.Unlocked;
      set => this.UnlockCondition.IsBought = value;
    }
  }
}
