// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Providers.BossBattleInstancesProvider
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.WorldObjects.Providers
{
  internal class BossBattleInstancesProvider : IInstancesProvider
  {
    private const int MountainWidth = 160;
    private readonly InstanceComparer _comparer = new InstanceComparer();
    private IList<MountainPattern> _topMountainPatterns;
    private IList<MountainPattern> _bottomMountainPatterns;
    private Vector2 _lastTopPosition;
    private Vector2 _lastBottomPosition;

    public List<Pattern> Patterns { get; set; }

    public List<Instance> Instances { get; set; }

    public int RecommendedLengthForNextInstances { get; private set; }

    public BossBattleInstancesProvider()
    {
      this.Patterns = new List<Pattern>();
      this.Instances = new List<Instance>();
    }

    public void Init()
    {
      this.InitIdFactory();
      this.Instances.Sort((IComparer<Instance>) this._comparer);
      this._topMountainPatterns = (IList<MountainPattern>) new List<MountainPattern>();
      this._bottomMountainPatterns = (IList<MountainPattern>) new List<MountainPattern>();
      foreach (Pattern pattern in this.Patterns)
      {
        if (pattern is MountainPattern)
        {
          MountainPattern mountainPattern = (MountainPattern) pattern;
          if (mountainPattern.MountainType == MountainType.Normal)
          {
            if (mountainPattern.Alignment == VerticalAlignment.Top)
              this._topMountainPatterns.Add(mountainPattern);
            else
              this._bottomMountainPatterns.Add(mountainPattern);
          }
        }
      }
      Mountain mountain1 = (Mountain) null;
      Mountain mountain2 = (Mountain) null;
      foreach (Instance instance in this.Instances)
      {
        if (instance is Mountain)
        {
          Mountain mountain3 = (Mountain) instance;
          if (((MountainPattern) instance.Pattern).Alignment == VerticalAlignment.Top)
          {
            if (mountain1 == null || (double) mountain3.Position.X > (double) mountain1.Position.X)
              mountain1 = mountain3;
          }
          else if (mountain2 == null || (double) mountain3.Position.X > (double) mountain2.Position.X)
            mountain2 = mountain3;
        }
      }
      this._lastTopPosition = mountain1 != null && mountain2 != null ? mountain1.Position : throw new Exception("Level has not contain mountains.");
      this._lastBottomPosition = mountain2.Position;
      this.RecommendedLengthForNextInstances = 1600;
    }

    public int Seek { get; private set; }

    public void LoadPatterns(IList<Pattern> patterns)
    {
      this.Patterns.AddRange((IEnumerable<Pattern>) patterns);
    }

    public void LoadInstances(IList<Instance> instances)
    {
      this.Instances.AddRange((IEnumerable<Instance>) instances);
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

    public IList<Instance> GetNextInstances(int length)
    {
      int seek = this.Seek;
      int num = seek + length;
      while ((double) this._lastTopPosition.X + 160.0 < (double) num)
      {
        Mountain nextMountain = this.GetNextMountain(this._topMountainPatterns, this._lastTopPosition);
        this._lastTopPosition = nextMountain.Position;
        this.Instances.Add((Instance) nextMountain);
      }
      while ((double) this._lastBottomPosition.X + 160.0 < (double) num)
      {
        Mountain nextMountain = this.GetNextMountain(this._bottomMountainPatterns, this._lastBottomPosition);
        this._lastBottomPosition = nextMountain.Position;
        this.Instances.Add((Instance) nextMountain);
      }
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

    private Mountain GetNextMountain(
      IList<MountainPattern> patterns,
      Vector2 previosMountainPosition)
    {
      Mountain instance = Mountain.GetInstance();
      int index = CommonRandom.Instance.Random.Next(patterns.Count);
      MountainPattern pattern = patterns[index];
      instance.Id = IdFactory.Instance.GetId();
      instance.ZIndex = 5f;
      instance.SetPosition(previosMountainPosition.X + 160f, previosMountainPosition.Y);
      instance.Init((Pattern) pattern);
      return instance;
    }

    public Pattern GetPattern(string descriptionId)
    {
      foreach (Pattern pattern in this.Patterns)
      {
        if (pattern.DescriptionId == descriptionId)
          return pattern;
      }
      throw new ArgumentException(string.Format("Pattern with descriptionId '{0} not found.'", (object) descriptionId));
    }

    public Pattern GetPattern(UnitType unitType)
    {
      List<HelicopterPattern> list = this.Patterns.OfType<HelicopterPattern>().Where<HelicopterPattern>((Func<HelicopterPattern, bool>) (pattern => pattern.UnitType == unitType)).ToList<HelicopterPattern>();
      if (list.Count > 0)
        return (Pattern) list[CommonRandom.Instance.Random.Next(list.Count)];
      throw new ArgumentException(string.Format("Pattern with unitType '{0} not found.'", (object) unitType));
    }
  }
}
