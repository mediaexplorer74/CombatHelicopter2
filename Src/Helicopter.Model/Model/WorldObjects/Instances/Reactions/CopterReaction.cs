// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Reactions.CopterReaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Patterns;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Reactions
{
  internal class CopterReaction(Instance owner) : Reaction(owner)
  {
    public override void ProximityTo(Instance instance)
    {
      if (instance is Shield || instance.IsTemporary || instance.IsNeedRemove || instance.State == 1)
        return;
      switch (instance)
      {
        case PlasmaBeam _:
          return;
        case LandingElementInstance _:
          if ((((LandingElementInstance) instance).Pattern.ElementType & LandingElementType.Background) != (LandingElementType) 0)
            return;
          break;
      }
      ((Copter) this.Owner).AwayFromObstacles(instance);
    }

    public override void ReactTo(Instance instance)
    {
      if (!(instance is SmartPlayer))
        return;
      this.Owner.State = 1;
    }
  }
}
