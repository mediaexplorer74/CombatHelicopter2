// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Reactions.PlasmaBeamReaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Reactions
{
  internal class PlasmaBeamReaction(Instance owner) : Reaction(owner)
  {
    public override void ProximityTo(Instance instance)
    {
    }

    public override void ReactTo(Instance instance)
    {
      if (instance is Bullet || instance == ((PlasmaBeam) this.Owner).Owner || instance is LandingElementInstance)
        return;
      Instance capturedInstance = ((PlasmaBeam) this.Owner).CapturedInstance;
      if (capturedInstance != null)
      {
        IUnit owner = (IUnit) ((PlasmaBeam) this.Owner).Owner;
        if (owner is SmartPlayer)
        {
          if ((double) instance.Position.X >= (double) capturedInstance.Position.X || (double) instance.Position.X <= (double) ((Instance) owner).Contour.Rectangle.Right)
            return;
          ((PlasmaBeam) this.Owner).CapturedInstance = instance;
        }
        else
        {
          if (instance.Contour.Rectangle.Right <= capturedInstance.Contour.Rectangle.Right || (double) instance.Contour.Rectangle.Right >= (double) ((Instance) owner).Position.X)
            return;
          ((PlasmaBeam) this.Owner).CapturedInstance = instance;
        }
      }
      else
        ((PlasmaBeam) this.Owner).CapturedInstance = instance;
    }
  }
}
