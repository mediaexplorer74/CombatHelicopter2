// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.PageFlipTracker
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class PageFlipTracker
  {
    public const GestureType GesturesNeeded = GestureType.HorizontalDrag | GestureType.Flick | GestureType.DragComplete;
    public static TimeSpan FlipDuration = TimeSpan.FromSeconds(0.3);
    public static double FlipExponent = 3.0;
    public static int PreviewMargin = 20;
    public static float DragToFlipTheshold = 0.333333343f;
    private DateTime flipStartTime;
    private float flipStartOffset;
    public List<int> PageWidthList = new List<int>();

    public int CurrentPage { get; private set; }

    public float CurrentPageOffset { get; private set; }

    public bool IsLeftPageVisible
    {
      get => this.PageWidthList.Count >= 2 && (double) this.CurrentPageOffset > 0.0;
    }

    public bool IsRightPageVisible
    {
      get
      {
        return this.PageWidthList.Count >= 2 && (double) this.CurrentPageOffset + (double) this.EffectivePageWidth(this.CurrentPage) <= (double) TouchPanel.DisplayWidth;
      }
    }

    public bool InFlip { get; private set; }

    public float FlipAlpha { get; private set; }

    public int EffectivePageWidth(int page)
    {
      return Math.Max(TouchPanel.DisplayWidth - PageFlipTracker.PreviewMargin, this.PageWidthList[page]);
    }

    public void Update()
    {
      if (!this.InFlip)
        return;
      TimeSpan timeSpan = DateTime.Now - this.flipStartTime;
      if (timeSpan >= PageFlipTracker.FlipDuration)
      {
        this.EndFlip();
      }
      else
      {
        this.FlipAlpha = (float) (1.0 - Math.Pow(1.0 - Math.Max(timeSpan.TotalSeconds / PageFlipTracker.FlipDuration.TotalSeconds, 0.0), PageFlipTracker.FlipExponent));
        this.CurrentPageOffset = this.flipStartOffset * (1f - this.FlipAlpha);
      }
    }

    public void HandleInput(InputState input)
    {
      foreach (GestureSample gesture in input.Gestures)
      {
        switch (gesture.GestureType)
        {
          case GestureType.HorizontalDrag:
            this.CurrentPageOffset += gesture.Delta.X;
            this.flipStartOffset = this.CurrentPageOffset;
            continue;
          case GestureType.Flick:
            if ((double) Math.Abs(gesture.Delta.X) > (double) Math.Abs(gesture.Delta.Y))
            {
              if ((double) gesture.Delta.X > 0.0)
              {
                this.BeginFlip(-1);
                continue;
              }
              this.BeginFlip(1);
              continue;
            }
            continue;
          case GestureType.DragComplete:
            if (!this.InFlip)
            {
              if ((double) this.CurrentPageOffset < (double) -TouchPanel.DisplayWidth * (double) PageFlipTracker.DragToFlipTheshold)
              {
                this.BeginFlip(1);
                continue;
              }
              if ((double) this.CurrentPageOffset + (double) TouchPanel.DisplayWidth * (1.0 - (double) PageFlipTracker.DragToFlipTheshold) > (double) this.EffectivePageWidth(this.CurrentPage))
              {
                this.BeginFlip(-1);
                continue;
              }
              this.BeginFlip(0);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }

    private void BeginFlip(int pageDelta)
    {
      if (this.PageWidthList.Count == 0)
        return;
      int currentPage = this.CurrentPage;
      this.CurrentPage = (this.CurrentPage + pageDelta + this.PageWidthList.Count) % this.PageWidthList.Count;
      if (pageDelta > 0)
        this.CurrentPageOffset += (float) this.EffectivePageWidth(currentPage);
      else if (pageDelta < 0)
        this.CurrentPageOffset -= (float) this.EffectivePageWidth(this.CurrentPage);
      this.InFlip = true;
      this.FlipAlpha = 0.0f;
      this.flipStartOffset = this.CurrentPageOffset;
      this.flipStartTime = DateTime.Now;
    }

    private void EndFlip()
    {
      this.InFlip = false;
      this.FlipAlpha = 1f;
      this.CurrentPageOffset = 0.0f;
    }
  }
}
