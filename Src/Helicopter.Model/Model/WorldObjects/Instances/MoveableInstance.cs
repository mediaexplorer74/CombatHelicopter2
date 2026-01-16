// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.MoveableInstance
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances
{
  public abstract class MoveableInstance : Instance
  {
    protected const float MinimumSpeed = -5000f;
    protected const float MaximumSpeed = 5000f;
    public Vector2 Acceleration = Vector2.Zero;
    public Vector2 Speed;
    private int _maximumVerticalPosition;
    private int _minimumVerticalPosition;

    public override void Update(float elapsedSeconds)
    {
      this.UpdateSpeed(elapsedSeconds);
      this.UpdatePosition(elapsedSeconds);
      this.UpdateState(elapsedSeconds);
    }

    public override void Init(Pattern pattern)
    {
      base.Init(pattern);
      this._minimumVerticalPosition = -this.Contour.Rectangle.Height;
      this._maximumVerticalPosition = 480 + this.Contour.Rectangle.Height;
    }

    private void UpdatePosition(float elapsedSeconds)
    {
      Vector2 position = this.Position + this.Speed * elapsedSeconds;
      position.Y = MathHelper.Clamp(position.Y, (float) this._minimumVerticalPosition, (float) this._maximumVerticalPosition);
      this.SetPosition(position);
    }

    protected virtual void UpdateSpeed(float elapsedSeconds)
    {
      this.Speed += this.Acceleration * elapsedSeconds;
    }

    protected abstract void UpdateState(float elapsedSeconds);

    public override void ResetState()
    {
      base.ResetState();
      this.Speed = Vector2.Zero;
      this.Acceleration = Vector2.Zero;
    }
  }
}
