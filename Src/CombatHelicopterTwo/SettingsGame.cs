// Decompiled with JetBrains decompiler
// Type: Helicopter.SettingsGame
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.Sounds;
using System;
using System.Globalization;
using System.Xml.Linq;
using Windows.Storage;

#nullable disable
namespace Helicopter
{
  internal class SettingsGame
  {
    public const bool SHOW_BANNER = true;
    public const string APPLICATION_ID = "8e8908ac-2fee-4c35-8388-0e505db6fd85";
    public const string UNIT_ID = "77542";
    public const string GAME_ID = "d1bc1b13-9737-40c4-aff2-234e26f9185b";
    public const string GAME_SECRET = "LZ+YL+jNqQEzCv0zTmyIDIKd9/jtAEYZIz57R1SB8OmctFy/+U35vQ==";
    public const string GAME_CURRENCY_CODE = "AQP";
    public static int LaunchNumber;
    public static bool IsRateIted;
    public static DateTime LastDailyBonusDate = DateTime.MinValue;
    public static int LastDailyBonus = 1;
    public static DateTime CurrentTime;
    public static bool NeedMapTutorial = true;
    public static bool NeedChallengeTutorial = true;
    public static bool NeedAfter22Tutorial;
    private static bool _sound;

    public static bool Sound
    {
      get => SettingsGame._sound;
      set
      {
        SettingsGame._sound = value;
        Audio.Instance.EnableSound = value;
      }
    }

    public static bool Vibro { get; set; }

    static SettingsGame()
    {
      SettingsGame.Sound = true;
      SettingsGame.Vibro = true;
    }

    public static void Deserialize(XElement xElement)
    {
      SettingsGame.LaunchNumber = int.Parse(xElement.Element((XName) "LaunchNumber").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      SettingsGame.IsRateIted = bool.Parse(xElement.Element((XName) "IsRateIted").Value);
      SettingsGame.LastDailyBonusDate = DateTime.Parse(xElement.Element((XName) "LastDailyBonusDate").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      SettingsGame.LastDailyBonus = int.Parse(xElement.Element((XName) "LastDailyBonus").Value, (IFormatProvider) CultureInfo.InvariantCulture);
      SettingsGame.Sound = bool.Parse(xElement.Element((XName) "Sound").Value);
      SettingsGame.Vibro = bool.Parse(xElement.Element((XName) "Vibro").Value);
      SettingsGame.NeedChallengeTutorial = bool.Parse(xElement.Element((XName) "NeedChallengeTutorial").Value);
      SettingsGame.NeedMapTutorial = bool.Parse(xElement.Element((XName) "NeedMapTutorial").Value);
      SettingsGame.NeedAfter22Tutorial = bool.Parse(xElement.Element((XName) "NeedAfter22Tutorial").Value);
    }

    public static XElement Serialize()
    {
      XElement xelement = new XElement((XName) "Settings");
      xelement.Add((object) new XElement((XName) "LaunchNumber", (object) SettingsGame.LaunchNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "IsRateIted", (object) SettingsGame.IsRateIted.ToString()));
      xelement.Add((object) new XElement((XName) "NeedMapTutorial", (object) SettingsGame.NeedMapTutorial.ToString()));
      xelement.Add((object) new XElement((XName) "NeedChallengeTutorial", (object) SettingsGame.NeedChallengeTutorial.ToString()));
      xelement.Add((object) new XElement((XName) "NeedAfter22Tutorial", (object) SettingsGame.NeedAfter22Tutorial.ToString()));
      xelement.Add((object) new XElement((XName) "IsRateIted", (object) SettingsGame.IsRateIted.ToString()));
      xelement.Add((object) new XElement((XName) "LastDailyBonusDate", (object) SettingsGame.LastDailyBonusDate.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "LastDailyBonus", (object) SettingsGame.LastDailyBonus.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      xelement.Add((object) new XElement((XName) "Sound", (object) SettingsGame.Sound.ToString()));
      xelement.Add((object) new XElement((XName) "Vibro", (object) SettingsGame.Vibro.ToString()));
      return xelement;
    }

    public static void Load()
    {
      var settings = ApplicationData.Current.LocalSettings;
      if (!settings.Values.ContainsKey("Settings"))
        return;
      var xml = settings.Values["Settings"]?.ToString();
      if (string.IsNullOrEmpty(xml))
        return;
      SettingsGame.Deserialize(XElement.Parse(xml));
    }

    public static void Save()
    {
      var settings = ApplicationData.Current.LocalSettings;
      settings.Values["Settings"] = SettingsGame.Serialize().ToString();
    }
  }
}
