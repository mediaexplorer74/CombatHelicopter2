// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Descriptions.LevelRestorer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Background;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using Helicopter.Model.WorldObjects.Providers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.Descriptions
{
  public class LevelRestorer
  {
    private static Helicopter.Model.WorldObjects.Background.Background RestoreBackground(
      IEnumerable<BackgroundDesc> backgroundDescs)
    {
      Helicopter.Model.WorldObjects.Background.Background background = new Helicopter.Model.WorldObjects.Background.Background();
      foreach (BackgroundDesc backgroundDesc in backgroundDescs)
      {
        BackgroundLayer backgroundLayer = new BackgroundLayer()
        {
          Position = new Vector2((float) backgroundDesc.Position.X, (float) backgroundDesc.Position.Y),
          Remoteness = backgroundDesc.Remoteness,
          TexturePath = backgroundDesc.TexturePath
        };
        background.Layers.Add(backgroundLayer);
      }
      background.Layers.Sort(new Comparison<BackgroundLayer>(BackgroundLayer.Comparer));
      return background;
    }

    public static Instance RestoreInstance(
      InstanceDesc instanceDesc,
      Dictionary<int, Pattern> patterns)
    {
      Pattern pattern = patterns[instanceDesc.PatternId];
      Instance instance1;
      switch (instanceDesc)
      {
        case MountainDesc _:
          Mountain instance2 = Mountain.GetInstance();
          instance2.Id = instanceDesc.InstanceId;
          instance2.SetPosition((float) instanceDesc.Position.X, (float) instanceDesc.Position.Y);
          instance1 = (Instance) instance2;
          instance1.ZIndex = 5f;
          break;
        case HelicopterDesc _:
          HelicopterPattern helicopterPattern = (HelicopterPattern) pattern;
          Copter copter = helicopterPattern.UnitType == UnitType.Boss3 || helicopterPattern.UnitType == UnitType.Boss4 || helicopterPattern.UnitType == UnitType.Boss5 ? (Copter) MothershipCopter.GetInstance() : Copter.GetInstance();
          copter.Id = instanceDesc.InstanceId;
          copter.IsBoss = (helicopterPattern.UnitType & UnitType.Boss) != (UnitType) 0;
          copter.SetPosition((float) instanceDesc.Position.X, (float) instanceDesc.Position.Y);
          copter.Team = 2;
          instance1 = (Instance) copter;
          instance1.ZIndex = 6f;
          break;
        case CannonDesc _:
          Cannon instance3 = Cannon.GetInstance();
          instance3.Id = instanceDesc.InstanceId;
          instance3.SetPosition((float) instanceDesc.Position.X, (float) instanceDesc.Position.Y);
          instance3.Team = 2;
          instance1 = (Instance) instance3;
          instance1.ZIndex = 4f;
          break;
        default:
          throw new Exception(string.Format("Unknown pattern type - '{0}'.", (object) instanceDesc.GetType()));
      }
      instance1.Init(pattern);
      return instance1;
    }

    public static Pattern RestorePattern(PatternDesc patternDesc)
    {
      Pattern pattern;
      switch (patternDesc)
      {
        case MountainPatternDesc _:
          MountainPatternDesc mountainPatternDesc = (MountainPatternDesc) patternDesc;
          pattern = (Pattern) new MountainPattern()
          {
            Alignment = mountainPatternDesc.Alignment,
            MountainType = mountainPatternDesc.MountainType
          };
          break;
        case HelicopterPatternDesc _:
          HelicopterPatternDesc helicopterPatternDesc = (HelicopterPatternDesc) patternDesc;
          pattern = (Pattern) new HelicopterPattern()
          {
            WeaponSlots = helicopterPatternDesc.WeaponSlots,
            MotionType = helicopterPatternDesc.MotionType,
            PatrolingSpeed = helicopterPatternDesc.PatrolingSpeed,
            ObstaclesReboundYSpeed = helicopterPatternDesc.ObstaclesReboundYSpeed,
            PursuitAcceleration = helicopterPatternDesc.PursuitAcceleration,
            PursuitMaxYSpeed = helicopterPatternDesc.PursuitMaxYSpeed,
            PursuitXSpeed = helicopterPatternDesc.PursuitXSpeed,
            HeightCompensation = helicopterPatternDesc.HeightCompensation,
            HitShotCorridor = helicopterPatternDesc.HitShotCorridor,
            StartPursuitDistance = helicopterPatternDesc.StartPursuitDistance,
            Price = helicopterPatternDesc.Price,
            Energy = helicopterPatternDesc.Energy,
            DroidPatternId = helicopterPatternDesc.DroidPatternId,
            FirstWeaponRate = helicopterPatternDesc.FirstWeaponRate,
            FirstWeaponShootingTime = helicopterPatternDesc.FirstWeaponShootingTime,
            FirstWeaponReloadTime = helicopterPatternDesc.FirstWeaponReloadTime,
            SecondWeaponRate = helicopterPatternDesc.SecondWeaponRate,
            SecondWeaponShootingTime = helicopterPatternDesc.SecondWeaponShootingTime,
            SecondWeaponReloadTime = helicopterPatternDesc.SecondWeaponReloadTime,
            CollisionDamage = helicopterPatternDesc.CollisionDamage,
            UnitType = helicopterPatternDesc.UnitType,
            ShootDelay = helicopterPatternDesc.ShootDelay
          };
          break;
        case CannonPatternDesc _:
          CannonPatternDesc cannonPatternDesc = (CannonPatternDesc) patternDesc;
          CannonPattern cannonPattern = new CannonPattern()
          {
            Alignment = cannonPatternDesc.Alignment,
            UnitType = cannonPatternDesc.UnitType,
            MoutionRange = cannonPatternDesc.MoutionRange,
            MoutonPath = cannonPatternDesc.MoutonPath,
            MoutonSpeed = cannonPatternDesc.MoutonSpeed,
            HitCorridor = cannonPatternDesc.HitCorridor,
            Price = cannonPatternDesc.Price,
            Energy = cannonPatternDesc.Energy,
            CollisionDamage = cannonPatternDesc.CollisionDamage
          };
          cannonPattern.WeaponSlotDesc = cannonPatternDesc.WeaponSlotDesc;
          pattern = (Pattern) cannonPattern;
          break;
        default:
          throw new Exception(string.Format("Unknown pattern type - '{0}'.", (object) patternDesc.GetType()));
      }
      pattern.Id = patternDesc.PatternId;
      pattern.DescriptionId = patternDesc.DescriptionId;
      pattern.Contour = patternDesc.Contour;
      if (pattern is CannonPattern)
        pattern.Contour.IntersectRectangleOnly = true;
      pattern.Sprites = patternDesc.Sprites;
      return pattern;
    }

    public static GameWorld RestoreWorld(LevelDesc desc)
    {
      GameWorld world = new GameWorld();
      world.Mode = desc.Mode;
      switch (desc.Mode)
      {
        case EpisodeMode.Story:
          world.InstancesProvider = (IInstancesProvider) new SimpleInstancesProvider();
          world.Length = desc.Length;
          world.EpisodeLength = world.Length;
          break;
        case EpisodeMode.BossBattle:
          world.InstancesProvider = (IInstancesProvider) new BossBattleInstancesProvider();
          world.Length = -1;
          break;
        case EpisodeMode.Challenge:
          world.InstancesProvider = (IInstancesProvider) new ChallengeProvider();
          world.Length = -1;
          world.EpisodeLength = 36800;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      world.WorldType = desc.WorldType;
      world.Background = LevelRestorer.RestoreBackground((IEnumerable<BackgroundDesc>) desc.Backgrounds);
      Dictionary<int, Pattern> patterns = desc.Patterns.Select<PatternDesc, Pattern>((Func<PatternDesc, Pattern>) (patternDesc => LevelRestorer.RestorePattern(patternDesc))).ToDictionary<Pattern, int>((Func<Pattern, int>) (pattern => pattern.Id));
      List<Instance> list = desc.Instances.Select<InstanceDesc, Instance>((Func<InstanceDesc, Instance>) (instanceDesc => LevelRestorer.RestoreInstance(instanceDesc, patterns))).ToList<Instance>();
      world.InstancesProvider.LoadPatterns((IList<Pattern>) new List<Pattern>((IEnumerable<Pattern>) patterns.Values));
      world.InstancesProvider.LoadInstances((IList<Instance>) list);
      world.InstancesProvider.Init();
      foreach (Instance instance in list.Where<Instance>((Func<Instance, bool>) (x => x is IUnit && (double) x.Position.X < (double) world.Length)))
        world.TotalScoreForUnits += ((IUnit) instance).Score;
      return world;
    }
  }
}
