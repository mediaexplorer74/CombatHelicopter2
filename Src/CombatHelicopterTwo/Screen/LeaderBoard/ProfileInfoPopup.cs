// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.LeaderBoard.ProfileInfoPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Windows.System;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Screen.LeaderBoard
{
  internal class ProfileInfoPopup : GameScreen
  {
    private readonly BasicControl _root = new BasicControl();
    private bool _isNicknameChanged;
    private TextControl _nameText;

    public event EventHandler<UserNameEventArgs> NicknameChanged;

    private void OnNicknameChanged(UserNameEventArgs e)
    {
      EventHandler<UserNameEventArgs> nicknameChanged = this.NicknameChanged;
      if (nicknameChanged == null)
        return;
      nicknameChanged((object) this, e);
    }

    public string Nickname { get; set; }

    public ProfileInfoPopup() => this.IsPopup = true;

    private void UpdateNicknameText()
    {
      this._nameText.Text = string.IsNullOrEmpty(this.Nickname) ? "" : this.Nickname.ToLower();
      this._nameText.Position = new Vector2((float) ((800.0 - (double) this._nameText.Size.X) / 2.0), (float) ((double) byte.MaxValue + (44.0 - (double) this._nameText.Size.Y) / 2.0));
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._root.Size = new Vector2(800f, 480f);
      Sprite sprite = ResourcesManager.Instance.GetSprite("PopUpWindow/userNamePopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite, new Vector2((float) (400 - sprite.Bounds.Width / 2), (float) (240 - sprite.Bounds.Height / 2))));
      MenuControl child1 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butOk"), ResourcesManager.Instance.GetSprite("PopUpWindow/butOkSelect"), new Vector2(326f, 345f));
      SpriteFont resource = ResourcesManager.Instance.GetResource<SpriteFont>("Fonts/coalition24");
      this._nameText = new TextControl(this.Nickname.ToLower(), resource);
      this._nameText.Scale = 0.6f;
      this._root.AddChild((BasicControl) this._nameText);
      this.UpdateNicknameText();
      child1.Clicked += new EventHandler<EventArgs>(this.OnOkClicked);
      this._root.AddChild((BasicControl) child1);
      MenuControl child2 = new MenuControl(new Rectangle(170, 259, 460, 42));
      child2.Children.Clear();
      child2.Clicked += new EventHandler<EventArgs>(this.OnCangeNameClick);
      this._root.AddChild((BasicControl) child2);
    }

    private void OnCangeNameClick(object sender, EventArgs e)
    {
      this._isNicknameChanged = false;
    }

    private void OnCloseKeyboard(IAsyncResult ar)
    {
      string nickname = this.Nickname;
      try
      {
        string str = nickname;
        if (str == null)
          return;
        this.Nickname = str;
        if (this.Nickname.Length > 20)
          this.Nickname = this.Nickname.Substring(0, 20);
        this.UpdateNicknameText();
        this._isNicknameChanged = true;
      }
      catch (Exception ex)
      {
        this.Nickname = nickname;
        this.UpdateNicknameText();
      }
    }

    public override void OnBackButton() => this.Close();

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      base.HandleInput(input);
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

    private void OnOkClicked(object sender, EventArgs e) => this.Close();

    private void Close()
    {
      this.ExitScreen();
      if (!this._isNicknameChanged)
        return;
      this.OnNicknameChanged(new UserNameEventArgs()
      {
        NewName = this.Nickname
      });
    }
  }
}
