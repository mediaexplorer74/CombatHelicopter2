// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.LandingReaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Patterns;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  internal class LandingReaction(Instance owner) : Reaction(owner)
  {
    public LandingElementInstance Owner
    {
      get => (LandingElementInstance) base.Owner;
      set => this.Owner = (Instance) value;
    }

    public override void ProximityTo(Instance instance)
    {
    }

    public override void ReactTo(Instance instance)
    {
      bool flag = (this.Owner.Pattern.ElementType & LandingElementType.Block) != (LandingElementType) 0;
      if (instance is SmartPlayer && flag)
      {
        GameWorld gameWorld = instance.GameWorld;
        if (this.Owner.Pattern.Alignment == VerticalAlignment.Bottom)
        {
          if (gameWorld.EnableLandingZone)
            instance.GameWorld.InvokeLanding();
          else
            ((SmartPlayer) instance).ReboundUp(true);
        }
        else
          ((SmartPlayer) instance).ReboundDown(true);
      }
      if (!(instance is Copter) || !flag)
        return;
      instance.State = 1;
    }
  }
}
