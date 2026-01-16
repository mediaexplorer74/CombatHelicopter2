// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.ScreenManager
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Utils.SoundManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.BaseScreens
{
  public class ScreenManager
  {
    private readonly InputState _input = new InputState();
    private readonly List<GameScreen> _screens = new List<GameScreen>();
    private readonly List<GameScreen> _screensToUpdate = new List<GameScreen>();
    private SpriteBatch _spriteBatch;
    private DrawContext _drawContext;
    public readonly AudioManager AudioManager = new AudioManager();
    public bool IsActive = true;
    public HelicopterGame Game;

    public SpriteBatch SpriteBatch => this._spriteBatch;

    public GraphicsDevice GraphicsDevice { get; set; }

    public void Update(GameTime gameTime)
    {
      this._input.Update();
      this._screensToUpdate.Clear();
      foreach (GameScreen screen in this._screens)
        this._screensToUpdate.Add(screen);
      bool flag = !this.IsActive;
      while (this._screensToUpdate.Count > 0)
      {
        GameScreen gameScreen = this._screensToUpdate[this._screensToUpdate.Count - 1];
        this._screensToUpdate.RemoveAt(this._screensToUpdate.Count - 1);
        gameScreen.Update(gameTime);
        if (!flag)
        {
          gameScreen.HandleInput(this._input);
          flag = true;
        }
        int num = gameScreen.IsPopup ? 1 : 0;
      }
      foreach (GameScreen screen in this._screens)
        screen.OnTop = false;
      this._screens[this._screens.Count - 1].OnTop = true;
    }

    public void Draw(GameTime gameTime)
    {
      this._drawContext.GameTime = gameTime;
      this._drawContext.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
      for (int index = 0; index < this._screens.Count; ++index)
        this._screens[index].Draw(this._drawContext);
      this._drawContext.SpriteBatch.End();
    }

    public void AddScreen(GameScreen screen)
    {
      screen.ScreenManager = this;
      screen.LoadContent();
      this._screens.Add(screen);
    }

    public void AddWithoutLoading(GameScreen screen) => this._screens.Add(screen);

    public GameScreen[] GetScreens() => this._screens.ToArray();

    public void Initialize()
    {
    }

    public void LoadContent()
    {
      this._spriteBatch = new SpriteBatch(this.GraphicsDevice);
      this._drawContext = new DrawContext()
      {
        SpriteBatch = this._spriteBatch,
        Device = this.GraphicsDevice,
        BlankTexture = ResourcesManager.BlankSprite.Texture
      };
      foreach (GameScreen screen in this._screens)
        screen.LoadContent();
    }

    public void RemoveScreen(GameScreen screen)
    {
      screen.UnloadContent();
      this._screens.Remove(screen);
      this._screensToUpdate.Remove(screen);
    }

    public void TraceScreens()
    {
    }

    protected void UnloadContent()
    {
      foreach (GameScreen screen in this._screens)
        screen.UnloadContent();
    }

    public void ExitAllScreens()
    {
      while (this._screens.Count > 0)
        this._screens.First<GameScreen>().ExitScreen();
    }

    public void BackButtonPress()
    {
      if (this._screens.Count <= 0)
        return;
      this._screens[this._screens.Count - 1].OnBackButton();
    }
  }
}
