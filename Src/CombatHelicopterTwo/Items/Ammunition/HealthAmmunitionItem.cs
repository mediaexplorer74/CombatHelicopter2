// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.Ammunition.HealthAmmunitionItem
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter.Items.Ammunition
{
  internal class HealthAmmunitionItem : AmmunitionItem
  {
    private float _volume;

    public float Volume
    {
      get => this._volume;
      set
      {
        this._volume = value;
        if ((double) this._volume > 0.0)
          return;
        Gamer.Instance.HealthBonus.Item = (HealthAmmunitionItem) null;
      }
    }

    public override string Id
    {
      get => this.Volume.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public HealthAmmunitionItem()
    {
      this.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemHealth",
        OnCopterTexture = "Hangar/Amunition color/health"
      };
    }
  }
}
