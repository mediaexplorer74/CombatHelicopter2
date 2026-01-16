// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.Shield
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  public class Shield : Instance
  {
    private static readonly ObjectPool<Shield> _pool = new ObjectPool<Shield>((ICreation<Shield>) new Shield.Creator());
    public Shield.ShieldPosition SpawnPosition;
    public Instance Owner;

    public static Shield GetInstance() => Shield._pool.GetObject();

    public override void Release() => Shield._pool.Release(this);

    protected Shield()
    {
      Contour contour = new Contour();
      contour.SetPoints(new Point(0, 0), new Point(30, 0), new Point(75, 20), new Point(100, 50), new Point(110, 70), new Point(100, 120), new Point(90, 140), new Point(80, 160), new Point(60, 170), new Point(0, 180));
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
      this.Reaction = (Reaction) new ShieldReaction((Instance) this);
      this.IsTemporary = false;
    }

    public override void Update(float elapsedSeconds)
    {
      base.Update(elapsedSeconds);
      this.SetPosition(this.SpawnPosition(this));
    }

    public override void ResetState()
    {
      base.ResetState();
      Contour contour = new Contour();
      contour.SetPoints(new Point(0, 0), new Point(30, 0), new Point(75, 20), new Point(100, 50), new Point(110, 70), new Point(100, 120), new Point(90, 140), new Point(80, 160), new Point(60, 170), new Point(0, 180));
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
      this.Reaction = (Reaction) new ShieldReaction((Instance) this);
      this.IsTemporary = false;
    }

    protected class Creator : ICreation<Shield>
    {
      public Shield Create() => new Shield();
    }

    public delegate Vector2 ShieldPosition(Shield bullet);
  }
}
