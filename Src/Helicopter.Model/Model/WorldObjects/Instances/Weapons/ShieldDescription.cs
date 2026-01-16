// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.ShieldDescription
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class ShieldDescription : WeaponDescription
  {
    public float RechargeSpeed = 10f;
    public float DecreaseSpeed = 20f;
    public float CurrentEnergy = 100f;
    public float MaxEnergy = 100f;
  }
}
