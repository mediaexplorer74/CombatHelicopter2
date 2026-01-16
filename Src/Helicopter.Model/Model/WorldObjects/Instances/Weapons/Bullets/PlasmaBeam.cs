// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.Bullets.PlasmaBeam
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.Primitives;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.WorldObjects.Instances.Reactions;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons.Bullets
{
  public class PlasmaBeam : Instance
  {
    public const int DefaultBeamWidth = 605;
    public const int DefaultBeamHeight = 1;
    private static readonly ObjectPool<PlasmaBeam> _pool = new ObjectPool<PlasmaBeam>((ICreation<PlasmaBeam>) new PlasmaBeam.Creator());
    internal Tweener BeamHeightTweener;
    private Instance _capturedInstance;
    public Instance Owner;
    public PlasmaGunWeapon Weapon;
    public float Damage;
    public float Angle;
    public Vector2 IntersectionPosition;

    public static PlasmaBeam GetInstance() => PlasmaBeam._pool.GetObject();

    public override void Release() => PlasmaBeam._pool.Release(this);

    public event EventHandler CaptureLost;

    public event EventHandler EnemyCaptured;

    public Instance CapturedInstance
    {
      get => this._capturedInstance;
      set
      {
        if (this._capturedInstance == value)
          return;
        this._capturedInstance = value;
        if (this._capturedInstance == null)
          this.InvokeCaptureLost();
        else
          this.InvokeEnemyCaptured();
        this.IntersectionPosition = new Vector2(-100f, 0.0f);
      }
    }

    public Vector2 GlobalStartPosition
    {
      get
      {
        return new Vector2(this.Owner.Position.X + this.Weapon.WeaponPosition.X + this.Weapon.BulletSpawnPosition.X, this.Owner.Position.Y + this.Weapon.WeaponPosition.Y);
      }
    }

    protected PlasmaBeam()
    {
      Contour contour = new Contour();
      contour.SetPoints(new Point(0, 0), new Point(605, 0), new Point(605, 1), new Point(0, 1));
      this.BeamHeightTweener = new Tweener(0.0f, 5f, 0.5f, new TweeningFunction(Linear.EaseInOut));
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
      this.Reaction = (Reaction) new PlasmaBeamReaction((Instance) this);
      this.State = 0;
    }

    public void InvokeCaptureLost()
    {
      EventHandler captureLost = this.CaptureLost;
      if (captureLost == null)
        return;
      captureLost((object) this, EventArgs.Empty);
    }

    public void InvokeEnemyCaptured()
    {
      EventHandler enemyCaptured = this.EnemyCaptured;
      if (enemyCaptured == null)
        return;
      enemyCaptured((object) this, EventArgs.Empty);
    }

    public override void ResetState()
    {
      base.ResetState();
      Contour contour = new Contour();
      contour.SetPoints(new Point(0, 0), new Point(605, 0), new Point(605, 1), new Point(0, 1));
      this.CapturedInstance = (Instance) null;
      this.BeamHeightTweener = new Tweener(0.0f, 5f, 0.5f, new TweeningFunction(Linear.EaseInOut));
      BulletPattern bulletPattern = new BulletPattern();
      bulletPattern.Contour = contour;
      this.Init((Pattern) bulletPattern);
      this.Reaction = (Reaction) new PlasmaBeamReaction((Instance) this);
      this.InvokeCaptureLost();
      this.CaptureLost = (EventHandler) null;
      this.EnemyCaptured = (EventHandler) null;
    }

    public override void Update(float elapsedSeconds)
    {
      if (this.Owner == null || this.Owner.IsNeedRemove || this.Owner.State == 1)
        this.IsNeedRemove = true;
      if (this.BeamHeightTweener.Running)
      {
        this.BeamHeightTweener.Update(elapsedSeconds);
        this.Contour.SetPoints(Point.Zero, new Point(605, 0), new Point(605, (int) this.BeamHeightTweener.Position), new Point(0, (int) this.BeamHeightTweener.Position));
      }
      Vector2 globalStartPosition = this.GlobalStartPosition;
      if (this.CapturedInstance != null)
      {
        if (this.Owner is Copter)
        {
          List<Vector2> vector2List = Contour.IntersectionWithLine(this.CapturedInstance.Contour, new Vector2(globalStartPosition.X - 605f, globalStartPosition.Y), globalStartPosition);
          float num = float.MinValue;
          foreach (Vector2 vector2 in vector2List)
          {
            if ((double) vector2.X > (double) num)
            {
              num = vector2.X;
              this.IntersectionPosition = vector2;
            }
          }
        }
        else
        {
          List<Vector2> vector2List = Contour.IntersectionWithLine(this.CapturedInstance.Contour, globalStartPosition, new Vector2(globalStartPosition.X + 605f, globalStartPosition.Y));
          float num = float.MaxValue;
          foreach (Vector2 vector2 in vector2List)
          {
            if ((double) vector2.X < (double) num)
            {
              num = vector2.X;
              this.IntersectionPosition = vector2;
            }
          }
        }
        if (this.CapturedInstance is IUnit)
        {
          float num = 0.0f;
          if (this.Owner is SmartPlayer)
          {
            SmartPlayer owner = (SmartPlayer) this.Owner;
            if (owner.DamageModifier != null)
              num = owner.DamageModifier.DamageCoef;
          }
          ((IUnit) this.CapturedInstance).HandleDamage(this.Damage * (1f + num) * elapsedSeconds, DamageType.Bullet);
        }
      }
      globalStartPosition.X = (double) this.Angle < 1.5707963705062866 ? globalStartPosition.X : globalStartPosition.X - (float) this.Contour.Rectangle.Width;
      globalStartPosition.Y += this.Weapon.BulletSpawnPosition.Y - this.BeamHeightTweener.Position / 2f;
      this.SetPosition(globalStartPosition);
      if (this.CapturedInstance != null)
      {
        if (this.Owner is SmartPlayer)
        {
          if (this.CapturedInstance.State == 1 || this.CapturedInstance.IsNeedRemove || (double) this.CapturedInstance.Position.X < (double) this.Owner.Contour.Rectangle.Right || !this.CapturedInstance.Contour.Rectangle.Intersects(this.Contour.Rectangle))
            this.CapturedInstance = (Instance) null;
        }
        else if (this.CapturedInstance.State == 1 || this.CapturedInstance.IsNeedRemove || (double) this.CapturedInstance.Contour.Rectangle.X > (double) this.Owner.Position.X || !this.CapturedInstance.Contour.Rectangle.Intersects(this.Contour.Rectangle))
          this.CapturedInstance = (Instance) null;
      }
      if (this.CapturedInstance == this.Owner)
        this.CapturedInstance = (Instance) null;
      this.IntersectionPosition.Y = (float) this.Contour.Rectangle.Center.Y;
    }

    protected override void OnStateChanged(object sender, StateChangeEventArgs<int> e)
    {
      int nextState = e.NextState;
    }

    protected class Creator : ICreation<PlasmaBeam>
    {
      public PlasmaBeam Create() => new PlasmaBeam();
    }
  }
}
