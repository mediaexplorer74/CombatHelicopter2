// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.DoubleRocketBullet
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  internal class DoubleRocketBullet : RocketBullet
  {
    private static ObjectPool<DoubleRocketBullet> _pool = new ObjectPool<DoubleRocketBullet>((ICreation<DoubleRocketBullet>) new DoubleRocketBullet.Creator());

    public static DoubleRocketBullet GetInstance() => DoubleRocketBullet._pool.GetObject();

    public override void Release() => DoubleRocketBullet._pool.Release(this);

    protected DoubleRocketBullet()
    {
      Contour contour = new Contour();
      contour.SetPoints(new Point(0, 0), new Point(35, 0), new Point(35, 9), new Point(0, 9));
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
    }

    public override void ResetState()
    {
      base.ResetState();
      Contour contour = new Contour();
      contour.SetPoints(new Point(0, 0), new Point(35, 0), new Point(35, 9), new Point(0, 9));
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
    }

    protected new class Creator : ICreation<DoubleRocketBullet>
    {
      public DoubleRocketBullet Create() => new DoubleRocketBullet();
    }
  }
}
