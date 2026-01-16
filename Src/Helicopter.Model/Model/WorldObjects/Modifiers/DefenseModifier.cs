// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Modifiers.DefenseModifier
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Modifiers
{
  public class DefenseModifier : Modifier
  {
    private readonly Dictionary<DamageType, float> _defenseCoefs;

    public DefenseModifier()
    {
      this._defenseCoefs = new Dictionary<DamageType, float>();
      this._defenseCoefs.Add(DamageType.Bullet, 0.0f);
      this._defenseCoefs.Add(DamageType.Rocket, 0.0f);
      this._defenseCoefs.Add(DamageType.Collision, 0.0f);
    }

    public void AddDefenseCoef(DamageType damageType, float coef)
    {
      this._defenseCoefs[damageType] = coef;
    }

    public float GetDefenseCoef(DamageType damageType) => this._defenseCoefs[damageType];
  }
}
