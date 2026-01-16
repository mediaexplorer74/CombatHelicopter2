// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.PanelControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class PanelControl : BasicControl
  {
    public void LayoutColumn(float xMargin, float yMargin, float ySpacing)
    {
      float num = yMargin;
      for (int childIndex = 0; childIndex < this.ChildCount; ++childIndex)
      {
        BasicControl basicControl = this[childIndex];
        basicControl.Position = new Vector2()
        {
          X = xMargin,
          Y = num
        };
        num += basicControl.Size.Y + ySpacing;
      }
      this.InvalidateAutoSize();
    }

    public void LayoutRow(float xMargin, float yMargin, float xSpacing)
    {
      float num = xMargin;
      for (int childIndex = 0; childIndex < this.ChildCount; ++childIndex)
      {
        BasicControl basicControl = this[childIndex];
        basicControl.Position = new Vector2()
        {
          X = num,
          Y = yMargin
        };
        num += basicControl.Size.X + xSpacing;
      }
      this.InvalidateAutoSize();
    }

    public void LayoutColumnRight()
    {
      foreach (BasicControl child in this.Children)
        child.Position = new Vector2(this.Size.X - child.Size.X, child.Position.Y);
    }
  }
}
