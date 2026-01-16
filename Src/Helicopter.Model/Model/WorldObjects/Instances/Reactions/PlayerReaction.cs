// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Reactions.PlayerReaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Patterns;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Reactions
{
  internal class PlayerReaction : Reaction
  {
    private readonly SmartPlayer _player;

    public PlayerReaction(SmartPlayer player)
      : base((Instance) player)
    {
      this._player = player;
    }

    public override void ProximityTo(Instance instance)
    {
    }

    public override void ReactTo(Instance instance)
    {
      switch (instance)
      {
        case Mountain _:
          MountainPattern pattern = (MountainPattern) instance.Pattern;
          if (pattern.Alignment == VerticalAlignment.Bottom || pattern.Alignment == VerticalAlignment.None)
            this._player.ReboundUp((pattern.MountainType & MountainType.Up) != (MountainType) 0 || pattern.MountainType == MountainType.Peak);
          else
            this._player.ReboundDown((pattern.MountainType & MountainType.Down) != (MountainType) 0 || pattern.MountainType == MountainType.Peak);
          this._player.HandleCollisionDamage(this._player.EnergyOnMountainCollision);
          break;
        case IUnit _:
          this._player.HandleCollisionDamage(((IUnit) instance).CollisionDamage);
          this._player.InvokeEnemyRammed();
          break;
      }
    }
  }
}
