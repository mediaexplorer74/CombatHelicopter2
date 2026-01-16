// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.Scripts.StraightPlayerFly
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common.Tween;

#nullable disable
namespace Helicopter.Model.WorldObjects.Scripts
{
  internal class StraightPlayerFly : Script
  {
    private GameWorld _gameWorld;
    private Tweener _toCenter;

    public StraightPlayerFly(GameWorld gameWorld)
    {
      this._gameWorld = gameWorld;
      this._toCenter = new Tweener(this._gameWorld.Player.Position.Y, 240f, 0.5f, new TweeningFunction(Quadratic.EaseIn));
    }

    public override void Update(float elapsedSeconds)
    {
      this._gameWorld.Player.Speed.Y = -10f;
      this._gameWorld.Player.Acceleration.X = 1000f;
      this._gameWorld.UsualUpdate(elapsedSeconds);
      this._toCenter.Update(elapsedSeconds);
      this._gameWorld.Player.Position.Y = this._toCenter.Position;
      if ((double) this._gameWorld.Player.Position.X <= (double) this._gameWorld.ActiveArea.Right)
        return;
      this.OnEnd();
    }
  }
}
