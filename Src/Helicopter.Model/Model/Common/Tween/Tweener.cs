// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.Tween.Tweener
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Common.Tween
{
  public class Tweener
  {
    protected float _duration;
    protected TweeningFunction _tweeningFunction;
    private bool _running = true;
    private float _position;

    public event EventHandler<EventArgs> Ended;

    public event EventHandler<EventArgs> Updated;

    public void InvokeUpdated()
    {
      EventHandler<EventArgs> updated = this.Updated;
      if (updated == null)
        return;
      updated((object) this, EventArgs.Empty);
    }

    public float Position
    {
      get => this._position;
      protected set
      {
        this._position = value;
        this.InvokeUpdated();
      }
    }

    protected float _from { get; set; }

    public float From
    {
      get => this._from;
      set => this._from = value;
    }

    protected float Change { get; set; }

    public float To => this._from + this.Change;

    public float Duration => this._duration;

    protected float Elapsed { get; set; }

    public bool Running
    {
      get => this._running;
      protected set => this._running = value;
    }

    public TweeningFunction TweeningFunction => this._tweeningFunction;

    public Tweener(float from, float to, float duration, TweeningFunction tweeningFunction)
    {
      this._from = from;
      this.Position = from;
      this.Change = to - from;
      this._tweeningFunction = tweeningFunction;
      this._duration = duration;
    }

    public Tweener(float from, float to, TimeSpan duration, TweeningFunction tweeningFunction)
      : this(from, to, (float) duration.TotalSeconds, tweeningFunction)
    {
    }

    public virtual void Update(float elapsedSeconds)
    {
      if (!this.Running || (double) this.Elapsed >= (double) this.Duration)
        return;
      this.Position = this.TweeningFunction(this.Elapsed, this._from, this.Change, this.Duration);
      this.Elapsed += elapsedSeconds;
      if ((double) this.Elapsed < (double) this.Duration)
        return;
      this.Elapsed = this.Duration;
      this.Position = this._from + this.Change;
      this.OnEnd();
    }

    protected void OnEnd()
    {
      this.Running = false;
      if (this.Ended == null)
        return;
      this.Ended((object) this, new EventArgs());
    }

    public void ForceMoveToEnd()
    {
      this.Position = this.To;
      this.Running = false;
      this.Elapsed = this.Duration;
    }

    public void Reset()
    {
      this.Elapsed = 0.0f;
      this.Position = this.From;
    }

    public void Reset(float to)
    {
      this.Change = to - this.Position;
      this.Reset();
    }

    public void Reverse()
    {
      this.Elapsed = 0.0f;
      this.Change = (float) (-(double) this.Change + ((double) this._from + (double) this.Change - (double) this.Position));
      this._from = this.Position;
    }

    public void Start() => this.Running = true;

    public void Stop() => this.Running = false;

    public override string ToString()
    {
      return string.Format("Tween {0} -> {1} in {2}s. Elapsed {3:##0.##}s", (object) this._from, (object) (float) ((double) this._from + (double) this.Change), (object) this.Duration, (object) this.Elapsed);
    }

    public void Init(float from, float to, float duration, TweeningFunction tweeningFunction)
    {
      this.Elapsed = 0.0f;
      this._from = from;
      this.Position = from;
      this.Change = to - from;
      this._tweeningFunction = tweeningFunction;
      this._duration = duration;
    }

    public delegate void EndHandler();
  }
}
