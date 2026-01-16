// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.LandingElementInstance
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Patterns;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  internal class LandingElementInstance : Instance
  {
    private static readonly ObjectPool<LandingElementInstance> _pool = new ObjectPool<LandingElementInstance>((ICreation<LandingElementInstance>) new LandingElementInstance.Creator());

    public static LandingElementInstance GetInstance() => LandingElementInstance._pool.GetObject();

    public override void Release() => LandingElementInstance._pool.Release(this);

    public bool HasLamp { get; set; }

    protected LandingElementInstance()
    {
    }

    public LandingElementPattern Pattern
    {
      get => (LandingElementPattern) base.Pattern;
      set => base.Pattern = value;
    }

    public void Init(LandingElementPattern pattern)
    {
      this.Init((Helicopter.Model.WorldObjects.Patterns.Pattern) pattern);
      this.Reaction = (Reaction) new LandingReaction((Instance) this);
    }

    protected class Creator : ICreation<LandingElementInstance>
    {
      public LandingElementInstance Create() => new LandingElementInstance();
    }
  }
}
