// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.DeviceBonus.HealthPack50
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.WorldObjects.DeviceBonus
{
  public class HealthPack50(IUnit owner) : GameItem(owner)
  {
    public override void Apply()
    {
      if ((double) this.Owner.Energy >= 0.05000000074505806)
        return;
      this.Owner.Energy = 0.5f * this.Owner.MaxEnergy;
    }
  }
}
