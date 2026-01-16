// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.MothershipCopterSpriteObject
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Sounds;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  internal class MothershipCopterSpriteObject : CopterSpriteObject
  {
    private static readonly ObjectPool<MothershipCopterSpriteObject> _pool = new ObjectPool<MothershipCopterSpriteObject>((ICreation<MothershipCopterSpriteObject>) new MothershipCopterSpriteObject.Creator());
    private SimpleSpriteObject _droidDoor;
    private ISpriteObject _body1;
    private ISpriteObject _body2;
    private ISpriteObject _body3;
    private ISpriteObject _body4;
    private ISpriteObject _propeller1;
    private ISpriteObject _propeller2;
    private ISpriteObject _door1;
    private ISpriteObject _door2;
    private ISpriteObject _door3;
    private ISpriteObject _door4;
    private Vector2 _defaultDoorPosition = Vector2.Zero;
    private CommonAnimatedSprite _deadBreath;
    private CommonAnimatedSprite _fire;
    private CommonAnimatedSprite _sparks;
    private MothershipCopter _mothership;
    private readonly Vector2 _deadBreathPosition = new Vector2(0.0f, 142f);
    private readonly Vector2 _firePosition = new Vector2(170f, 10f);
    private readonly Vector2 _sparksPosition = new Vector2(310f, 80f);
    private bool _explosionsStarted;
    private float _explosionElapsedTime;
    private List<MothershipCopterSpriteObject.AnimationInfo> _smallExplosions = new List<MothershipCopterSpriteObject.AnimationInfo>();

    public static MothershipCopterSpriteObject GetInstance()
    {
      return MothershipCopterSpriteObject._pool.GetObject();
    }

    protected override void ReleaseFromPool() => MothershipCopterSpriteObject._pool.Release(this);

    protected MothershipCopterSpriteObject()
    {
    }

    public override void Update(Camera camera, float elapsedSeconds)
    {
      base.Update(camera, elapsedSeconds);
      this._droidDoor.SetOffset(this._droidDoor.Offset.X, this._defaultDoorPosition.Y + (float) ((MothershipCopter) this.Instance).DoorOffset);
      this._deadBreath.Update(elapsedSeconds);
      this._fire.Update(elapsedSeconds);
      this._sparks.Update(elapsedSeconds);
      if (this._explosionsStarted)
      {
        this._explosionElapsedTime += elapsedSeconds;
        foreach (MothershipCopterSpriteObject.AnimationInfo smallExplosion in this._smallExplosions)
        {
          if (!smallExplosion.Started && (double) this._explosionElapsedTime > (double) smallExplosion.Delay)
          {
            smallExplosion.Started = true;
            smallExplosion.Sprite.Play();
          }
        }
      }
      foreach (MothershipCopterSpriteObject.AnimationInfo smallExplosion in this._smallExplosions)
        smallExplosion.Sprite.Update(elapsedSeconds);
    }

    public override void Init(Instance instance)
    {
      base.Init(instance);
      this._mothership = (MothershipCopter) instance;
      this._deadBreath = CommonAnimatedSprite.GetInstance();
      this._deadBreath.Init("Effects/DeadBreath/DeadBreathXML");
      this._fire = CommonAnimatedSprite.GetInstance();
      this._fire.Init("Effects/FireBig/FireBigXML");
      this._fire.IsLooped = true;
      this._sparks = CommonAnimatedSprite.GetInstance();
      this._sparks.Init("Effects/Sparks/SparksXML");
      this._sparks.IsLooped = true;
      this._deadBreath.Playing = false;
      this._fire.Playing = false;
      this._sparks.Playing = false;
      this._mothership.StartBreath += new EventHandler(this.OnStartBreath);
      this._mothership.HealthPhaseChanged += new EventHandler(this.OnHealthPhaseChanged);
      this._mothership.StateChanged += new EventHandler<StateChangeEventArgs<int>>(this.OnMothershipStateChanged);
      this._body1 = this.GetChild("CopterBody1");
      this._body2 = this.GetChild("CopterBody2");
      this._body3 = this.GetChild("CopterBody3");
      this._body4 = this.GetChild("CopterBody4");
      this._propeller1 = this.GetChild("CopterPropeller1");
      this._propeller2 = this.GetChild("CopterPropeller2");
      this._door1 = this.GetChild("DroidDoor1");
      this._door2 = this.GetChild("DroidDoor2");
      this._door3 = this.GetChild("DroidDoor3");
      this._door4 = this.GetChild("DroidDoor4");
      this._droidDoor = (SimpleSpriteObject) this._door1;
      this.InitHealthPhase(this._mothership.HealthPhase);
      this._defaultDoorPosition = this._droidDoor.Offset;
      CommonAnimatedSprite instance1 = CommonAnimatedSprite.GetInstance();
      instance1.Init("Effects/Explosion3_65p/Explosion3_65XML");
      instance1.Playing = false;
      CommonAnimatedSprite instance2 = CommonAnimatedSprite.GetInstance();
      instance2.Init("Effects/Explosion3_65p/Explosion3_65XML");
      instance2.Playing = false;
      CommonAnimatedSprite instance3 = CommonAnimatedSprite.GetInstance();
      instance3.Init("Effects/Explosion3_65p/Explosion3_65XML");
      instance3.Playing = false;
      CommonAnimatedSprite instance4 = CommonAnimatedSprite.GetInstance();
      instance4.Init("Effects/Explosion3_65p/Explosion3_65XML");
      instance4.Playing = false;
      CommonAnimatedSprite instance5 = CommonAnimatedSprite.GetInstance();
      instance5.Init("Effects/Explosion3_65p/Explosion3_65XML");
      instance5.Playing = false;
      instance5.Ended += new EventHandler(this.smallExplosion5_Ended);
      this._smallExplosions.Add(new MothershipCopterSpriteObject.AnimationInfo()
      {
        Sprite = instance1,
        Position = new Vector2(35f, 110f),
        Delay = 0.0f
      });
      this._smallExplosions.Add(new MothershipCopterSpriteObject.AnimationInfo()
      {
        Sprite = instance2,
        Position = new Vector2(170f, 45f),
        Delay = 0.15f
      });
      this._smallExplosions.Add(new MothershipCopterSpriteObject.AnimationInfo()
      {
        Sprite = instance3,
        Position = new Vector2(305f, 85f),
        Delay = 0.1f
      });
      this._smallExplosions.Add(new MothershipCopterSpriteObject.AnimationInfo()
      {
        Sprite = instance4,
        Position = new Vector2(125f, 110f),
        Delay = 0.15f
      });
      this._smallExplosions.Add(new MothershipCopterSpriteObject.AnimationInfo()
      {
        Sprite = instance5,
        Position = new Vector2(225f, 110f),
        Delay = 0.2f
      });
    }

    private void OnMothershipStateChanged(object sender, StateChangeEventArgs<int> e)
    {
      if (this._mothership.State != 1)
        return;
      BossSounds.Instance.StopSounds();
      BossSounds.Instance.PlayExplosion(4);
    }

    private void smallExplosion5_Ended(object sender, EventArgs e)
    {
      this._explosionsStarted = false;
      this._explosionElapsedTime = 0.0f;
      this.InitHealthPhase(this._mothership.HealthPhase);
    }

    private void InitHealthPhase(MothershipCopter.BossHealthPhase healthPhase)
    {
      this._body1.Visible = false;
      this._body2.Visible = false;
      this._body3.Visible = false;
      this._body4.Visible = false;
      this._propeller1.Visible = false;
      this._propeller2.Visible = false;
      this._door1.Visible = false;
      this._door2.Visible = false;
      this._door3.Visible = false;
      this._door4.Visible = false;
      this._fire.Visible = false;
      this._sparks.Visible = false;
      switch (healthPhase)
      {
        case MothershipCopter.BossHealthPhase.Phase100:
          this._body1.Visible = true;
          this._propeller1.Visible = true;
          this._door1.Visible = true;
          this._droidDoor = (SimpleSpriteObject) this._door1;
          break;
        case MothershipCopter.BossHealthPhase.Phase70:
          this._body2.Visible = true;
          this._propeller2.Visible = true;
          this._door2.Visible = true;
          this._droidDoor = (SimpleSpriteObject) this._door2;
          this._fire.Visible = true;
          this._fire.Play();
          break;
        case MothershipCopter.BossHealthPhase.Phase40:
          this._body3.Visible = true;
          this._propeller2.Visible = true;
          this._door3.Visible = true;
          this._fire.Visible = true;
          this._fire.Play();
          this._sparks.Visible = true;
          this._sparks.Play();
          this._droidDoor = (SimpleSpriteObject) this._door3;
          break;
        case MothershipCopter.BossHealthPhase.Phase10:
          this._body4.Visible = true;
          this._propeller2.Visible = true;
          this._door4.Visible = true;
          this._fire.Visible = true;
          this._fire.Play();
          this._sparks.Visible = true;
          this._sparks.Play();
          this._droidDoor = (SimpleSpriteObject) this._door4;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (healthPhase));
      }
    }

    private void OnHealthPhaseChanged(object sender, EventArgs e)
    {
      if (this._mothership.HealthPhase == MothershipCopter.BossHealthPhase.Phase100)
      {
        this.InitHealthPhase(this._mothership.HealthPhase);
      }
      else
      {
        this._explosionsStarted = true;
        this._explosionElapsedTime = 0.0f;
        foreach (MothershipCopterSpriteObject.AnimationInfo smallExplosion in this._smallExplosions)
          smallExplosion.Started = false;
        switch (this._mothership.HealthPhase)
        {
          case MothershipCopter.BossHealthPhase.Phase70:
            BossSounds.Instance.PlayExplosion(1);
            break;
          case MothershipCopter.BossHealthPhase.Phase40:
            BossSounds.Instance.PlayExplosion(2);
            BossSounds.Instance.PlaySparlkes();
            break;
          case MothershipCopter.BossHealthPhase.Phase10:
            BossSounds.Instance.PlayExplosion(3);
            break;
        }
      }
    }

    private void OnStartBreath(object sender, EventArgs e) => this._deadBreath.Play();

    public override void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
    {
      base.Draw(spriteBatch, parentPosition);
      this._deadBreath.Draw(spriteBatch, this.Position + this._deadBreathPosition);
      this._fire.Draw(spriteBatch, this.Position + this._firePosition);
      this._sparks.Draw(spriteBatch, this.Position + this._sparksPosition);
      foreach (MothershipCopterSpriteObject.AnimationInfo smallExplosion in this._smallExplosions)
        smallExplosion.Sprite.Draw(spriteBatch, this.Position + smallExplosion.Position);
    }

    public override void ResetState()
    {
      base.ResetState();
      this._deadBreath.Release();
      this._fire.Release();
      this._sparks.Release();
      foreach (MothershipCopterSpriteObject.AnimationInfo smallExplosion in this._smallExplosions)
        smallExplosion.Sprite.Release();
      this._smallExplosions.Clear();
      this._mothership.StartBreath -= new EventHandler(this.OnStartBreath);
      this._mothership.HealthPhaseChanged -= new EventHandler(this.OnHealthPhaseChanged);
    }

    protected new class Creator : ICreation<MothershipCopterSpriteObject>
    {
      public MothershipCopterSpriteObject Create() => new MothershipCopterSpriteObject();
    }

    private class AnimationInfo
    {
      public CommonAnimatedSprite Sprite { get; set; }

      public Vector2 Position { get; set; }

      public float Delay { get; set; }

      public bool Started { get; set; }
    }
  }
}
