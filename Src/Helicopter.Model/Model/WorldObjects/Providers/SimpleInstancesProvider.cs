// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Providers.SimpleInstancesProvider
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.WorldObjects.Providers
{
  public class SimpleInstancesProvider : IInstancesProvider
  {
    private readonly InstanceComparer _comparer = new InstanceComparer();

    public List<Pattern> Patterns { get; set; }

    public List<Instance> Instances { get; set; }

    public SimpleInstancesProvider()
    {
      this.Patterns = new List<Pattern>();
      this.Instances = new List<Instance>();
    }

    public void Init()
    {
      this.InitIdFactory();
      this.Instances.Sort((IComparer<Instance>) this._comparer);
      this.RecommendedLengthForNextInstances = 1600;
    }

    public int Seek { get; private set; }

    public int RecommendedLengthForNextInstances { get; private set; }

    public void LoadPatterns(IList<Pattern> patterns)
    {
      this.Patterns.AddRange((IEnumerable<Pattern>) patterns);
    }

    public void LoadInstances(IList<Instance> instances)
    {
      this.Instances.AddRange((IEnumerable<Instance>) instances);
    }

    public IList<Instance> GetNextInstances(int length)
    {
      int seek = this.Seek;
      int num = seek + length;
      List<Instance> nextInstances = new List<Instance>();
      for (int index = 0; index < this.Instances.Count; ++index)
      {
        Instance instance = this.Instances[index];
        if ((double) instance.Position.X >= (double) seek && (double) instance.Position.X < (double) num)
        {
          nextInstances.Add(instance);
          this.Instances.Remove(instance);
          --index;
        }
      }
      this.Seek += length;
      return (IList<Instance>) nextInstances;
    }

    private void InitIdFactory()
    {
      int num = this.Patterns.Select<Pattern, int>((Func<Pattern, int>) (pattern => pattern.Id)).Concat<int>((IEnumerable<int>) new int[1]
      {
        -1
      }).Max();
      IdFactory.Instance.SetPrevId(this.Instances.Select<Instance, int>((Func<Instance, int>) (instance => instance.Id)).Concat<int>((IEnumerable<int>) new int[1]
      {
        num
      }).Max());
    }
  }
}
