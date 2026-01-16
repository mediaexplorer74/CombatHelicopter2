// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Buttons.DisableControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Screen.MapScreen.Buttons
{
  internal class DisableControl : BasicControl
  {
    private Sprite _disabledTexture;
    private Sprite _enabledTexture;
    private Sprite _enabledTextureSelected;
    private Sprite _starTexture;
    private Rectangle _contactArea;
    private float _scale;
    private Vector2Tweener _tween;
    private Vector2[] _starPositions;

    private float Scale
    {
      get => this._scale;
      set
      {
        this._scale = value;
        this._disabledTexture.Scale = this._enabledTexture.Scale = this._starTexture.Scale = this._enabledTextureSelected.Scale = new Vector2(this._scale);
      }
    }

    public event EventHandler<EventArgs> Clicked;

    public bool IsPressed { get; set; }

    public bool IsAnimation { get; set; }

    public Vector2 InitialPosition { get; set; }

    public int StarsNumber { get; set; }

    public DisableControl(FourTexturePack textures, Vector2 position, Rectangle contactArea)
      : this()
    {
      this._contactArea = contactArea;
      this.Init(textures);
      this.Position = position;
      this.InitialPosition = position;
      this.CalculateStarsPosition();
    }

    public DisableControl(FourTexturePack textures, Vector2 position)
      : this(textures, position, new Rectangle((int) position.X, (int) position.Y, textures.StateOne.Bounds.Width, textures.StateOne.Bounds.Height))
    {
      this.Scale = 1f;
    }

    public DisableControl()
    {
      this.Children = new List<BasicControl>();
      this.Enabled = true;
      this.Visible = true;
      this.IsAnimation = false;
      this.IsPressed = false;
    }

    public override void Update(GameTime gametime)
    {
      if (this.IsAnimation)
      {
        this._tween.Update((float) gametime.ElapsedGameTime.TotalSeconds);
        this.Position = this._tween.CurrentPosition;
      }
      base.Update(gametime);
    }

    public void Init(FourTexturePack textures)
    {
      this.Init(textures.StateOne, textures.StateOneSelected, textures.StateTwo, textures.StateTwoSelected);
    }

    public void Init(FourTexturePack textures, Vector2 position)
    {
      this.Init(textures);
      this.Position = position;
      this.InitialPosition = position;
      this._contactArea.X = (int) this.Position.X;
      this._contactArea.Y = (int) this.Position.Y;
      this.CalculateStarsPosition();
    }

    private void CalculateStarsPosition()
    {
      if (this._starTexture == null)
        return;
      float num1 = 0.0f;
      int num2 = 0;
      this._starPositions = new Vector2[3]
      {
        new Vector2((float) (this._contactArea.Center.X - this._starTexture.Bounds.Width - num2), (float) this._contactArea.Bottom - (float) (5.0 * (double) this._starTexture.Bounds.Height / 7.0) - num1),
        new Vector2((float) this._contactArea.Center.X - (float) this._starTexture.Bounds.Width / 2f - (float) num2, (float) (this._contactArea.Bottom - this._starTexture.Bounds.Height / 2) - num1),
        new Vector2((float) (this._contactArea.Center.X - num2), (float) this._contactArea.Bottom - (float) (5.0 * (double) this._starTexture.Bounds.Height / 7.0) - num1)
      };
    }

    private void Init(Sprite enabled, Sprite enabledSelected, Sprite disabled, Sprite starTexture)
    {
      this._contactArea = new Rectangle((int) this.Position.X, (int) this.Position.Y, enabled.Bounds.Width, enabled.Bounds.Height);
      this._enabledTexture = enabled;
      this._enabledTextureSelected = enabledSelected;
      this._disabledTexture = disabled;
      this._starTexture = starTexture;
      this.CalculateStarsPosition();
    }

    public override void Draw(DrawContext context)
    {
      if (!this.Visible)
        return;
      base.Draw(context);
      if (this.Enabled)
      {
        if (this.IsPressed)
          this._enabledTextureSelected.Draw(context.SpriteBatch, this.Position);
        else
          this._enabledTexture.Draw(context.SpriteBatch, this.Position);
        if (this.IsAnimation || this._starTexture == null)
          return;
        for (int index = 0; index < this.StarsNumber; ++index)
        {
          Vector2 starPosition = this._starPositions[index];
          this._starTexture.Draw(context.SpriteBatch, starPosition);
        }
      }
      else
        this._disabledTexture.Draw(context.SpriteBatch, this.Position);
    }

    public virtual void Action()
    {
      if (this.Clicked != null)
        this.Clicked((object) this, (EventArgs) null);
      this.IsPressed = false;
    }

    public void Animation(
      Vector2 from,
      Vector2 to,
      float time,
      TweeningFunction tweeningFunction,
      bool needDisapperAfterEnd)
    {
      if (this.IsAnimation)
        return;
      this.IsAnimation = true;
      this._tween = new Vector2Tweener(from, to, time, tweeningFunction);
      this.Position = from;
      this.Scale = needDisapperAfterEnd ? 1f : 0.0f;
      this._tween.Ended += (EventHandler<EventArgs>) delegate
      {
        this.IsAnimation = false;
        this.Visible = !needDisapperAfterEnd;
        this.Scale = 1f;
      };
      this._tween.Updated += (EventHandler<EventArgs>) delegate
      {
        if (needDisapperAfterEnd)
          this.Scale = 1f - this._tween.Percent;
        else
          this.Scale = this._tween.Percent;
      };
    }

    public override void HandleInput(InputState input)
    {
      if (!this.Enabled || this.IsAnimation || !this.Visible)
        return;
      foreach (TouchLocation touchLocation in input.TouchState)
      {
        switch (touchLocation.State)
        {
          case TouchLocationState.Invalid:
          case TouchLocationState.Moved:
            continue;
          case TouchLocationState.Released:
            if (this._contactArea.Contains((int) touchLocation.Position.X, (int) touchLocation.Position.Y) && this.IsPressed)
            {
              this.Action();
              continue;
            }
            this.IsPressed = false;
            continue;
          case TouchLocationState.Pressed:
            if (this._contactArea.Contains((int) touchLocation.Position.X, (int) touchLocation.Position.Y))
            {
              this.IsPressed = true;
              continue;
            }
            continue;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      base.HandleInput(input);
    }
  }
}
