// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Instance
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Primitives;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  public abstract class Instance : IReusable
  {
    public static bool ShowCredits;
    public Vector2 PrevPos;
    public Vector2 Position;
    private GameWorld _gameWorld;
    private int _state;

    public event EventHandler<StateChangeEventArgs<int>> StateChanged;

    public int Id { get; set; }

    public Pattern Pattern { get; set; }

    public float ZIndex { get; set; }

    public Contour Contour { get; set; }

    public Contour IncreasedContour { get; set; }

    public Reaction Reaction { get; set; }

    public bool IsTemporary { get; set; }

    public bool IsNeedRemove { get; set; }

    public GameWorld GameWorld
    {
      get => this._gameWorld;
      set
      {
        if (value == null && this._gameWorld != null)
          this._gameWorld.RemoveInstance(this);
        this._gameWorld = value;
      }
    }

    public float Rotation { get; set; }

    public int State
    {
      get => this._state;
      set
      {
        if (this._state == value)
          return;
        int state = this._state;
        this._state = value;
        this.ChangeState(state, value);
      }
    }

    protected Instance()
    {
      this.Contour = new Contour();
      this.IncreasedContour = new Contour();
    }

    public virtual void ResetState()
    {
      this.Id = 0;
      this.ZIndex = 0.0f;
      this.IsTemporary = false;
      this.Rotation = 0.0f;
      this.IsNeedRemove = false;
      this.PrevPos = Vector2.Zero;
      this.Position = Vector2.Zero;
      this.Contour.Clear();
      this.IncreasedContour.Clear();
      this.State = 0;
      this.StateChanged = (EventHandler<StateChangeEventArgs<int>>) null;
      this.Reaction = (Reaction) null;
      this.Pattern = (Pattern) null;
      this.GameWorld = (GameWorld) null;
    }

    public virtual void Update(float elapsedSeconds)
    {
    }

    protected void ChangeState(int oldState, int newState)
    {
      if (this.StateChanged == null)
        return;
      this.StateChanged((object) this, new StateChangeEventArgs<int>(oldState, newState));
    }

    public virtual void Init(Pattern pattern)
    {
      this.Pattern = pattern;
      this.Contour.CopyFrom(pattern.Contour);
      this.IncreasedContour.CopyFrom(pattern.Contour);
      this.IncreasedContour.Increase(20);
      this.Contour.SetLocation((int) this.Position.X, (int) this.Position.Y);
      this.IncreasedContour.SetLocation((int) this.Position.X, (int) this.Position.Y);
      this.StateChanged += new EventHandler<StateChangeEventArgs<int>>(this.OnStateChanged);
    }

    protected virtual void OnStateChanged(object sender, StateChangeEventArgs<int> e)
    {
    }

    public bool Intersects(Instance obj) => this.Contour.Intersects(obj.Contour);

    public bool IntersectsIncreasedContour(Instance obj)
    {
      return this.IncreasedContour.Intersects(obj.IncreasedContour);
    }

    public virtual void Release()
    {
    }

    public void SetPosition(Vector2 position) => this.SetPosition(position.X, position.Y);

    public void SetPosition(float x, float y)
    {
      this.PrevPos = this.Position;
      this.Position.X = x;
      this.Position.Y = y;
      this.Contour.SetLocation((int) x, (int) y);
      this.IncreasedContour.SetLocation((int) x, (int) y);
    }
  }
}
