// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.Sprites.CommonAnimatedSprite
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects.Sprites
{
  public class CommonAnimatedSprite : AnimatedSprite
  {
    private static readonly ObjectPool<CommonAnimatedSprite> _pool = new ObjectPool<CommonAnimatedSprite>((ICreation<CommonAnimatedSprite>) new CommonAnimatedSprite.Creator());
    private readonly List<TextureAtlasPart> _textureParts = new List<TextureAtlasPart>();

    public static CommonAnimatedSprite GetInstance() => CommonAnimatedSprite._pool.GetObject();

    protected override void ReleaseFromPool() => CommonAnimatedSprite._pool.Release(this);

    public override void ResetState()
    {
      this._textureParts.Clear();
      this.Ended = (EventHandler) null;
      this.Start = (EventHandler) null;
      this.Playing = true;
      this.IsLooped = false;
      base.ResetState();
    }

    public event EventHandler Ended;

    public event EventHandler Start;

    public bool Playing { get; set; }

    public bool IsLooped { get; set; }

    public float FullTime => this._frameTime * (float) this._textureParts.Count;

    protected CommonAnimatedSprite() => this.Playing = true;

    public override void Update(float elapsedSeconds)
    {
      if (this.Children != null)
      {
        foreach (Sprite child in this.Children)
          child.Update(elapsedSeconds);
      }
      if (!this.Visible || !this.Playing)
        return;
      int n = this._currentFrame;
      this._currentFrameTime += elapsedSeconds;
      if ((double) this._currentFrameTime > (double) this._frameTime && this._textureParts.Count > 0)
      {
        n = (this._currentFrame + 1) % this._textureParts.Count;
        this._currentFrameTime = 0.0f;
      }
      if (n == this._currentFrame)
        return;
      if (this._currentFrame == this._textureParts.Count - 1)
        this.OnAnimationEnded();
      this.SelectFrame(n);
    }

    public void OnAnimationEnded()
    {
      this.Visible = this.Playing = this.IsLooped;
      if (this.Ended == null)
        return;
      this.Ended((object) this, EventArgs.Empty);
    }

    public void Init(string fileName)
    {
      this._textureParts.Clear();
      TextureAtlas resource = ResourcesManager.Instance.GetResource<TextureAtlas>(fileName);
      foreach (TextureAtlasPart part in resource.Parts)
        this._textureParts.Add(part);
      if (resource.Parts.Count > 0)
        this._frameTime = resource.FullTime / (float) resource.Parts.Count;
      this.Texture = ResourcesManager.Instance.GetResource<Texture2D>(resource.ImagePath);
      this._currentFrameTime = 0.0f;
      this.Origin = new Vector2((float) Math.Max(this._textureParts[0].Width, this._textureParts[0].OffsetWidth) / 2f, (float) Math.Max(this._textureParts[0].Height, this._textureParts[0].OffsetHeight) / 2f);
      this.SelectFrame(0);
      this.Visible = this.Playing = false;
    }

    private void SelectFrame(int n)
    {
      this._currentFrame = n;
      TextureAtlasPart texturePart = this._textureParts[n];
      this.SourceRectangle = texturePart.Rectangle;
      this.Trimmed = (double) Math.Abs(texturePart.OffsetX + texturePart.OffsetY) > 0.001;
      this.TrimmedOffset.X = (float) texturePart.OffsetX;
      this.TrimmedOffset.Y = (float) texturePart.OffsetY;
    }

    public void Pause() => this.Playing = false;

    public void Play()
    {
      this._currentFrameTime = 0.0f;
      this.SelectFrame(0);
      this.Visible = this.Playing = true;
      if (this.Start == null)
        return;
      this.Start((object) this, EventArgs.Empty);
    }

    public void FlipOriginHorizontally()
    {
      this.Origin = new Vector2((float) Math.Max(this._textureParts[0].Width, this._textureParts[0].OffsetWidth), (float) Math.Max(this._textureParts[0].Height, this._textureParts[0].OffsetHeight)) - this.Origin;
    }

    protected new class Creator : ICreation<CommonAnimatedSprite>
    {
      public CommonAnimatedSprite Create()
      {
        CommonAnimatedSprite commonAnimatedSprite = new CommonAnimatedSprite();
        commonAnimatedSprite.Visible = true;
        return commonAnimatedSprite;
      }
    }
  }
}
