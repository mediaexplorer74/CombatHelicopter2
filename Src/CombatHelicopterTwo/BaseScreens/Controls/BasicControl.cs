// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.BasicControl
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
  public class BasicControl
  {
    private Vector2 _position;
    private Vector2 size;
    private bool sizeValid;
    private bool autoSize = true;
    public List<BasicControl> Children;
    public Vector2 touchPositionOffset;

    public virtual bool Visible { get; set; }

    public virtual Vector2 Position
    {
      get => this._position;
      set
      {
        this._position = value;
        if (this.Parent == null)
          return;
        this.Parent.InvalidateAutoSize();
      }
    }

    public virtual Vector2 Size
    {
      get
      {
        if (!this.sizeValid)
        {
          this.size = this.ComputeSize();
          this.sizeValid = true;
        }
        return this.size;
      }
      set
      {
        this.size = value;
        this.sizeValid = true;
        this.autoSize = false;
        if (this.Parent == null)
          return;
        this.Parent.InvalidateAutoSize();
      }
    }

    protected void InvalidateAutoSize()
    {
      if (!this.autoSize)
        return;
      this.sizeValid = false;
      if (this.Parent == null)
        return;
      this.Parent.InvalidateAutoSize();
    }

    public BasicControl Parent { get; set; }

    public int ChildCount => this.Children != null ? this.Children.Count : 0;

    public BasicControl this[int childIndex] => this.Children[childIndex];

    public Vector2 AbsolutePosition
    {
      get
      {
        Vector2 position = this.Position;
        if (this.Parent != null)
          position += this.Parent.AbsolutePosition;
        return position;
      }
    }

    public virtual bool Enabled { get; set; }

    public void AddChild(BasicControl child) => this.AddChild(child, this.ChildCount);

    public void AddChild(BasicControl child, int index)
    {
      child.Parent = this;
      if (this.Children == null)
        this.Children = new List<BasicControl>();
      this.Children.Insert(index, child);
      this.OnChildAdded(index, child);
    }

    public void RemoveChildAt(int index)
    {
      BasicControl child = this.Children[index];
      child.Parent = (BasicControl) null;
      this.Children.RemoveAt(index);
      this.OnChildRemoved(index, child);
    }

    public void RemoveChild(BasicControl child)
    {
      if (this.Children == null || !this.Children.Contains(child))
        return;
      if (child.Parent != this)
        throw new InvalidOperationException();
      this.RemoveChildAt(this.Children.IndexOf(child));
    }

    public virtual void Draw(DrawContext context)
    {
      Vector2 drawOffset = context.DrawOffset;
      for (int index = 0; index < this.ChildCount; ++index)
      {
        if (this.Children[index].Visible)
        {
          context.DrawOffset = drawOffset + this.Children[index].Position;
          this.Children[index].Draw(context);
        }
      }
    }

    public virtual void Update(GameTime gametime)
    {
      if (!this.Enabled)
        return;
      for (int index = 0; index < this.ChildCount; ++index)
        this.Children[index].Update(gametime);
    }

    public virtual void HandleInput(InputState input)
    {
      if (!this.Enabled)
        return;
      for (int index = 0; index < this.ChildCount; ++index)
        this.Children[index].HandleInput(input);
    }

    public virtual Vector2 ComputeSize()
    {
      if (this.Children == null || this.Children.Count == 0)
        return Vector2.Zero;
      Vector2 size = this.Children[0].Position + this.Children[0].Size;
      for (int index = 1; index < this.Children.Count; ++index)
      {
        Vector2 vector2 = this.Children[index].Position + this.Children[index].Size;
        size.X = Math.Max(size.X, vector2.X);
        size.Y = Math.Max(size.Y, vector2.Y);
      }
      return size;
    }

    protected virtual void OnChildAdded(int index, BasicControl child) => this.InvalidateAutoSize();

    protected virtual void OnChildRemoved(int index, BasicControl child)
    {
      this.InvalidateAutoSize();
    }

    public static void BatchDraw(
      BasicControl control,
      GraphicsDevice device,
      SpriteBatch spriteBatch,
      Vector2 offset,
      GameTime gameTime)
    {
      if (control == null || !control.Visible)
        return;
      control.Draw(new DrawContext()
      {
        Device = device,
        SpriteBatch = spriteBatch,
        DrawOffset = offset + control.Position,
        GameTime = gameTime
      });
    }

    public BasicControl()
    {
      this.Visible = true;
      this.Enabled = true;
    }

    public void RemoveAllChilds()
    {
      if (this.Children == null)
        return;
      while (this.Children.Count > 0)
        this.RemoveChildAt(0);
    }
  }
}
