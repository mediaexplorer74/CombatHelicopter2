// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.BackgroundSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class BackgroundSpriteObject : ISpriteObject, IReusable
  {
    private static readonly ObjectPool<BackgroundSpriteObject> _pool = new ObjectPool<BackgroundSpriteObject>((ICreation<BackgroundSpriteObject>) new BackgroundSpriteObject.Creator());
    private Color _color;
    private IList<BackgroundLayerSpriteObject> _layers;

    public void ResetState()
    {
      this.Sprite = (Sprite) null;
      this.Rotation = 0.0f;
      this.ZIndex = 0.0f;
      this.Children.Clear();
      this._layers.Clear();
      this.SpriteID = "";
    }

    public void Release()
    {
      this.Children.ForEach((Action<ISpriteObject>) (x => x.Release()));
      this.Sprite.Release();
      this.ReleaseFromPool();
    }

    public static BackgroundSpriteObject GetInstance() => BackgroundSpriteObject._pool.GetObject();

    protected void ReleaseFromPool() => BackgroundSpriteObject._pool.Release(this);

    protected BackgroundSpriteObject()
    {
    }

    public float ZIndex { get; set; }

    public Sprite Sprite { get; set; }

    public string SpriteID { get; set; }

    public float Rotation
    {
      get => this.Sprite.Rotation;
      set
      {
        this.Sprite.Rotation = value;
        this.Children.ForEach((Action<ISpriteObject>) (x => x.Rotation = value));
      }
    }

    public List<ISpriteObject> Children { get; set; }

    public bool Visible
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      foreach (BackgroundLayerSpriteObject layer in (IEnumerable<BackgroundLayerSpriteObject>) this._layers)
        layer.Draw(spriteBatch);
    }

    public virtual void Update(Camera camera, float elapsedSeconds)
    {
      foreach (BackgroundLayerSpriteObject layer in (IEnumerable<BackgroundLayerSpriteObject>) this._layers)
        layer.Update(camera, elapsedSeconds);
    }

    public void Init(Helicopter.Model.WorldObjects.Background.Background background)
    {
      this._layers = (IList<BackgroundLayerSpriteObject>) new List<BackgroundLayerSpriteObject>();
      foreach (BackgroundLayer layer in background.Layers)
      {
        BackgroundLayerSpriteObject layerSpriteObject = new BackgroundLayerSpriteObject();
        layerSpriteObject.Init(layer);
        this._layers.Add(layerSpriteObject);
      }
    }

    protected class Creator : ICreation<BackgroundSpriteObject>
    {
      public BackgroundSpriteObject Create() => new BackgroundSpriteObject();
    }
  }
}
