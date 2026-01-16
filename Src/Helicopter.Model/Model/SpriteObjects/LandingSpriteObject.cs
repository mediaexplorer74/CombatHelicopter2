// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.LandingSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class LandingSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<LandingSpriteObject> _pool = new ObjectPool<LandingSpriteObject>((ICreation<LandingSpriteObject>) new LandingSpriteObject.Creator());
    private static readonly Rectangle _blockRectangle = new Rectangle(0, 0, 160, 200);
    private static readonly Rectangle _labelRectangle = new Rectangle(0, 0, 102, 96);
    private static readonly Rectangle _lampDestSize = new Rectangle(0, 0, 64, 64);
    private static readonly Rectangle _lampFrameSize = new Rectangle(0, 0, 64, 64);

    public static LandingSpriteObject GetInstance() => LandingSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => LandingSpriteObject._pool.Release(this);

    protected LandingSpriteObject()
    {
    }

    public void Init(LandingElementInstance instance)
    {
      this.Init((Instance) instance);
      bool flag = instance.Pattern.Alignment == VerticalAlignment.Top;
      this.ZIndex = (float) (int) ((double) instance.ZIndex * 10.0);
      switch (instance.Pattern.ElementType)
      {
        case LandingElementType.StartBlock:
          this.Sprite = this.GetBlockSprite(flag ? "GameWorld/Objects/Transition/TunelElementA_VF" : "GameWorld/Objects/Transition/TunelElementA");
          this.AddAnimation(6, flag ? 148 : -10);
          break;
        case LandingElementType.MediumBlock:
          this.Sprite = this.GetBlockSprite(flag ? "GameWorld/Objects/Transition/TunelElementB_VF" : "GameWorld/Objects/Transition/TunelElementB");
          if (!instance.HasLamp)
            break;
          this.AddAnimation(22, flag ? 133 : 5);
          break;
        case LandingElementType.EndBlock:
          this.Sprite = this.GetBlockSprite(flag ? "GameWorld/Objects/Transition/TunelElementA_HF_VF" : "GameWorld/Objects/Transition/TunelElementA_HF");
          this.AddAnimation(91, flag ? 148 : -10);
          break;
        case LandingElementType.StartShield:
          this.Sprite = this.GetBlockSprite(flag ? "GameWorld/Objects/Transition/TunelBgA_VF" : "GameWorld/Objects/Transition/TunelBgA");
          break;
        case LandingElementType.MediumShield:
          this.Sprite = this.GetBlockSprite(flag ? "GameWorld/Objects/Transition/TunelBgB_VF" : "GameWorld/Objects/Transition/TunelBgB");
          break;
        case LandingElementType.EndShield:
          this.Sprite = this.GetBlockSprite(flag ? "GameWorld/Objects/Transition/TunelBgA_HF_VF" : "GameWorld/Objects/Transition/TunelBgA_HF");
          break;
        case LandingElementType.Label:
          this.Sprite = ResourcesManager.Instance.GetSprite("GameWorld/Objects/Transition/landingZone");
          break;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Unknown Element type '{0}'", (object) instance.Pattern.ElementType));
      }
    }

    private void AddAnimation(int x, int y)
    {
      SimpleSpriteObject instance1 = SimpleSpriteObject.GetInstance();
      AnimatedSprite instance2 = AnimatedSprite.GetInstance();
      Sprite sprite = ResourcesManager.Instance.GetSprite("GameWorld/Objects/Transition/RedLight");
      instance2.Init(sprite, LandingSpriteObject._lampDestSize, LandingSpriteObject._lampFrameSize, 0.5f, Vector2.Zero, true);
      instance1.Sprite = (Sprite) instance2;
      instance1.ZIndex = this.ZIndex + 1f;
      instance1.Offset.X = (float) x;
      instance1.Offset.Y = (float) y;
      this.AddChildren((ISpriteObject) instance1);
    }

    private Sprite GetBlockSprite(string textureName)
    {
      return ResourcesManager.Instance.GetSprite(textureName);
    }

    protected class Creator : ICreation<LandingSpriteObject>
    {
      public LandingSpriteObject Create() => new LandingSpriteObject();
    }
  }
}
