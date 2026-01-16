// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.WeaponItem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Items
{
  internal class WeaponItem : Item
  {
    public WeaponType WeaponType { get; set; }

    public Slot Slot { get; set; }

    public float Damage { get; set; }

    public WeaponRate Rate { get; set; }

    public override string Id => this.WeaponType.ToString();
  }
}
