// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.PageFlipControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class PageFlipControl : PanelControl
  {
    private PageFlipTracker tracker = new PageFlipTracker();

    protected override void OnChildAdded(int index, BasicControl child)
    {
      this.tracker.PageWidthList.Insert(index, (int) child.Size.X);
    }

    protected override void OnChildRemoved(int index, BasicControl child)
    {
      this.tracker.PageWidthList.RemoveAt(index);
    }

    public override void Update(GameTime gametime)
    {
      this.tracker.Update();
      base.Update(gametime);
    }

    public override void HandleInput(InputState input)
    {
      this.tracker.HandleInput(input);
      if (this.ChildCount <= 0)
        return;
      this[this.tracker.CurrentPage].HandleInput(input);
    }

    public override void Draw(DrawContext context)
    {
      int childCount = this.ChildCount;
      if (childCount < 2)
      {
        base.Draw(context);
      }
      else
      {
        Vector2 drawOffset = context.DrawOffset;
        int currentPage = this.tracker.CurrentPage;
        float currentPageOffset = this.tracker.CurrentPageOffset;
        context.DrawOffset = drawOffset + new Vector2()
        {
          X = currentPageOffset
        };
        this[currentPage].Draw(context);
        if ((double) currentPageOffset > 0.0)
        {
          int num = (currentPage + childCount - 1) % childCount;
          context.DrawOffset.X = drawOffset.X + currentPageOffset - (float) this.tracker.EffectivePageWidth(num);
          this[num].Draw(context);
        }
        if ((double) currentPageOffset + (double) this[currentPage].Size.X >= (double) context.Device.Viewport.Width)
          return;
        int childIndex = (currentPage + 1) % childCount;
        context.DrawOffset.X = drawOffset.X + currentPageOffset + (float) this.tracker.EffectivePageWidth(currentPage);
        this[childIndex].Draw(context);
      }
    }
  }
}
