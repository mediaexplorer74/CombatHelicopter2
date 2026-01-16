// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.CannonSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class CannonSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<CannonSpriteObject> _pool = new ObjectPool<CannonSpriteObject>((ICreation<CannonSpriteObject>) new CannonSpriteObject.Creator());
    private readonly Tweener _alphaNumberTweener;
    public Vector2 FireAfterDeadPosition;
    private ISpriteObject _deadSpriteObject;
    private CommonAnimatedSprite _deathSprite;
    private string _price;
    private SpriteFont _priceFont;
    private bool _isSecondStage;

    public static CannonSpriteObject GetInstance() => CannonSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => CannonSpriteObject._pool.Release(this);

    public CommonAnimatedSprite DeathSprite
    {
      get => this._deathSprite;
      set
      {
        this._deathSprite = value;
        this._deathSprite.Ended -= new EventHandler(this.OnDeathSpriteEnded);
        this._deathSprite.Ended += new EventHandler(this.OnDeathSpriteEnded);
      }
    }

    public CommonAnimatedSprite FireAfterDeadSprite { get; set; }

    protected CannonSpriteObject()
    {
      this._alphaNumberTweener = new Tweener(0.0f, 1f, 0.4f, new TweeningFunction(Linear.EaseIn));
      this._alphaNumberTweener.Ended += new EventHandler<EventArgs>(this.OnAlphaNumberTweenerOnEnded);
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      if (this.Instance.State == 1)
      {
        this._deadSpriteObject.Draw(spriteBatch, this.Position);
        Vector2 position = this.Position;
        position.X += (float) this.Instance.Contour.Rectangle.Width / 2f;
        if (((Cannon) this.Instance).CannonPattern.Alignment == VerticalAlignment.Top)
          position.Y += (float) (3.0 * (double) this.Instance.Contour.Rectangle.Height / 4.0);
        else
          position.Y += (float) this.Instance.Contour.Rectangle.Height / 6f;
        this.DeathSprite.Draw(spriteBatch, position);
        if (Instance.ShowCredits)
          this.DrawPrice(spriteBatch, position);
        if (this.FireAfterDeadSprite == null)
          return;
        this.FireAfterDeadSprite.Draw(spriteBatch, position + this.FireAfterDeadPosition);
      }
      else
        base.Draw(spriteBatch, parentPosition);
    }

    public override void Update(Camera camera, float elapsedSeconds)
    {
      if (this.Instance.State == 1)
      {
        this._alphaNumberTweener.Update(elapsedSeconds);
        this.DeathSprite.Update(elapsedSeconds);
        if (this.FireAfterDeadSprite != null)
          this.FireAfterDeadSprite.Update(elapsedSeconds);
      }
      base.Update(camera, elapsedSeconds);
      this._deadSpriteObject.Update(camera, elapsedSeconds);
    }

    private void OnDeathSpriteEnded(object sender, EventArgs e)
    {
      this._deathSprite.Visible = false;
    }

    private void OnAlphaNumberTweenerOnEnded(object x, EventArgs y)
    {
      this._alphaNumberTweener.Init(0.0f, 1f, 0.15f, new TweeningFunction(Linear.EaseIn));
      this._alphaNumberTweener.Ended -= new EventHandler<EventArgs>(this.OnAlphaNumberTweenerOnEnded);
      this._isSecondStage = true;
      this._alphaNumberTweener.Start();
    }

    public override void OnStateChanged(object instance, StateChangeEventArgs<int> stateChangeEvent)
    {
      if (stateChangeEvent.NextState != 1)
        return;
      this._alphaNumberTweener.Start();
    }

    public override void AddChildren(ISpriteObject spriteObject)
    {
      if (spriteObject.SpriteID == "CannonDead")
        this._deadSpriteObject = spriteObject;
      else
        base.AddChildren(spriteObject);
    }

    private void DrawPrice(SpriteBatch spriteBatch, Vector2 position)
    {
      if (this._isSecondStage)
      {
        position += new Vector2(0.0f, -100f) + new Vector2(0.0f, -100f) * this._alphaNumberTweener.Position;
        Color color = Color.Black * (1f - this._alphaNumberTweener.Position);
        spriteBatch.DrawString(this._priceFont, this._price, position, color);
      }
      else
      {
        position += new Vector2(0.0f, -100f) * this._alphaNumberTweener.Position;
        Color color = Color.Black * this._alphaNumberTweener.Position;
        spriteBatch.DrawString(this._priceFont, this._price, position, color);
      }
    }

    public override void ResetState()
    {
      this.DeathSprite.Release();
      this._deathSprite = (CommonAnimatedSprite) null;
      this._deadSpriteObject.Release();
      this._deathSprite = (CommonAnimatedSprite) null;
      if (this.FireAfterDeadSprite != null)
        this.FireAfterDeadSprite.Release();
      this.FireAfterDeadSprite = (CommonAnimatedSprite) null;
      this.FireAfterDeadPosition = Vector2.Zero;
      this._alphaNumberTweener.Init(0.0f, 1f, 0.4f, new TweeningFunction(Linear.EaseIn));
      this._alphaNumberTweener.Ended += new EventHandler<EventArgs>(this.OnAlphaNumberTweenerOnEnded);
      this._isSecondStage = false;
      base.ResetState();
    }

    public override void Init(Instance instance)
    {
      if (!(instance is Cannon))
        throw new ArgumentOutOfRangeException(nameof (instance), "It can be only Cannon type");
      this._priceFont = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition20");
      this._price = string.Format("+&{0}", (object) ((CannonPattern) instance.Pattern).Price);
      base.Init(instance);
    }

    protected class Creator : ICreation<CannonSpriteObject>
    {
      public CannonSpriteObject Create() => new CannonSpriteObject();
    }
  }
}
