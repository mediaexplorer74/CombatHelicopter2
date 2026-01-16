// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Providers.IInstancesProvider
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Providers
{
  public interface IInstancesProvider
  {
    int Seek { get; }

    int RecommendedLengthForNextInstances { get; }

    void LoadPatterns(IList<Pattern> patterns);

    void LoadInstances(IList<Instance> instances);

    IList<Instance> GetNextInstances(int length);

    void Init();
  }
}
