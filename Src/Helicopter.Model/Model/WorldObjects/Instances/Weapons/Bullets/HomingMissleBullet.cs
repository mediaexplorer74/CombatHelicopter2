// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.HomingMissleBullet
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  internal class HomingMissleBullet : RocketBullet, IReusable
  {
    private static ObjectPool<HomingMissleBullet> _pool = new ObjectPool<HomingMissleBullet>((ICreation<HomingMissleBullet>) new HomingMissleBullet.Creator());

    public static HomingMissleBullet GetInstance() => HomingMissleBullet._pool.GetObject();

    public override void Release() => HomingMissleBullet._pool.Release(this);

    protected HomingMissleBullet()
    {
      Contour contour = new Contour();
      contour.AddRange((IEnumerable<Point>) new Point[4]
      {
        new Point(0, 0),
        new Point(38, 0),
        new Point(38, 10),
        new Point(0, 10)
      });
      contour.UpdateRectangle();
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
      this._offsetYTargetable = 250f;
    }

    public override void ResetState()
    {
      base.ResetState();
      Contour contour = new Contour();
      contour.AddRange((IEnumerable<Point>) new Point[4]
      {
        new Point(0, 0),
        new Point(38, 0),
        new Point(38, 10),
        new Point(0, 10)
      });
      contour.UpdateRectangle();
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
      this._offsetYTargetable = 250f;
    }

    protected new class Creator : ICreation<HomingMissleBullet>
    {
      public HomingMissleBullet Create() => new HomingMissleBullet();
    }
  }
}
