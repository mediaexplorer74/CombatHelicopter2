// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Buttons.InteractiveLevelButtonControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Playing;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

#nullable disable
namespace Helicopter.Screen.MapScreen.Buttons
{
  internal class InteractiveLevelButtonControl : BasicControl
  {
    private const float AnimationTime = 0.15f;
    private DisableControl _buttonOneControl;
    private DisableControl _buttonThreeControl;
    private DisableControl _buttonTwoControl;
    private DisableControl _levelButtonControl;
    private FourTexturePack _levelButtonTexturePack;
    private FourTexturePack _levelButtonSelectedTexturePack;
    private bool _isCurrent;
    private bool _isOpened;
    private Sprite _disableTexture;

    public event ButtonClickHandler ButtonClicked;

    public event EventHandler<EventArgs> Opened;

    public event EventHandler<EventArgs> Closed;

    public bool IsOpened
    {
      get => this._isOpened;
      set
      {
        this._isOpened = value;
        if (this.IsOpened)
        {
          if (this.Opened == null)
            return;
          this.Opened((object) this, (EventArgs) null);
        }
        else
        {
          if (this.Closed == null)
            return;
          this.Closed((object) this, (EventArgs) null);
        }
      }
    }

    public InteractiveLevelButtonControl(WorldType type)
    {
      InteractiveLevelButtonControl levelButtonControl = this;
      LocationHistory locationHistory = new LocationHistory();
      this._buttonOneControl = new DisableControl();
      this._buttonOneControl.Clicked += (EventHandler<EventArgs>) ((x, y) =>
      {
        if (levelButtonControl.ButtonClicked == null)
          return;
        levelButtonControl.ButtonClicked(type, 1);
      });
      this._buttonTwoControl = new DisableControl();
      this._buttonTwoControl.Clicked += (EventHandler<EventArgs>) ((x, y) =>
      {
        if (levelButtonControl.ButtonClicked == null)
          return;
        levelButtonControl.ButtonClicked(type, 2);
      });
      this._buttonThreeControl = new DisableControl();
      this._buttonThreeControl.Clicked += (EventHandler<EventArgs>) ((x, y) =>
      {
        if (levelButtonControl.ButtonClicked == null)
          return;
        levelButtonControl.ButtonClicked(type, 3);
      });
      this._levelButtonControl = new DisableControl();
      this._levelButtonControl.Clicked += new EventHandler<EventArgs>(this.OnClick);
    }

    public override void Update(GameTime gameTime) => this._levelButtonControl.Update(gameTime);

    public override void Draw(DrawContext context) => this._levelButtonControl.Draw(context);

    private void OnClick(object sender, EventArgs e)
    {
      if (this._levelButtonControl.Children.All<BasicControl>((Func<BasicControl, bool>) (x => ((DisableControl) x).IsAnimation)))
        return;
      if (this._levelButtonControl.Children.Any<BasicControl>((Func<BasicControl, bool>) (x => x.Visible)))
        this.Close();
      else
        this.Open();
    }

    public override void HandleInput(InputState inputState)
    {
      this._levelButtonControl.HandleInput(inputState);
    }

    public void Init(LocationHistory mission, bool isCurrent, bool threeEpisode)
    {
      this._isCurrent = isCurrent;
      this._levelButtonControl.Init(new FourTexturePack()
      {
        StateOne = isCurrent ? this._levelButtonTexturePack.StateTwo : this._levelButtonTexturePack.StateOne,
        StateOneSelected = isCurrent ? this._levelButtonTexturePack.StateTwoSelected : this._levelButtonTexturePack.StateOneSelected,
        StateTwo = this._disableTexture
      });
      this._levelButtonControl.Enabled = mission.IsUnlocked;
      this.IsOpened = isCurrent;
      this.InitButtonControl(this._buttonOneControl, mission.FirstEpisode, isCurrent);
      this.AddEpisodeButton(this._buttonOneControl);
      this.InitButtonControl(this._buttonTwoControl, mission.SecondEpisode, isCurrent);
      this.AddEpisodeButton(this._buttonTwoControl);
      if (threeEpisode)
      {
        this.InitButtonControl(this._buttonThreeControl, mission.ThirdEpisode, isCurrent);
        this.AddEpisodeButton(this._buttonThreeControl);
      }
      else
      {
        this._buttonThreeControl.Enabled = false;
        this._buttonThreeControl.Visible = false;
      }
    }

