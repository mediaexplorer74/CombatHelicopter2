// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.Sprites.AnimatedSprite
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Helicopter.Model.SpriteObjects.Sprites
{
  public class AnimatedSprite : Sprite
  {
    private static readonly ObjectPool<AnimatedSprite> _pool = new ObjectPool<AnimatedSprite>((ICreation<AnimatedSprite>) new AnimatedSprite.Creator());
    private static float _commonAnimationTimer;
    protected int _currentFrame;
    protected float _currentFrameTime;
    protected Rectangle _frameRectangle;
    protected float _frameTime;
    private int _framesCount;
    private bool _syncAnimation;
    protected Rectangle SpriteSourceRectangle;

    public static AnimatedSprite GetInstance() => AnimatedSprite._pool.GetObject();

    protected override void ReleaseFromPool() => AnimatedSprite._pool.Release(this);

    public override void ResetState()
    {
      base.ResetState();
      this._syncAnimation = false;
      this._framesCount = 0;
      this._currentFrame = 0;
      this._currentFrameTime = 0.0f;
      this._frameRectangle = Rectangle.Empty;
      this._frameTime = 0.0f;
    }

    protected AnimatedSprite()
    {
    }

    public override void Update(float elapsedSeconds)
    {
      int num = this._currentFrame;
      if (this._syncAnimation)
      {
        num = (int) ((double) AnimatedSprite._commonAnimationTimer / (double) this._frameTime) % this._framesCount;
      }
      else
      {
        this._currentFrameTime += elapsedSeconds;
        if ((double) this._currentFrameTime > (double) this._frameTime)
        {
          num = (this._currentFrame + 1) % this._framesCount;
          this._currentFrameTime = 0.0f;
        }
      }
      if (num != this._currentFrame)
      {
        this.SourceRectangle.X = this.SpriteSourceRectangle.X + this._frameRectangle.Width * num;
        this._currentFrame = num;
      }
      base.Update(elapsedSeconds);
    }

    public void Init(
      Texture2D spriteSheet,
      Rectangle frameDestRect,
      Rectangle frameSourceRect,
      float frameTime,
      Vector2 offsetParent,
      bool sync)
    {
      this._syncAnimation = sync;
      this.Init(spriteSheet, frameDestRect, frameSourceRect, frameTime, offsetParent);
    }

    public void Init(
      Texture2D spriteSheet,
      Rectangle frameDestRect,
      Rectangle frameSourceRect,
      float frameTime,
      Vector2 offsetParent)
    {
      this.Init(spriteSheet, offsetParent);
      this._frameTime = frameTime;
      this._frameRectangle = frameSourceRect;
      this.SourceRectangle = frameSourceRect;
      this._currentFrameTime = 0.0f;
      this._framesCount = spriteSheet.Width / frameSourceRect.Width;
    }

    public void Init(
      Sprite spriteSheet,
      Rectangle frameDestRect,
      Rectangle frameSourceRect,
      float frameTime,
      Vector2 offsetParent,
      bool sync)
    {
      this._syncAnimation = sync;
      this.Init(spriteSheet, frameDestRect, frameSourceRect, frameTime, offsetParent);
    }

    public void Init(
      Sprite spriteSheet,
      Rectangle frameDestRect,
      Rectangle frameSourceRect,
      float frameTime,
      Vector2 offsetParent)
    {
      this.Init(spriteSheet.Texture, offsetParent);
      this.SpriteSourceRectangle = spriteSheet.SourceRectangle;
      this._frameTime = frameTime;
      this._frameRectangle = frameSourceRect;
      this.SourceRectangle = frameSourceRect;
      this.SourceRectangle.X += this.SpriteSourceRectangle.X;
      this.SourceRectangle.Y = this.SpriteSourceRectangle.Y;
      this._currentFrameTime = 0.0f;
      this._framesCount = spriteSheet.SourceRectangle.Width / frameSourceRect.Width;
    }

    public static void UpdatecommonAnimationTimer(float elapsedSeconds)
    {
      AnimatedSprite._commonAnimationTimer += elapsedSeconds;
    }

    protected new class Creator : ICreation<AnimatedSprite>
    {
      public AnimatedSprite Create() => new AnimatedSprite();
    }
  }
}
