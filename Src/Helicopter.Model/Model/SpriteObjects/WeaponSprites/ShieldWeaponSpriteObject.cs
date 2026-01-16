// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.ShieldWeaponSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects.WeaponSprites
{
  internal class ShieldWeaponSpriteObject : WeaponSpriteObject
  {
    public override void Init(SpriteObject parent, Weapon weapon)
    {
      this.Offset = weapon.BaseWeaponPosition;
      this.TexturePath = "GameWorld/Objects/Weapon/weaponS2_4";
      base.Init(parent, weapon);
      this.Weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapon.ShootingState += new EventHandler(this.OnShootingState);
    }

    private void OnShootingState(object sender, EventArgs e)
    {
    }
  }
}
