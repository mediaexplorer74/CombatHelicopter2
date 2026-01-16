// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.ResourcesManager
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Descriptions;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace Helicopter.Model.Common
{
  public class ResourcesManager
  {
    private static ResourcesManager _instance;
    private readonly Dictionary<string, object> _allResources;
    private ContentManager _content;
    public static Sprite BlankSprite;
    private List<string> _textureList = new List<string>();

    public static ResourcesManager Instance => ResourcesManager._instance;

    private ResourcesManager() => this._allResources = new Dictionary<string, object>();

    public static void CreateInstance(ContentManager content)
    {
      if (ResourcesManager._instance != null)
        return;
      ResourcesManager._instance = new ResourcesManager()
      {
        _content = content
      };
      ResourcesManager.BlankSprite = Sprite.GetInstance(ResourcesManager.Instance.GetResource<Texture2D>("blank"));
      ResourcesManager.BlankSprite.Scale = new Vector2(100f);
      ResourcesManager._instance.CreateListOfAllTexture();
    }

    public T GetResource<T>(string name)
    {
      if (this._allResources.ContainsKey(name))
        return (T) this._allResources[name];
      T resource = this._content.Load<T>(name);
      Type type1 = typeof (T);
      Type type2 = typeof (TextureAtlas);
      this._allResources.Add(name, (object) resource);
      return resource;
    }

    public Sprite GetSprite(string texturePath)
    {
      if (this._allResources.ContainsKey(texturePath))
      {
        Sprite instance = Sprite.GetInstance();
        instance.Init(this.GetResource<Texture2D>(texturePath));
        return instance;
      }
      if (this._textureList.Contains(texturePath))
      {
        Sprite instance = Sprite.GetInstance();
        instance.Init(this.GetResource<Texture2D>(texturePath));
        return instance;
      }
      if (!TextureAtlasManager.IsLoaded(texturePath))
      {
        List<string> resource = this.GetResource<List<string>>("assets");
        string str1 = ((IEnumerable<string>) texturePath.Split(new char[1]
        {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries)).First<string>();
        foreach (string str2 in resource)
        {
          if (str2.Contains("TextureAtlas") && str2.StartsWith(str1))
            TextureAtlasManager.ParsePackedTexture(this.GetResource<TextureAtlas>(((IEnumerable<string>) str2.Split(new string[1]
            {
              "___________"
            }, StringSplitOptions.RemoveEmptyEntries)).First<string>()));
        }
      }
      return TextureAtlasManager.IsLoaded(texturePath) ? TextureAtlasManager.GetSprite(texturePath) : throw new FileNotFoundException(string.Format("Can't find spritesheet which contains file: {0}", (object) texturePath));
    }

    public void CreateListOfAllTexture()
    {
      List<string> resource = this.GetResource<List<string>>("assets");
      for (int index = 0; index < resource.Count; ++index)
      {
        string[] strArray = resource[index].Split(new string[1]
        {
          "___________"
        }, StringSplitOptions.RemoveEmptyEntries);
        string str = strArray[0];
        if (strArray[1] == "Texture2D")
          this._textureList.Add(str);
      }
    }

    public void LoadFromFolder(string folder)
    {
      List<string> resource = this.GetResource<List<string>>("assets");
      for (int index = 0; index < resource.Count; ++index)
      {
        string str = resource[index];
        if (str.StartsWith(folder))
        {
          string[] strArray = str.Split(new string[1]
          {
            "___________"
          }, StringSplitOptions.RemoveEmptyEntries);
          string name = strArray[0];
          if (strArray.Length > 1)
          {
            switch (strArray[1])
            {
              case "Texture2D":
                this.GetResource<Texture2D>(name);
                continue;
              case "LevelProcessor":
                this.GetResource<LevelDesc>(name);
                continue;
              case "Font":
                this.GetResource<SpriteFont>(name);
                continue;
              case "SoundEffectProcessor":
                this.GetResource<SoundEffect>(name);
                continue;
              case "TextureAtlas":
                TextureAtlasManager.ParsePackedTexture(this.GetResource<TextureAtlas>(name));
                continue;
              default:
                continue;
            }
          }
        }
      }
    }
  }
}
