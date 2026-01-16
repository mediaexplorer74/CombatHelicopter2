// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Reactions.BulletReaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Reactions
{
  internal class BulletReaction(Bullet owner) : Reaction((Instance) owner)
  {
    public override void ProximityTo(Instance instance)
    {
    }

    public override void ReactTo(Instance instance)
    {
      if (instance is Mountain)
      {
        this.Owner.State = 1;
        Bullet bullet = (Bullet) this.Owner;
        List<Vector2> source = Contour.IntersectionWithLine(instance.Contour, bullet.PrevPos, bullet.Position);
        if (source.Count <= 0)
          return;
        float min = source.Min<Vector2>((Func<Vector2, float>) (x => (x - bullet.PrevPos).Length()));
        this.Owner.Position = source.FirstOrDefault<Vector2>((Func<Vector2, bool>) (x => (double) Math.Abs((x - bullet.PrevPos).Length() - min) < 0.0099999997764825821));
      }
      else
      {
        if (instance is SmartPlayer)
        {
          SmartPlayer smartPlayer = (SmartPlayer) instance;
          Bullet owner = (Bullet) this.Owner;
          if (smartPlayer.State == 2 && !owner.IsDirectionChanged)
          {
            this.KillBullet(instance);
            return;
          }
        }
        if (!(instance is IUnit) || instance.State == 1 || ((IUnit) instance).Team == ((Bullet) this.Owner).Team)
          return;
        Bullet owner1 = (Bullet) this.Owner;
        float num = 0.0f;
        if (owner1.Owner is SmartPlayer owner2 && owner2.DamageModifier != null)
          num = owner2.DamageModifier.DamageCoef;
        ((IUnit) instance).HandleDamage(owner1.Damage * (1f + num), owner1.DamageType);
        if (instance.State == 1)
          this.Owner.IsNeedRemove = true;
        else
          this.KillBullet(instance);
        if (!(((Bullet) this.Owner).Owner is SmartPlayer))
          return;
        ((SmartPlayer) ((Bullet) this.Owner).Owner).OnBulletHit();
      }
    }

    private void KillBullet(Instance instance)
    {
      this.Owner.State = 1;
      if (!(this.Owner is MoveableInstance))
        return;
      ((MoveableInstance) this.Owner).Speed = ((MoveableInstance) instance).Speed;
      (this.Owner as Bullet).Acceleration = ((MoveableInstance) instance).Acceleration;
      ((Bullet) this.Owner).DeadBy = instance as MoveableInstance;
    }
  }
}
