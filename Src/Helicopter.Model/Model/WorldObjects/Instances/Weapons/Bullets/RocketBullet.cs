// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.RocketBullet
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  internal class RocketBullet : Bullet, IReusable
  {
    private const float CorrectionTime = 0.02f;
    private static ObjectPool<RocketBullet> _pool = new ObjectPool<RocketBullet>((ICreation<RocketBullet>) new RocketBullet.Creator());
    protected float _elapsedFromCreation;
    protected bool _isFreeFly;
    public float LaunchEngineTime = 0.2f;
    public bool HasStartStage;
    protected float _angleSpeed = 2f;
    protected float _targetAngle;
    protected bool _isCorrected;
    protected float _offsetXTargetable;
    protected float _offsetYTargetable = 25f;
    protected float _targetableWidth = 600f;
    private float _fromLastCorrection = 0.02f;

    public static RocketBullet GetInstance() => RocketBullet._pool.GetObject();

    public override void Release() => RocketBullet._pool.Release(this);

    public Instance Target { get; set; }

    protected RocketBullet()
    {
      Contour contour = new Contour();
      contour.AddRange((IEnumerable<Point>) new Point[4]
      {
        new Point(0, 0),
        new Point(30, 0),
        new Point(30, 9),
        new Point(0, 9)
      });
      contour.UpdateRectangle();
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
    }

    protected override void UpdateSpeed(float elapsedSeconds)
    {
      if (!this._isFreeFly && this.HasStartStage)
      {
        if ((double) this._elapsedFromCreation < (double) this.LaunchEngineTime)
          this._elapsedFromCreation += elapsedSeconds;
        else
          this._isFreeFly = true;
      }
      else
      {
        if (((IUnit) this.Owner).Team == 1)
        {
          try
          {
            this.ControlAI(elapsedSeconds);
          }
          catch (NullReferenceException ex)
          {
          }
        }
        base.UpdateSpeed(elapsedSeconds);
      }
    }

    protected virtual void ControlAI(float elapsedSeconds)
    {
      this._fromLastCorrection -= elapsedSeconds;
      if ((double) this._fromLastCorrection < 0.0)
      {
        if (this.Target != null)
        {
          Vector2 position = this.Position;
          Point point = this.Target.Contour.Rectangle.Center;
          if (this.Target is Cannon && ((CannonPattern) (this.Target as Cannon).Pattern).Alignment == VerticalAlignment.Bottom)
            point = new Point(this.Target.Contour.Rectangle.Center.X, this.Target.Contour.Rectangle.Top);
          this._targetAngle = (float) Math.Atan((double) Math.Abs((float) (((double) point.Y - (double) position.Y) / ((double) point.X - (double) position.X))));
          if ((double) point.X < (double) position.X)
            this._targetAngle = (double) point.Y >= (double) position.Y ? 3.14159274f - this._targetAngle : 3.14159274f + this._targetAngle;
          else if ((double) point.Y < (double) position.Y)
            this._targetAngle = -this._targetAngle;
        }
        else
        {
          this.Target = this.FindTarget();
          this._targetAngle = this.Angle;
        }
        this._fromLastCorrection = 0.02f;
      }
      if ((double) Math.Abs(this._targetAngle - this.Angle) <= 0.05000000074505806)
        return;
      if ((double) this._targetAngle > (double) this.Angle)
        this.Angle += this._angleSpeed * elapsedSeconds;
      else
        this.Angle -= this._angleSpeed * elapsedSeconds;
    }

    protected Instance FindTarget()
    {
      IEnumerable<Instance> source = (double) this.Angle >= 1.5707963705062866 || (double) this.Angle <= -1.5707963705062866 ? this.GameWorld.ActiveInstances.Where<Instance>((Func<Instance, bool>) (x => x is IUnit && (x as IUnit).Team != ((IUnit) this.Owner).Team && x.State != 1 && (double) x.Position.X < (double) this.Position.X)) : this.GameWorld.ActiveInstances.Where<Instance>((Func<Instance, bool>) (x => x is IUnit && (x as IUnit).Team != ((IUnit) this.Owner).Team && x.State != 1 && (double) x.Position.X > (double) this.Position.X + (double) this._offsetXTargetable && (double) x.Position.Y > (double) this.Position.Y - (double) this._offsetYTargetable && (double) x.Position.Y < (double) this.Position.Y + (double) this._offsetYTargetable));
      if (!source.Any<Instance>())
        return (Instance) null;
      Instance target = source.First<Instance>();
      float num1 = Math.Abs((source.First<Instance>().Position - this.Position).Length());
      int num2 = source.Count<Instance>();
      for (int index = 0; index < num2; ++index)
      {
        Instance instance = source.ElementAt<Instance>(index);
        float num3 = Math.Abs((instance.Position - this.Position).Length());
        if ((double) num3 < (double) num1)
        {
          num1 = num3;
          target = instance;
        }
      }
      return target;
    }

    public override void ResetState()
    {
      base.ResetState();
      this._elapsedFromCreation = 0.0f;
      this._isFreeFly = false;
      this.HasStartStage = false;
      this._targetAngle = 0.0f;
      this._isCorrected = false;
      this.Target = (Instance) null;
      this._fromLastCorrection = 0.02f;
      Contour contour = new Contour();
      contour.AddRange((IEnumerable<Point>) new Point[4]
      {
        new Point(0, 0),
        new Point(30, 0),
        new Point(30, 9),
        new Point(0, 9)
      });
      contour.UpdateRectangle();
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
    }

    protected class Creator : ICreation<RocketBullet>
    {
      public RocketBullet Create() => new RocketBullet();
    }
  }
}
