// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.BulletSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class BulletSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<BulletSpriteObject> _pool = new ObjectPool<BulletSpriteObject>((ICreation<BulletSpriteObject>) new BulletSpriteObject.Creator());
    private CommonAnimatedSprite _deathSprite;
    private Vector2 _effectPosition;

    public static BulletSpriteObject GetInstance() => BulletSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => BulletSpriteObject._pool.Release(this);

    public override void ResetState()
    {
      this._effectPosition = Vector2.Zero;
      if (this._deathSprite != null)
      {
        this._deathSprite.Ended -= new EventHandler(this.OnDeathSpriteEnded);
        this._deathSprite.Release();
      }
      this._deathSprite = (CommonAnimatedSprite) null;
      this.IsVisible = true;
      base.ResetState();
    }

    public CommonAnimatedSprite DeathSprite
    {
      get => this._deathSprite;
      set
      {
        this._deathSprite = value;
        this._deathSprite.Ended -= new EventHandler(this.OnDeathSpriteEnded);
        this._deathSprite.Ended += new EventHandler(this.OnDeathSpriteEnded);
      }
    }

    protected bool IsVisible { get; set; }

    protected BulletSpriteObject() => this.IsVisible = true;

    public override void Update(Camera camera, float elapsedSeconds)
    {
      if (!this.IsVisible)
        return;
      base.Update(camera, elapsedSeconds);
      this.Rotation = ((Bullet) this.Instance).Angle;
      this._effectPosition = new Vector2((float) (this.Instance.Contour.Rectangle.Center.X - camera.Screen.X), this.Position.Y);
      if (this.Instance.State != 1 || this._deathSprite == null)
        return;
      this._deathSprite.Update(elapsedSeconds);
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      if (!this.IsVisible)
        return;
      if (this.Instance.State == 1)
      {
        if (this._deathSprite == null)
          return;
        this._deathSprite.Draw(spriteBatch, this._effectPosition);
      }
      else
        base.Draw(spriteBatch, parentPosition);
    }

    private void OnDeathSpriteEnded(object sender, EventArgs e)
    {
      this.Instance.IsNeedRemove = true;
      this.IsVisible = false;
    }

    public override void OnStateChanged(object instance, StateChangeEventArgs<int> stateChangeEvent)
    {
      if (stateChangeEvent.NextState != 1)
        return;
      if (((Bullet) this.Instance).Owner is Copter)
        this._deathSprite.SpriteEffects = SpriteEffects.FlipHorizontally;
      if (this._deathSprite == null)
        return;
      this._deathSprite.Play();
    }

    protected class Creator : ICreation<BulletSpriteObject>
    {
      public BulletSpriteObject Create() => new BulletSpriteObject();
    }
  }
}
