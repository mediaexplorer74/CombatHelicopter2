// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.IUnit
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Modifiers;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  public interface IUnit
  {
    float Energy { get; set; }

    float MaxEnergy { get; set; }

    float EnergyRegeneration { get; set; }

    int Team { get; set; }

    float AmmoDefenseModifier { get; set; }

    float MountainDefenseModifier { get; set; }

    float Score { get; }

    float Price { get; }

    UnitType UnitType { get; }

    event EventHandler<StateChangeEventArgs<int>> StateChanged;

    event EventHandler<PlayerEventArgs> Damaged;

    event EventHandler<WeaponEventArgs> WeaponFired;

    void HandleCollisionDamage(float damage);

    void HandleDamage(float damage, DamageType damageType);

    float CollisionDamage { get; set; }
  }
}
