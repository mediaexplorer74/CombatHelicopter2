// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Buttons.FlyingCloudControl
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Screen.MapScreen.Buttons
{
  internal class FlyingCloudControl : BasicControl
  {
    private Sprite _cloudTexture;
    private float _alpha;
    private Tweener _alphaTweener;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector2 _cloudPosition;
    private Vector2Tweener _positionTweener;
    private float _angle;
    private Rectangle _startArea;

    public override void Update(GameTime gametime)
    {
      base.Update(gametime);
      float totalSeconds = (float) gametime.ElapsedGameTime.TotalSeconds;
      if (this._alphaTweener.Running)
      {
        this._alphaTweener.Update(totalSeconds);
        this._alpha = this._alphaTweener.Position;
        this._cloudTexture.Color = Color.White * this._alpha;
      }
      if (!this._positionTweener.Running)
        return;
      this._positionTweener.Update(totalSeconds);
      this._cloudPosition = this._positionTweener.CurrentPosition;
    }

    public FlyingCloudControl()
    {
      this._alphaTweener = new Tweener(0.0f, 0.0f, 0.0f, new TweeningFunction(Linear.EaseIn));
      this._positionTweener = new Vector2Tweener(Vector2.Zero, Vector2.Zero, 0.0f, new TweeningFunction(Linear.EaseIn));
      this._positionTweener.Ended += (EventHandler<EventArgs>) ((x, y) =>
      {
        this._positionTweener.Init(this.RandomVectorInRectangle(this._startArea), new Vector2(this._startPosition.X - 480f * (float) Math.Tan((double) this._angle), 480f), this._positionTweener.Duration, this._positionTweener.TweeningFunction);
        this._positionTweener.Start();
        this._alphaTweener.Reset();
        this._alphaTweener.Start();
      });
    }

    public override void Draw(DrawContext context)
    {
      this._cloudTexture.Draw(context.SpriteBatch, this._cloudPosition);
      base.Draw(context);
    }

    public void Init(
      Sprite cloud,
      Rectangle startArea,
      float angle,
      float minTime,
      float maxTime,
      float minAlphaTime,
      float maxAlphaTime)
    {
      this._cloudTexture = cloud;
      this._angle = angle;
      this._startArea = startArea;
      this._startPosition = this.RandomVectorInRectangle(startArea);
      this._endPosition.X = this._startPosition.X - (float) startArea.Height * (float) Math.Tan((double) this._angle);
      this._endPosition.Y = (float) startArea.Height;
      this._alphaTweener.Init(0.0f, 1f, minAlphaTime + (float) (CommonRandom.Instance.Random.NextDouble() * ((double) maxAlphaTime - (double) minAlphaTime)), new TweeningFunction(Linear.EaseInOut));
      this._alphaTweener.Start();
      this._positionTweener.Init(this._startPosition, this._endPosition, minTime + (float) (CommonRandom.Instance.Random.NextDouble() * ((double) maxTime - (double) minTime)), new TweeningFunction(Linear.EaseInOut));
      this._positionTweener.Start();
    }

    private Vector2 RandomVectorInRectangle(Rectangle startArea)
    {
      return new Vector2()
      {
        X = (float) CommonRandom.Instance.Random.Next(startArea.Left, startArea.Right),
        Y = (float) CommonRandom.Instance.Random.Next(startArea.Top, startArea.Bottom)
      };
    }
  }
}
