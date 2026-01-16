// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.GameplayPopups.PausePopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Sounds;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class PausePopup : GameScreen
  {
    private readonly BasicControl _root = new BasicControl();

    public event EventHandler MainMenu;

    public event EventHandler Restart;

    public event EventHandler Resume;

    public PausePopup() => this.IsPopup = true;

    public override void LoadContent()
    {
      base.LoadContent();
      this._root.Size = new Vector2(800f, 480f);
      Sprite sprite = ResourcesManager.Instance.GetSprite("PopUpWindow/pausePopUpBg2");
      this._root.AddChild((BasicControl) new TexturedControl(sprite, new Vector2((float) (400 - sprite.Bounds.Width / 2), 87f)));
    }

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
    }

    public override void OnBackButton() => this.InvokeResume(EventArgs.Empty);

    public override void UnloadContent()
    {
      base.UnloadContent();
      Audio.Instance.MasterVolume = 1f;
    }

    public override void Update(GameTime gameTime)
    {
      this._root.Update(gameTime);
      base.Update(gameTime);
    }

    public override void Draw(DrawContext drawContext)
    {
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    private void OnMainMenu(EventArgs e)
    {
      EventHandler mainMenu = this.MainMenu;
      if (mainMenu == null)
        return;
      mainMenu((object) this, e);
    }

    private void OnRestart(EventArgs e)
    {
      EventHandler restart = this.Restart;
      if (restart == null)
        return;
      restart((object) this, e);
    }

    public void InvokeResume(EventArgs e)
    {
      EventHandler resume = this.Resume;
      if (resume == null)
        return;
      resume((object) this, e);
    }

    public void Close() => this.ExitScreen();

    public void Init(bool isStory)
    {
      Audio.Instance.MasterVolume = 0.5f;
      PanelControl child1 = new PanelControl();
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butResume2"), ResourcesManager.Instance.GetSprite("PopUpWindow/butResumeSelect2"), Vector2.Zero);
      child2.Clicked += (EventHandler<EventArgs>) ((x, y) => this.InvokeResume(y));
      child1.AddChild((BasicControl) child2);
      MenuControl child3 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butRestart"), ResourcesManager.Instance.GetSprite("PopUpWindow/butRestartSelect"), Vector2.Zero);
      child3.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnRestart(y));
      child1.AddChild((BasicControl) child3);
      MenuControl child4 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butMainMenu"), ResourcesManager.Instance.GetSprite("PopUpWindow/butMainMenuSelect"), Vector2.Zero);
      child4.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnMainMenu(y));
      child1.AddChild((BasicControl) child4);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/sliderBg");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("PopUpWindow/sliderSound");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("PopUpWindow/sliderSoundSelect");
      SliderControl child5 = new SliderControl(sprite1, sprite2, sprite3, Vector2.Zero)
      {
        IsOn = SettingsGame.Sound
      };
      child5.StateChanged += new EventHandler<BooleanEventArgs>(this.OnSoundStateChanged);
      child1.AddChild((BasicControl) child5);
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("PopUpWindow/sliderVibra");
      Sprite sprite5 = ResourcesManager.Instance.GetSprite("PopUpWindow/sliderVibraSelect");
      SliderControl child6 = new SliderControl(sprite1, sprite4, sprite5, Vector2.Zero)
      {
        IsOn = SettingsGame.Vibro
      };
      child6.StateChanged += (EventHandler<BooleanEventArgs>) ((x, y) => SettingsGame.Vibro = y.State);
      child1.AddChild((BasicControl) child6);
      child1.LayoutColumn(0.0f, 0.0f, 10f);
      child6.Position = new Vector2(child6.Position.X, child6.Position.Y - 2f);
      child5.Position = new Vector2(child5.Position.X, child5.Position.Y - 2f);
      child1.Position = new Vector2((float) (400.0 - (double) child1.Size.X / 2.0), 166f);
      this._root.AddChild((BasicControl) child1);
    }

    private void OnSoundStateChanged(object sender, BooleanEventArgs e)
    {
      this.ScreenManager.AudioManager.SoundStateChanged(sender, e);
    }
  }
}
