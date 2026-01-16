// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.BulletSprites.ShieldBulletSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects.BulletSprites
{
  internal class ShieldBulletSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<ShieldBulletSpriteObject> _pool = new ObjectPool<ShieldBulletSpriteObject>((ICreation<ShieldBulletSpriteObject>) new ShieldBulletSpriteObject.Creator());

    public static ShieldBulletSpriteObject GetInstance()
    {
      return ShieldBulletSpriteObject._pool.GetObject();
    }

    protected override void ReleaseFromPool() => ShieldBulletSpriteObject._pool.Release(this);

    public override void ResetState() => base.ResetState();

    private ShieldBulletSpriteObject()
    {
    }

    public override void Init(Instance instance)
    {
      if (!(instance is Shield))
        throw new InvalidCastException("It can be only shield");
      this.Sprite = (Sprite) CommonAnimatedSprite.GetInstance();
      ((CommonAnimatedSprite) this.Sprite).Init("Effects/Shield/Shield1XML");
      this.Sprite.Origin = Vector2.Zero;
      ((CommonAnimatedSprite) this.Sprite).IsLooped = true;
      ((CommonAnimatedSprite) this.Sprite).Play();
      if ((instance as Shield).Owner is Copter)
        this.Sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
      base.Init(instance);
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      base.Draw(spriteBatch, parentPosition);
    }

    protected class Creator : ICreation<ShieldBulletSpriteObject>
    {
      public ShieldBulletSpriteObject Create() => new ShieldBulletSpriteObject();
    }
  }
}
