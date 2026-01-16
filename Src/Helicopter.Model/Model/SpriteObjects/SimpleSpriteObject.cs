// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.SimpleSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  public class SimpleSpriteObject : ISpriteObject, IReusable
  {
    private static readonly ObjectPool<SimpleSpriteObject> _pool = new ObjectPool<SimpleSpriteObject>((ICreation<SimpleSpriteObject>) new SimpleSpriteObject.Creator());
    public Vector2 Offset;
    public Vector2 RotatedOffset;
    protected float _rotation;

    public void ResetState()
    {
      this._rotation = 0.0f;
      this.Parent = (ISpriteObject) null;
      this.Sprite = (Sprite) null;
      this.Offset = Vector2.Zero;
      this.ZIndex = 0.0f;
      this.Children.Clear();
      this.Visible = true;
    }

    public void Release()
    {
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      this.Sprite.Release();
      this.ReleaseFromPool();
    }

    public static SimpleSpriteObject GetInstance() => SimpleSpriteObject._pool.GetObject();

    protected void ReleaseFromPool() => SimpleSpriteObject._pool.Release(this);

    public ISpriteObject Parent { get; set; }

    public bool Visible { get; set; }

    protected SimpleSpriteObject()
    {
      this.Children = new List<ISpriteObject>();
      this.Visible = true;
    }

    protected SimpleSpriteObject(Sprite sprite)
    {
      this.Children = new List<ISpriteObject>();
      this.Sprite = sprite;
      this.Visible = true;
    }

    public float ZIndex { get; set; }

    public List<ISpriteObject> Children { get; set; }

    public Sprite Sprite { get; set; }

    public string SpriteID { get; set; }

    public float Rotation
    {
      get => this._rotation;
      set
      {
        if ((double) Math.Abs(this._rotation - value) < 9.9999997473787516E-05)
          return;
        this._rotation = value;
        this.RotatedOffset.X = (float) ((double) this.Offset.X * Math.Cos((double) this.Rotation) - (double) this.Offset.Y * Math.Sin((double) this.Rotation));
        this.RotatedOffset.Y = (float) ((double) this.Offset.X * Math.Sin((double) this.Rotation) + (double) this.Offset.Y * Math.Cos((double) this.Rotation));
        this.Sprite.Rotation = this._rotation;
        this.Children.ForEach((Action<ISpriteObject>) (x => x.Rotation = this._rotation));
      }
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      if (!this.Visible)
        return;
      parentPosition += this.RotatedOffset;
      this.Sprite.Draw(spriteBatch, parentPosition);
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Draw(spriteBatch, parentPosition)));
    }

    public virtual void Update(Camera camera, float elapsedSeconds)
    {
      if (!this.Visible)
        return;
      this.Sprite.Update(elapsedSeconds);
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Update(camera, elapsedSeconds)));
    }

    public void AddChildren(ISpriteObject spriteObject)
    {
      if (this.Children.Contains(spriteObject))
        return;
      this.Children.Add(spriteObject);
    }

    public void RemoveChildren(ISpriteObject spriteObject)
    {
      if (!this.Children.Contains(spriteObject))
        return;
      this.Children.Remove(spriteObject);
    }

    public void SetOffset(float x, float y)
    {
      this.Offset.X = x;
      this.Offset.Y = y;
      this.RotatedOffset.X = (float) ((double) this.Offset.X * Math.Cos((double) this.Rotation) - (double) this.Offset.Y * Math.Sin((double) this.Rotation));
      this.RotatedOffset.Y = (float) ((double) this.Offset.X * Math.Sin((double) this.Rotation) + (double) this.Offset.Y * Math.Cos((double) this.Rotation));
    }

    protected class Creator : ICreation<SimpleSpriteObject>
    {
      public SimpleSpriteObject Create() => new SimpleSpriteObject();
    }
  }
}
