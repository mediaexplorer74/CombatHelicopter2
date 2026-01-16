// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Patterns.LandingElementPattern
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Instances;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  internal class LandingElementPattern : Pattern
  {
    public LandingElementType ElementType { get; set; }

    public VerticalAlignment Alignment { get; set; }

    public LandingElementPattern(LandingElementType elementType)
    {
      this.ElementType = elementType;
      this.InitContour();
    }

    public LandingElementPattern(LandingElementType elementType, VerticalAlignment alignment)
    {
      this.ElementType = elementType;
      this.Alignment = alignment;
      this.InitContour();
    }

    private void InitContour()
    {
      this.Contour = new Contour();
      if (this.ElementType == LandingElementType.Label)
      {
        this.Contour.Add(new Point(0, 0));
        this.Contour.Add(new Point(102, 0));
        this.Contour.Add(new Point(102, 96));
        this.Contour.Add(new Point(0, 96));
      }
      else
      {
        this.Contour.Add(new Point(0, 0));
        this.Contour.Add(new Point(160, 0));
        this.Contour.Add(new Point(160, 200));
        this.Contour.Add(new Point(0, 200));
      }
      this.Contour.UpdateRectangle();
    }
  }
}
