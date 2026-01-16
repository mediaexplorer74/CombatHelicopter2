// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Mountain
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Patterns;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  public class Mountain : Instance
  {
    private static readonly ObjectPool<Mountain> _pool = new ObjectPool<Mountain>((ICreation<Mountain>) new Mountain.Creator());

    public static Mountain GetInstance() => Mountain._pool.GetObject();

    public override void Release() => Mountain._pool.Release(this);

    protected Mountain()
    {
    }

    public override void Init(Pattern pattern)
    {
      if (!(pattern is MountainPattern))
        throw new Exception("Not correct pattern type");
      base.Init(pattern);
      this.Reaction = (Reaction) new MountainReaction(this);
    }

    protected class Creator : ICreation<Mountain>
    {
      public Mountain Create() => new Mountain();
    }
  }
}
