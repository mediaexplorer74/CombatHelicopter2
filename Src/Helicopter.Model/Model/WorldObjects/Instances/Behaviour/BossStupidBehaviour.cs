// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Behaviour.BossStupidBehaviour
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Behaviour
{
  internal class BossStupidBehaviour : IBehaviour
  {
    private BossStupidBehaviour.FlyDirection _currentDirection;

    public BossStupidBehaviour(Copter owner) => this.Owner = owner;

    public Copter Owner { get; set; }

    public void AwayFromObstacles(Instance obstacle)
    {
      if (((MothershipCopter) this.Owner).BehaviorType != MothershipCopter.BossBehaviorType.Battle)
        return;
      this._currentDirection = this.Owner.Contour.Rectangle.Center.Y <= obstacle.Contour.Rectangle.Center.Y ? BossStupidBehaviour.FlyDirection.Down : BossStupidBehaviour.FlyDirection.Up;
      this.ChangeDirection();
    }

    public void Update(float elapsedSeconds)
    {
    }

    private void ChangeDirection()
    {
      float minValue = this.Owner.ObstaclesReboundYSpeed - 20f;
      float maxValue = this.Owner.ObstaclesReboundYSpeed + 20f;
      if (this._currentDirection == BossStupidBehaviour.FlyDirection.Up)
      {
        this.Owner.Speed.Y = (float) CommonRandom.Instance.Random.Next((int) minValue, (int) maxValue);
        this._currentDirection = BossStupidBehaviour.FlyDirection.Down;
      }
      else
      {
        this.Owner.Speed.Y = (float) -CommonRandom.Instance.Random.Next((int) minValue, (int) maxValue);
        this._currentDirection = BossStupidBehaviour.FlyDirection.Up;
      }
    }

    private enum FlyDirection
    {
      Up,
      Down,
    }
  }
}
