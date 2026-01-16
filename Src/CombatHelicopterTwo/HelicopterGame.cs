// Decompiled with JetBrains decompiler
// Type: Helicopter.HelicopterGame
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Playing;
using Helicopter.Screen;
using Helicopter.Screen.MainMenu;
using Helicopter.Screen.Popups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter
{
  public class HelicopterGame
  {
    private ScreenManager _screenManager;

    public event EventHandler<BooleanEventArgs> BannerStateChanged;

    public event EventHandler Quit;

    public event EventHandler<InAppEventArgs> ShopNeeded;

    public SpriteBatch SpriteBatch => this._screenManager.SpriteBatch;

    public bool IsQuitCan { get; set; }

    public void Draw(GameTime gameTime) => this._screenManager.Draw(gameTime);

    public void Update(GameTime gameTime) => this._screenManager.Update(gameTime);

    public void OnActivated(object sender, EventArgs args)
    {
      this.LoadSettings();
      ++SettingsGame.LaunchNumber;
    }

    public void OnBannerStateChanged(BooleanEventArgs e)
    {
      EventHandler<BooleanEventArgs> bannerStateChanged = this.BannerStateChanged;
      if (bannerStateChanged == null)
        return;
      bannerStateChanged((object) null, e);
    }

    public void OnDeactivated(object sender, EventArgs args) => HelicopterGame.SaveSettings();

    public void OnExiting(object sender, EventArgs args) => HelicopterGame.SaveSettings();

    public void OnQuit(EventArgs e)
    {
      EventHandler quit = this.Quit;
      if (quit == null)
        return;
      quit((object) null, e);
    }

    public void InvokeShopNeeded(string productID)
    {
      EventHandler<InAppEventArgs> shopNeeded = this.ShopNeeded;
      if (shopNeeded == null)
        return;
      shopNeeded((object) null, new InAppEventArgs()
      {
        ProductID = productID
      });
    }

    public void BackButtonPressed() => this._screenManager.BackButtonPress();

    public void Init(GraphicsDevice graphicsDevice, ContentManager content)
    {
      ResourcesManager.CreateInstance(content);
      this._screenManager = new ScreenManager()
      {
        GraphicsDevice = graphicsDevice
      };
      this._screenManager.LoadContent();
      this._screenManager.Game = this;
      GameProcess.Instance.Init(this._screenManager);
      ((MainMenuScreen) GameProcess.Instance.Navigator.ShowScreen(ScreenType.MainMenu)).ShowPopup();
      Gamer.Instance.InitItems();
    }

    protected void LoadSettings()
    {
      SettingsGame.Load();
      Gamer.Instance.Load();
    }

    public static void SaveSettings()
    {
      SettingsGame.Save();
      Gamer.Instance.Save();
    }

    public void AddMoneyForGamer(float count) => Gamer.Instance.Money.AddMoney(count);

    public void ShowTransitionComplitedPopup()
    {
      this._screenManager.AddScreen((GameScreen) new TransactionCompletedPopUp());
    }

    public void ShowTransitionFailedPopup()
    {
      this._screenManager.AddScreen((GameScreen) new TransactionDeclinedPopUp());
    }
  }
}
