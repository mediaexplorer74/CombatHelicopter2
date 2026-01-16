// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.LeaderBoard.LeaderboardRecordControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Helicopter.Screen.LeaderBoard
{
  internal class LeaderboardRecordControl : PanelControl
  {
    private LeaderboardRecord _record;
    private bool _lightBackground;
    private TexturedControl _background;
    private readonly Color _lightBgColor = new Color(43, 38, 18);
    private readonly Color _recordsColor = new Color(218, 193, 96);
    private readonly Color _myRecordsColor = Color.White;
    private TextControl _nicknameControl;
    private Vector2 _lastPositionOnScreen = Vector2.Zero;

    public event EventHandler Tap;

    private void OnTap(EventArgs e)
    {
      EventHandler tap = this.Tap;
      if (tap == null)
        return;
      tap((object) this, e);
    }

    public bool LightBackground
    {
      get => this._lightBackground;
      set
      {
        this._lightBackground = value;
        this.UpdateBackground();
      }
    }

    public LeaderboardRecord Record
    {
      get => this._record;
      set
      {
        this._record = value;
        this.UpdateLayout();
      }
    }

    public SpriteFont Font { get; set; }

    public Sprite BlankTexture { get; set; }

    public Dictionary<Rank, Sprite> RankTextures { get; set; }

    public Dictionary<Rank, string> RankTexts { get; set; }

    public LeaderboardRecordControl() => this.Children = new List<BasicControl>();

    public void UpdateLayout() => this.CreateStandartLayout();

    private void CreateStandartLayout()
    {
      this.Children.Clear();
      Color color = this._record.IsMyself ? this._myRecordsColor : this._recordsColor;
      this.Size = new Vector2(586f, 32f);
      TextControl child1 = new TextControl(this._record.Number.ToString((IFormatProvider) CultureInfo.InvariantCulture), this.Font)
      {
        Color = color,
        Scale = 0.5f
      };
      float y = (float) ((32.0 - (double) child1.Size.Y) / 2.0);
      child1.Position = new Vector2(75f - child1.Size.X, y);
      this.AddChild((BasicControl) child1);
      TexturedControl child2 = new TexturedControl(this.RankTextures[this._record.Rank], Vector2.Zero);
      child2.Position = new Vector2(100f, (float) ((32.0 - (double) child2.Size.Y) / 2.0));
      this.AddChild((BasicControl) child2);
      TextControl child3 = new TextControl(this.RankTexts[this._record.Rank].ToLower(), this.Font);
      child3.Color = color;
      child3.Scale = 0.5f;
      child3.Position = new Vector2(child2.Position.X + 20f, y);
      this.AddChild((BasicControl) child3);
      try
      {
        TextControl textControl = new TextControl(this.GetNameDisplayText(this._record.Name).ToLower(), this.Font);
        textControl.Color = color;
        textControl.Position = new Vector2(250f, y);
        textControl.Scale = 0.5f;
        this._nicknameControl = textControl;
        this.AddChild((BasicControl) this._nicknameControl);
      }
      catch (Exception ex)
      {
        this._nicknameControl = (TextControl) null;
      }
      TextControl child4 = new TextControl(this._record.Scores.ToString("0", (IFormatProvider) CultureInfo.InvariantCulture), this.Font)
      {
        Color = color,
        Scale = 0.5f
      };
      child4.Position = new Vector2(585f - child4.Size.X, y);
      this.AddChild((BasicControl) child4);
    }

    public override void Draw(DrawContext context)
    {
      base.Draw(context);
      this._lastPositionOnScreen = context.DrawOffset;
    }

    public void UpdateBackground()
    {
      if (this._background != null && this.Children.Contains((BasicControl) this._background))
      {
        this.Children.Remove((BasicControl) this._background);
        this._background = (TexturedControl) null;
      }
      if (!this._lightBackground)
        return;
      this._background = new TexturedControl(this.BlankTexture, Vector2.Zero);
      this._background.Color = this._lightBgColor;
      this._background.ImageSize = this.Size;
      this.AddChild((BasicControl) this._background, 0);
    }

    private string GetNameDisplayText(string fullName)
    {
      return fullName.Length <= 20 ? fullName : fullName.Substring(0, 20);
    }

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      foreach (GestureSample gesture in input.Gestures)
      {
        if (gesture.GestureType == GestureType.Tap && new Rectangle((int) this._lastPositionOnScreen.X, (int) this._lastPositionOnScreen.Y, (int) this.Size.X, (int) this.Size.Y).Contains((int) gesture.Position.X, (int) gesture.Position.Y))
          this.OnTap(EventArgs.Empty);
      }
    }
  }
}
