// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.GameplayPopups.ChallengeGameOverPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Windows.Storage;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class ChallengeGameOverPopup : BasePopup
  {
    private readonly Vector2Tweener _bigNumberPositionTweener = new Vector2Tweener(
        Vector2.Zero, Vector2.Zero, 0.0f, new TweeningFunction(Linear.EaseIn));

    private readonly Tweener _bigNumberTweener = new Tweener(0.0f, 0.0f, 0.0f, new TweeningFunction(Linear.EaseIn));
    private TextControl _messageControl;
    private int _money;
    private int _points;
    private BasicControl _root;
    private SpriteFont bigFont;
    private Color color;

    public event EventHandler Leaderboard;

    public event EventHandler Menu;

    public event EventHandler Replay;

    public ChallengeGameOverPopup()
    {
      this.IsPopup = true;
      this._bigNumberTweener.Ended += new EventHandler<EventArgs>(this.OnBigNumberEnd);
      this._bigNumberTweener.Stop();
    }

    public override void Draw(DrawContext drawContext)
    {
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
      this._root.Size = new Vector2(800f, 480f);
      TexturedControl child1 = new TexturedControl(ResourcesManager.Instance.GetSprite("PopUpWindow/gameOverPopUpBg"), Vector2.Zero);
      child1.Position = new Vector2(400f, 240f) - child1.Size / 2f;
      this._root.AddChild((BasicControl) child1);
      string text = string.Format("points: {0}\n+&{1}", (object) this._points, (object) this._money);
      this.bigFont = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition40");
      this.color = new Color(254, 224, 23);
      TextControl textControl = new TextControl(text, this.bigFont, this.color);
      textControl.Origin = new Vector2(0.5f, 0.5f);
      textControl.Position = Vector2.Zero;
      textControl.Scale = 0.0f;
      this._messageControl = textControl;
      this._root.AddChild((BasicControl) this._messageControl);
      PanelControl child2 = new PanelControl();
      MenuControl child3 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butMenuShort"), ResourcesManager.Instance.GetSprite("PopUpWindow/butMenuShortSelect"), Vector2.Zero);
      child3.Clicked += new EventHandler<EventArgs>(this.OnMapButtonClicked);
      child2.AddChild((BasicControl) child3);
      MenuControl child4 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butLeaderboard"), ResourcesManager.Instance.GetSprite("PopUpWindow/butLeaderboardSelect"), Vector2.Zero);
      child4.Clicked += new EventHandler<EventArgs>(this.OnLeaderboardClicked);
      child2.AddChild((BasicControl) child4);
      MenuControl child5 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butReplayShort"), ResourcesManager.Instance.GetSprite("PopUpWindow/butReplayShortSeect"), Vector2.Zero);
      child5.Clicked += new EventHandler<EventArgs>(this.OnRetryClicked);
      child2.AddChild((BasicControl) child5);
      child2.LayoutRow(0.0f, 0.0f, 7f);
      child2.Position = new Vector2((float) (400.0 - (double) child2.Size.X / 2.0), 345f);
      this._root.AddChild((BasicControl) child2);
      base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
      if (this._bigNumberTweener.Running)
      {
        float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
        this._bigNumberTweener.Update(totalSeconds);
        this._bigNumberPositionTweener.Update(totalSeconds);
        this._messageControl.Scale = this._bigNumberTweener.Position;
        this._messageControl.Position = this._bigNumberPositionTweener.CurrentPosition;
      }
      base.Update(gameTime);
    }

    private void OnBigNumberEnd(object sender, EventArgs e)
    {
      var settings = ApplicationData.Current.LocalSettings;
      int num = 0;
      if (settings.Values.ContainsKey("HeightScores"))
        int.TryParse(settings.Values["HeightScores"]?.ToString(), out num);
      if (this._points > num)
        num = this._points;
      TextControl child = new TextControl(string.Format(
          "best: {0}\n total credits: {1}&", (object) num, (object) Gamer.Instance.Money.Count), 
          ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition12"), this.color * 0.4f);
      child.Origin = new Vector2(0.5f, 0.5f);
      child.Position = new Vector2(400f, 310f);
      this._root.AddChild((BasicControl) child);
    }

    public void OnLeaderboard(EventArgs e)
    {
      EventHandler leaderboard = this.Leaderboard;
      if (leaderboard == null)
        return;
      leaderboard((object) this, e);
    }

    private void OnLeaderboardClicked(object sender, EventArgs e) => this.OnLeaderboard(e);

    public void OnMap(EventArgs e)
    {
      EventHandler menu = this.Menu;
      if (menu == null)
        return;
      menu((object) this, e);
    }

    private void OnMapButtonClicked(object sender, EventArgs e) => this.OnMap(EventArgs.Empty);

    public void OnReplay(EventArgs e)
    {
      EventHandler replay = this.Replay;
      if (replay == null)
        return;
      replay((object) this, e);
    }

    private void OnRetryClicked(object sender, EventArgs e) => this.OnReplay(EventArgs.Empty);

    public void InitScore(int points, int money)
    {
      this._points = points;
      this._money = money;
      this._bigNumberTweener.Init(10f, 0.7f, 0.15f, new TweeningFunction(Quadratic.EaseOut));
      this._bigNumberTweener.Start();
      this._bigNumberPositionTweener.Init(new Vector2(100f, -100f), new Vector2(400f, 250f), 0.15f, new TweeningFunction(Exponential.EaseOut));
      this._bigNumberPositionTweener.Start();
    }
  }
}
