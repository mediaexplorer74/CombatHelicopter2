// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.Controls.SpecialSlotTwoStateMenuControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class SpecialSlotTwoStateMenuControl : MenuControl
  {
    private bool _isActive;
    public Sprite Animated;
    public Vector2 Offset;
    public float Alpha = 1f;

    public bool IsActive
    {
      get => this._isActive;
      set
      {
        if (this._isActive == value)
          return;
        this._isActive = value;
        if (!this._isActive)
          return;
        this.OnSelectEntry();
      }
    }

    public override bool IsSelected
    {
      get => this._isSelected;
      set => this._isSelected = value;
    }

    public bool HasNew { get; set; }

    public SpecialSlotTwoStateMenuControl(
      Sprite usual,
      Sprite hasNew,
      Sprite animation,
      Vector2 position,
      Rectangle entryRectangle)
      : base(usual, hasNew, position)
    {
      this.EntryPosition = entryRectangle;
      this.Animated = (Sprite) AnimatedSprite.GetInstance();
      Rectangle rectangle = new Rectangle(0, 0, animation.Bounds.Width / 4, animation.Bounds.Height);
      ((AnimatedSprite) this.Animated).Init(animation, rectangle, rectangle, 0.2f, Vector2.Zero);
      this.Offset = new Vector2((float) (usual.Bounds.Width - rectangle.Width), (float) (usual.Bounds.Height - rectangle.Height)) / 2f;
      this.Position = position;
      this._active.Visible = false;
      this._passive.Visible = false;
      this._disabled.Visible = false;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.Visible)
        return;
      if (this.IsActive)
        this.Animated.Draw(context.SpriteBatch, context.DrawOffset + this.Offset);
      else if (this.HasNew)
        this._active.Sprite.Draw(context.SpriteBatch, context.DrawOffset);
      else
        this._passive.Sprite.Draw(context.SpriteBatch, context.DrawOffset);
      if (this.ItemTexture != null)
      {
        this.ItemTexture.Color = Color.White * this.Alpha;
        this.ItemTexture.Draw(context.SpriteBatch, context.DrawOffset);
      }
      Vector2 drawOffset = context.DrawOffset;
      for (int index = 0; index < this.ChildCount; ++index)
      {
        BasicControl child = this.Children[index];
        if (child.Visible)
        {
          context.DrawOffset = drawOffset + child.Position;
          child.Draw(context);
        }
      }
    }

    public override void Update(GameTime gametime)
    {
      this.Animated.Update((float) gametime.ElapsedGameTime.TotalSeconds);
      base.Update(gametime);
    }

    public Sprite ItemTexture { get; set; }
  }
}
