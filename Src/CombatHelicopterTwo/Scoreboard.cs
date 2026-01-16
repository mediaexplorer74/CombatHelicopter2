// Decompiled with JetBrains decompiler
// Type: Helicopter.Scoreboard
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;
using Windows.Storage;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter
{
  internal class Scoreboard
  {
    public const string HighScoresKey = "HeightScores";
    private static Scoreboard _instance;

    public static Scoreboard Instance
    {
      get => Scoreboard._instance ?? (Scoreboard._instance = new Scoreboard());
    }

    public Scoreboard()
    {
    }

    public int GetHighScores()
    {
      var settings = ApplicationData.Current.LocalSettings;
      if (settings.Values.TryGetValue(HighScoresKey, out object value))
      {
        int result;
        if (int.TryParse(value?.ToString(), out result))
          return result;
      }
      return 0;
    }

    public void SendScore(float points, Rank rank)
    {
      this.SetHighScores((int) points);
    }

    public void SetHighScores(int scores)
    {
      if (this.GetHighScores() >= scores)
        return;
      var settings = ApplicationData.Current.LocalSettings;
      settings.Values[HighScoresKey] = scores.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
