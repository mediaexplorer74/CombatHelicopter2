// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.BulletSprites.RocketSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects.BulletSprites
{
  internal class RocketSpriteObject : BulletSpriteObject
  {
    private static readonly ObjectPool<RocketSpriteObject> _pool = new ObjectPool<RocketSpriteObject>((ICreation<RocketSpriteObject>) new RocketSpriteObject.Creator());

    public static RocketSpriteObject GetInstance() => RocketSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => RocketSpriteObject._pool.Release(this);

    protected RocketSpriteObject()
    {
    }

    public override void Init(Instance instance)
    {
      if (!(instance is RocketBullet))
        throw new ArgumentOutOfRangeException(nameof (instance), "It can be only RocketBullet type");
      base.Init(instance);
      this.Sprite = Sprite.GetInstance();
      string str1 = "GameWorld/Objects/Weapon";
      string str2 = !(instance is HomingMissleBullet) ? (!(instance is DoubleRocketBullet) ? "weaponS2_1patron" : "weaponS2_2patron") : "weaponS2_3patron";
      this.Sprite = SpriteObjectPool.Instance.GetSimpleSOByDesc(new SpriteDescription()
      {
        TexturePath = str1 + "/" + str2
      }).Sprite;
      CommonAnimatedSprite instance1 = CommonAnimatedSprite.GetInstance();
      instance1.Init("Effects/RocketFire/RocketFireXML");
      instance1.IsLooped = true;
      instance1.OffsetParent = new Vector2((float) (-(double) this.Sprite.SourceSize.X / 2.0), 0.0f);
      instance1.Play();
      SimpleSpriteObject instance2 = SimpleSpriteObject.GetInstance();
      instance2.Sprite = (Sprite) instance1;
      instance2.Offset = new Vector2(0.0f, this.Sprite.SourceSize.Y / 2f);
      instance2.RotatedOffset = new Vector2(instance2.Offset.X, instance2.Offset.Y);
      this.AddChildren((ISpriteObject) instance2);
      CommonAnimatedSprite instance3 = CommonAnimatedSprite.GetInstance();
      instance3.Init("Effects/RocketSmoke/RocketSmokeXML");
      instance3.IsLooped = true;
      instance3.OffsetParent = new Vector2((float) (-(double) instance1.SourceSize.X / 2.0), 0.0f);
      instance3.Play();
      SimpleSpriteObject instance4 = SimpleSpriteObject.GetInstance();
      instance4.Sprite = (Sprite) instance3;
      instance4.Offset = new Vector2((float) -(int) ((double) instance3.SourceSize.X / 2.0), (float) (int) ((double) this.Sprite.SourceSize.Y / 2.0));
      instance4.RotatedOffset = new Vector2(instance4.Offset.X, instance4.Offset.Y);
      this.AddChildren((ISpriteObject) instance4);
      this.Sprite.Rotation = this.Instance.Rotation;
    }

    protected new class Creator : ICreation<RocketSpriteObject>
    {
      public RocketSpriteObject Create() => new RocketSpriteObject();
    }
  }
}
