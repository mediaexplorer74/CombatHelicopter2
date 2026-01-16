// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.CopterSharkRocketLauncherWeaponSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects.WeaponSprites
{
  internal class CopterSharkRocketLauncherWeaponSpriteObject : WeaponSpriteObject
  {
    private static readonly ObjectPool<CopterSharkRocketLauncherWeaponSpriteObject> _pool = new ObjectPool<CopterSharkRocketLauncherWeaponSpriteObject>((ICreation<CopterSharkRocketLauncherWeaponSpriteObject>) new CopterSharkRocketLauncherWeaponSpriteObject.Creator());

    public static CopterSharkRocketLauncherWeaponSpriteObject GetInstance()
    {
      return CopterSharkRocketLauncherWeaponSpriteObject._pool.GetObject();
    }

    public new virtual void Release()
    {
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      this.ReleaseFromPool();
    }

    protected new virtual void ReleaseFromPool()
    {
      CopterSharkRocketLauncherWeaponSpriteObject._pool.Release(this);
    }

    private CopterSharkRocketLauncherWeaponSpriteObject()
    {
    }

    public override void Init(SpriteObject parent, Weapon weapon)
    {
      this.Offset = weapon.BaseWeaponPosition;
      this.TexturePath = "GameWorld/Objects/Weapon/weaponS2_1_type2";
      this.FireAnimation = CommonAnimatedSprite.GetInstance();
      this.FireAnimation.Init("Effects/SmokeDown/SmokeDownXML");
      base.Init(parent, weapon);
    }

    internal override Vector2 FireAnimationPosition() => this.Size / 2f;

    protected new class Creator : ICreation<CopterSharkRocketLauncherWeaponSpriteObject>
    {
      public CopterSharkRocketLauncherWeaponSpriteObject Create()
      {
        return new CopterSharkRocketLauncherWeaponSpriteObject();
      }
    }
  }
}
