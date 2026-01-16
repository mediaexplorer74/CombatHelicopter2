// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.ISpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  public interface ISpriteObject
  {
    List<ISpriteObject> Children { get; set; }

    Sprite Sprite { get; set; }

    string SpriteID { get; set; }

    float Rotation { get; set; }

    float ZIndex { get; set; }

    bool Visible { get; set; }

    void Draw(SpriteBatch spriteBatch, Vector2 parentPosition);

    void Update(Camera camera, float elapsedSeconds);

    void Release();
  }
}
