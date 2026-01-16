// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Behaviour.StupidBehaviour
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Behaviour
{
  internal class StupidBehaviour : IBehaviour
  {
    private const float AppearanceTime = 1f;
    private const float AppearanceStartSpeed = 0.5f;
    private float _appearancePeriod;
    private float _elapsedTimeFromLastDirectionChange;
    private StupidBehaviour.FlyDirection _currentDirection;

    public Copter Owner { get; set; }

    public StupidBehaviour(Copter owner) => this.Owner = owner;

    public void Update(float elapsedSeconds)
    {
      if ((double) this._appearancePeriod < 1.0)
      {
        this._appearancePeriod += elapsedSeconds;
        this.Owner.Speed.X = (float) (0.5 + (double) (this.Owner.PursuitXSpeed - 0.5f) * (double) this._appearancePeriod / 1.0);
      }
      else
      {
        this._elapsedTimeFromLastDirectionChange -= elapsedSeconds;
        if ((double) this._elapsedTimeFromLastDirectionChange >= 0.0)
          return;
        this.ChangeDirection();
      }
    }

    private void ChangeDirection()
    {
      this._elapsedTimeFromLastDirectionChange = (float) CommonRandom.Instance.Random.Next(2, 4);
      float minValue = this.Owner.ObstaclesReboundYSpeed - 20f;
      float maxValue = this.Owner.ObstaclesReboundYSpeed + 20f;
      if (this._currentDirection == StupidBehaviour.FlyDirection.Up)
      {
        this.Owner.Speed.Y = (float) CommonRandom.Instance.Random.Next((int) minValue, (int) maxValue);
        this._currentDirection = StupidBehaviour.FlyDirection.Down;
      }
      else
      {
        this.Owner.Speed.Y = (float) -CommonRandom.Instance.Random.Next((int) minValue, (int) maxValue);
        this._currentDirection = StupidBehaviour.FlyDirection.Up;
      }
    }

    public void AwayFromObstacles(Instance obstacle)
    {
      this._currentDirection = this.Owner.Contour.Rectangle.Center.Y <= obstacle.Contour.Rectangle.Center.Y ? StupidBehaviour.FlyDirection.Down : StupidBehaviour.FlyDirection.Up;
      this.ChangeDirection();
    }

    private enum FlyDirection
    {
      Up,
      Down,
    }
  }
}
