// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.GameplayPopups.NewAchievementPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items.AchievementsSystem;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class NewAchievementPopup : BasePopup
  {
    private Achievement _achievement;
    private BasicControl _root;
    private Rectangle _infoRect = new Rectangle(171, 220, 457, 123);
    private string _bgTexturePath;

    public NewAchievementPopup()
    {
      this.IsPopup = true;
      this.Back += new EventHandler(this.OnBack);
    }

    public void Init(Achievement achievement)
    {
      this._achievement = achievement;
      this._bgTexturePath = "PopUpWindow/achivePopUpBg";
    }

    public override void Draw(DrawContext drawContext)
    {
      if (!this.OnTop)
        return;
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    private void OnBack(object sender, EventArgs e) => this.ExitScreen();

    private void OnOkButtonClicked(object sender, EventArgs e) => this.ExitScreen();

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      this._root.HandleInput(input);
    }

    public override void LoadContent()
    {
      this._root = new BasicControl();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite(this._bgTexturePath);
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2), (float) (245 - sprite1.Bounds.Height / 2))));
      BasicControl child1 = new BasicControl();
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butOk"), ResourcesManager.Instance.GetSprite("PopUpWindow/butOkSelect"), Vector2.Zero);
      child2.Clicked += new EventHandler<EventArgs>(this.OnOkButtonClicked);
      child1.AddChild((BasicControl) child2);
      child1.Position = new Vector2((float) (402.0 - (double) child1.Size.X / 2.0), 350f);
      this._root.AddChild(child1);
      if (this._achievement == null)
        return;
      Sprite sprite2 = ResourcesManager.Instance.GetSprite(this._achievement.Texture);
      TexturedControl child3 = new TexturedControl(sprite2, Vector2.Zero);
      child3.Position = new Vector2((float) (this._infoRect.X + 80), (float) this._infoRect.Center.Y);
      child3.Origin = new Vector2((float) sprite2.Bounds.Width / 2f, (float) sprite2.Bounds.Height / 2f);
      this._root.AddChild((BasicControl) child3);
      PanelControl child4 = new PanelControl();
      SpriteFont resource1 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition15");
      TextControl child5 = new TextControl(this._achievement.Name.ToLowerInvariant(), resource1)
      {
        MaxSymbolsPerLine = 15
      };
      child5.RebuildLines();
      child5.Color = new Color(169, 162, 27);
      child4.AddChild((BasicControl) child5);
      SpriteFont resource2 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition11");
      TextControl child6 = new TextControl(this._achievement.Description.ToLowerInvariant(), resource2)
      {
        MaxSymbolsPerLine = 20
      };
      child6.RebuildLines();
      child6.Color = new Color(169, 162, 27) * 0.5f;
      child4.AddChild((BasicControl) child6);
      child4.LayoutColumn(0.0f, 0.0f, 3f);
      child4.Position = new Vector2((float) (this._infoRect.X + 160), (float) this._infoRect.Center.Y - child4.Size.Y / 2f);
      this._root.AddChild((BasicControl) child4);
    }
  }
}
