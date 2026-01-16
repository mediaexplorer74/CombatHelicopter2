// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Descriptions.LevelDesc
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.Descriptions
{
  public class LevelDesc
  {
    public IList<BackgroundDesc> Backgrounds { get; set; }

    public IList<PatternDesc> Patterns { get; set; }

    public IList<InstanceDesc> Instances { get; set; }

    public int Length { get; set; }

    public PatternDesc HeroDesc { get; set; }

    public EpisodeMode Mode { get; set; }

    public WorldType WorldType { get; set; }
  }
}
