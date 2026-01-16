// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Descriptions.CannonPatternDesc
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Model.Descriptions
{
  public class CannonPatternDesc : PatternDesc
  {
    public float MoutonPath;
    public float MoutonSpeed;
    public float HitCorridor;
    public float MoutionRange;
    public float Price;
    public float Energy;
    public float CollisionDamage;

    public VerticalAlignment Alignment { get; set; }

    public WeaponSlotDescription WeaponSlotDesc { get; set; }

    public UnitType UnitType { get; set; }
  }
}
