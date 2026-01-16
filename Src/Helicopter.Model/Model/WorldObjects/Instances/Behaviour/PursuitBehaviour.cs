// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Behaviour.PursuitBehaviour
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Behaviour
{
  internal class PursuitBehaviour : IBehaviour
  {
    private const float AppearanceTime = 1f;
    private const float AppearanceStartSpeed = -0.5f;
    private float _appearancePeriod;

    public PursuitBehaviour(Copter owner)
    {
      this.Owner = owner;
      this.Owner.Speed.Y = (float) CommonRandom.Instance.Random.Next(-10, 10);
    }

    public Copter Owner { get; set; }

    public void AwayFromObstacles(Instance obstacle)
    {
      if (obstacle is SmartPlayer)
        return;
      if (this.Owner.Contour.Rectangle.Center.Y > obstacle.Contour.Rectangle.Center.Y)
        this.Owner.Speed.Y = this.Owner.ObstaclesReboundYSpeed;
      else
        this.Owner.Speed.Y = -this.Owner.ObstaclesReboundYSpeed;
    }

    public void Update(float elapsedSeconds)
    {
      if ((double) this._appearancePeriod < 1.0)
      {
        this._appearancePeriod += elapsedSeconds;
        float num = this.Owner.PursuitXSpeed * -0.5f;
        float pursuitXspeed = this.Owner.PursuitXSpeed;
        this.Owner.Speed.X = num + (float) (((double) pursuitXspeed - (double) num) * (double) this._appearancePeriod / 1.0);
      }
      this.PursuitCopter(elapsedSeconds);
    }

    private void PursuitCopter(float elapsedSeconds)
    {
      int num = this.Owner.Contour.Rectangle.Center.Y - this.Owner.GameWorld.Player.Contour.Rectangle.Center.Y - 20;
      this.Owner.Speed.Y += (float) -Math.Sign(num) * this.Owner.PursuitAcceleration * elapsedSeconds;
      if (Math.Abs(num) < 5)
        this.Owner.Speed.Y = 0.0f;
      this.Owner.Speed.Y = MathHelper.Clamp(this.Owner.Speed.Y, -this.Owner.PursuitMaxYSpeed, this.Owner.PursuitMaxYSpeed);
    }
  }
}
