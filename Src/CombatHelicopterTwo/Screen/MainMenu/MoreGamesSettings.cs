// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MainMenu.MoreGamesSettings
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Windows.Storage;

#nullable disable
namespace Helicopter.Screen.MainMenu
{
  public class MoreGamesSettings
  {
    private const string Filename = "MoreGamesSettings.xml";

    public bool AllowJevelGodBonus { get; set; }

    public bool AllowBubbleBurstBonus { get; set; }

    public bool AllowCombatHelicopterBonus { get; set; }

    public bool AllowJevelLinesBonus { get; set; }

    public MoreGamesSettings()
    {
      this.AllowJevelGodBonus = true;
      this.AllowBubbleBurstBonus = true;
      this.AllowCombatHelicopterBonus = true;
      this.AllowJevelLinesBonus = true;
    }

    public static MoreGamesSettings Load()
    {
      MoreGamesSettings defaults = new MoreGamesSettings();
      try
      {
        StorageFile file = ApplicationData.Current.LocalFolder.GetFileAsync(Filename).AsTask().GetAwaiter().GetResult();
        string content = FileIO.ReadTextAsync(file).AsTask().GetAwaiter().GetResult();
        MoreGamesSettings result = new MoreGamesSettings();
        XElement root = XDocument.Parse(content).Root;
        if (root != null)
        {
          XElement xelement1 = root.Element((XName) "AllowJevelGodBonus");
          if (xelement1 != null)
            result.AllowJevelGodBonus = bool.Parse(xelement1.Value);
          XElement xelement2 = root.Element((XName) "AllowBubbleBurstBonus");
          if (xelement2 != null)
            result.AllowBubbleBurstBonus = bool.Parse(xelement2.Value);
          XElement xelement3 = root.Element((XName) "AllowCombatHelicopterBonus");
          if (xelement3 != null)
            result.AllowCombatHelicopterBonus = bool.Parse(xelement3.Value);
          XElement xelement4 = root.Element((XName) "AllowJevelLinesBonus");
          if (xelement4 != null)
            result.AllowJevelLinesBonus = bool.Parse(xelement4.Value);
        }
        return result;
      }
      catch
      {
        return defaults;
      }
    }

    public void Save()
    {
      try
      {
        XDocument xdocument = new XDocument();
        XElement content = new XElement((XName) "Root");
        content.Add((object) new XElement((XName) "AllowJevelGodBonus", (object) this.AllowJevelGodBonus.ToString()));
        content.Add((object) new XElement((XName) "AllowBubbleBurstBonus", (object) this.AllowBubbleBurstBonus.ToString()));
        content.Add((object) new XElement((XName) "AllowCombatHelicopterBonus", (object) this.AllowCombatHelicopterBonus.ToString()));
        content.Add((object) new XElement((XName) "AllowJevelLinesBonus", (object) this.AllowJevelLinesBonus.ToString()));
        xdocument.Add((object) content);
        string xml = xdocument.ToString();
        StorageFile file = ApplicationData.Current.LocalFolder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting).AsTask().GetAwaiter().GetResult();
        FileIO.WriteTextAsync(file, xml).AsTask().GetAwaiter().GetResult();
      }
      catch
      {
      }
    }
  }
}
