// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Providers.ChallengeProvider
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Providers
{
  public class ChallengeProvider : IInstancesProvider
  {
    public const int MountainWidth = 160;
    private const int MountainHeight = 200;
    private const int FirstSegmentCorridor = 400;
    private const int TopVisibleBorder = 39;
    private const int TopMountainBorder = 55;
    private const int BottomMountainBorder = 450;
    private const int BottomVisibleBorder = 500;
    private const int SegmentLength = 1600;
    private const int ThickCorridorMin = 350;
    private const int ThickCorridorMax = 400;
    private const int ThinCorridorMin = 150;
    private const int ThinCorridorMax = 250;
    public const int StartLandingLenght = 1600;
    public const int EndLandingLenght = 1600;
    public const int LevelLength = 36800;
    private readonly InstanceComparer _comparer = new InstanceComparer();
    private readonly IDictionary<int, ChallengeProvider.MountainConnectionsInfo> _topConnections;
    private readonly IDictionary<int, ChallengeProvider.MountainConnectionsInfo> _bottomConnections;
    private readonly Dictionary<UnitType, int> _unitsProbability;
    private MountainType _alloweableTopMountains = MountainType.Normal;
    private MountainType _alloweableBottonMountains = MountainType.Normal;
    private int _allSegmentsCount;
    private IList<MountainPattern> _topMountainPatterns;
    private IList<MountainPattern> _bottomMountainPatterns;
    private IDictionary<LandingElementType, LandingElementPattern> _topLandingPatterns;
    private IDictionary<LandingElementType, LandingElementPattern> _bottomLandingPatterns;
    private ChallengeProvider.MountainConnectionsInfo _lastTopConnections;
    private ChallengeProvider.MountainConnectionsInfo _lastBottomConnections;

    public Dictionary<UnitType, List<Pattern>> Patterns { get; set; }

    public List<Instance> Instances { get; set; }

    private int EpisodeNumber { get; set; }

    public ChallengeProvider()
    {
      this._unitsProbability = new Dictionary<UnitType, int>();
      this._topConnections = (IDictionary<int, ChallengeProvider.MountainConnectionsInfo>) new Dictionary<int, ChallengeProvider.MountainConnectionsInfo>();
      this._bottomConnections = (IDictionary<int, ChallengeProvider.MountainConnectionsInfo>) new Dictionary<int, ChallengeProvider.MountainConnectionsInfo>();
      this.Instances = new List<Instance>();
    }

    public int RecommendedLengthForNextInstances { get; private set; }

    public void UpdateEpisodeNumber(int episodeNumber)
    {
      this.EpisodeNumber = episodeNumber;
      this.InitGenerationSettings();
    }

    public void InitGenerationSettings()
    {
      this._unitsProbability.Clear();
      int num1;
      int num2;
      int num3;
      int num4;
      int num5;
      int num6;
      int num7;
      int num8;
      int num9;
      switch (this.EpisodeNumber)
      {
        case 0:
          num1 = 100;
          num2 = 0;
          num3 = 0;
          num4 = 100;
          num5 = 0;
          num6 = 0;
          num7 = 100;
          num8 = 0;
          num9 = 0;
          break;
        case 1:
          num1 = 85;
          num2 = 15;
          num3 = 0;
          num4 = 85;
          num5 = 15;
          num6 = 0;
          num7 = 70;
          num8 = 30;
          num9 = 0;
          break;
        case 2:
          num1 = 70;
          num2 = 30;
          num3 = 0;
          num4 = 70;
          num5 = 30;
          num6 = 0;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 3:
          num1 = 50;
          num2 = 40;
          num3 = 10;
          num4 = 50;
          num5 = 40;
          num6 = 10;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 4:
          num1 = 40;
          num2 = 40;
          num3 = 20;
          num4 = 40;
          num5 = 40;
          num6 = 20;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 5:
          num1 = 10;
          num2 = 60;
          num3 = 30;
          num4 = 10;
          num5 = 60;
          num6 = 30;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 6:
          num1 = 10;
          num2 = 50;
          num3 = 40;
          num4 = 10;
          num5 = 50;
          num6 = 40;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 7:
          num1 = 10;
          num2 = 40;
          num3 = 50;
          num4 = 10;
          num5 = 40;
          num6 = 50;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 8:
          num1 = 10;
          num2 = 30;
          num3 = 70;
          num4 = 10;
          num5 = 30;
          num6 = 70;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        case 9:
          num1 = 10;
          num2 = 20;
          num3 = 80;
          num4 = 10;
          num5 = 20;
          num6 = 80;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
        default:
          num1 = 10;
          num2 = 10;
          num3 = 90;
          num4 = 10;
          num5 = 10;
          num6 = 90;
          num7 = 33;
          num8 = 33;
          num9 = 33;
          break;
      }
      this._unitsProbability.Add(UnitType.LightHelicopter, num1);
      this._unitsProbability.Add(UnitType.MediumHelicopter, num2);
      this._unitsProbability.Add(UnitType.HeavyHelicopter, num3);
      this._unitsProbability.Add(UnitType.LightTurret, num4);
      this._unitsProbability.Add(UnitType.MediumTurret, num5);
      this._unitsProbability.Add(UnitType.HeavyTurret, num6);
      this._unitsProbability.Add(UnitType.Droid, num7);
      this._unitsProbability.Add(UnitType.Egg, num8);
      this._unitsProbability.Add(UnitType.ArmedEgg, num9);
    }

    private void InitThickTunnelProbability(
      ChallengeProvider.SegmentGenerationProperties properties)
    {
      int num = CommonRandom.Instance.Random.Next(2);
      switch (this.EpisodeNumber)
      {
        case 0:
          if (num == 0)
          {
            properties.CoptersCount = 2;
            properties.TurretsCount = 1;
            break;
          }
          properties.CoptersCount = 2;
          properties.DroidsCount = 2;
          break;
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
        case 9:
          if (num == 0)
          {
            properties.CoptersCount = 3;
            properties.TurretsCount = 1;
            break;
          }
          properties.CoptersCount = 3;
          properties.DroidsCount = 2;
          break;
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
        case 15:
        case 16:
        case 17:
        case 18:
        case 19:
          if (num == 0)
          {
            properties.CoptersCount = 3;
            properties.TurretsCount = 1;
            break;
          }
          properties.CoptersCount = 3;
          properties.DroidsCount = 2;
          break;
        default:
          if (num == 0)
          {
            properties.CoptersCount = 3;
            properties.TurretsCount = 1;
            break;
          }
          properties.CoptersCount = 3;
          properties.DroidsCount = 2;
          break;
      }
    }

    private void InitThinTunnelProbability(
      ChallengeProvider.SegmentGenerationProperties properties)
    {
      int num = CommonRandom.Instance.Random.Next(2);
      switch (this.EpisodeNumber)
      {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
        case 9:
          if (num == 0)
          {
            properties.DroidsCount = 4;
            break;
          }
          properties.DroidsCount = 2;
          properties.CoptersCount = 1;
          break;
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
        case 15:
        case 16:
        case 17:
        case 18:
        case 19:
          if (num == 0)
          {
            properties.DroidsCount = 4;
            break;
          }
          properties.DroidsCount = 2;
          properties.CoptersCount = 1;
          break;
        default:
          if (num == 0)
          {
            properties.DroidsCount = 4;
            break;
          }
          properties.DroidsCount = 2;
          properties.CoptersCount = 1;
          break;
      }
    }

    private IEnumerable<Instance> GetSegmentInstances(
      int offset,
      ChallengeProvider.SegmentGenerationProperties properties)
    {
      List<Instance> segmentInstances = new List<Instance>();
      IList<ChallengeProvider.TunnelPartInfo> mountains = this.GetMountains(offset, properties);
      foreach (ChallengeProvider.TunnelPartInfo tunnelPartInfo in (IEnumerable<ChallengeProvider.TunnelPartInfo>) mountains)
      {
        segmentInstances.Add((Instance) tunnelPartInfo.TopMountain);
        segmentInstances.Add((Instance) tunnelPartInfo.BottomMountain);
      }
      IList<Instance> segmentUnits = this.GetSegmentUnits(offset, properties, mountains);
      segmentInstances.AddRange((IEnumerable<Instance>) segmentUnits);
      return (IEnumerable<Instance>) segmentInstances;
    }

    private IList<Instance> GetSegmentUnits(
      int offset,
      ChallengeProvider.SegmentGenerationProperties properties,
      IList<ChallengeProvider.TunnelPartInfo> tunnel)
    {
      List<Instance> segmentUnits = new List<Instance>();
      if (properties.CoptersCount > 0)
      {
        int num1 = offset;
        int num2 = offset;
        int num3 = 300;
        int num4 = 300;
        for (int index = 0; index < properties.CoptersCount; index += 2)
        {
          Copter copter1 = this.GetCopter();
          int maxValue1 = num3 - copter1.Contour.Rectangle.Width;
          int num5 = CommonRandom.Instance.Random.Next(maxValue1);
          num3 = maxValue1 - num5 + 300;
          Vector2 copterPosition1 = this.GetCopterPosition(num1 + num5, copter1, ChallengeProvider.CopterAllignment.Bottom, (IEnumerable<ChallengeProvider.TunnelPartInfo>) tunnel);
          copter1.SetPosition(copterPosition1);
          segmentUnits.Add((Instance) copter1);
          num1 += num5 + 1000 + copter1.Contour.Rectangle.Width;
          if (properties.CoptersCount - index > 1)
          {
            Copter copter2 = this.GetCopter();
            int maxValue2 = num4 - copter2.Contour.Rectangle.Width;
            int num6 = CommonRandom.Instance.Random.Next(maxValue2);
            num4 = maxValue2 - num6 + 300;
            Vector2 copterPosition2 = this.GetCopterPosition(num2 + num6, copter2, ChallengeProvider.CopterAllignment.Top, (IEnumerable<ChallengeProvider.TunnelPartInfo>) tunnel);
            copter2.SetPosition(copterPosition2);
            segmentUnits.Add((Instance) copter2);
            num2 += num6 + 1000 + copter2.Contour.Rectangle.Width;
          }
        }
      }
      if (properties.DroidsCount > 0)
      {
        int num7 = 1600 / properties.DroidsCount;
        int num8 = num7;
        int num9 = offset;
        for (int index = 0; index < properties.DroidsCount; ++index)
        {
          Copter droid = this.GetDroid();
          int maxValue = num8 - droid.Contour.Rectangle.Width;
          int num10 = CommonRandom.Instance.Random.Next(num7 / 2, maxValue);
          num8 = maxValue - num10 + num7;
          Vector2 copterPosition = this.GetCopterPosition(num9 + num10, droid, ChallengeProvider.CopterAllignment.Center, (IEnumerable<ChallengeProvider.TunnelPartInfo>) tunnel);
          droid.SetPosition(copterPosition);
          segmentUnits.Add((Instance) droid);
          num9 += num10 + droid.Contour.Rectangle.Width;
        }
      }
      if (properties.TurretsCount > 0)
      {
        int num11 = 1600 / properties.TurretsCount;
        int num12 = num11;
        int num13 = offset;
        for (int index = 0; index < properties.TurretsCount; ++index)
        {
          Cannon cannon = this.GetCannon();
          int maxValue = num12 - cannon.Contour.Rectangle.Width;
          int num14 = CommonRandom.Instance.Random.Next(num11 / 2, maxValue);
          num12 = maxValue - num14 + num11;
          Vector2 turretPosition = this.GetTurretPosition(num13 + num14, cannon, (IEnumerable<ChallengeProvider.TunnelPartInfo>) tunnel);
          cannon.SetPosition(turretPosition);
          segmentUnits.Add((Instance) cannon);
          num13 += num14 + cannon.Contour.Rectangle.Width;
        }
      }
      return (IList<Instance>) segmentUnits;
    }

    private Vector2 GetCopterPosition(
      int x,
      Copter copter,
      ChallengeProvider.CopterAllignment allignment,
      IEnumerable<ChallengeProvider.TunnelPartInfo> tunnel)
    {
      Vector2 copterPosition = new Vector2((float) x, -1000f);
      int height = copter.Contour.Rectangle.Height;
      foreach (ChallengeProvider.TunnelPartInfo tunnelPartInfo in tunnel)
      {
        if (x >= tunnelPartInfo.Start && x < tunnelPartInfo.End)
        {
          int num1 = (tunnelPartInfo.TopConnection.MaxHeight + tunnelPartInfo.BottomConnection.MaxHeight) / 2;
          switch (allignment)
          {
            case ChallengeProvider.CopterAllignment.Top:
              int num2 = (num1 - tunnelPartInfo.TopConnection.MaxHeight - height) / 2;
              copterPosition.Y = (float) (tunnelPartInfo.TopConnection.MaxHeight + num2);
              return copterPosition;
            case ChallengeProvider.CopterAllignment.Center:
              copterPosition.Y = (float) num1;
              return copterPosition;
            case ChallengeProvider.CopterAllignment.Bottom:
              int num3 = (tunnelPartInfo.BottomConnection.MaxHeight - num1 - height) / 2;
              copterPosition.Y = (float) (num1 + num3);
              return copterPosition;
            default:
              continue;
          }
        }
      }
      return copterPosition;
    }

    private Vector2 GetTurretPosition(
      int x,
      Cannon cannon,
      IEnumerable<ChallengeProvider.TunnelPartInfo> tunnel)
    {
      Vector2 turretPosition = new Vector2((float) x, -1000f);
      VerticalAlignment alignment = cannon.CannonPattern.Alignment;
      bool flag = false;
      foreach (ChallengeProvider.TunnelPartInfo tunnelPartInfo in tunnel)
      {
        Mountain mountain = alignment == VerticalAlignment.Top ? tunnelPartInfo.TopMountain : tunnelPartInfo.BottomMountain;
        ChallengeProvider.MountainConnectionsInfo mountainConnectionsInfo = alignment == VerticalAlignment.Top ? tunnelPartInfo.TopConnection : tunnelPartInfo.BottomConnection;
        if (x >= tunnelPartInfo.Start && x < tunnelPartInfo.End || flag)
        {
          flag = true;
          if (((MountainPattern) mountain.Pattern).MountainType == MountainType.Normal)
          {
            turretPosition.X = (float) (tunnelPartInfo.Start + (mountain.Contour.Rectangle.Width - cannon.Contour.Rectangle.Width) / 2);
            turretPosition.Y = alignment != VerticalAlignment.Top ? (float) (mountainConnectionsInfo.MaxHeight - 30) : (float) (mountainConnectionsInfo.MaxHeight - 50);
            return turretPosition;
          }
        }
      }
      return turretPosition;
    }

    private Cannon GetCannon()
    {
      int num = CommonRandom.Instance.Random.Next(this._unitsProbability[UnitType.LightTurret] + this._unitsProbability[UnitType.MediumTurret] + this._unitsProbability[UnitType.HeavyTurret]);
      List<Pattern> pattern = this.Patterns[num >= this._unitsProbability[UnitType.LightTurret] ? (num >= this._unitsProbability[UnitType.LightTurret] + this._unitsProbability[UnitType.MediumTurret] ? UnitType.HeavyTurret : UnitType.MediumTurret) : UnitType.LightTurret];
      CannonPattern cannonPattern = (CannonPattern) pattern[CommonRandom.Instance.Random.Next(pattern.Count)];
      Cannon instance = Cannon.GetInstance();
      instance.ZIndex = 4f;
      instance.Init((Pattern) cannonPattern);
      return instance;
    }

    private Copter GetDroid()
    {
      int num = CommonRandom.Instance.Random.Next(this._unitsProbability[UnitType.Droid] + this._unitsProbability[UnitType.Egg] + this._unitsProbability[UnitType.ArmedEgg]);
      List<Pattern> pattern1 = this.Patterns[num >= this._unitsProbability[UnitType.Droid] ? (num >= this._unitsProbability[UnitType.Droid] + this._unitsProbability[UnitType.Egg] ? UnitType.ArmedEgg : UnitType.Egg) : UnitType.Droid];
      HelicopterPattern pattern2 = (HelicopterPattern) pattern1[CommonRandom.Instance.Random.Next(pattern1.Count)];
      Copter instance = Copter.GetInstance();
      instance.ZIndex = 6f;
      instance.Init((Pattern) pattern2);
      this.CorrectCopterComplexity(instance, pattern2);
      return instance;
    }

    private Copter GetCopter()
    {
      int num1 = CommonRandom.Instance.Random.Next(this._unitsProbability[UnitType.LightHelicopter] + this._unitsProbability[UnitType.MediumHelicopter] + this._unitsProbability[UnitType.HeavyHelicopter]);
      List<Pattern> pattern1 = this.Patterns[num1 >= this._unitsProbability[UnitType.LightHelicopter] ? (num1 >= this._unitsProbability[UnitType.LightHelicopter] + this._unitsProbability[UnitType.MediumHelicopter] ? UnitType.HeavyHelicopter : UnitType.MediumHelicopter) : UnitType.LightHelicopter];
      HelicopterPattern pattern2 = (HelicopterPattern) pattern1[CommonRandom.Instance.Random.Next(pattern1.Count)];
      Copter instance = Copter.GetInstance();
      instance.ZIndex = 6f;
      float num2 = 1f;
      if (this.EpisodeNumber > 4)
        num2 = Math.Min((float) (1.0 + (double) (this.EpisodeNumber - 4) * 0.25), 4f);
      instance.DamageFactor = num2;
      instance.Init((Pattern) pattern2);
      this.CorrectCopterComplexity(instance, pattern2);
      return instance;
    }

    private void CorrectCopterComplexity(Copter copter, HelicopterPattern pattern)
    {
      if (this.EpisodeNumber <= 4)
        return;
      float num1 = (float) (this.EpisodeNumber - 4);
      float num2 = (float) ((double) num1 * (double) pattern.Energy * 0.05000000074505806);
      copter.Energy += num2;
      float num3 = (float) ((double) num1 * (double) pattern.Price * 0.05000000074505806);
      copter.Price += num3;
      copter.Score += num3;
    }

    private ChallengeProvider.SegmentGenerationProperties GetSegmentProperties(float startOffset)
    {
      ChallengeProvider.SegmentGenerationProperties properties = new ChallengeProvider.SegmentGenerationProperties();
      if ((double) startOffset < 3200.0)
      {
        properties.Corridor = 400;
        this.InitThickTunnelProbability(properties);
      }
      else if ((double) startOffset >= 36800.0)
      {
        properties.Corridor = 400;
        this.InitThickTunnelProbability(properties);
      }
      else if (this._allSegmentsCount % 2 == 0 || (double) startOffset >= 35200.0)
      {
        properties.Corridor = CommonRandom.Instance.Random.Next(350, 400);
        this.InitThickTunnelProbability(properties);
      }
      else
      {
        properties.Corridor = CommonRandom.Instance.Random.Next(150, 250);
        this.InitThinTunnelProbability(properties);
      }
      return properties;
    }

    private IEnumerable<Instance> GetLandingZone(int offset, int length, bool start)
    {
      int num1 = 0;
      LandingElementType elementType1 = start ? LandingElementType.MediumBlock : LandingElementType.StartBlock;
      LandingElementType elementType2 = start ? LandingElementType.MediumShield : LandingElementType.StartShield;
      LandingElementType elementType3 = start ? LandingElementType.EndBlock : LandingElementType.MediumBlock;
      LandingElementType elementType4 = start ? LandingElementType.EndShield : LandingElementType.MediumShield;
      List<Instance> landingZone = new List<Instance>()
      {
        (Instance) this.GetLandingElement(elementType1, VerticalAlignment.Top, offset + num1, -80),
        (Instance) this.GetLandingElement(elementType2, VerticalAlignment.Top, offset + num1, 40),
        (Instance) this.GetLandingElement(elementType2, VerticalAlignment.Bottom, offset + num1, 240),
        (Instance) this.GetLandingElement(elementType1, VerticalAlignment.Bottom, offset + num1, 400)
      };
      int num2 = num1 + 160;
      int num3 = length / 160 - 2;
      for (int index = 0; index < num3; ++index)
      {
        bool flag = index % 2 == 1;
        LandingElementInstance landingElement1 = this.GetLandingElement(LandingElementType.MediumBlock, VerticalAlignment.Top, offset + num2, -80);
        landingElement1.HasLamp = flag;
        landingZone.Add((Instance) landingElement1);
        landingZone.Add((Instance) this.GetLandingElement(LandingElementType.MediumShield, VerticalAlignment.Top, offset + num2, 40));
        landingZone.Add((Instance) this.GetLandingElement(LandingElementType.MediumShield, VerticalAlignment.Bottom, offset + num2, 240));
        LandingElementInstance landingElement2 = this.GetLandingElement(LandingElementType.MediumBlock, VerticalAlignment.Bottom, offset + num2, 400);
        landingElement2.HasLamp = flag;
        landingZone.Add((Instance) landingElement2);
        landingZone.Add((Instance) this.GetLandingElement(LandingElementType.Label, VerticalAlignment.Top, offset + num2 - 53, 260));
        num2 += 160;
      }
      landingZone.Add((Instance) this.GetLandingElement(elementType3, VerticalAlignment.Top, offset + num2, -80));
      landingZone.Add((Instance) this.GetLandingElement(elementType4, VerticalAlignment.Top, offset + num2, 40));
      landingZone.Add((Instance) this.GetLandingElement(elementType4, VerticalAlignment.Bottom, offset + num2, 240));
      landingZone.Add((Instance) this.GetLandingElement(elementType3, VerticalAlignment.Bottom, offset + num2, 400));
      landingZone.Add((Instance) this.GetLandingElement(LandingElementType.Label, VerticalAlignment.Top, offset + num2 - 53, 260));
      return (IEnumerable<Instance>) landingZone;
    }

    private LandingElementInstance GetLandingElement(
      LandingElementType elementType,
      VerticalAlignment alignment,
      int x,
      int y)
    {
      LandingElementPattern pattern = alignment == VerticalAlignment.Top ? this._topLandingPatterns[elementType] : this._bottomLandingPatterns[elementType];
      LandingElementInstance instance = LandingElementInstance.GetInstance();
      instance.Init(pattern);
      instance.Id = IdFactory.Instance.GetId();
      instance.ZIndex = (elementType & LandingElementType.Background) == (LandingElementType) 0 ? 1f : 0.0f;
      if (elementType == LandingElementType.Label)
        instance.ZIndex = 1f;
      instance.SetPosition((float) x, (float) y);
      return instance;
    }

    private IList<ChallengeProvider.TunnelPartInfo> GetMountains(
      int offset,
      ChallengeProvider.SegmentGenerationProperties properties)
    {
      IList<ChallengeProvider.TunnelPartInfo> mountains = (IList<ChallengeProvider.TunnelPartInfo>) new List<ChallengeProvider.TunnelPartInfo>();
      Random random = CommonRandom.Instance.Random;
      bool flag = false;
      int num1 = properties.Corridor;
      int num2 = this._lastBottomConnections.MaxHeight - this._lastTopConnections.MaxHeight;
      if (num2 < properties.Corridor)
      {
        num1 = num2;
        flag = true;
      }
      int num3 = 0;
      while (num3 < 1600)
      {
        if (flag)
        {
          int num4 = this._lastBottomConnections.MaxHeight - this._lastTopConnections.MaxHeight;
          if (num4 >= properties.Corridor)
          {
            num1 = properties.Corridor;
            flag = false;
          }
          else if (num4 > num1)
            num1 = num4;
        }
        ChallengeProvider.TunnelPartInfo tunnelPartInfo = new ChallengeProvider.TunnelPartInfo()
        {
          Start = num3 + offset
        };
        tunnelPartInfo.End = tunnelPartInfo.Start + 160;
        MountainPattern topMountainPattern;
        int num5;
        int num6;
        ChallengeProvider.MountainConnectionsInfo absoluteConnections1;
        do
        {
          topMountainPattern = this._topMountainPatterns[random.Next(this._topMountainPatterns.Count - 1)];
          ChallengeProvider.MountainConnectionsInfo topConnection = this._topConnections[topMountainPattern.Id];
          num5 = this._lastTopConnections.RightConnection - topConnection.LeftConnection;
          num6 = num5 + topConnection.RightConnection - topConnection.LeftConnection;
          absoluteConnections1 = topConnection.GetAbsoluteConnections(num5);
        }
        while ((this._alloweableTopMountains & topMountainPattern.MountainType) == (MountainType) 0 || num5 > 39 || num6 > 39 || absoluteConnections1.RightConnection < 55 || this._lastBottomConnections.MaxHeight - absoluteConnections1.MaxHeight < num1);
        Mountain nextMountain1 = this.GetNextMountain(topMountainPattern, new Vector2((float) (offset + num3), (float) num5));
        this._alloweableTopMountains = this.GetAlloweableNextMountains(topMountainPattern.MountainType);
        tunnelPartInfo.TopConnection = absoluteConnections1;
        tunnelPartInfo.TopMountain = nextMountain1;
        this._lastTopConnections = absoluteConnections1;
        MountainPattern bottomMountainPattern;
        int num7;
        ChallengeProvider.MountainConnectionsInfo absoluteConnections2;
        do
        {
          bottomMountainPattern = this._bottomMountainPatterns[random.Next(this._bottomMountainPatterns.Count - 1)];
          ChallengeProvider.MountainConnectionsInfo bottomConnection = this._bottomConnections[bottomMountainPattern.Id];
          num7 = this._lastBottomConnections.RightConnection - bottomConnection.LeftConnection;
          absoluteConnections2 = bottomConnection.GetAbsoluteConnections(num7);
        }
        while ((this._alloweableBottonMountains & bottomMountainPattern.MountainType) == (MountainType) 0 || num7 + 200 < 500 || absoluteConnections2.RightConnection > 450 || absoluteConnections2.MaxHeight - absoluteConnections1.MaxHeight < num1);
        Mountain nextMountain2 = this.GetNextMountain(bottomMountainPattern, new Vector2((float) (offset + num3), (float) num7));
        this._alloweableBottonMountains = this.GetAlloweableNextMountains(bottomMountainPattern.MountainType);
        tunnelPartInfo.BottomConnection = absoluteConnections2;
        tunnelPartInfo.BottomMountain = nextMountain2;
        mountains.Add(tunnelPartInfo);
        num3 += 160;
        this._lastBottomConnections = absoluteConnections2;
      }
      return mountains;
    }

    private Mountain GetNextMountain(MountainPattern pattern, Vector2 position)
    {
      Mountain instance = Mountain.GetInstance();
      instance.Id = IdFactory.Instance.GetId();
      instance.ZIndex = 5f;
      instance.SetPosition(position);
      instance.Init((Pattern) pattern);
      return instance;
    }

    private ChallengeProvider.MountainConnectionsInfo GetTopConnections(
      MountainPattern mountainPattern)
    {
      ChallengeProvider.MountainConnectionsInfo topConnections = new ChallengeProvider.MountainConnectionsInfo();
      switch (mountainPattern.MountainType)
      {
        case MountainType.Normal:
          topConnections.LeftConnection = 180;
          topConnections.RightConnection = 180;
          break;
        case MountainType.Up1:
          topConnections.LeftConnection = 130;
          topConnections.RightConnection = 180;
          break;
        case MountainType.Up2:
          topConnections.LeftConnection = 80;
          topConnections.RightConnection = 180;
          break;
        case MountainType.Up3:
          topConnections.LeftConnection = 80;
          topConnections.RightConnection = 230;
          break;
        case MountainType.Down1:
          topConnections.LeftConnection = 180;
          topConnections.RightConnection = 130;
          break;
        case MountainType.Down2:
          topConnections.LeftConnection = 180;
          topConnections.RightConnection = 80;
          break;
        case MountainType.Down3:
          topConnections.LeftConnection = 230;
          topConnections.RightConnection = 80;
          break;
        case MountainType.Peak:
          topConnections.LeftConnection = 80;
          topConnections.RightConnection = 80;
          break;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Unknown Mountain Type '{0}'.", (object) mountainPattern.MountainType));
      }
      topConnections.MaxHeight = Math.Max(topConnections.LeftConnection, topConnections.RightConnection);
      if (mountainPattern.MountainType == MountainType.Peak)
        topConnections.MaxHeight += 90;
      return topConnections;
    }

    private ChallengeProvider.MountainConnectionsInfo GetBottomConnections(
      MountainPattern mountainPattern)
    {
      ChallengeProvider.MountainConnectionsInfo bottomConnections = new ChallengeProvider.MountainConnectionsInfo();
      switch (mountainPattern.MountainType)
      {
        case MountainType.Normal:
          bottomConnections.LeftConnection = 30;
          bottomConnections.RightConnection = 30;
          break;
        case MountainType.Up1:
          bottomConnections.LeftConnection = 80;
          bottomConnections.RightConnection = 30;
          break;
        case MountainType.Up2:
          bottomConnections.LeftConnection = 130;
          bottomConnections.RightConnection = 30;
          break;
        case MountainType.Up3:
          bottomConnections.LeftConnection = 180;
          bottomConnections.RightConnection = 30;
          break;
        case MountainType.Down1:
          bottomConnections.LeftConnection = 30;
          bottomConnections.RightConnection = 80;
          break;
        case MountainType.Down2:
          bottomConnections.LeftConnection = 30;
          bottomConnections.RightConnection = 130;
          break;
        case MountainType.Down3:
          bottomConnections.LeftConnection = 30;
          bottomConnections.RightConnection = 180;
          break;
        case MountainType.Peak:
          bottomConnections.LeftConnection = 130;
          bottomConnections.RightConnection = 130;
          break;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Unknown Mountain Type '{0}'.", (object) mountainPattern.MountainType));
      }
      bottomConnections.MaxHeight = Math.Min(bottomConnections.LeftConnection, bottomConnections.RightConnection);
      if (mountainPattern.MountainType == MountainType.Peak)
        bottomConnections.MaxHeight -= 90;
      return bottomConnections;
    }

    private MountainType GetAlloweableNextMountains(MountainType currentMountainType)
    {
      switch (currentMountainType)
      {
        case MountainType.Normal:
          return MountainType.Up | MountainType.Down | MountainType.Normal | MountainType.Peak;
        case MountainType.Up1:
        case MountainType.Up2:
          return MountainType.Normal | MountainType.Up1 | MountainType.Up2 | MountainType.Peak;
        case MountainType.Up3:
          return MountainType.Normal;
        case MountainType.Down1:
        case MountainType.Down2:
          return MountainType.Normal | MountainType.Down1 | MountainType.Down2;
        case MountainType.Down3:
          return MountainType.Normal;
        case MountainType.Peak:
          return MountainType.Down | MountainType.Normal;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Unknown Mountain Type '{0}'.", (object) currentMountainType));
      }
    }

    public void Init()
    {
      this.EpisodeNumber = 0;
      this.InitGenerationSettings();
      IdFactory.Instance.SetPrevId(1000);
      this.Instances.Sort((IComparer<Instance>) this._comparer);
      this._lastTopConnections = new ChallengeProvider.MountainConnectionsInfo(55, 55, 55);
      this._lastBottomConnections = new ChallengeProvider.MountainConnectionsInfo(450, 450, 450);
      this._topLandingPatterns = (IDictionary<LandingElementType, LandingElementPattern>) new Dictionary<LandingElementType, LandingElementPattern>()
      {
        {
          LandingElementType.Label,
          new LandingElementPattern(LandingElementType.Label, VerticalAlignment.Top)
        },
        {
          LandingElementType.StartBlock,
          new LandingElementPattern(LandingElementType.StartBlock, VerticalAlignment.Top)
        },
        {
          LandingElementType.MediumBlock,
          new LandingElementPattern(LandingElementType.MediumBlock, VerticalAlignment.Top)
        },
        {
          LandingElementType.EndBlock,
          new LandingElementPattern(LandingElementType.EndBlock, VerticalAlignment.Top)
        },
        {
          LandingElementType.StartShield,
          new LandingElementPattern(LandingElementType.StartShield, VerticalAlignment.Top)
        },
        {
          LandingElementType.MediumShield,
          new LandingElementPattern(LandingElementType.MediumShield, VerticalAlignment.Top)
        },
        {
          LandingElementType.EndShield,
          new LandingElementPattern(LandingElementType.EndShield, VerticalAlignment.Top)
        }
      };
      this._bottomLandingPatterns = (IDictionary<LandingElementType, LandingElementPattern>) new Dictionary<LandingElementType, LandingElementPattern>()
      {
        {
          LandingElementType.StartBlock,
          new LandingElementPattern(LandingElementType.StartBlock, VerticalAlignment.Bottom)
        },
        {
          LandingElementType.MediumBlock,
          new LandingElementPattern(LandingElementType.MediumBlock, VerticalAlignment.Bottom)
        },
        {
          LandingElementType.EndBlock,
          new LandingElementPattern(LandingElementType.EndBlock, VerticalAlignment.Bottom)
        },
        {
          LandingElementType.StartShield,
          new LandingElementPattern(LandingElementType.StartShield, VerticalAlignment.Bottom)
        },
        {
          LandingElementType.MediumShield,
          new LandingElementPattern(LandingElementType.MediumShield, VerticalAlignment.Bottom)
        },
        {
          LandingElementType.EndShield,
          new LandingElementPattern(LandingElementType.EndShield, VerticalAlignment.Bottom)
        }
      };
      this.RecommendedLengthForNextInstances = 3200;
    }

    public int Seek { get; private set; }

    public void LoadPatterns(IList<Pattern> patterns)
    {
      this._topMountainPatterns = (IList<MountainPattern>) new List<MountainPattern>();
      this._bottomMountainPatterns = (IList<MountainPattern>) new List<MountainPattern>();
      this.Patterns = new Dictionary<UnitType, List<Pattern>>();
      foreach (Pattern pattern in (IEnumerable<Pattern>) patterns)
      {
        if (pattern is MountainPattern)
        {
          MountainPattern mountainPattern = (MountainPattern) pattern;
          if (mountainPattern.Alignment == VerticalAlignment.Top)
          {
            this._topMountainPatterns.Add(mountainPattern);
            this._topConnections.Add(mountainPattern.Id, this.GetTopConnections(mountainPattern));
          }
          else
          {
            this._bottomMountainPatterns.Add(mountainPattern);
            this._bottomConnections.Add(mountainPattern.Id, this.GetBottomConnections(mountainPattern));
          }
        }
        if (pattern is HelicopterPattern || pattern is CannonPattern)
        {
          UnitType key = UnitType.None;
          if (pattern is HelicopterPattern)
            key = ((HelicopterPattern) pattern).UnitType;
          if (pattern is CannonPattern)
            key = ((CannonPattern) pattern).UnitType;
          if (!this.Patterns.ContainsKey(key))
            this.Patterns.Add(key, new List<Pattern>());
          this.Patterns[key].Add(pattern);
        }
      }
    }

    public void LoadInstances(IList<Instance> instances)
    {
      this.Instances.AddRange((IEnumerable<Instance>) instances);
    }

    public IList<Instance> GetNextInstances(int length)
    {
      List<Instance> nextInstances = new List<Instance>();
      if (this.Seek < 1600)
      {
        IEnumerable<Instance> landingZone = this.GetLandingZone(0, 1600, true);
        nextInstances.AddRange(landingZone);
        this.Seek += 1600;
      }
      int num = Math.Min(Math.Max(36800 - (this.Seek - 1600), 0), length) / 1600;
      if (this.Seek < 38400)
      {
        int seek = this.Seek;
        for (int index = 0; index < num; ++index)
        {
          ChallengeProvider.SegmentGenerationProperties segmentProperties = this.GetSegmentProperties((float) seek);
          IEnumerable<Instance> segmentInstances = this.GetSegmentInstances(seek, segmentProperties);
          nextInstances.AddRange(segmentInstances);
          seek += 1600;
          ++this._allSegmentsCount;
          if (this._allSegmentsCount == 50)
            break;
        }
        this.Seek += 1600 * num;
      }
      if (this.Seek >= 38400)
      {
        IEnumerable<Instance> landingZone = this.GetLandingZone(this.Seek, 1600, false);
        nextInstances.AddRange(landingZone);
        this.Seek += 1600;
      }
      return (IList<Instance>) nextInstances;
    }

    private class SegmentGenerationProperties
    {
      public int Corridor { get; set; }

      public int CoptersCount { get; set; }

      public int TurretsCount { get; set; }

      public int DroidsCount { get; set; }
    }

    private class TunnelPartInfo
    {
      public int Start { get; set; }

      public int End { get; set; }

      public ChallengeProvider.MountainConnectionsInfo TopConnection { get; set; }

      public ChallengeProvider.MountainConnectionsInfo BottomConnection { get; set; }

      public Mountain TopMountain { get; set; }

      public Mountain BottomMountain { get; set; }
    }

    private enum CopterAllignment
    {
      Top,
      Center,
      Bottom,
    }

    private class MountainConnectionsInfo
    {
      public int LeftConnection { get; set; }

      public int RightConnection { get; set; }

      public int MaxHeight { get; set; }

      public MountainConnectionsInfo()
      {
      }

      public MountainConnectionsInfo(int leftConnection, int rightConnection, int maxValue)
      {
        this.LeftConnection = leftConnection;
        this.RightConnection = rightConnection;
        this.MaxHeight = maxValue;
      }

      public ChallengeProvider.MountainConnectionsInfo GetAbsoluteConnections(int yPosition)
      {
        return new ChallengeProvider.MountainConnectionsInfo(this.LeftConnection + yPosition, this.RightConnection + yPosition, this.MaxHeight + yPosition);
      }
    }
  }
}
