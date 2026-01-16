// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.Sprites.Sprite
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
  public class Sprite : IReusable
  {
    private static readonly ObjectPool<Sprite> _pool = new ObjectPool<Sprite>((ICreation<Sprite>) new Sprite.Creator());
    protected internal List<Sprite> Children;
    private float _rotation;
    private Vector2 _scale;
    private SpriteEffects _spriteEffects;
    public Rectangle SourceRectangle;
    public Vector2 TrimmedOffset = Vector2.Zero;

    public static Sprite GetInstance()
    {
      Sprite instance = Sprite._pool.GetObject();
      instance.Texture = ResourcesManager.BlankSprite.Texture;
      return instance;
    }

    public static Sprite GetInstance(Texture2D texture)
    {
      Sprite instance = Sprite._pool.GetObject();
      instance.Init(texture);
      return instance;
    }

    public virtual void Release()
    {
      if (this.Children != null)
        this.Children.ForEach((Action<Sprite>) (x => x.Release()));
      this.ReleaseFromPool();
    }

    protected virtual void ReleaseFromPool() => Sprite._pool.Release(this);

    public Vector2 Origin { get; set; }

    public float Rotation
    {
      get => this._rotation;
      set
      {
        if ((double) Math.Abs(this._rotation - value) < 9.9999997473787516E-05)
          return;
        this._rotation = value;
        this.OffsetParentRotated = this.RotatePoint(this.OffsetParent, -this.Rotation);
        if (this.Children == null)
          return;
        this.Children.ForEach((Action<Sprite>) (x => x.Rotation = this._rotation));
      }
    }

    public Rectangle Bounds
    {
      get => new Rectangle(0, 0, (int) this.SourceSize.X, (int) this.SourceSize.Y);
    }

    protected Vector2 RotatePoint(Vector2 point, float angle)
    {
      point.X = (float) ((double) point.X * Math.Cos((double) angle) - (double) point.Y * Math.Sin((double) angle));
      point.Y = (float) ((double) point.X * Math.Sin((double) angle) + (double) point.Y * Math.Cos((double) angle));
      return point;
    }

    public Color Color { get; set; }

    public Vector2 Scale
    {
      get => this._scale;
      set => this._scale = value;
    }

    public Vector2 ScaledSize => this.SourceSize * this.Scale;

    public Vector2 SourceSize
    {
      get => new Vector2((float) this.SourceRectangle.Width, (float) this.SourceRectangle.Height);
    }

    public Vector2 OffsetParent { get; set; }

    public Vector2 OffsetParentRotated { get; set; }

    public SpriteEffects SpriteEffects
    {
      get => this._spriteEffects;
      set
      {
        if (this._spriteEffects == value)
          return;
        this._spriteEffects = value;
        if (this.Children == null)
          return;
        this.Children.ForEach((Action<Sprite>) (x => x.SpriteEffects = value));
      }
    }

    public Texture2D Texture { get; protected set; }

    public float Alpha { get; set; }

    public bool Visible { get; set; }

    public bool Trimmed { get; set; }

    protected Sprite()
    {
      this.Color = Color.White;
      this.Alpha = 1f;
      this.Visible = true;
      this._scale = new Vector2(1f, 1f);
      this.Children = new List<Sprite>();
    }

    public virtual void ResetState()
    {
      if (this.Children != null)
        this.Children.Clear();
      this.OffsetParent = Vector2.Zero;
      this._scale = new Vector2(1f, 1f);
      this.SpriteEffects = SpriteEffects.None;
      this.SourceRectangle = Rectangle.Empty;
      this.Texture = (Texture2D) null;
      this.Origin = Vector2.Zero;
      this.Visible = true;
      this.Alpha = 1f;
      this.Rotation = 0.0f;
      this.Color = Color.White;
      this.Trimmed = false;
      this.TrimmedOffset = Vector2.Zero;
    }

    public virtual void Update(float elapsedSeconds)
    {
      if (this.Children == null)
        return;
      foreach (Sprite child in this.Children)
        child.Update(elapsedSeconds);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 destination)
    {
      this.Draw(spriteBatch, destination, this.Color);
    }

    protected virtual void Draw(SpriteBatch spriteBatch, Vector2 destination, Color color)
    {
      if (this.Visible)
      {
        if (this.Trimmed)
          spriteBatch.Draw(this.Texture, destination + this.TrimmedOffset, new Rectangle?(this.SourceRectangle), color * this.Alpha, this.Rotation, this.Origin, this._scale, this.SpriteEffects, 0.0f);
        else
          spriteBatch.Draw(this.Texture, destination, new Rectangle?(this.SourceRectangle), color * this.Alpha, this.Rotation, this.Origin, this._scale, this.SpriteEffects, 0.0f);
      }
      if (this.Children == null)
        return;
      foreach (Sprite child in this.Children)
        child.Draw(spriteBatch, destination + child.OffsetParentRotated, color);
    }

    public void AddChildren(Sprite sprite)
    {
      if (this.Children == null)
        this.Children = new List<Sprite>();
      this.Children.Add(sprite);
    }

    public void Init(Texture2D texture, Rectangle sourceRectangle)
    {
      this.Texture = texture ?? ResourcesManager.BlankSprite.Texture;
      this.SourceRectangle = sourceRectangle;
      this.Origin = Vector2.Zero;
      this.Rotation = 0.0f;
    }

    public void Init(Texture2D texture, Rectangle sourceRectangle, Vector2 offsetParent)
    {
      this.Init(texture, sourceRectangle);
      this.OffsetParent = offsetParent;
    }

    public void Init(Texture2D texture, Vector2 offsetParent)
    {
      this.Init(texture, texture.Bounds, offsetParent);
    }

    public void Init(Texture2D texture) => this.Init(texture, Vector2.Zero);

    public void RemoveChildren(Sprite sprite)
    {
      if (this.Children == null || !this.Children.Contains(sprite))
        return;
      this.Children.Remove(sprite);
    }

    public virtual Sprite Copy()
    {
      List<Sprite> spriteList = new List<Sprite>();
      this.Children.ForEach(new Action<Sprite>(spriteList.Add));
      return new Sprite()
      {
        Texture = this.Texture,
        _rotation = this._rotation,
        _scale = this._scale,
        SpriteEffects = this.SpriteEffects,
        Color = this.Color,
        Alpha = this.Alpha,
        OffsetParent = this.OffsetParent,
        OffsetParentRotated = this.OffsetParentRotated,
        Origin = this.Origin,
        Rotation = this.Rotation,
        Scale = this.Scale,
        SourceRectangle = this.SourceRectangle,
        Visible = this.Visible,
        _spriteEffects = this._spriteEffects,
        Children = spriteList
      };
    }

    protected class Creator : ICreation<Sprite>
    {
      public Sprite Create() => new Sprite();
    }
  }
}
