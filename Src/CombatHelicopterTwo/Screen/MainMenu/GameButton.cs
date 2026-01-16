// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.MainMenu.GameButton
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter.Screen.MainMenu
{
  internal class GameButton : BasicControl
  {
    public event EventHandler<EventArgs> Clicked;

    public Sprite IconTexture { get; set; }

    public string GameName { get; set; }

    public bool AllowBonus { get; set; }

    public int Bonus { get; set; }

    public PromoGame GameType { get; set; }

    public GameButton()
    {
      Sprite sprite = ResourcesManager.Instance.GetSprite("MoreGames/itemBg");
      MenuControl child = new MenuControl(sprite, sprite, Vector2.Zero);
      child.Clicked += new EventHandler<EventArgs>(this.OnClicked);
      this.AddChild((BasicControl) child);
    }

    private void OnClicked(object sender, EventArgs e) => this.InvokeClicked(EventArgs.Empty);

    public void InvokeClicked(EventArgs e)
    {
      EventHandler<EventArgs> clicked = this.Clicked;
      if (clicked == null)
        return;
      clicked((object) this, e);
    }

    public void Init()
    {
      this.AddChild((BasicControl) new TexturedControl(this.IconTexture, new Vector2(15f, 15f)));
      TextControl child1 = new TextControl(this.GameName, ResourcesManager.Instance.GetResource<SpriteFont>("fonts/Segoe18"));
      child1.CenteredX = true;
      child1.MaxSymbolsPerLine = 15;
      child1.RebuildLines();
      child1.Position = new Vector2((float) ((162.0 - (double) child1.Size.X) / 2.0), 145f);
      this.AddChild((BasicControl) child1);
      Vector2 position = new Vector2(41f, 200f);
      TexturedControl child2 = new TexturedControl(ResourcesManager.Instance.GetSprite("MoreGames/credits"), position);
      if (!this.AllowBonus)
        child2.Color = Color.Gray;
      this.AddChild((BasicControl) child2);
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition15");
      TextControl child3 = new TextControl(string.Format("+ {0}", (object) this.Bonus.ToString("0 000", (IFormatProvider) CultureInfo.InvariantCulture)), resource)
      {
        Color = this.AllowBonus ? new Color(254, 242, 23) : new Color(132, 109, 25)
      };
      child3.Position = new Vector2((float) ((162.0 - (double) child3.Size.X) / 2.0), 275f);
      this.AddChild((BasicControl) child3);
    }
  }
}
