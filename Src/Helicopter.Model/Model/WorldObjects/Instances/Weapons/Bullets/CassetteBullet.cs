// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.CassetteBullet
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Primitives;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  internal class CassetteBullet : Bullet, IReusable
  {
    private static ObjectPool<CassetteBullet> _pool = new ObjectPool<CassetteBullet>((ICreation<CassetteBullet>) new CassetteBullet.Creator());
    public int NumberOfBalls;
    public float StartAngle;
    public float EndAngle;
    public float TimeToExplosion = 0.5f;
    public int ChildLinearSpeed = 700;
    public int ChildLinearAcceleration = 400;

    public static CassetteBullet GetInstance() => CassetteBullet._pool.GetObject();

    public override void Release() => CassetteBullet._pool.Release(this);

    protected CassetteBullet() => this.InitBullet();

    private void InitBullet()
    {
      Contour contour = new Contour();
      Point offset = new Point(30, 0);
      contour.AddRange((IEnumerable<Point>) new Point[4]
      {
        new Point(0, 0),
        new Point(28 + offset.X, 0),
        new Point(28 + offset.X, 9),
        new Point(0, 9)
      });
      contour.UpdateRectangle();
      this.Init((Pattern) BulletPattern.CreateInstance("GameWorld/Objects/Weapon/weaponS2_5patron1", new Rectangle(0, 0, 28, 9), offset, contour));
    }

    private void Explosion()
    {
      this.State = 1;
      for (int index = 0; index < this.NumberOfBalls; ++index)
      {
        MachineGunBullet instance = MachineGunBullet.GetInstance();
        instance.Angle = MathHelper.ToRadians(this.StartAngle + (float) index * (this.EndAngle - this.StartAngle) / (float) this.NumberOfBalls);
        instance.Owner = this.Owner;
        instance.Position = this.Position;
        instance.LinearSpeed = (float) this.ChildLinearSpeed;
        instance.LinearAcceleration = (float) this.ChildLinearAcceleration;
        instance.Damage = this.Damage;
        instance.Team = ((IUnit) this.Owner).Team;
        instance.GameWorld = this.Owner.GameWorld;
        Contour contour = new Contour();
        Point offset = new Point(30, 0);
        contour.AddRange((IEnumerable<Point>) new Point[4]
        {
          new Point(0, 0),
          new Point(13 + offset.X, 0),
          new Point(13 + offset.X, 7),
          new Point(0, 7)
        });
        contour.UpdateRectangle();
        instance.Init((Pattern) BulletPattern.CreateInstance("GameWorld/Objects/Weapon/weaponS2_5patron2", new Rectangle(0, 0, 13, 7), offset, contour));
        this.Owner.GameWorld.AddInstance((Instance) instance);
      }
    }

    protected override void OnStateChanged(object sender, StateChangeEventArgs<int> e)
    {
      if (e.NextState != 1)
        return;
      this.DeadBy = (MoveableInstance) this;
      CassetteBullet cassetteBullet1 = this;
      cassetteBullet1.Speed = cassetteBullet1.Speed * 0.2f;
      CassetteBullet cassetteBullet2 = this;
      cassetteBullet2.Acceleration = cassetteBullet2.Acceleration * -1f;
    }

    protected override void UpdateState(float elapsedSeconds)
    {
      this.TimeToExplosion -= elapsedSeconds;
      if ((double) this.TimeToExplosion < 0.0 && this.State != 1)
        this.Explosion();
      this.UpdateSpeed(elapsedSeconds);
    }

    public override void ResetState()
    {
      base.ResetState();
      this.InitBullet();
      this.TimeToExplosion = 0.5f;
      this.ChildLinearSpeed = 700;
      this.ChildLinearAcceleration = 400;
    }

    protected class Creator : ICreation<CassetteBullet>
    {
      public CassetteBullet Create() => new CassetteBullet();
    }
  }
}
