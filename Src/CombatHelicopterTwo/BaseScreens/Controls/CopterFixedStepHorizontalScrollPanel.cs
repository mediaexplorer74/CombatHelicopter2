// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.CopterFixedStepHorizontalScrollPanel
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;
using Helicopter.Screen;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class CopterFixedStepHorizontalScrollPanel : FixedStepHorizontalScrollPanel
  {
    public HangerScreen.Slots Slot
    {
      get => ((CopterControl) this.CurrentChildren).CurrentSlot;
      set => ((CopterControl) this.CurrentChildren).CurrentSlot = value;
    }

    public HelicopterItem CurrentHelicopter
    {
      get => ((CopterControl) this.CurrentChildren).Item as HelicopterItem;
    }

    protected override void MotionChange(bool state)
    {
      ((CopterControl) this.CurrentChildren).SlotsState(!state);
    }
  }
}
