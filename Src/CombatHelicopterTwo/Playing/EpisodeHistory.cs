// Modified by MediaExplorer (2026)
// Type: Helicopter.Playing.EpisodeHistory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System.Xml.Linq;

#nullable disable
namespace Helicopter.Playing
{
  public class EpisodeHistory
  {
    public bool IsAvailiable { get; set; }

    public bool IsCompleted { get; set; }

    public int Stars { get; set; }

    public XElement Serialize()
    {
      XElement xelement = new XElement((XName) SerializationIDs.EpisodeHistory);
      xelement.Add((object) new XElement((XName) SerializationIDs.IsAvailiable, (object) this.IsAvailiable));
      xelement.Add((object) new XElement((XName) SerializationIDs.IsCompleted, (object) this.IsCompleted));
      xelement.Add((object) new XElement((XName) SerializationIDs.Stars, (object) this.Stars));
      return xelement;
    }

    public void Deserialize(XElement element)
    {
      XElement xelement1 = element.Element((XName) SerializationIDs.IsAvailiable);
      if (xelement1 != null)
        this.IsAvailiable = bool.Parse(xelement1.Value);
      XElement xelement2 = element.Element((XName) SerializationIDs.IsCompleted);
      if (xelement2 != null)
        this.IsCompleted = bool.Parse(xelement2.Value);
      XElement xelement3 = element.Element((XName) SerializationIDs.Stars);
      if (xelement3 == null)
        return;
      this.Stars = int.Parse(xelement3.Value);
    }
  }
}
