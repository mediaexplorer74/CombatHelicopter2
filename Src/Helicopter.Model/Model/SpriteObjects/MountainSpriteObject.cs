// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.MountainSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class MountainSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<MountainSpriteObject> _pool = new ObjectPool<MountainSpriteObject>((ICreation<MountainSpriteObject>) new MountainSpriteObject.Creator());

    public static MountainSpriteObject GetInstance() => MountainSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => MountainSpriteObject._pool.Release(this);

    protected MountainSpriteObject()
    {
    }

    public override void OnStateChanged(object instance, StateChangeEventArgs<int> stateChangeEvent)
    {
    }

    protected class Creator : ICreation<MountainSpriteObject>
    {
      public MountainSpriteObject Create() => new MountainSpriteObject();
    }
  }
}
