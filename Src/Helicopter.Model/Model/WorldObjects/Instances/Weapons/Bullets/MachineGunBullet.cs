// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.MachineGunBullet
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
  internal class MachineGunBullet : Bullet
  {
    private static ObjectPool<MachineGunBullet> _pool = new ObjectPool<MachineGunBullet>((ICreation<MachineGunBullet>) new MachineGunBullet.Creator());

    public static MachineGunBullet GetInstance() => MachineGunBullet._pool.GetObject();

    public override void Release() => MachineGunBullet._pool.Release(this);

    public override void ResetState()
    {
      base.ResetState();
      this.InitBullet();
    }

    private void InitBullet()
    {
      Contour contour = new Contour();
      Point offset = new Point(30, 0);
      contour.AddRange((IEnumerable<Point>) new Point[4]
      {
        new Point(0, 0),
        new Point(31 + offset.X, 0),
        new Point(31 + offset.X, 5),
        new Point(0, 5)
      });
      contour.UpdateRectangle();
      this.Init((Pattern) BulletPattern.CreateInstance("GameWorld/Objects/Weapon/weaponS1_1patron2", new Rectangle(0, 0, 31, 5), offset, contour));
    }

    protected MachineGunBullet() => this.InitBullet();

    protected class Creator : ICreation<MachineGunBullet>
    {
      public MachineGunBullet Create() => new MachineGunBullet();
    }
  }
}
