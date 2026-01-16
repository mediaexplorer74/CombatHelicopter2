// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.SmartPlayerSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  public class SmartPlayerSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<SmartPlayerSpriteObject> _pool = new ObjectPool<SmartPlayerSpriteObject>((ICreation<SmartPlayerSpriteObject>) new SmartPlayerSpriteObject.Creator());
    private Vector2 _deathPosition;
    private ISpriteObject _damageTexture1;
    private ISpriteObject _damageTexture2;
    public CommonAnimatedSprite ShieldAnimation;

    public static SmartPlayerSpriteObject GetInstance()
    {
      return SmartPlayerSpriteObject._pool.GetObject();
    }

    protected override void ReleaseFromPool() => SmartPlayerSpriteObject._pool.Release(this);

    public override void ResetState()
    {
      base.ResetState();
      this._deathPosition = Vector2.Zero;
      this._damageTexture1.Release();
      this._damageTexture2.Release();
      this.ShieldAnimation.Release();
      this._damageTexture1 = (ISpriteObject) null;
      this._damageTexture2 = (ISpriteObject) null;
      this.ShieldAnimation = (CommonAnimatedSprite) null;
    }

    public CommonAnimatedSprite DeathSprite { get; set; }

    protected SmartPlayerSpriteObject()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      if (this.Instance.State == 1)
      {
        this.DeathSprite.Draw(spriteBatch, this._deathPosition);
      }
      else
      {
        base.Draw(spriteBatch, parentPosition);
        SmartPlayer instance = (SmartPlayer) this.Instance;
        if ((double) instance.EnergyPercent < 0.6)
        {
          if ((double) instance.EnergyPercent > 0.3)
            this._damageTexture1.Draw(spriteBatch, this.Position);
          else
            this._damageTexture2.Draw(spriteBatch, this.Position);
        }
        if (instance.State != 3)
          return;
        this.ShieldAnimation.Draw(spriteBatch, this.Position + new Vector2((float) this.Instance.Contour.Rectangle.Width / 2f, (float) this.Instance.Contour.Rectangle.Height / 2f));
      }
    }

    public override void Update(Camera camera, float elapsedSeconds)
    {
      if (this.Instance.State == 1)
      {
        this._deathPosition = (this.Instance as SmartPlayer).DeathPosition;
        this._deathPosition.X -= (float) camera.Screen.X;
        this._deathPosition.Y -= (float) camera.Screen.Y;
        this.DeathSprite.Update(elapsedSeconds);
      }
      if (this.Instance.State == 3)
        this.ShieldAnimation.Update(elapsedSeconds);
      base.Update(camera, elapsedSeconds);
    }

    public override void AddChildren(ISpriteObject spriteObject)
    {
      switch (spriteObject.SpriteID)
      {
        case "Damaged1":
          this._damageTexture1 = spriteObject;
          break;
        case "Damaged2":
          this._damageTexture2 = spriteObject;
          break;
        default:
          base.AddChildren(spriteObject);
          break;
      }
    }

    public override void UpdateRotation()
    {
      base.UpdateRotation();
      this._damageTexture1.Rotation = this._damageTexture2.Rotation = this.Rotation;
      this.ShieldAnimation.Rotation = this.Rotation;
    }

    protected class Creator : ICreation<SmartPlayerSpriteObject>
    {
      public SmartPlayerSpriteObject Create() => new SmartPlayerSpriteObject();
    }
  }
}
