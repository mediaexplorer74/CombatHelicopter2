// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.LoadingScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Helicopter.Screen
{
  internal class LoadingScreen : GameScreen
  {
    private Tweener _alphaTweener;
    private Sprite _background;
    private GameScreen _from;
    private bool _reverted;
    private GameScreen _toScreen;

    public LoadingScreen() => this.IsPopup = true;

    public override void Update(GameTime gameTime)
    {
      if (this._toScreen.IsFullyLoaded && !this._alphaTweener.Running && this._reverted)
      {
        this._toScreen.TransitionComplete();
        this.ExitScreen();
      }
      if (this._toScreen.IsFullyLoaded && !this._alphaTweener.Running && !this._reverted)
      {
        this._reverted = true;
        this._alphaTweener.Start();
      }
      float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
      if (!this._alphaTweener.Running)
        return;
      this._alphaTweener.Update(totalSeconds);
    }

    public override void Draw(DrawContext drawContext)
    {
      this._background.Color = Color.White * this._alphaTweener.Position;
      this._background.Draw(drawContext.SpriteBatch, Vector2.Zero);
    }

    public override void OnBackButton()
    {
    }

    private void OnTweenerEnd(object sender, EventArgs e)
    {
      this._alphaTweener.Ended -= new EventHandler<EventArgs>(this.OnTweenerEnd);
      this._alphaTweener.Init(1f, 0.0f, 0.8f, new TweeningFunction(Linear.EaseIn));
      this._alphaTweener.Stop();
      Task.Run(() => this.OnLoad());
    }

    private void OnLoad()
    {
      if (this._from != null)
        this._from.ExitScreen();
      this.ScreenManager.AddScreen(this._toScreen);
      this.ScreenManager.AddScreen((GameScreen) this);
    }

    public void Init(ScreenManager screenManager, GameScreen from, GameScreen toScreen)
    {
      this._from = from;
      this._toScreen = toScreen;
      this._background = ResourcesManager.Instance.GetSprite("Loading/loading");
      this._alphaTweener = new Tweener(0.0f, 1f, 0.2f, new TweeningFunction(Linear.EaseOut));
      this._alphaTweener.Ended += new EventHandler<EventArgs>(this.OnTweenerEnd);
      this._alphaTweener.Start();
      this.ScreenManager = screenManager;
      if (from != null)
      {
        this.ScreenManager.ExitAllScreens();
        this.ScreenManager.AddWithoutLoading(this._from);
      }
      else
        this.ScreenManager.ExitAllScreens();
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = false
      });
    }
  }
}
