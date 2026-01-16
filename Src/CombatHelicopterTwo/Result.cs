// Modified by MediaExplorer (2026)
// Type: Helicopter.Result
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

#nullable disable
namespace Helicopter
{
  public struct Result(int rank, string name, double points, double diamonds, string country)
  {
    public string Country = country;
    public double Diamonds = diamonds;
    public string Name = name;
    public double Points = points;
    public int Rank = rank;
  }
}
