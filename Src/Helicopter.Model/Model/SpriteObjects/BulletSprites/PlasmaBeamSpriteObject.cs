// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.BulletSprites.PlasmaBeamSpriteObject
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
namespace Helicopter.Model.SpriteObjects.BulletSprites
{
  internal class PlasmaBeamSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<PlasmaBeamSpriteObject> _pool = new ObjectPool<PlasmaBeamSpriteObject>((ICreation<PlasmaBeamSpriteObject>) new PlasmaBeamSpriteObject.Creator());
    private CommonAnimatedSprite _damageDot;
    private Rectangle _destination;
    private CommonAnimatedSprite _loopedAnimation;
    private Vector2 _dotPosition;
    private Vector2 _dest;

    public static PlasmaBeamSpriteObject GetInstance() => PlasmaBeamSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => PlasmaBeamSpriteObject._pool.Release(this);

    public Sprite BeamTexture { get; set; }

    private PlasmaBeamSpriteObject() => this.ZIndex = 100500f;

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      spriteBatch.Draw(this.BeamTexture.Texture, this._destination, new Rectangle?(this.BeamTexture.SourceRectangle), Color.White);
      if (this._destination.Height != 5)
        return;
      this._loopedAnimation.Draw(spriteBatch, this._dest);
      if ((double) this._dotPosition.X >= 800.0)
        return;
      this._damageDot.Draw(spriteBatch, this._dotPosition);
    }

    public override void Update(Camera camera, float elapsedSeconds)
    {
      this._destination = this.Instance.Contour.Rectangle;
      this._destination.X -= camera.Screen.X;
      this._destination.Y -= camera.Screen.Y;
      this._damageDot.Update(elapsedSeconds);
      this._loopedAnimation.Update(elapsedSeconds);
      PlasmaBeam instance = this.Instance as PlasmaBeam;
      if (((PlasmaBeam) this.Instance).Owner is Copter)
      {
        if ((double) instance.IntersectionPosition.X > 0.0)
        {
          this._destination.X = (int) ((double) instance.IntersectionPosition.X - (double) camera.Screen.X);
          this._destination.Width = (int) ((double) instance.GlobalStartPosition.X - (double) instance.IntersectionPosition.X);
        }
        else
        {
          this._destination.X = (int) ((double) instance.GlobalStartPosition.X - 605.0 - (double) camera.Screen.X);
          this._destination.Width = 605;
        }
        this._dest = new Vector2((float) this._destination.Right - this._loopedAnimation.SourceSize.X / 2f, (float) this._destination.Center.Y);
        this._dotPosition = new Vector2((float) this._destination.X, (float) this._destination.Center.Y);
      }
      else
      {
        this._destination.Width = (double) instance.IntersectionPosition.X <= 0.0 ? 605 : (int) ((double) instance.IntersectionPosition.X - (double) instance.GlobalStartPosition.X);
        this._dest = new Vector2((float) this._destination.X + this._loopedAnimation.SourceSize.X / 2f, (float) this._destination.Center.Y);
        this._dotPosition = new Vector2((float) this._destination.Right, (float) this._destination.Center.Y);
      }
    }

    public override void Init(Instance instance)
    {
      PlasmaBeam plasmaBeam = instance is PlasmaBeam ? (PlasmaBeam) instance : throw new InvalidCastException("It can be only plasmabeam");
      this._damageDot = CommonAnimatedSprite.GetInstance();
      if (plasmaBeam.Owner is Copter)
        this._damageDot.Init("Effects/PlazmaShotRed/PlazmaPointXML");
      else
        this._damageDot.Init("Effects/PlazmaShot/PlazmaPoint/PlazmaPointXML");
      this._damageDot.IsLooped = true;
      this._damageDot.Origin = new Vector2(128f, 88f);
      this._damageDot.Play();
      this._loopedAnimation = CommonAnimatedSprite.GetInstance();
      if (plasmaBeam.Owner is Copter)
        this._loopedAnimation.Init("Effects/PlazmaShotRed/PlazmaShotLoopXML");
      else
        this._loopedAnimation.Init("Effects/PlazmaShot/PlazmaShotLoopXML");
      this._loopedAnimation.Origin = new Vector2(19f, 26f);
      this._loopedAnimation.IsLooped = true;
      this._loopedAnimation.FlipOriginHorizontally();
      this._loopedAnimation.Play();
      if (plasmaBeam.Owner is Copter)
      {
        this._loopedAnimation.SpriteEffects = SpriteEffects.FlipHorizontally;
        this._loopedAnimation.Origin = new Vector2(this._loopedAnimation.SourceSize.X - this._loopedAnimation.Origin.X, this._loopedAnimation.Origin.Y);
        this._damageDot.SpriteEffects = SpriteEffects.FlipHorizontally;
        this._damageDot.Origin = new Vector2(this._damageDot.SourceSize.X - this._damageDot.Origin.X, this._damageDot.Origin.Y);
      }
      this.BeamTexture = !(plasmaBeam.Owner is Copter) ? ResourcesManager.Instance.GetSprite("Effects/PlazmaShot/PlazmaRay") : ResourcesManager.Instance.GetSprite("Effects/PlazmaShotRed/PlazmaRay");
      base.Init(instance);
      this.ZIndex = 100500f;
    }

    public override void ResetState()
    {
      this._damageDot.Release();
      this._damageDot = (CommonAnimatedSprite) null;
      this._loopedAnimation.Release();
      this._loopedAnimation = (CommonAnimatedSprite) null;
      this._destination = Rectangle.Empty;
      this.BeamTexture = (Sprite) null;
      base.ResetState();
      this.ZIndex = 100500f;
    }

    protected class Creator : ICreation<PlasmaBeamSpriteObject>
    {
      public PlasmaBeamSpriteObject Create() => new PlasmaBeamSpriteObject();
    }
  }
}
