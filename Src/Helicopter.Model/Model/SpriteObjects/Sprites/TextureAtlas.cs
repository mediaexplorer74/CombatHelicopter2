// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.Sprites.TextureAtlas
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects.Sprites
{
  public class TextureAtlas
  {
    public string ImagePath { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public float FullTime { get; set; }

    public List<TextureAtlasPart> Parts { get; set; }

    public TextureAtlas() => this.Parts = new List<TextureAtlasPart>();
  }
}
