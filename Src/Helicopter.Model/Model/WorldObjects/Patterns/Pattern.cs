// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Patterns.Pattern
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  public class Pattern
  {
    public string DescriptionId { get; set; }

    public int Id { get; set; }

    public Contour Contour { get; set; }

    public List<SpriteDescription> Sprites { get; set; }

    protected Pattern() => this.Contour = new Contour();
  }
}
