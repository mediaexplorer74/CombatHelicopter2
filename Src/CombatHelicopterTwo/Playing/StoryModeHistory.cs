// Decompiled with JetBrains decompiler
// Type: Helicopter.Playing.StoryModeHistory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.WorldObjects;
using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Helicopter.Playing
{
  public class StoryModeHistory
  {
    public LocationHistory[] LocationHistories;

    public int TotalStars
    {
      get
      {
        return ((IEnumerable<LocationHistory>) this.LocationHistories).Sum<LocationHistory>((Func<LocationHistory, int>) (x => x.FirstEpisode.Stars + x.SecondEpisode.Stars + x.ThirdEpisode.Stars));
      }
    }

    public StoryModeHistory()
    {
      this.CreateEmptyHistory();
      this.Load();
    }

    public EpisodeHistory GetEpisodeHistory(WorldType locayion, int episode)
    {
      return this.LocationHistories[(int) (locayion - 1)].GetEpisodeHistory(episode);
    }

    public void CompleteEpisode(WorldType location, int episode, int stars)
    {
      int index = (int) (location - 1);
      LocationHistory locationHistory = this.LocationHistories[index];
      if (episode == 1)
      {
        locationHistory.FirstEpisode.IsCompleted = true;
        locationHistory.FirstEpisode.Stars = Math.Max(locationHistory.FirstEpisode.Stars, stars);
        locationHistory.SecondEpisode.IsAvailiable = true;
      }
      if (episode == 2)
      {
        locationHistory.SecondEpisode.IsCompleted = true;
        locationHistory.SecondEpisode.Stars = Math.Max(locationHistory.SecondEpisode.Stars, stars);
        if (location != WorldType.EnemyBase)
          this.LocationHistories[index + 1].FirstEpisode.IsAvailiable = true;
        else
          locationHistory.ThirdEpisode.IsAvailiable = true;
      }
      Gamer.Instance.UpdateRank(this.TotalStars);
      this.Save();
      if (episode != 3)
        return;
      locationHistory.ThirdEpisode.IsCompleted = true;
      locationHistory.ThirdEpisode.Stars = Math.Max(locationHistory.ThirdEpisode.Stars, stars);
      if (location == WorldType.EnemyBase)
        return;
      this.LocationHistories[index + 1].FirstEpisode.IsAvailiable = true;
    }

    private void CreateEmptyHistory()
    {
      this.LocationHistories = new LocationHistory[5]
      {
        new LocationHistory(),
        new LocationHistory(),
        new LocationHistory(),
        new LocationHistory(),
        new LocationHistory()
      };
      this.LocationHistories[0].FirstEpisode.IsAvailiable = true;
    }

    private void Deserialize(XElement element)
    {
      int index = 0;
      foreach (XElement element1 in element.Elements())
      {
        this.LocationHistories[index].Deserialize(element1);
        ++index;
      }
    }

    public bool IsUnlocked(int levelNumber) => this.LocationHistories[levelNumber - 1].IsUnlocked;

    private XElement Serialize()
    {
      XElement xelement = new XElement((XName) SerializationIDs.StoryModeHistory);
      foreach (LocationHistory locationHistory in this.LocationHistories)
        xelement.Add((object) locationHistory.Serialize());
      return xelement;
    }

    private void Save()
    {
      var settings = ApplicationData.Current.LocalSettings;
      settings.Values[SerializationIDs.StoryModeHistory] = this.Serialize().ToString();
    }

    private void Load()
    {
      var settings = ApplicationData.Current.LocalSettings;
      if (!settings.Values.ContainsKey(SerializationIDs.StoryModeHistory))
        return;
      var xml = settings.Values[SerializationIDs.StoryModeHistory]?.ToString();
      if (string.IsNullOrEmpty(xml))
        return;
      this.Deserialize(XElement.Parse(xml));
    }
  }
}
