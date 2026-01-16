// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Patterns.CannonPattern
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  public class CannonPattern : Pattern
  {
    public float CollisionDamage;
    public WeaponSlotDescription WeaponSlotDesc;
    public float MoutonPath;
    public float MoutonSpeed;
    public float HitCorridor;
    public float MoutionRange;
    public float Price;
    public float Energy;

    public VerticalAlignment Alignment { get; set; }

    public UnitType UnitType { get; set; }
  }
}
