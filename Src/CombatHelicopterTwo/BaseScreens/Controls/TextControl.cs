// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.TextControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class TextControl : BasicControl
  {
    public Color Color;
    private List<string> _lines = new List<string>();
    private SpriteFont font;
    private string text;
    private float _scale;

    public Vector2 Origin { get; set; }

    public string Text
    {
      get => this.text;
      set
      {
        if (!(this.text != value))
          return;
        this.text = value;
        this.CreateLines(this.text, this.MaxSymbolsPerLine);
      }
    }

    public SpriteFont Font
    {
      get => this.font;
      set
      {
        if (this.font == value)
          return;
        this.font = value;
        this.InvalidateAutoSize();
      }
    }

    public int MaxSymbolsPerLine { get; set; }

    public float Scale
    {
      get => this._scale;
      set
      {
        if ((double) Math.Abs(this._scale - value) <= 0.0099999997764825821)
          return;
        this._scale = value;
        this.Size = this.ComputeSize();
        this.InvalidateAutoSize();
      }
    }

    public TextControl()
      : this(string.Empty, (SpriteFont) null, Color.White, Vector2.Zero)
    {
    }

    public TextControl(string text, SpriteFont font)
      : this(text, font, Color.White, Vector2.Zero)
    {
    }

    public TextControl(string text, SpriteFont font, Color color)
      : this(text, font, color, Vector2.Zero)
    {
    }

    public TextControl(string text, SpriteFont font, Color color, Vector2 position)
    {
      this.Color = color;
      this.Scale = 1f;
      this.MaxSymbolsPerLine = int.MaxValue;
      this.font = font;
      this.Text = text;
      this.Position = position;
    }

    public override void Draw(DrawContext context)
    {
      base.Draw(context);
      if (this.Centered)
        this.DrawStringsCentered(context.SpriteBatch, context.DrawOffset);
      else if (this.CenteredX)
        this.DrawStringsCenteredX(context.SpriteBatch, context.DrawOffset);
      else
        this.DrawStrings(context.SpriteBatch, context.DrawOffset);
    }

    public bool Centered { get; set; }

    public bool CenteredX { get; set; }

    public override Vector2 ComputeSize()
    {
      Vector2 size = new Vector2();
      foreach (string line in this._lines)
      {
        if (!string.IsNullOrEmpty(line))
        {
          Vector2 vector2 = this.font.MeasureString(line) * this.Scale;
          size.Y += vector2.Y;
          size.X = (double) size.X > (double) vector2.X ? size.X : vector2.X;
        }
      }
      return size;
    }

    private void CreateLines(string drawText, int maxSymbolsPerLine)
    {
      this._lines.Clear();
      if (drawText.Contains("\n"))
      {
        this._lines.AddRange((IEnumerable<string>) drawText.Split(new string[1]
        {
          "\n"
        }, StringSplitOptions.RemoveEmptyEntries));
      }
      else
      {
        string[] strArray = drawText.Split(new string[1]
        {
          " "
        }, StringSplitOptions.RemoveEmptyEntries);
        string str1 = "";
        foreach (string str2 in strArray)
        {
          if (str1.Length + str2.Length > maxSymbolsPerLine)
          {
            this._lines.Add(str1);
            str1 = "";
          }
          str1 = str1 + str2 + " ";
        }
        this._lines.Add(str1);
      }
      this.Size = this.ComputeSize();
    }

    public void RebuildLines() => this.CreateLines(this.text, this.MaxSymbolsPerLine);

    public void DrawStrings(SpriteBatch spriteBatch, Vector2 leftTopPos)
    {
      float num = (float) this.font.LineSpacing * this.Scale;
      float y = leftTopPos.Y;
      for (int index = 0; index < this._lines.Count; ++index)
      {
        string line = this._lines[index];
        Vector2 position = new Vector2(leftTopPos.X, y + (float) index * num);
        spriteBatch.DrawString(this.font, line, position, this.Color, 0.0f, this.Origin * this.font.MeasureString(line), this.Scale, SpriteEffects.None, 1f);
      }
    }

    public void DrawStringsCentered(SpriteBatch spriteBatch, Vector2 centerPosition)
    {
      float num = (float) this.font.LineSpacing * this.Scale;
      float y = centerPosition.Y;
      for (int index = 0; index < this._lines.Count; ++index)
      {
        string line = this._lines[index];
        Vector2 position = new Vector2(centerPosition.X, (float) ((double) y + (double) index * (double) num - (double) this._lines.Count * (double) num / 2.0));
        spriteBatch.DrawString(this.font, line, position, this.Color, 0.0f, this.Origin * this.font.MeasureString(line), this.Scale, SpriteEffects.None, 1f);
      }
    }

    public void DrawStringsCenteredX(SpriteBatch spriteBatch, Vector2 centerPosition)
    {
      float num = (float) this.font.LineSpacing * this.Scale;
      float y = centerPosition.Y;
      for (int index = 0; index < this._lines.Count; ++index)
      {
        string line = this._lines[index];
        Vector2 vector2 = this.font.MeasureString(line);
        Vector2 position = new Vector2(centerPosition.X + (float) (((double) this.Size.X - (double) vector2.X) / 2.0), y + (float) index * num);
        spriteBatch.DrawString(this.font, line, position, this.Color, 0.0f, this.Origin * vector2, this.Scale, SpriteEffects.None, 1f);
      }
    }
  }
}
