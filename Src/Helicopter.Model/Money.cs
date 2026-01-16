// Modified by MediaExplorer (2026)
// Type: Helicopter.Money
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter
{
  public class Money
  {
    private float _currentMoney;

    private float CurrentMoney
    {
      get => this._currentMoney;
      set
      {
        this._currentMoney = MathHelper.Clamp(value, 0.0f, float.MaxValue);
        this.OnMoneyChanged();
      }
    }

    public int Count => (int) this.CurrentMoney;

    public void AddMoney(float count) => this.CurrentMoney += count;

    public bool TryPaidMoney(float price)
    {
      if ((double) this.CurrentMoney < (double) price - 1.4012984643248171E-45)
        return false;
      this.CurrentMoney -= price;
      return true;
    }

    public event EventHandler MoneyChanged;

    public void OnMoneyChanged()
    {
      if (this.MoneyChanged == null)
        return;
      this.MoneyChanged((object) this, EventArgs.Empty);
    }

    public string Serialize()
    {
      return this._currentMoney.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public static Money Deserialize(string value)
    {
      return new Money()
      {
        _currentMoney = float.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture)
      };
    }

    public void SetDebugMoney(float money) => this._currentMoney = money;
  }
}
