// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.MenuControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class MenuControl : BasicControl
  {
    protected internal TexturedControl _active;
    protected TexturedControl _disabled;
    protected bool _isSelected;
    protected bool _isdisabled;
    protected internal TexturedControl _passive;

    public event EventHandler<EventArgs> Clicked;

    public Rectangle EntryPosition { get; set; }

    public Vector2 Origin { get; set; }

    public override Vector2 Size
    {
      get => base.Size;
      set
      {
        base.Size = value;
        this.EntryPosition = new Rectangle((int) this.Position.X, (int) this.Position.Y, (int) this.Size.X, (int) this.Size.Y);
      }
    }

    public override Vector2 Position
    {
      get => base.Position;
      set
      {
        base.Position = value;
        this.EntryPosition = new Rectangle((int) this.Position.X, (int) this.Position.Y, (int) this.Size.X, (int) this.Size.Y);
      }
    }

    public virtual bool IsSelected
    {
      get => this._isSelected;
      set
      {
        this._isSelected = value;
        if (this.IsDisabled)
          return;
        this._active.Visible = value;
        this._passive.Visible = !value;
      }
    }

    public bool IsDisabled
    {
      get => this._isdisabled;
      set
      {
        this._isdisabled = value;
        if (value)
        {
          this._disabled.Visible = true;
          this._active.Visible = this._passive.Visible = false;
        }
        else
        {
          this._disabled.Visible = false;
          this.IsSelected = this._isSelected;
        }
      }
    }

    public MenuControl(Sprite passive, Sprite active, Vector2 position)
    {
      this.CreateControls(passive, active, (Sprite) null);
      this.Position = position;
      this.Size = passive.SourceSize;
    }

    private void CreateControls(Sprite passive, Sprite active, Sprite disabled)
    {
      Sprite blankSprite = ResourcesManager.BlankSprite;
      if (this._passive == null)
      {
        this._passive = new TexturedControl(passive ?? blankSprite, Vector2.Zero);
        this.AddChild((BasicControl) this._passive);
      }
      else
        this._passive.Sprite = passive ?? blankSprite;
      if (this._active == null)
      {
        this._active = new TexturedControl(active ?? blankSprite, Vector2.Zero);
        this.AddChild((BasicControl) this._active);
      }
      else
        this._active.Sprite = active ?? blankSprite;
      if (this._disabled == null)
      {
        this._disabled = new TexturedControl(disabled ?? blankSprite, Vector2.Zero);
        this.AddChild((BasicControl) this._disabled);
      }
      else
        this._disabled.Sprite = disabled ?? blankSprite;
      this.IsSelected = false;
      this.IsDisabled = false;
    }

    public MenuControl(Rectangle entryPosition)
    {
      this.CreateControls((Sprite) null, (Sprite) null, (Sprite) null);
      this.Position = new Vector2((float) entryPosition.X, (float) entryPosition.Y);
      this.Size = new Vector2((float) entryPosition.Width, (float) entryPosition.Height);
      this.IsSelected = false;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.Visible)
        return;
      base.Draw(context);
    }

    public override void HandleInput(InputState input)
    {
      if (!this.Visible || this.IsDisabled)
        return;
      Rectangle entryPosition = this.EntryPosition;
      if (this.Parent != null)
        entryPosition.Offset((int) this.Parent.AbsolutePosition.X, (int) this.Parent.AbsolutePosition.Y);
      entryPosition.Offset(-(int) this.Origin.X, -(int) this.Origin.Y);
      foreach (TouchLocation touchLocation in input.TouchState)
      {
        Vector2 position = touchLocation.Position;
        if (this.Parent != null)
          position += this.Parent.touchPositionOffset;
        switch (touchLocation.State)
        {
          case TouchLocationState.Invalid:
          case TouchLocationState.Moved:
            continue;
          case TouchLocationState.Released:
            if (entryPosition.Contains((int) position.X, (int) position.Y) && this.IsSelected)
              this.OnSelectEntry();
            this.IsSelected = false;
            continue;
          case TouchLocationState.Pressed:
            if (entryPosition.Contains((int) position.X, (int) position.Y))
            {
              this.IsSelected = true;
              continue;
            }
            continue;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      base.HandleInput(input);
    }

    protected internal virtual void OnSelectEntry()
    {
      if (this.Clicked == null)
        return;
      this.Clicked((object) this, EventArgs.Empty);
    }

    public MenuControl Clone()
    {
      return new MenuControl(this._passive.Sprite, this._active.Sprite, this.Position)
      {
        _disabled = this._disabled,
        IsDisabled = this.IsDisabled,
        EntryPosition = this.EntryPosition
      };
    }

    public void SetDisableButton(Sprite disable) => this._disabled.Sprite = disable;
  }
}
