// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.SpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  public class SpriteObject : ISpriteObject, IReusable
  {
    public Vector2 Position;

    public Instance Instance { get; protected set; }

    public SpriteObject() => this.Children = new List<ISpriteObject>();

    public virtual void ResetState()
    {
      this.Position = Vector2.Zero;
      this.SpriteID = "";
      this.ZIndex = 0.0f;
      this.Instance.StateChanged -= new EventHandler<StateChangeEventArgs<int>>(this.OnStateChanged);
      this.Sprite = (Sprite) null;
      this.Instance = (Instance) null;
      this.Children.Clear();
    }

    public float ZIndex { get; set; }

    public Sprite Sprite { get; set; }

    public string SpriteID { get; set; }

    public bool Visible { get; set; }

    public float Rotation
    {
      get => this.Instance.Rotation;
      set
      {
        this.Instance.Rotation = value;
        this.UpdateRotation();
      }
    }

    public List<ISpriteObject> Children { get; set; }

    public virtual void Release()
    {
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      if (this.Sprite != null)
        this.Sprite.Release();
      if (this.Instance != null)
        this.Instance.Release();
      this.Visible = true;
      this.ReleaseFromPool();
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      if (!this.Visible)
        return;
      if (this.Sprite != null)
        this.Sprite.Draw(spriteBatch, this.Position);
      foreach (ISpriteObject child in this.Children)
        child.Draw(spriteBatch, this.Position);
    }

    public virtual void Update(Camera camera, float elapsedSeconds)
    {
      if (!this.Visible)
        return;
      this.UpdateRotation();
      if (this.Sprite != null)
        this.Sprite.Update(elapsedSeconds);
      this.Position.X = (float) (this.Instance.Contour.Rectangle.X - camera.Screen.X);
      this.Position.Y = (float) (this.Instance.Contour.Rectangle.Y - camera.Screen.Y);
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Update(camera, elapsedSeconds)));
    }

    public virtual void OnStateChanged(object instance, StateChangeEventArgs<int> stateChangeEvent)
    {
    }

    public virtual void AddChildren(ISpriteObject spriteObject)
    {
      if (this.Children.Contains(spriteObject))
        return;
      this.Children.Add(spriteObject);
    }

    public ISpriteObject GetChild(string spriteId)
    {
      return this.Children == null ? (ISpriteObject) null : this.Children.First<ISpriteObject>((Func<ISpriteObject, bool>) (x => x.SpriteID == spriteId));
    }

    public virtual void Init(Instance instance)
    {
      this.Instance = instance;
      this.ZIndex = instance.ZIndex;
      this.Visible = true;
      instance.StateChanged += new EventHandler<StateChangeEventArgs<int>>(this.OnStateChanged);
    }

    protected virtual void ReleaseFromPool()
    {
    }

    public void RemoveChildren(ISpriteObject spriteObject)
    {
      if (!this.Children.Contains(spriteObject))
        return;
      this.Children.Remove(spriteObject);
    }

    public virtual void UpdateRotation()
    {
      if (this.Sprite != null)
        this.Sprite.Rotation = this.Instance.Rotation;
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Rotation = this.Instance.Rotation));
    }
  }
}
