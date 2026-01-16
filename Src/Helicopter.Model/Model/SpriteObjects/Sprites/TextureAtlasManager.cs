// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.Sprites.TextureAtlasManager
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects.Sprites
{
  internal class TextureAtlasManager
  {
    private static readonly Dictionary<string, TextureAtlasManager.PartDesc> _loadedPackedTextures = new Dictionary<string, TextureAtlasManager.PartDesc>();

    public static void ParsePackedTexture(TextureAtlas atlas)
    {
      foreach (TextureAtlasPart part in atlas.Parts)
        TextureAtlasManager._loadedPackedTextures[part.FullName] = new TextureAtlasManager.PartDesc()
        {
          SourceRectangle = part.Rectangle,
          TextureName = atlas.ImagePath
        };
    }

    public static bool IsLoaded(string texturePath)
    {
      return TextureAtlasManager._loadedPackedTextures.ContainsKey(texturePath);
    }

    public static Sprite GetSprite(string texturePath)
    {
      Sprite instance = Sprite.GetInstance();
      Texture2D resource = ResourcesManager.Instance.GetResource<Texture2D>(TextureAtlasManager._loadedPackedTextures[texturePath].TextureName);
      instance.Init(resource, TextureAtlasManager._loadedPackedTextures[texturePath].SourceRectangle);
      return instance;
    }

    private struct PartDesc
    {
      public string TextureName;
      public Rectangle SourceRectangle;
    }
  }
}
