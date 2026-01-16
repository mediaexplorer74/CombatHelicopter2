// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.LevelProgressControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class LevelProgressControl : BasicControl
  {
    private readonly Sprite _copterSign;
    private readonly Sprite _emptyLine;
    private readonly Sprite _fullLine;
    private Vector2 _copterPosition;
    private Rectangle _fullLineSourceRectangle;
    private float _progress;

    public float Progress
    {
      get => this._progress;
      set
      {
        this._progress = value;
        this._copterPosition = new Vector2(this.Position.X + (float) this._fullLineSourceRectangle.Width * this._progress, this.Position.Y);
        this._fullLine.SourceRectangle.Width = (int) ((double) this._fullLineSourceRectangle.Width * (double) this._progress);
      }
    }

    public LevelProgressControl(
      Sprite fullLine,
      Sprite emptyLine,
      Sprite copter,
      Vector2 position)
    {
      this._fullLine = fullLine;
      this._fullLineSourceRectangle = this._fullLine.SourceRectangle;
      this._emptyLine = emptyLine;
      if (copter != null)
      {
        this._copterSign = copter;
        this._copterSign.Origin = this._copterSign.SourceSize / 2f;
      }
      this.Position = position;
      this.Progress = 0.0f;
    }

    public override void Draw(DrawContext context)
    {
      this._emptyLine.Draw(context.SpriteBatch, this.Position);
      this._fullLine.Draw(context.SpriteBatch, this.Position);
      if (this._copterSign == null)
        return;
      this._copterSign.Draw(context.SpriteBatch, this._copterPosition);
    }
  }
}
