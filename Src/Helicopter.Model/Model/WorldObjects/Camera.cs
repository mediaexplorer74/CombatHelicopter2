// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Camera
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common.Tween;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  public class Camera
  {
    private const float MaxPlayerOffset = 200f;
    private const float MaxLayerOffset = 20f;
    public Rectangle Screen;
    private Point _playerCenter;
    private Rectangle _playerDest;
    private Tweener _tweener;
    private bool _isShaking;
    public static int NumberOfShakes = 2;
    public static int CurrentNumberOfShakes;
    public static int FromShake = -5;
    public static int ToShake = 5;
    public static float DurationShake = 0.15f;

    public Camera()
    {
      this.Screen = new Rectangle(0, 0, 800, 480);
      this._tweener = new Tweener((float) Camera.FromShake, (float) Camera.ToShake, Camera.DurationShake, new TweeningFunction(Quadratic.EaseInOut));
      this._tweener.Ended += (EventHandler<EventArgs>) ((x, y) =>
      {
        this._tweener.Reverse();
        this._tweener.Start();
        ++Camera.CurrentNumberOfShakes;
        if (Camera.CurrentNumberOfShakes != Camera.NumberOfShakes)
          return;
        this.IsShaking = false;
      });
    }

    public Rectangle FromWorldToScreen(Rectangle rectangleInWorld) => rectangleInWorld;

    public void UpdateCamera(Rectangle activeArea, Rectangle playerDest, float elapsedSeconds)
    {
      if (!this.IsShaking)
      {
        this.Screen.X = activeArea.X;
        this._playerDest = playerDest;
        this._playerCenter.X = this._playerDest.X + this._playerDest.Width / 2;
        this._playerCenter.Y = this._playerDest.Y + this._playerDest.Height / 2;
        this.Screen.Y = (int) (float) (((double) (activeArea.Y + activeArea.Height / 2) - (double) this._playerCenter.Y) / 200.0 * 20.0);
      }
      else
      {
        this._tweener.Update(elapsedSeconds);
        this.Screen.X = activeArea.X;
        this.Screen.Y = (int) this._tweener.Position;
      }
    }

    public bool IsShaking
    {
      get => this._isShaking;
      set
      {
        this._isShaking = value;
        if (!this._isShaking)
          return;
        Camera.CurrentNumberOfShakes = 0;
      }
    }
  }
}
