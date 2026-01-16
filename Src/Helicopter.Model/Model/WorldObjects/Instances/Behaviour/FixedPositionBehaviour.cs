// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Behaviour.FixedPositionBehaviour
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Behaviour
{
  internal class FixedPositionBehaviour : IBehaviour
  {
    public FixedPositionBehaviour(Copter owner)
    {
      this.Owner = owner;
      this.Owner.Speed = Vector2.Zero;
      this.Owner.Acceleration = Vector2.Zero;
    }

    public Copter Owner { get; set; }

    public void AwayFromObstacles(Instance obstacle)
    {
    }

    public void Update(float elapsedSeconds)
    {
    }
  }
}
