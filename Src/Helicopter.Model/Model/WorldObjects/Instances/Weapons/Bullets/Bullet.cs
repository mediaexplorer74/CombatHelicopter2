// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.Bullet
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  public abstract class Bullet : MoveableInstance
  {
    public const int Normal = 1;
    public const int Death = 1;
    public float LinearAcceleration;
    public float LinearSpeed;
    public Instance Owner;
    public float X2DamageProbability;
    public float X4DamageProbability;
    public float DamageFactor = 1f;
    private float _damage;
    public Weapon Weapon;
    public MoveableInstance DeadBy;

    public int Team { get; set; }

    public float Angle { get; set; }

    public float Damage
    {
      get
      {
        if ((double) this.X4DamageProbability > CommonRandom.Instance.Random.NextDouble())
          return this._damage * 4f * this.DamageFactor;
        return (double) this.X2DamageProbability > CommonRandom.Instance.Random.NextDouble() ? this._damage * 2f * this.DamageFactor : this._damage * this.DamageFactor;
      }
      set => this._damage = value;
    }

    public DamageType DamageType { get; set; }

    public bool IsDirectionChanged { get; set; }

    public override void Init(Pattern pattern)
    {
      if (!(pattern is BulletPattern))
        throw new ArgumentOutOfRangeException("Pattern must be BulletPattern");
      base.Init(pattern);
      this.IsTemporary = true;
      this.Reaction = (Reaction) new BulletReaction(this);
      this.ZIndex = 100500f;
      this.StateChanged -= new EventHandler<StateChangeEventArgs<int>>(((Instance) this).OnStateChanged);
      this.StateChanged += new EventHandler<StateChangeEventArgs<int>>(((Instance) this).OnStateChanged);
    }

    protected override void OnStateChanged(object sender, StateChangeEventArgs<int> e)
    {
      if (e.NextState == 1)
      {
        this.Speed = Vector2.Zero;
        this.Acceleration = Vector2.Zero;
        this.LinearSpeed = 0.0f;
        this.LinearAcceleration = 0.0f;
      }
      base.OnStateChanged(sender, e);
    }

    protected override void UpdateSpeed(float elapsedSeconds)
    {
      if (this.State != 1)
      {
        Vector2 vector2 = new Vector2((float) Math.Cos((double) this.Angle), (float) Math.Sin((double) this.Angle));
        this.LinearSpeed += this.LinearAcceleration * elapsedSeconds;
        this.Speed = this.LinearSpeed * vector2;
      }
      if (this.DeadBy == null)
        return;
      this.Speed = this.DeadBy.Speed;
      this.Acceleration = this.DeadBy.Acceleration;
    }

    protected override void UpdateState(float elapsedSeconds)
    {
    }

    public override void ResetState()
    {
      base.ResetState();
      this.Weapon = (Weapon) null;
      this.Team = 0;
      this.Angle = 0.0f;
      this._damage = 0.0f;
      this.IsTemporary = true;
      this.Owner = (Instance) null;
      this.IsDirectionChanged = false;
      this.State = 0;
      this.Reaction = (Reaction) new BulletReaction(this);
      this.DeadBy = (MoveableInstance) null;
      this.DamageFactor = 1f;
    }
  }
}
