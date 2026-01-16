// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.WeaponSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects.WeaponSprites
{
  public class WeaponSpriteObject : ISpriteObject, IReusable
  {
    private static readonly ObjectPool<WeaponSpriteObject> _pool = new ObjectPool<WeaponSpriteObject>((ICreation<WeaponSpriteObject>) new WeaponSpriteObject.Creator());
    protected string TexturePath;
    public Vector2 RotatedOffset;
    public Vector2 Size;

    public virtual void Release()
    {
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      this.ReleaseFromPool();
    }

    public static WeaponSpriteObject GetInstance() => WeaponSpriteObject._pool.GetObject();

    protected virtual void ReleaseFromPool() => WeaponSpriteObject._pool.Release(this);

    public Vector2 Offset { get; set; }

    public Weapon Weapon { get; protected set; }

    protected SpriteObject Parent { get; set; }

    public Sprite Sprite { get; set; }

    public bool InvertXDirection
    {
      get => this.Sprite.SpriteEffects == SpriteEffects.FlipHorizontally;
      set
      {
        if (value)
        {
          this.Sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
          if (this.FireAnimation == null)
            return;
          this.FireAnimation.SpriteEffects = SpriteEffects.FlipHorizontally;
        }
        else
        {
          if (this.FireAnimation != null)
            this.FireAnimation.SpriteEffects = SpriteEffects.None;
          foreach (ISpriteObject child in this.Children)
            child.Sprite.SpriteEffects = SpriteEffects.None;
        }
      }
    }

    public CommonAnimatedSprite FireAnimation { get; set; }

    protected WeaponSpriteObject() => this.Children = new List<ISpriteObject>();

    public virtual void ResetState()
    {
      this.Parent = (SpriteObject) null;
      this.Offset = Vector2.Zero;
      this.RotatedOffset = Vector2.Zero;
      this.Weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapon = (Weapon) null;
      this.ZIndex = 0.0f;
      if (this.FireAnimation != null)
      {
        this.FireAnimation.Release();
        this.FireAnimation = (CommonAnimatedSprite) null;
      }
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      this.Children.Clear();
    }

    public float ZIndex { get; set; }

    public string SpriteID { get; set; }

    public float Rotation
    {
      get => this.Sprite.Rotation;
      set
      {
        this.Sprite.Rotation = value;
        this.RotatedOffset.X = (float) ((double) this.Offset.X * Math.Cos((double) this.Rotation) - (double) this.Offset.Y * Math.Sin((double) this.Rotation));
        this.RotatedOffset.Y = (float) ((double) this.Offset.X * Math.Sin((double) this.Rotation) + (double) this.Offset.Y * Math.Cos((double) this.Rotation));
        this.Children.ForEach((Action<ISpriteObject>) (x => x.Rotation = value));
      }
    }

    public List<ISpriteObject> Children { get; set; }

    public bool Visible { get; set; }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      parentPosition += this.RotatedOffset;
      this.Sprite.Draw(spriteBatch, parentPosition);
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Draw(spriteBatch, parentPosition)));
      if (this.FireAnimation == null)
        return;
      this.FireAnimation.Draw(spriteBatch, parentPosition + this.FireAnimationPosition());
    }

    public virtual void Update(Camera camera, float elapsedSeconds)
    {
      this.Sprite.Update(elapsedSeconds);
      if (this.FireAnimation != null)
        this.FireAnimation.Update(elapsedSeconds);
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Update(camera, elapsedSeconds)));
    }

    protected virtual void OnWeaponFired(object sender, WeaponEventArgs e)
    {
      if (this.FireAnimation == null)
        return;
      this.FireAnimation.Play();
    }

    public virtual void Init(SpriteObject parent, Weapon weapon)
    {
      this.Parent = parent;
      this.Weapon = weapon;
      this.Weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapon.Fired += new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      SimpleSpriteObject simpleSoByDesc = SpriteObjectPool.Instance.GetSimpleSOByDesc(new SpriteDescription()
      {
        TexturePath = this.TexturePath
      });
      simpleSoByDesc.Offset = this.Offset;
      this.Size = simpleSoByDesc.Sprite.SourceSize;
      this.Sprite = simpleSoByDesc.Sprite;
      if (weapon.WeaponSlotDescription == null)
        return;
      this.InvertXDirection = weapon.WeaponSlotDescription.InvertXDirection;
      if (this.InvertXDirection && parent is CopterSpriteObject && this.FireAnimation != null)
        this.FireAnimation.FlipOriginHorizontally();
      this.ZIndex = weapon.WeaponSlotDescription.SpriteZIndex;
    }

    internal virtual Vector2 FireAnimationPosition() => this.Weapon.BulletSpawnPosition;

    internal void Init(
      SpriteObject parent,
      Sprite sprite,
      CommonAnimatedSprite fireAnimation,
      Weapon weapon,
      Vector2 offset)
    {
      this.Sprite = sprite;
      this.Parent = parent;
      this.FireAnimation = fireAnimation;
      this.Weapon = weapon;
      this.Offset = offset;
      this.Weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
      this.Weapon.Fired += new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
    }

    protected class Creator : ICreation<WeaponSpriteObject>
    {
      public WeaponSpriteObject Create() => new WeaponSpriteObject();
    }
  }
}
