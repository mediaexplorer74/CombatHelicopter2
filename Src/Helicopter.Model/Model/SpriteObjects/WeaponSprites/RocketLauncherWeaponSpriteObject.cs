// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.RocketLauncherWeaponSpriteObject
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
  internal class RocketLauncherWeaponSpriteObject : WeaponSpriteObject
  {
    private static readonly ObjectPool<RocketLauncherWeaponSpriteObject> _pool = new ObjectPool<RocketLauncherWeaponSpriteObject>((ICreation<RocketLauncherWeaponSpriteObject>) new RocketLauncherWeaponSpriteObject.Creator());

    public new virtual void Release()
    {
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      this.ReleaseFromPool();
    }

    public static RocketLauncherWeaponSpriteObject GetInstance()
    {
      return RocketLauncherWeaponSpriteObject._pool.GetObject();
    }

    protected override void ReleaseFromPool()
    {
      RocketLauncherWeaponSpriteObject._pool.Release(this);
    }

    private RocketLauncherWeaponSpriteObject()
    {
    }

    public override void Init(SpriteObject parent, Weapon weapon)
    {
      this.Offset = weapon.BaseWeaponPosition;
      this.TexturePath = "GameWorld/Objects/Weapon/weaponS2_1";
      this.FireAnimation = CommonAnimatedSprite.GetInstance();
      this.FireAnimation.Init("Effects/SmokeDown/SmokeDownXML");
      base.Init(parent, weapon);
    }

    internal override Vector2 FireAnimationPosition() => this.Size / 2f;

    protected new class Creator : ICreation<RocketLauncherWeaponSpriteObject>
    {
      public RocketLauncherWeaponSpriteObject Create() => new RocketLauncherWeaponSpriteObject();
    }
  }
}
