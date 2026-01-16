// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Reactions.ShieldReaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Reactions
{
  internal class ShieldReaction(Instance owner) : Reaction(owner)
  {
    public override void ProximityTo(Instance instance)
    {
    }

    public override void ReactTo(Instance instance)
    {
      if ((instance is Copter || instance is Cannon) && (instance as IUnit).Team != ((IUnit) ((Shield) this.Owner).Owner).Team)
        instance.State = 1;
      if (!(instance is Bullet) || ((IUnit) (instance as Bullet).Owner).Team == ((IUnit) ((Shield) this.Owner).Owner).Team)
        return;
      this.KillBullet((Bullet) instance);
    }

    private void KillBullet(Bullet instance)
    {
      instance.State = 1;
      instance.DeadBy = (MoveableInstance) ((Shield) this.Owner).Owner;
    }
  }
}
