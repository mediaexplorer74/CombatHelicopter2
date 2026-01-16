// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Instances.Behaviour.AttackBehaviour
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Behaviour
{
  internal class AttackBehaviour : IBehaviour
  {
    public Copter Owner { get; set; }

    public AttackBehaviour(Copter owner)
    {
      this.Owner = owner;
      if ((double) this.Owner.Speed.X <= 0.0)
        return;
      this.Owner.Speed.X *= -1f;
    }

    public void Update(float elapsedSeconds)
    {
    }

    public void AwayFromObstacles(Instance obstacle)
    {
    }
  }
}
