// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.GameplayPopups.StoryEpicWin
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Utils.SoundManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class StoryEpicWin : BasePopup
  {
    private BasicControl _root;

    public event EventHandler ChallengeFight;

    public event EventHandler Menu;

    public StoryEpicWin(int stars) => this.IsPopup = true;

    public override void Draw(DrawContext drawContext)
    {
      if (!this.OnTop)
        return;
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      this._root.HandleInput(input);
    }

    public override void LoadContent()
    {
      this._root = new BasicControl();
      this._root.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite("PopUpWindow/victoryPopUpBg2"), new Vector2(52f, 74f)));
      MenuControl child1 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butMainMenu"), ResourcesManager.Instance.GetSprite("PopUpWindow/butMainMenuSelect"), new Vector2(282f, 254f));
      child1.Clicked += new EventHandler<EventArgs>(this.OnMainMenuButtonClicked);
      this._root.AddChild((BasicControl) child1);
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butSurvival"), ResourcesManager.Instance.GetSprite("PopUpWindow/butSurvivalSelect"), new Vector2(499f, 254f));
      child2.Clicked += new EventHandler<EventArgs>(this.OnChallengeClicked);
      this._root.AddChild((BasicControl) child2);
      this._root.AddChild((BasicControl) new TextControl("", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition12"), Color.Black, new Vector2(485f, 394f))
      {
        Centered = true,
        Origin = new Vector2(0.5f, 0.0f),
        MaxSymbolsPerLine = 35,
        Text = "Great Job! Good pilot like you deserves a top spot in our Global Leaderboard. Select SURVIVAL to complete with other Players!".ToLowerInvariant()
      });
      BackgroundSounds.Instance.PlayEpicWin();
    }

    private void InvokeChallengeButtonPressed(EventArgs e)
    {
      EventHandler challengeFight = this.ChallengeFight;
      if (challengeFight == null)
        return;
      challengeFight((object) this, e);
    }

    private void InvokeMainMenuPressed(EventArgs e)
    {
      EventHandler menu = this.Menu;
      if (menu == null)
        return;
      menu((object) this, e);
    }

    private void OnChallengeClicked(object sender, EventArgs e)
    {
      this.InvokeChallengeButtonPressed(EventArgs.Empty);
    }

    private void OnMainMenuButtonClicked(object sender, EventArgs e)
    {
      this.InvokeMainMenuPressed(EventArgs.Empty);
    }
  }
}
