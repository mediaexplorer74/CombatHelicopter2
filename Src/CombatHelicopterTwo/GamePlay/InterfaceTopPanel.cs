// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.InterfaceTopPanel
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter.GamePlay
{
  internal class InterfaceTopPanel : BasicControl
  {
    private const int SidePartWidth = 152;
    private const int CenterPartWidth = 548;
    private const string ScoreStringConst = "score";
    private InterfaceTopPanel.TexturesPack _canyonPack;
    private Vector2 _centerPartPosition;
    private InterfaceTopPanel.TexturesPack _currentPack;
    private bool _drawLandingZone;
    private InterfaceTopPanel.TexturesPack _enemyBasePack;
    private InterfaceTopPanel.TexturesPack _icePack;
    private InterfaceTopPanel.TexturesPack _junglePack;
    private InterfaceTopPanel.TexturesPack _landingPack;
    private Vector2 _landingCenterDest;
    private Rectangle _landingCenterSource;
    private Vector2 _landingLeftDest;
    private Rectangle _landingLeftSource;
    private Vector2 _landingRightDest;
    private Rectangle _landingRightSource;
    private Vector2 _leftPartPosition;
    private Vector2 _rightPartPosition;
    private SpriteFont _scoreFont;
    private string _scores;
    private InterfaceTopPanel.TexturesPack _vulcanPack;
    private static readonly Vector2 PositionScore = new Vector2(3f, 2f);
    private static readonly Vector2 PositionCount = new Vector2(3f, 18f);

    public event EventHandler Pause;

    public override void Draw(DrawContext context)
    {
      this._currentPack.Left.Draw(context.SpriteBatch, this._leftPartPosition);
      this._currentPack.Right.Draw(context.SpriteBatch, this._rightPartPosition);
      this._currentPack.Center.Draw(context.SpriteBatch, this._centerPartPosition);
      if (this._drawLandingZone)
      {
        if (this._landingPack.Left.SourceRectangle.Width > 0)
          this._landingPack.Left.Draw(context.SpriteBatch, this._landingLeftDest);
        if (this._landingPack.Right.SourceRectangle.Width > 0)
          this._landingPack.Right.Draw(context.SpriteBatch, this._landingRightDest);
        if (this._landingPack.Center.SourceRectangle.Width > 0)
          this._landingPack.Center.Draw(context.SpriteBatch, this._landingCenterDest);
      }
      this.DrawScores(context.SpriteBatch);
      base.Draw(context);
    }

    public void InvokePause()
    {
      EventHandler pause = this.Pause;
      if (pause == null)
        return;
      pause((object) this, EventArgs.Empty);
    }

    private void OnPauseClicked(object sender, EventArgs e) => this.InvokePause();

    public void CurrentTexturePack(WorldType type)
    {
      this._currentPack = this.GetTexturesPack(type);
    }

    private void DrawScores(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(this._scoreFont, "score", InterfaceTopPanel.PositionScore, Color.Yellow);
      spriteBatch.DrawString(this._scoreFont, this._scores, InterfaceTopPanel.PositionCount, Color.Yellow);
    }

    private InterfaceTopPanel.TexturesPack GetTexturesPack(WorldType type)
    {
      switch (type)
      {
        case WorldType.Canyon:
          return this._canyonPack;
        case WorldType.Jungle:
          return this._junglePack;
        case WorldType.Ice:
          return this._icePack;
        case WorldType.Vulcan:
          return this._vulcanPack;
        case WorldType.EnemyBase:
          return this._enemyBasePack;
        case WorldType.LandingZone:
          return this._landingPack;
        default:
          throw new ArgumentOutOfRangeException(string.Format("Unknown World Type '{0}'.", (object) type));
      }
    }

    public void HideLandingZone() => this._drawLandingZone = false;

    public void Initialize(WorldType type)
    {
      this._scoreFont = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition11");
      this._canyonPack = new InterfaceTopPanel.TexturesPack();
      this._canyonPack.Left = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level1/leftTopPanel1");
      this._canyonPack.Right = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level1/rightTopPanel1");
      this._canyonPack.Center = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level1/centerTopPanel1");
      this._junglePack = new InterfaceTopPanel.TexturesPack();
      this._junglePack.Left = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level2/leftTopPanel2");
      this._junglePack.Right = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level2/rightTopPanel2");
      this._junglePack.Center = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level2/centerTopPanel2");
      this._icePack = new InterfaceTopPanel.TexturesPack();
      this._icePack.Left = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level3/leftTopPanel3");
      this._icePack.Right = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level3/rightTopPanel3");
      this._icePack.Center = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level3/centerTopPanel3");
      this._vulcanPack = new InterfaceTopPanel.TexturesPack();
      this._vulcanPack.Left = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level4/leftTopPanel4");
      this._vulcanPack.Right = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level4/rightTopPanel4");
      this._vulcanPack.Center = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level4/centerTopPanel4");
      this._enemyBasePack = new InterfaceTopPanel.TexturesPack();
      this._enemyBasePack.Left = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level5/leftTopPanel5");
      this._enemyBasePack.Right = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level5/rightTopPanel5");
      this._enemyBasePack.Center = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level5/centerTopPanel5");
      this._landingPack = this._enemyBasePack.Copy();
      this._landingLeftSource = this._landingPack.Left.SourceRectangle;
      this._landingRightSource = this._landingPack.Right.SourceRectangle;
      this._landingCenterSource = this._landingPack.Center.SourceRectangle;
      this._leftPartPosition = Vector2.Zero;
      this._rightPartPosition = new Vector2(648f, 0.0f);
      this._centerPartPosition = new Vector2(126f, 0.0f);
      this._currentPack = this.GetTexturesPack(type);
      TextControl child1 = new TextControl("pause", this._scoreFont, Color.Yellow, new Vector2(694f, 9f));
      MenuControl child2 = new MenuControl(new Rectangle(640, 0, 160, 160));
      child2.Children.Clear();
      child2.Clicked += new EventHandler<EventArgs>(this.OnPauseClicked);
      this.AddChild((BasicControl) child1);
      this.AddChild((BasicControl) child2);
    }

    public void SetScoreCount(int scores)
    {
      this._scores = scores.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public void UpdateLandingZone(int landingZoneStart, int landingZoneEnd)
    {
      this._drawLandingZone = true;
      int num1 = Math.Max(landingZoneStart, 0);
      int num2 = Math.Min(landingZoneEnd, 800);
      if ((double) num1 < (double) this._leftPartPosition.X + 152.0 && (double) num2 > (double) this._leftPartPosition.X)
      {
        this._landingPack.Left.SourceRectangle.X = this._landingLeftSource.X + Math.Max(num1 - (int) this._leftPartPosition.X, 0);
        this._landingPack.Left.SourceRectangle.Width = Math.Max(152 - Math.Max((int) this._leftPartPosition.X + 152 - num2, 0) - (this._landingPack.Left.SourceRectangle.X - this._landingLeftSource.X), 0);
        this._landingLeftDest.X = (float) (this._landingPack.Left.SourceRectangle.X - this._landingLeftSource.X);
        this._landingLeftDest.X += (float) (int) this._leftPartPosition.X;
      }
      else
        this._landingPack.Left.SourceRectangle.Width = 0;
      if ((double) num1 < (double) this._centerPartPosition.X + 548.0 && (double) num2 > (double) this._centerPartPosition.X)
      {
        this._landingPack.Center.SourceRectangle.X = this._landingCenterSource.X + Math.Max(num1 - (int) this._centerPartPosition.X, 0);
        this._landingPack.Center.SourceRectangle.Width = Math.Max(548 - Math.Max((int) this._centerPartPosition.X + 548 - num2, 0) - (this._landingPack.Center.SourceRectangle.X - this._landingCenterSource.X), 0);
        this._landingCenterDest.X = (float) (this._landingPack.Center.SourceRectangle.X - this._landingCenterSource.X);
        this._landingCenterDest.X += (float) (int) this._centerPartPosition.X;
      }
      else
        this._landingPack.Center.SourceRectangle.Width = 0;
      if ((double) num1 < (double) this._rightPartPosition.X + 152.0 && (double) num2 > (double) this._rightPartPosition.X)
      {
        this._landingPack.Right.SourceRectangle.X = this._landingRightSource.X + Math.Max(num1 - (int) this._rightPartPosition.X, 0);
        this._landingPack.Right.SourceRectangle.Width = Math.Max(152 - Math.Max((int) this._rightPartPosition.X + 152 - num2, 0) - (this._landingPack.Right.SourceRectangle.X - this._landingRightSource.X), 0);
        this._landingRightDest.X = (float) (this._landingPack.Right.SourceRectangle.X - this._landingRightSource.X);
        this._landingRightDest.X += (float) (int) this._rightPartPosition.X;
      }
      else
        this._landingPack.Right.SourceRectangle.Width = 0;
    }

    public void UpdateWorldType(WorldType type) => this._currentPack = this.GetTexturesPack(type);

    public class TexturesPack
    {
      public Sprite Left { get; set; }

      public Sprite Right { get; set; }

      public Sprite Center { get; set; }

      public InterfaceTopPanel.TexturesPack Copy()
      {
        return new InterfaceTopPanel.TexturesPack()
        {
          Left = this.Left.Copy(),
          Right = this.Right.Copy(),
          Center = this.Center.Copy()
        };
      }
    }
  }
}
