// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Patterns.BulletPattern
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Primitives;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Helicopter.Model.WorldObjects.Patterns
{
  public class BulletPattern : Pattern
  {
    public static BulletPattern CreateInstance(
      string texturePath,
      Rectangle frameRectangle,
      Point offset,
      Contour contour)
    {
      BulletPattern instance = new BulletPattern();
      instance.Sprites = new List<SpriteDescription>()
      {
        new SpriteDescription()
        {
          Offset = offset,
          TexturePath = texturePath,
          FrameRate = 0.0f,
          FrameRectangle = contour.Rectangle,
          SpriteId = IdFactory.Instance.GetId().ToString((IFormatProvider) CultureInfo.InvariantCulture)
        }
      };
      instance.Contour = contour;
      return instance;
    }
  }
}
