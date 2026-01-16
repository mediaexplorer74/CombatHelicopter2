// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.RocketWeaponDescription
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  public class RocketWeaponDescription : WeaponDescription
  {
    public float LaunchEngineTime = 0.2f;
    public float StartAccelerationY = 300f;
    public float StartAccelerationX;
    public float StartSpeedX = 10f;
    public float StartSpeedY = 300f;
    public bool HasStartStage;
  }
}
