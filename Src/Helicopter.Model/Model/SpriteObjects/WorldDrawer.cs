// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.WorldDrawer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

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
  public class WorldDrawer
  {
    private readonly Camera _camera = new Camera();
    private readonly SpriteObjectComparer _comparer = new SpriteObjectComparer();
    private readonly IList<Instance> _removeItems = (IList<Instance>) new List<Instance>();
    private BackgroundSpriteObject _background;

    public GameWorld World { get; private set; }

    public Dictionary<Instance, SpriteObject> SpriteObjects { get; private set; }

    public List<SpriteObject> SpriteObjectsList { get; set; }

    public SmartPlayer Player { get; set; }

    public WorldDrawer()
    {
      this.SpriteObjects = new Dictionary<Instance, SpriteObject>();
      this.SpriteObjectsList = new List<SpriteObject>();
    }

    public void Update(float elapsedSeconds)
    {
      this._camera.UpdateCamera(this.World.ActiveArea, this.World.Player.Contour.Rectangle, elapsedSeconds);
      this._background.Update(this._camera, elapsedSeconds);
      AnimatedSprite.UpdatecommonAnimationTimer(elapsedSeconds);
      for (int index = this.SpriteObjects.Keys.Count - 1; index >= 0; --index)
      {
        Instance instance = this.SpriteObjects.Keys.ElementAt<Instance>(index);
        if (!this.World.ActiveInstances.Contains(instance))
          this.Remove(instance);
      }
      foreach (Instance activeInstance in this.World.ActiveInstances)
      {
        if (!this.SpriteObjects.Keys.Contains<Instance>(activeInstance))
        {
          SpriteObject spriteObject = SpriteObjectPool.Instance.GetSpriteObject(activeInstance, this._camera);
          this.SpriteObjects[activeInstance] = spriteObject;
          this.SpriteObjectsList.Add(spriteObject);
        }
        this.SpriteObjects[activeInstance].Update(this._camera, elapsedSeconds);
      }
      this.SpriteObjectsList.Sort((IComparer<SpriteObject>) this._comparer);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this._background.Draw(spriteBatch, Vector2.Zero);
      for (int index = 0; index < this.SpriteObjectsList.Count; ++index)
        this.SpriteObjectsList[index].Draw(spriteBatch, Vector2.Zero);
    }

    public void Init(GameWorld world)
    {
      this.World = world;
      world.Player.Damaged += (EventHandler<PlayerEventArgs>) ((x, y) => this._camera.IsShaking = true);
      world.Player.MountainCollided += (EventHandler<PlayerEventArgs>) ((x, y) => this._camera.IsShaking = true);
      this._background = BackgroundSpriteObject.GetInstance();
      this._background.Init(this.World.Background);
    }

    public void Remove(Instance instance)
    {
      if (!this.SpriteObjects.ContainsKey(instance))
        return;
      SpriteObject spriteObject = this.SpriteObjects[instance];
      SpriteObjectPool.Instance.Release((ISpriteObject) spriteObject);
      this.SpriteObjects.Remove(instance);
      this.SpriteObjectsList.Remove(spriteObject);
    }
  }
}
