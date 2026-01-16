// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Reactions.Reaction
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Reactions
{
  public abstract class Reaction
  {
    public Instance Owner { get; protected set; }

    protected Reaction(Instance owner) => this.Owner = owner;

    public abstract void ProximityTo(Instance instance);

    public abstract void ReactTo(Instance instance);
  }
}
