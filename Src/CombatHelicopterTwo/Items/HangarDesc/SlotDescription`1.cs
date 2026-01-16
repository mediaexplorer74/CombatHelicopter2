// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.HangarDesc.SlotDescription`1
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter.Items.HangarDesc
{
  internal class SlotDescription<T> where T : Helicopter.Items.Item
  {
    public T Item { get; set; }

    public bool IsInstalled => (object) this.Item != null;

    public void Clear() => this.Item = default (T);

    public string Serialize() => !this.IsInstalled ? string.Empty : this.Item.Id;
  }
}
