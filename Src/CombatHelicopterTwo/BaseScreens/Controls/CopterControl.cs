// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.CopterControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Screen;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class CopterControl : BasicControl
  {
    private readonly Dictionary<HangerScreen.Slots, SpecialSlotTwoStateMenuControl> _slots = new Dictionary<HangerScreen.Slots, SpecialSlotTwoStateMenuControl>();
    private HangerScreen.Slots _currentSlot;
    public BasicControl DamagedCopter;
    public BasicControl DamagedWing;

    public Helicopter.Items.Item Item { get; set; }

    public HangerScreen.Slots CurrentSlot
    {
      get => this._currentSlot;
      set
      {
        this._currentSlot = this._slots.ContainsKey(value) ? value : HangerScreen.Slots.FirstWeapon;
        this._slots[this._currentSlot].IsActive = true;
        this._slots[this._currentSlot].HasNew = false;
      }
    }

    public void AddSlot(HangerScreen.Slots slot, SpecialSlotTwoStateMenuControl control)
    {
      this.RegisterSlot(slot, control);
      this.AddChild((BasicControl) control);
    }

    public void SlotsState(bool on)
    {
      foreach (BasicControl basicControl in this._slots.Values)
        basicControl.Visible = on;
    }

    internal void RegisterSlot(HangerScreen.Slots slot, SpecialSlotTwoStateMenuControl control)
    {
      control.Clicked += (EventHandler<EventArgs>) ((sender, y) =>
      {
        this.CurrentSlot = slot;
        foreach (SpecialSlotTwoStateMenuControl stateMenuControl in this._slots.Values)
          stateMenuControl.IsActive = sender == stateMenuControl;
      });
      this._slots.Add(slot, control);
    }

    public void AddDamagedCopter(BasicControl damagedCopter)
    {
      this.DamagedCopter = damagedCopter;
      this.AddChild(this.DamagedCopter);
    }

    public void AddDamagedWing(BasicControl damagedWing)
    {
      this.DamagedWing = damagedWing;
      this.AddChild(this.DamagedWing);
    }

    public void RemoveDamaged()
    {
      if (this.DamagedCopter != null && this.Children.Contains(this.DamagedCopter))
        this.RemoveChild(this.DamagedCopter);
      if (this.DamagedWing == null || !this.Children.Contains(this.DamagedWing))
        return;
      this.RemoveChild(this.DamagedWing);
    }
  }
}
