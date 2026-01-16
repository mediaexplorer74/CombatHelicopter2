// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.MainMenu.MoreGamesSettings
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

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
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      MoreGamesSettings moreGamesSettings1 = new MoreGamesSettings();
      if (!storeForApplication.FileExists("MoreGamesSettings.xml"))
        return moreGamesSettings1;
      IsolatedStorageFileStream storageFileStream = (IsolatedStorageFileStream) null;
      StreamReader streamReader = (StreamReader) null;
      try
      {
        storageFileStream = storeForApplication.OpenFile("MoreGamesSettings.xml", FileMode.Open, FileAccess.Read);
        streamReader = new StreamReader((Stream) storageFileStream);
        MoreGamesSettings moreGamesSettings2 = new MoreGamesSettings();
        XElement root = XDocument.Parse(streamReader.ReadToEnd()).Root;
        if (root != null)
        {
          XElement xelement1 = root.Element((XName) "AllowJevelGodBonus");
          if (xelement1 != null)
            moreGamesSettings2.AllowJevelGodBonus = bool.Parse(xelement1.Value);
          XElement xelement2 = root.Element((XName) "AllowBubbleBurstBonus");
          if (xelement2 != null)
            moreGamesSettings2.AllowBubbleBurstBonus = bool.Parse(xelement2.Value);
          XElement xelement3 = root.Element((XName) "AllowCombatHelicopterBonus");
          if (xelement3 != null)
            moreGamesSettings2.AllowCombatHelicopterBonus = bool.Parse(xelement3.Value);
          XElement xelement4 = root.Element((XName) "AllowJevelLinesBonus");
          if (xelement4 != null)
            moreGamesSettings2.AllowJevelLinesBonus = bool.Parse(xelement4.Value);
        }
        return moreGamesSettings2;
      }
      catch (Exception ex)
      {
        return moreGamesSettings1;
      }
      finally
      {
        streamReader?.Close();
        storageFileStream?.Close();
      }
    }

    public void Save()
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      IsolatedStorageFileStream storageFileStream = (IsolatedStorageFileStream) null;
      try
      {
        if (storeForApplication.FileExists("MoreGamesSettings.xml"))
          storeForApplication.DeleteFile("MoreGamesSettings.xml");
        storageFileStream = storeForApplication.OpenFile("MoreGamesSettings.xml", FileMode.Create, FileAccess.Write);
        XDocument xdocument = new XDocument();
        XElement content = new XElement((XName) "Root");
        content.Add((object) new XElement((XName) "AllowJevelGodBonus", (object) this.AllowJevelGodBonus.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        content.Add((object) new XElement((XName) "AllowBubbleBurstBonus", (object) this.AllowBubbleBurstBonus.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        content.Add((object) new XElement((XName) "AllowCombatHelicopterBonus", (object) this.AllowCombatHelicopterBonus.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        content.Add((object) new XElement((XName) "AllowJevelLinesBonus", (object) this.AllowJevelLinesBonus.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        xdocument.Add((object) content);
        xdocument.Save((Stream) storageFileStream);
      }
      catch (Exception ex)
      {
      }
      finally
      {
        storageFileStream?.Close();
      }
    }
  }
}
