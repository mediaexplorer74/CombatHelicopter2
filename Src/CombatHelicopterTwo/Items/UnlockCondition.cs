// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.UnlockCondition
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.AchievementsSystem;

#nullable disable
namespace Helicopter.Items
{
  internal class UnlockCondition
  {
    public Achievement UnlockAchievement { get; set; }

    public int Price { get; set; }

    public bool IsBought
    {
      get => this.UnlockAchievement.Achieved;
      set => this.UnlockAchievement.Achieved = value;
    }

    public bool Unlocked => this.UnlockAchievement.Achieved;

    public UnlockCondition() => this.UnlockAchievement = new Achievement();

    public bool CheckConditions(Gamer gamer) => true;
  }
}
