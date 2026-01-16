// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.ItemFixedStepHorizontalScrollPanel
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.Ammunition;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class ItemFixedStepHorizontalScrollPanel : FixedStepHorizontalScrollPanel
  {
    public Helicopter.Items.Item CurrentItem
    {
      get => ((ItemTexturedControl) this.CurrentChildren.Children[0]).Item;
      set
      {
        for (int index = 0; index < this.Children.Count; ++index)
        {
          if (((ItemTexturedControl) this.Children[index].Children[0]).Item == value)
          {
            this.InstantNavigateToItem(index);
            break;
          }
        }
      }
    }

    public Helicopter.Items.Item NextItem => this.NextControl.Item;

    public ItemTexturedControl CurrentControl
    {
      get => (ItemTexturedControl) this.CurrentChildren.Children[0];
    }

    public ItemTexturedControl NextControl => (ItemTexturedControl) this.NextChildren.Children[0];

    public override void Draw(DrawContext context) => base.Draw(context);

    public void AddElement(
      Helicopter.Items.Item item,
      SpriteFont fontHeader,
      SpriteFont fontDescription,
      Color color)
    {
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/purchased");
      Sprite sprite2;
      Sprite sprite3;
      if (item is AmmunitionItem)
      {
        sprite2 = (Sprite) null;
        sprite3 = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).OnShopTexture);
      }
      else
      {
        sprite2 = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).OnShopTexture);
        sprite3 = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).OnShopBoughtTexture);
      }
      float y = 5f;
      BasicControl child = new BasicControl();
      child.Size = new Vector2(480f, 146f);
      child.Position = new Vector2(this.Size.X, 0.0f);
      child.AddChild((BasicControl) new ItemTexturedControl(item, sprite3, new Vector2((float) ((double) child.Size.X / 2.0 - (double) sprite3.Bounds.Width / 2.0), (float) ((double) child.Size.Y / 2.0 - (double) sprite3.Bounds.Height / 2.0)))
      {
        Installed = sprite1,
        Locked = sprite2
      });
      child.AddChild((BasicControl) new TextControl(item.Name.ToLowerInvariant(), fontHeader, color, new Vector2((float) ((double) child.Size.X / 2.0 - (double) fontHeader.MeasureString(item.Name).X / 2.0), y)));
      child.AddChild((BasicControl) new TextControl(item.Description.ToLowerInvariant(), fontDescription, color, new Vector2(child.Size.X / 2f, child.Size.Y - fontDescription.MeasureString(item.Description).Y - y))
      {
        MaxSymbolsPerLine = 35,
        Origin = new Vector2(0.5f, 0.0f)
      });
      this.AddChild(child);
    }
  }
}
