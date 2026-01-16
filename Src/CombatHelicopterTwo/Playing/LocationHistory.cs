// Modified by MediaExplorer (2026)
// Type: Helicopter.Playing.LocationHistory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Helicopter.Playing
{
  public class LocationHistory
  {
    public bool IsFullCompleted
    {
      get
      {
        return this.FirstEpisode.IsCompleted && this.SecondEpisode.IsCompleted && this.ThirdEpisode.IsCompleted;
      }
    }

    public LocationHistory()
    {
      this.FirstEpisode = new EpisodeHistory();
      this.SecondEpisode = new EpisodeHistory();
      this.ThirdEpisode = new EpisodeHistory();
    }

    public EpisodeHistory FirstEpisode { get; private set; }

    public EpisodeHistory SecondEpisode { get; private set; }

    public EpisodeHistory ThirdEpisode { get; private set; }

    public bool IsUnlocked => this.FirstEpisode.IsAvailiable;

    public XElement Serialize()
    {
      XElement xelement = new XElement((XName) SerializationIDs.LocationHistory);
      xelement.Add((object) this.FirstEpisode.Serialize());
      xelement.Add((object) this.SecondEpisode.Serialize());
      xelement.Add((object) this.ThirdEpisode.Serialize());
      return xelement;
    }

    public void Deserialize(XElement element)
    {
      int num = 0;
      foreach (XElement element1 in element.Elements())
      {
        switch (num)
        {
          case 0:
            this.FirstEpisode.Deserialize(element1);
            break;
          case 1:
            this.SecondEpisode.Deserialize(element1);
            break;
          case 2:
            this.ThirdEpisode.Deserialize(element1);
            break;
        }
        ++num;
      }
    }

    public EpisodeHistory GetEpisodeHistory(int episodeNumber)
    {
      if (episodeNumber == 1)
        return this.FirstEpisode;
      if (episodeNumber == 2)
        return this.SecondEpisode;
      if (episodeNumber == 3)
        return this.ThirdEpisode;
      throw new Exception(string.Format("Unknown episode number '{0}'", (object) episodeNumber));
    }
  }
}
