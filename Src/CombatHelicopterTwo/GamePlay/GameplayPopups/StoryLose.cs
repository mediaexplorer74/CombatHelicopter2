// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.GameplayPopups.StoryLose
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class StoryLose : BasePopup
  {
    private BasicControl _root;

    public event EventHandler Map;

    public event EventHandler Replay;

    public void OnMap(EventArgs e)
    {
      EventHandler map = this.Map;
      if (map == null)
        return;
      map((object) this, e);
    }

    public void OnReplay(EventArgs e)
    {
      EventHandler replay = this.Replay;
      if (replay == null)
        return;
      replay((object) this, e);
    }

    public StoryLose() => this.IsPopup = true;

    public override void Draw(DrawContext drawContext)
    {
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    private void OnMapButtonClicked(object sender, EventArgs e) => this.OnMap(EventArgs.Empty);

    private void OnRetryClicked(object sender, EventArgs e) => this.OnReplay(EventArgs.Empty);

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      this._root.HandleInput(input);
    }

    public override void LoadContent()
    {
      this._root = new BasicControl();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/losePopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2), (float) (245 - sprite1.Bounds.Height / 2))));
      BasicControl child1 = new BasicControl();
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butMap"), ResourcesManager.Instance.GetSprite("PopUpWindow/butMapSelect"), Vector2.Zero);
      child2.Clicked += new EventHandler<EventArgs>(this.OnMapButtonClicked);
      child1.AddChild((BasicControl) child2);
      MenuControl child3 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butReplay"), ResourcesManager.Instance.GetSprite("PopUpWindow/butReplaySelect"), new Vector2((float) ((double) child1.Size.X * 2.0 + 26.0), 0.0f));
      child3.Clicked += new EventHandler<EventArgs>(this.OnRetryClicked);
      child1.AddChild((BasicControl) child3);
      child1.Position = new Vector2((float) (400.0 - (double) child1.Size.X / 2.0), 350f);
      this._root.AddChild(child1);
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("PopUpWindow/skull");
      this._root.AddChild((BasicControl) new TexturedControl(sprite2, new Vector2((float) (400 - sprite2.Bounds.Width / 2), 240f)));
    }
  }
}
