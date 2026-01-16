// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.FixedStepHorizontalScrollPanel
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.Common.Tween;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  internal class FixedStepHorizontalScrollPanel : PanelControl
  {
    private readonly Vector2Tweener _positionTweener;
    private readonly RectangleTweener _rectangleTweener;
    private Rectangle _areaOnScreen;
    public bool IsUp = true;
    public Rectangle ViewRect = new Rectangle(0, 0, 800, 480);
    private int _currentChildrenIndex = -1;
    private MenuControl _leftArrow;
    private BasicControl _moveChildren;
    protected int _nextChildrenIndex;
    private Vector2 _offset;
    private MenuControl _rightArrow;
    private bool _isMoved;

    public Rectangle AreaOnScreen
    {
      get => this._areaOnScreen;
      set
      {
        this._areaOnScreen = value;
        this.ViewRect.Width = this._areaOnScreen.Width;
        this.ViewRect.Height = this._areaOnScreen.Height;
      }
    }

    public event EventHandler ItemChanged;

    public event EventHandler StartItemChanged;

    private int CurrentChildrenIndex
    {
      get => this._currentChildrenIndex;
      set
      {
        int currentChildrenIndex = this._currentChildrenIndex;
        this._currentChildrenIndex = (int) MathHelper.Clamp((float) value, 0.0f, (float) (this.Children.Count - 1));
        if (this._leftArrow != null)
          this._leftArrow.Visible = this._currentChildrenIndex != 0;
        if (this._rightArrow != null)
          this._rightArrow.Visible = this._currentChildrenIndex != this.Children.Count - 1;
        if (currentChildrenIndex == this._currentChildrenIndex)
          return;
        this.OnItemChanged();
      }
    }

    public BasicControl CurrentChildren
    {
      get
      {
        return this.Children[(int) MathHelper.Clamp((float) this.CurrentChildrenIndex, 0.0f, (float) (this.Children.Count - 1))];
      }
    }

    public BasicControl NextChildren
    {
      get
      {
        return this.Children[(int) MathHelper.Clamp((float) this._nextChildrenIndex, 0.0f, (float) (this.Children.Count - 1))];
      }
    }

    protected bool IsMoved
    {
      get => this._isMoved;
      set
      {
        this._isMoved = value;
        this.MotionChange(value);
      }
    }

    public override bool Visible
    {
      get => base.Visible;
      set
      {
        base.Visible = value;
        if (value)
          return;
        this._positionTweener.ForceMoveToEnd();
        this._offset = this._positionTweener.CurrentPosition;
      }
    }

    public FixedStepHorizontalScrollPanel()
    {
      this._rectangleTweener = new RectangleTweener(Rectangle.Empty, Rectangle.Empty, 0.0f, new TweeningFunction(Linear.EaseIn));
      this._rectangleTweener.Stop();
      this._rectangleTweener.Ended += new EventHandler<EventArgs>(this.OnTweenerEnd);
      this._positionTweener = new Vector2Tweener(Vector2.Zero, Vector2.Zero, 0.0f, new TweeningFunction(Linear.EaseIn));
      this._positionTweener.Stop();
      this._positionTweener.Ended += new EventHandler<EventArgs>(this.OnPositionTweenerEnd);
      this.Children = new List<BasicControl>();
      this.CurrentChildrenIndex = 0;
    }

    public override void Draw(DrawContext context)
    {
      if (!this.Visible)
        return;
      context.SpriteBatch.End();
      context.Device.Viewport = new Viewport(this.AreaOnScreen);
      context.SpriteBatch.Begin();
      context.DrawOffset.X = (float) -this.ViewRect.X;
      context.DrawOffset.Y = (float) -this.ViewRect.Y;
      context.DrawOffset += this._offset;
      Vector2 drawOffset = context.DrawOffset;
      for (int index = 0; index < this.ChildCount; ++index)
      {
        BasicControl child = this.Children[index];
        if (child.Visible && (double) child.Position.X < (double) this.ViewRect.Right && (double) child.Position.X + (double) child.Size.X > (double) this.ViewRect.Left)
        {
          context.DrawOffset = drawOffset + child.Position;
          child.Draw(context);
        }
      }
      if (this._moveChildren != null)
      {
        context.DrawOffset = drawOffset + this._moveChildren.Position;
        this._moveChildren.Draw(context);
      }
      if (this._leftArrow != null && this.CurrentChildrenIndex > 0)
      {
        context.DrawOffset = this._leftArrow.Position;
        this._leftArrow.Draw(context);
      }
      if (this._rightArrow != null && this.CurrentChildrenIndex < this.ChildCount - 1)
      {
        context.DrawOffset = this._rightArrow.Position;
        this._rightArrow.Draw(context);
      }
      context.DrawOffset = drawOffset;
      context.SpriteBatch.End();
      context.Device.Viewport = new Viewport(0, 0, 800, 480);
      context.SpriteBatch.Begin();
    }

    public override void Update(GameTime gametime)
    {
      if (!this.Visible)
        return;
      if (this._rectangleTweener.Running)
      {
        this._rectangleTweener.Update((float) gametime.ElapsedGameTime.TotalSeconds);
        this.ViewRect = this._rectangleTweener.CurrentPosition;
      }
      if (this._positionTweener.Running)
      {
        this._positionTweener.Update((float) gametime.ElapsedGameTime.TotalSeconds);
        this._offset = this._positionTweener.CurrentPosition;
      }
      if (this._leftArrow != null)
        this._leftArrow.Update(gametime);
      if (this._rightArrow != null)
        this._rightArrow.Update(gametime);
      base.Update(gametime);
    }

    public override void HandleInput(InputState input)
    {
      if (!this.Visible || !this.IsUp)
        return;
      this.touchPositionOffset = Vector2.Zero;
      if (this._leftArrow != null)
        this._leftArrow.HandleInput(input);
      if (this._rightArrow != null)
        this._rightArrow.HandleInput(input);
      this.touchPositionOffset = new Vector2((float) this.ViewRect.X, (float) this.ViewRect.Y);
      this.Children.ForEach((Action<BasicControl>) (x => x.touchPositionOffset = this.touchPositionOffset));
      if (!input.TouchState.All<TouchLocation>((Func<TouchLocation, bool>) (x => this.AreaOnScreen.Contains(new Point((int) x.Position.X, (int) x.Position.Y)))))
      {
        if (!this.IsMoved)
          return;
        this.OnDragComplete();
      }
      else
      {
        if (this.IsAnimation)
          return;
        foreach (GestureSample gesture in input.Gestures)
        {
          switch (gesture.GestureType)
          {
            case GestureType.HorizontalDrag:
              this.ViewRect.X -= (int) MathHelper.Clamp(gesture.Delta.X, -200f, 200f);
              this._nextChildrenIndex = (double) gesture.Delta.X <= 0.0 ? (int) MathHelper.Clamp((float) (this.CurrentChildrenIndex + 1), 0.0f, (float) (this.Children.Count - 1)) : (int) MathHelper.Clamp((float) (this.CurrentChildrenIndex - 1), 0.0f, (float) (this.Children.Count - 1));
              this.IsMoved = true;
              continue;
            case GestureType.Flick:
              if ((double) Math.Abs(gesture.Delta.X) > (double) Math.Abs(gesture.Delta.Y))
              {
                if ((double) gesture.Delta.X < -50.0)
                {
                  this.SlideRight();
                  continue;
                }
                if ((double) gesture.Delta.X > 50.0)
                {
                  this.SlideLeft();
                  continue;
                }
                continue;
              }
              continue;
            case GestureType.DragComplete:
              this.OnDragComplete();
              continue;
            default:
              continue;
          }
        }
        base.HandleInput(input);
      }
    }

    private bool IsAnimation => this._positionTweener.Running || this._rectangleTweener.Running;

    private void OnDragComplete()
    {
      if (this.IsAnimation)
        return;
      this.IsMoved = false;
      int num = (int) MathHelper.Clamp((float) this.ViewRect.X / (float) (int) this.Children[0].Size.X, 0.0f, (float) (this.Children.Count - 1));
      if ((double) this.ViewRect.X - (double) this.Children[num].Position.X > (double) this.Children[num].Position.X + (double) this.Children[num].Size.X - (double) this.ViewRect.X && num < this.Children.Count - 1)
        ++num;
      this.NavigateToItem(num);
    }

    protected void OnItemChanged()
    {
      if (this.ItemChanged == null)
        return;
      this.ItemChanged((object) this, EventArgs.Empty);
    }

    protected void OnStartItemChange()
    {
      if (this.StartItemChanged == null)
        return;
      this.StartItemChanged((object) this, EventArgs.Empty);
    }

    private void OnPositionTweenerEnd(object sender, EventArgs e)
    {
      if (!this.IsUp)
        return;
      this._leftArrow.Visible = this._rightArrow.Visible = true;
      this._moveChildren = (BasicControl) null;
      this.MotionChange(false);
    }

    private void OnTweenerEnd(object sender, EventArgs e)
    {
      this.CurrentChildrenIndex = this._nextChildrenIndex;
      this.MotionChange(false);
    }

    public void AddLeftArrow(MenuControl leftArrow)
    {
      this._leftArrow = this._leftArrow == null ? leftArrow : throw new Exception("left Arrow already set");
      this._leftArrow.Clicked += (EventHandler<EventArgs>) ((x, y) => this.SlideLeft());
      this._leftArrow.Parent = (BasicControl) this;
    }

    public void AddRightArrow(MenuControl rightArrow)
    {
      this._rightArrow = this._rightArrow == null ? rightArrow : throw new Exception("right Arrow already set");
      this._rightArrow.Clicked += (EventHandler<EventArgs>) ((x, y) => this.SlideRight());
      this._rightArrow.Parent = (BasicControl) this;
    }

    private void InitTweener()
    {
      if (this.IsAnimation)
        return;
      this.OnStartItemChange();
      this._rectangleTweener.Init(this.ViewRect, new Rectangle((int) this.Children[this._nextChildrenIndex].Position.X, 0, this.ViewRect.Width, this.ViewRect.Height), 0.5f, new TweeningFunction(Quadratic.EaseOut));
      this._rectangleTweener.Start();
      this.MotionChange(true);
    }

    public void InstantNavigateToItem(int itemNumber)
    {
      if (itemNumber < 0 || itemNumber >= this.Children.Count)
        return;
      this._rectangleTweener.Stop();
      this._nextChildrenIndex = this.CurrentChildrenIndex = itemNumber;
      this.ViewRect = new Rectangle((int) this.Children[itemNumber].Position.X, 0, this.ViewRect.Width, this.ViewRect.Height);
    }

    public void MoveDown(BasicControl control)
    {
      if (this.IsUp)
      {
        this.IsUp = false;
        this._positionTweener.Init(Vector2.Zero, new Vector2(0.0f, (float) this.AreaOnScreen.Height), 1f, new TweeningFunction(Quadratic.EaseInOut));
        this._positionTweener.Start();
        this.MotionChange(true);
        this.InstantNavigateToItem(this._nextChildrenIndex);
      }
      this._leftArrow.Visible = this._rightArrow.Visible = false;
      control.Position = new Vector2((float) this.ViewRect.X, control.Position.Y);
      this._moveChildren = control;
    }

    public void MoveUp()
    {
      this.IsUp = true;
      this._positionTweener.Init(new Vector2(0.0f, (float) this.AreaOnScreen.Height), Vector2.Zero, 1f, new TweeningFunction(Quadratic.EaseInOut));
      this._positionTweener.Start();
      this.MotionChange(true);
      this.InstantNavigateToItem(this._nextChildrenIndex);
    }

    public void InstantMoveUp()
    {
      this.IsUp = true;
      this._offset = Vector2.Zero;
    }

    public void NavigateToItem(int itemNumber)
    {
      this._nextChildrenIndex = itemNumber;
      this.InitTweener();
    }

    public void SlideLeft()
    {
      if (this.IsAnimation || !this.IsUp || this.CurrentChildrenIndex <= 0)
        return;
      this._nextChildrenIndex = this.CurrentChildrenIndex - 1;
      this.InitTweener();
    }

    public void SlideRight()
    {
      if (this.IsAnimation || !this.IsUp || this.CurrentChildrenIndex >= this.Children.Count - 1)
        return;
      this._nextChildrenIndex = this.CurrentChildrenIndex + 1;
      this.InitTweener();
    }

    protected virtual void MotionChange(bool state)
    {
    }
  }
}
