// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.SpriteObjectComparer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class SpriteObjectComparer : IComparer<SpriteObject>
  {
    public int Compare(SpriteObject x, SpriteObject y)
    {
      int num = x.Instance.ZIndex.CompareTo(y.Instance.ZIndex);
      return num != 0 ? num : x.Instance.Id.CompareTo(y.Instance.Id);
    }
  }
}
