// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Background.BackgroundLayer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Background
{
  public class BackgroundLayer
  {
    public Vector2 Position;

    public float Remoteness { get; set; }

    public string TexturePath { get; set; }

    public static int Comparer(BackgroundLayer x, BackgroundLayer y)
    {
      return y.Remoteness.CompareTo(x.Remoteness);
    }
  }
}
