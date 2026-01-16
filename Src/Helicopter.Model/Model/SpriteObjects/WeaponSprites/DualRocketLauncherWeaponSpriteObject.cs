// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.DualRocketLauncherWeaponSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.SpriteObjects.WeaponSprites
{
  internal class DualRocketLauncherWeaponSpriteObject : WeaponSpriteObject
  {
    public override void Init(SpriteObject parent, Weapon weapon)
    {
      this.Offset = weapon.BaseWeaponPosition;
      this.FireAnimation = CommonAnimatedSprite.GetInstance();
      this.FireAnimation.Init("Effects/SmokeDown/SmokeDownXML");
      this.TexturePath = "GameWorld/Objects/Weapon/weaponS2_2";
      base.Init(parent, weapon);
    }

    internal override Vector2 FireAnimationPosition() => this.Size / 2f;
  }
}
