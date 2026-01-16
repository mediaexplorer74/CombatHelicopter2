// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.CopterSpriteObject
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
  internal class CopterSpriteObject : SpriteObject
  {
    private static readonly ObjectPool<CopterSpriteObject> _pool = new ObjectPool<CopterSpriteObject>((ICreation<CopterSpriteObject>) new CopterSpriteObject.Creator());
    private readonly Tweener _alphaNumberTweener;
    private readonly Tweener _alphaTweener;
    private ISpriteObject _damagedSprite;
    private CommonAnimatedSprite _deathSprite;
    private string _price;
    private SpriteFont _priceFont;
    private Vector2 _numberAcceleration = new Vector2(0.0f, 0.0f);
    private Vector2 _numberVelocity = new Vector2(0.0f, -100f);
    private Vector2 _numberPosition;

    public static CopterSpriteObject GetInstance() => CopterSpriteObject._pool.GetObject();

    protected override void ReleaseFromPool() => CopterSpriteObject._pool.Release(this);

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

    protected bool IsVisible { get; set; }

    protected CopterSpriteObject()
    {
      this.IsVisible = true;
      this._alphaTweener = new Tweener(0.0f, 1f, 0.15f, new TweeningFunction(Bounce.EaseIn));
      this._alphaNumberTweener = new Tweener(0.0f, 1f, 0.28f, new TweeningFunction(Linear.EaseIn));
      this._alphaNumberTweener.Ended += new EventHandler<EventArgs>(this.OnAlphaNumberTweenerOnEnded);
      this._alphaTweener.Stop();
      this._alphaTweener.Ended += new EventHandler<EventArgs>(this.OnTweenerEnd);
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      if (!this.IsVisible)
        return;
      if (this.Instance.State == 1)
      {
        Vector2 position = this.Position;
        position.X += (float) this.Instance.Contour.Rectangle.Width / 2f;
        position.Y += (float) this.Instance.Contour.Rectangle.Height / 2f;
        this.DeathSprite.Draw(spriteBatch, position);
        if (!Instance.ShowCredits)
          return;
        this.DrawPrice(spriteBatch);
      }
      else
      {
        base.Draw(spriteBatch, parentPosition);
        Copter instance = (Copter) this.Instance;
        if ((double) Math.Abs(instance.Energy - instance.MaxEnergy) > 0.0099999997764825821 && this._damagedSprite != null)
          this._damagedSprite.Draw(spriteBatch, this.Position);
        if (!this._alphaTweener.Running)
          return;
        this.Children[0].Sprite.Color = Color.Red * this._alphaTweener.Position;
        this.Children[0].Draw(spriteBatch, this.Position);
        this.Children[0].Sprite.Color = Color.White;
      }
    }

    public override void Update(Camera camera, float elapsedSeconds)
    {
      if (!this.IsVisible)
        return;
      base.Update(camera, elapsedSeconds);
      if (this.Instance.State == 1)
      {
        this.DeathSprite.Update(elapsedSeconds);
        this._alphaNumberTweener.Update(elapsedSeconds);
      }
      this._numberVelocity += this._numberAcceleration * elapsedSeconds;
      this._numberPosition += this._numberVelocity * elapsedSeconds;
      if (!this._alphaTweener.Running)
        return;
      this._alphaTweener.Update(elapsedSeconds);
    }

    private void OnAlphaNumberTweenerOnEnded(object x, EventArgs y)
    {
      this._alphaNumberTweener.Init(1f, 0.0f, 0.28f, new TweeningFunction(Linear.EaseOut));
      this._alphaNumberTweener.Ended -= new EventHandler<EventArgs>(this.OnAlphaNumberTweenerOnEnded);
      this._numberAcceleration = new Vector2(0.0f, 50f);
      this._alphaNumberTweener.Start();
    }

    private void OnDamaged(object sender, PlayerEventArgs e) => this._alphaTweener.Start();

    private void OnDeathSpriteEnded(object x, EventArgs y)
    {
      this.IsVisible = false;
      this.Instance.IsNeedRemove = true;
    }

    public override void OnStateChanged(object instance, StateChangeEventArgs<int> stateChangeEvent)
    {
      if (stateChangeEvent.NextState != 1)
        return;
      this._alphaNumberTweener.Start();
      this._numberPosition = this.Position;
      this._numberPosition.X += (float) this.Instance.Contour.Rectangle.Width / 2f;
      this._numberPosition.Y += (float) this.Instance.Contour.Rectangle.Height / 2f;
    }

    private void OnTweenerEnd(object sender, EventArgs e) => this._alphaTweener.Reset();

    public override void AddChildren(ISpriteObject spriteObject)
    {
      if (spriteObject.SpriteID == "CopterDamaged")
        this._damagedSprite = spriteObject;
      else
        base.AddChildren(spriteObject);
    }

    private void DrawPrice(SpriteBatch spriteBatch)
    {
      Color color = Color.Black * this._alphaNumberTweener.Position;
      spriteBatch.DrawString(this._priceFont, this._price, this._numberPosition, color);
    }

    public override void Init(Instance instance)
    {
      if (!(instance is Copter))
        throw new ArgumentOutOfRangeException(nameof (instance), "It can be only Copter type");
      ((Copter) instance).Damaged += new EventHandler<PlayerEventArgs>(this.OnDamaged);
      this._priceFont = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition20");
      this._price = string.Format("+&{0}", (object) ((HelicopterPattern) instance.Pattern).Price);
      base.Init(instance);
    }

    public override void UpdateRotation()
    {
      base.UpdateRotation();
      if (this._damagedSprite == null)
        return;
      this._damagedSprite.Rotation = this.Rotation;
    }

    public override void ResetState()
    {
      this._deathSprite.Release();
      this._deathSprite = (CommonAnimatedSprite) null;
      if (this._damagedSprite != null)
      {
        this._damagedSprite.Release();
        this._damagedSprite = (ISpriteObject) null;
      }
      this.IsVisible = true;
      this._alphaTweener.Reset();
      this._alphaNumberTweener.Init(0.0f, 1f, 0.28f, new TweeningFunction(Linear.EaseIn));
      this._alphaNumberTweener.Ended += new EventHandler<EventArgs>(this.OnAlphaNumberTweenerOnEnded);
      this._numberPosition = Vector2.Zero;
      this._numberAcceleration = Vector2.Zero;
      this._numberVelocity = new Vector2(0.0f, -100f);
      base.ResetState();
    }

    protected class Creator : ICreation<CopterSpriteObject>
    {
      public CopterSpriteObject Create() => new CopterSpriteObject();
    }
  }
}
