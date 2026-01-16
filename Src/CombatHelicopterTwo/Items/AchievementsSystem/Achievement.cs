// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.AchievementsSystem.Achievement
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter.Items.AchievementsSystem
{
  internal class Achievement
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public string Texture { get; set; }

    public bool Achieved { get; set; }

    public bool Showable { get; set; }

    public int MoneyAward { get; set; }

    public Achievement() => this.Showable = true;
  }
}
