// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.SingleMachineGunWeaponSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.SpriteObjects.WeaponSprites
{
  internal class SingleMachineGunWeaponSpriteObject : WeaponSpriteObject
  {
    public override void Init(SpriteObject parent, Weapon weapon)
    {
      this.Offset = weapon.BaseWeaponPosition;
      this.FireAnimation = CommonAnimatedSprite.GetInstance();
      this.FireAnimation.Init("Effects/GunShot3/GunShot3XML");
      this.FireAnimation.Origin = new Vector2(18f, 23f);
      this.TexturePath = "GameWorld/Objects/Weapon/weaponS1_1";
      base.Init(parent, weapon);
    }
  }
}