    private void InitButtonControl(DisableControl control, EpisodeHistory episode, bool isVisible)
    {
      control.Enabled = episode.IsAvailiable;
      control.Visible = isVisible;
      control.StarsNumber = episode.Stars;
    }

    private void AddEpisodeButton(DisableControl control)
    {
      if (this._levelButtonControl.Children != null && this._levelButtonControl.Children.Contains((BasicControl) control))
        return;
      this._levelButtonControl.AddChild((BasicControl) control);
    }

    public void Init(
      FourTexturePack levelButton,
      FourTexturePack levelButtonSelectedTexturePack,
      Vector2 levelButtonPosition,
      FourTexturePack buttonOne,
      Vector2 buttonOnePosition,
      FourTexturePack buttonTwo,
      Vector2 buttonTwoPosition,
      FourTexturePack buttonThree,
      Vector2 buttonThreePosition,
      Sprite disableTexture,
      Sprite starTexture)
    {
      this._levelButtonTexturePack = levelButton;
      this._levelButtonSelectedTexturePack = levelButtonSelectedTexturePack;
      this._levelButtonControl.Init(levelButton, levelButtonPosition);
      this._buttonOneControl.Init(buttonOne, buttonOnePosition);
      this.AddEpisodeButton(this._buttonOneControl);
      this._buttonTwoControl.Init(buttonTwo, buttonTwoPosition);
      this.AddEpisodeButton(this._buttonTwoControl);
      if (buttonThree != null)
      {
        this._buttonThreeControl.Init(buttonThree, buttonThreePosition);
        this.AddEpisodeButton(this._buttonThreeControl);
      }
      else
      {
        this._buttonThreeControl.Enabled = false;
        this._buttonThreeControl.Visible = false;
      }
      this._disableTexture = disableTexture;
    }

    public void Close()
    {
      Vector2 center = new Vector2(this._levelButtonControl.InitialPosition.X + (float) this._levelButtonTexturePack.StateOne.Bounds.Center.X, this._levelButtonControl.InitialPosition.Y + (float) this._levelButtonTexturePack.StateOne.Bounds.Center.Y);
      this.IsOpened = false;
      this._levelButtonControl.Init(new FourTexturePack()
      {
        StateOne = this._isCurrent ? this._levelButtonTexturePack.StateTwo : this._levelButtonTexturePack.StateOne,
        StateOneSelected = this._isCurrent ? this._levelButtonTexturePack.StateTwoSelected : this._levelButtonTexturePack.StateOneSelected,
        StateTwo = this._disableTexture
      });
      this._levelButtonControl.Children.ForEach((Action<BasicControl>) (x =>
      {
        if (!x.Visible)
          return;
        ((DisableControl) x).Animation(((DisableControl) x).InitialPosition, center, 0.15f, new TweeningFunction(Quadratic.EaseIn), true);
      }));
    }

    public void Open()
    {
      Vector2 center = new Vector2(this._levelButtonControl.InitialPosition.X + (float) this._levelButtonTexturePack.StateOne.Bounds.Center.X, this._levelButtonControl.InitialPosition.Y + (float) this._levelButtonTexturePack.StateOne.Bounds.Center.Y);
      this.IsOpened = true;
      this._levelButtonControl.Init(this._levelButtonSelectedTexturePack);
      this._levelButtonControl.Children.ForEach((Action<BasicControl>) (x =>
      {
        if (x.Visible)
          return;
        x.Visible = true;
        ((DisableControl) x).Animation(center, ((DisableControl) x).InitialPosition, 0.15f, new TweeningFunction(Quadratic.EaseOut), false);
      }));
    }
  }
}
