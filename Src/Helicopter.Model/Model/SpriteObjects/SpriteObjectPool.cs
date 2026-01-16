// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.SpriteObjectPool
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.BulletSprites;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.SpriteObjects.WeaponSprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects
{
  public class SpriteObjectPool
  {
    private static SpriteObjectPool _instance;

    public static SpriteObjectPool Instance
    {
      get => SpriteObjectPool._instance ?? (SpriteObjectPool._instance = new SpriteObjectPool());
    }

    private BulletSpriteObject GetBulletSpriteObject(Bullet instance)
    {
      BulletSpriteObject instance1;
      if (instance is RocketBullet)
      {
        instance1 = (BulletSpriteObject) RocketSpriteObject.GetInstance();
        instance1.DeathSprite = CommonAnimatedSprite.GetInstance();
        instance1.DeathSprite.Init("Effects/RocketExplosion/RocketExplosionXML");
      }
      else
      {
        instance1 = BulletSpriteObject.GetInstance();
        instance1.DeathSprite = CommonAnimatedSprite.GetInstance();
        if (instance is CassetteBullet)
          instance1.DeathSprite.Init("Effects/ClasterPoof/ClasterPoofXML");
        else
          instance1.DeathSprite.Init("Effects/EnemyPoof/EnemyPoofXML");
      }
      return instance1;
    }

    private WeaponSpriteObject GetCannonWeaponSprite(
      SpriteObject parent,
      Weapon weapon,
      VerticalAlignment alignment)
    {
      Vector2 baseWeaponPosition = weapon.BaseWeaponPosition;
      SimpleSpriteObject.GetInstance();
      CommonAnimatedSprite fireAnimation = CommonAnimatedSprite.GetInstance();
      string str;
      switch (weapon.Type)
      {
        case WeaponType.CannonMachineGun:
          Vector2 vector2_1 = new Vector2(47f, 24f);
          fireAnimation.Init("GameWorld/Objects/Weapon/weaponCannon1/weaponCannon1XML");
          if (alignment == VerticalAlignment.Top)
          {
            str = "GameWorld/Objects/Weapon/weaponCannon1_VF";
            fireAnimation.SpriteEffects = SpriteEffects.FlipVertically;
            break;
          }
          str = "GameWorld/Objects/Weapon/weaponCannon1";
          break;
        case WeaponType.CannonRocketLauncher:
          Vector2 vector2_2 = new Vector2(50f, 24f);
          fireAnimation.Init("GameWorld/Objects/Weapon/weaponCannon2/weaponCannon2XML");
          if (alignment == VerticalAlignment.Top)
          {
            str = "GameWorld/Objects/Weapon/weaponCannon2_VF";
            fireAnimation.SpriteEffects = SpriteEffects.FlipVertically;
            break;
          }
          str = "GameWorld/Objects/Weapon/weaponCannon2";
          break;
        case WeaponType.CannonDualMachineGun:
          Vector2 vector2_3 = new Vector2(49f, 22f);
          fireAnimation.Init("GameWorld/Objects/Weapon/weaponCannon3/weaponCannon3XML");
          if (alignment == VerticalAlignment.Top)
          {
            str = "GameWorld/Objects/Weapon/weaponCannon3_VF";
            fireAnimation.SpriteEffects = SpriteEffects.FlipVertically;
            break;
          }
          str = "GameWorld/Objects/Weapon/weaponCannon3";
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      Sprite sprite = this.GetSimpleSOByDesc(new SpriteDescription()
      {
        TexturePath = str
      }).Sprite;
      fireAnimation.Origin = Vector2.Zero;
      fireAnimation.Start += (EventHandler) ((x, y) => sprite.Alpha = 0.0f);
      fireAnimation.Ended += (EventHandler) ((x, y) => sprite.Alpha = 1f);
      sprite.AddChildren((Sprite) fireAnimation);
      weapon.Fired += (EventHandler<WeaponEventArgs>) ((x, y) => fireAnimation.Play());
      WeaponSpriteObject instance = WeaponSpriteObject.GetInstance();
      instance.Init(parent, sprite, (CommonAnimatedSprite) null, weapon, baseWeaponPosition);
      if (weapon.WeaponSlotDescription != null)
        instance.ZIndex = weapon.WeaponSlotDescription.SpriteZIndex;
      return instance;
    }

    public ISpriteObject GetCopterWeaponSprite(SpriteObject parent, Weapon weapon)
    {
      WeaponSpriteObject copterWeaponSprite;
      switch (weapon.Type)
      {
        case WeaponType.CopterSingleMachineGun:
        case WeaponType.SingleMachineGun:
        case WeaponType.CannonMachineGun:
          copterWeaponSprite = (WeaponSpriteObject) new SingleMachineGunWeaponSpriteObject();
          break;
        case WeaponType.CopterDualMachineGun:
        case WeaponType.DualMachineGun:
          copterWeaponSprite = (WeaponSpriteObject) new DualMachineGunWeaponSpriteObject();
          break;
        case WeaponType.CopterRocketLauncher:
        case WeaponType.RocketLauncher:
        case WeaponType.CannonRocketLauncher:
        case WeaponType.CannonDualMachineGun:
          copterWeaponSprite = (WeaponSpriteObject) RocketLauncherWeaponSpriteObject.GetInstance();
          break;
        case WeaponType.CopterPlasmaGun:
        case WeaponType.PlasmaGun:
          copterWeaponSprite = (WeaponSpriteObject) PlasmaGunWeaponSprite.GetInstance();
          break;
        case WeaponType.CopterSharkRocketLauncher:
          copterWeaponSprite = (WeaponSpriteObject) CopterSharkRocketLauncherWeaponSpriteObject.GetInstance();
          break;
        case WeaponType.CopterShield:
        case WeaponType.Shield:
          copterWeaponSprite = (WeaponSpriteObject) new ShieldWeaponSpriteObject();
          break;
        case WeaponType.CopterCasseteRocket:
        case WeaponType.CasseteRocket:
        case WeaponType.CasseteRocketUpdated:
          copterWeaponSprite = (WeaponSpriteObject) new CasseteRocketWeaponSpriteObject();
          break;
        case WeaponType.BossSingleMachineGun:
          copterWeaponSprite = (WeaponSpriteObject) new BossSingleMachineGunWeaponSpriteObject();
          break;
        case WeaponType.BossDualMachineGun:
          copterWeaponSprite = (WeaponSpriteObject) new BossDualMachineGunWeaponSpriteObject();
          break;
        case WeaponType.BossRocketLauncher:
          copterWeaponSprite = (WeaponSpriteObject) new BossRocketLauncherWeaponSpriteObject();
          break;
        case WeaponType.DualRocketLauncher:
          copterWeaponSprite = (WeaponSpriteObject) new DualRocketLauncherWeaponSpriteObject();
          break;
        case WeaponType.HomingRocket:
          copterWeaponSprite = (WeaponSpriteObject) new HominRocketWeaponSpriteObject();
          break;
        case WeaponType.Vulcan:
          copterWeaponSprite = (WeaponSpriteObject) new VulcanWeaponSpriteObject();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      copterWeaponSprite.Init(parent, weapon);
      return (ISpriteObject) copterWeaponSprite;
    }

    private SpriteObject GetLandingSprite(LandingElementInstance instance)
    {
      LandingSpriteObject instance1 = LandingSpriteObject.GetInstance();
      instance1.Init(instance);
      return (SpriteObject) instance1;
    }

    public SmartPlayerSpriteObject GetPlayerSprite(Helicopter.Model.WorldObjects.Instances.Instance instance)
    {
      SmartPlayerSpriteObject instance1 = SmartPlayerSpriteObject.GetInstance();
      instance1.DeathSprite = CommonAnimatedSprite.GetInstance();
      instance1.DeathSprite.Init("Effects/Explosion7Short/Explosion7ShortXML");
      instance1.DeathSprite.Play();
      instance1.Init(instance);
      instance1.ShieldAnimation = CommonAnimatedSprite.GetInstance();
      instance1.ShieldAnimation.Init("Effects/MountainShied/MountainShied5XML");
      instance1.ShieldAnimation.IsLooped = true;
      instance1.ShieldAnimation.Play();
      SmartPlayer smartPlayer = (SmartPlayer) instance;
      ((PlayerPattern) smartPlayer.Pattern).IndestrictableAfterCollisionTime = instance1.ShieldAnimation.FullTime;
      smartPlayer.IndestrictableAfterCollisionTime = instance1.ShieldAnimation.FullTime;
      foreach (Weapon weapon in smartPlayer.Weapons)
      {
        if (weapon != null)
          instance1.AddChildren(this.GetCopterWeaponSprite((SpriteObject) instance1, weapon));
      }
      return instance1;
    }

    public SimpleSpriteObject GetSimpleSOByDesc(SpriteDescription description)
    {
      Sprite sprite = ResourcesManager.Instance.GetSprite(description.TexturePath);
      SimpleSpriteObject instance = SimpleSpriteObject.GetInstance();
      instance.Offset = new Vector2((float) description.Offset.X, (float) description.Offset.Y);
      instance.RotatedOffset = new Vector2((float) description.Offset.X, (float) description.Offset.Y);
      instance.ZIndex = (float) description.ZIndex;
      instance.SpriteID = description.SpriteId;
      if (!description.AnimatedSprite)
      {
        instance.Sprite = sprite;
      }
      else
      {
        instance.Sprite = (Sprite) AnimatedSprite.GetInstance();
        ((AnimatedSprite) instance.Sprite).Init(sprite, description.FrameRectangle, description.FrameRectangle, description.FrameRate, Vector2.Zero);
      }
      return instance;
    }

    public SpriteObject GetSpriteObject(Helicopter.Model.WorldObjects.Instances.Instance instance, Camera camera)
    {
      SpriteObject spriteObject = (SpriteObject) null;
      switch (instance)
      {
        case Cannon _:
          spriteObject = this.GetCannonSpriteObject(instance);
          break;
        case Bullet _:
          spriteObject = (SpriteObject) this.GetBulletSpriteObject((Bullet) instance);
          break;
        case Copter _:
          spriteObject = this.GetCopterSpriteObject(instance);
          break;
        case Mountain _:
          spriteObject = (SpriteObject) MountainSpriteObject.GetInstance();
          break;
        case SmartPlayer _:
          spriteObject = (SpriteObject) this.GetPlayerSprite(instance);
          break;
        case LandingElementInstance _:
          spriteObject = this.GetLandingSprite((LandingElementInstance) instance);
          break;
        case PlasmaBeam _:
          spriteObject = (SpriteObject) PlasmaBeamSpriteObject.GetInstance();
          break;
        case Shield _:
          spriteObject = (SpriteObject) ShieldBulletSpriteObject.GetInstance();
          break;
      }
      if (spriteObject == null)
        throw new ArgumentNullException(instance.GetType().Name);
      if (instance.Pattern.Sprites != null)
      {
        foreach (SpriteDescription sprite in instance.Pattern.Sprites)
          spriteObject.AddChildren((ISpriteObject) this.GetSimpleSOByDesc(sprite));
      }
      spriteObject.Init(instance);
      spriteObject.Children.Sort((Comparison<ISpriteObject>) ((v1, v2) => v1.ZIndex.CompareTo(v2.ZIndex)));
      return spriteObject;
    }

    private SpriteObject GetCannonSpriteObject(Helicopter.Model.WorldObjects.Instances.Instance instance)
    {
      SpriteObject instance1 = (SpriteObject) CannonSpriteObject.GetInstance();
      Cannon cannon = (Cannon) instance;
      WeaponSpriteObject cannonWeaponSprite = this.GetCannonWeaponSprite(instance1, cannon.Weapon, cannon.CannonPattern.Alignment);
      CannonSpriteObject cannonSO = (CannonSpriteObject) instance1;
      cannonSO.DeathSprite = CommonAnimatedSprite.GetInstance();
      cannonSO.FireAfterDeadSprite = CommonAnimatedSprite.GetInstance();
      switch (cannon.CannonPattern.Alignment)
      {
        case VerticalAlignment.None:
          cannonSO.FireAfterDeadSprite.Visible = false;
          cannonSO.FireAfterDeadSprite.IsLooped = true;
          cannonSO.DeathSprite.Ended += (EventHandler) ((x, y) => cannonSO.FireAfterDeadSprite.Play());
          cannonSO.DeathSprite.Play();
          cannonWeaponSprite.ZIndex = cannon.CannonPattern.WeaponSlotDesc.SpriteZIndex;
          instance1.AddChildren((ISpriteObject) cannonWeaponSprite);
          return instance1;
        case VerticalAlignment.Top:
          cannonSO.FireAfterDeadSprite.Init("Effects/FireTop/FireTopXML");
          cannonSO.DeathSprite.Init("Effects/Explosion_Turel2Top/Explosion_Turel2TopXML");
          cannonSO.FireAfterDeadPosition = new Vector2(0.0f, 35f);
          CommonAnimatedSprite instance2 = CommonAnimatedSprite.GetInstance();
          instance2.Init("Effects/Smoke/smokeXML");
          instance2.IsLooped = true;
          instance2.OffsetParent = new Vector2(0.0f, 20f);
          instance2.Play();
          cannonSO.FireAfterDeadSprite.AddChildren((Sprite) instance2);
          cannonSO.FireAfterDeadPosition = new Vector2(0.0f, 0.0f);
          goto case VerticalAlignment.None;
        case VerticalAlignment.Bottom:
          cannonSO.DeathSprite.Init("Effects/Explosion_Turel2/Explosion_Turel2XML");
          cannonSO.FireAfterDeadSprite.Init("Effects/Fire/FireXML");
          CommonAnimatedSprite instance3 = CommonAnimatedSprite.GetInstance();
          instance3.Init("Effects/Smoke/smokeXML");
          instance3.IsLooped = true;
          instance3.OffsetParent = new Vector2(0.0f, -20f);
          instance3.Play();
          cannonSO.FireAfterDeadSprite.AddChildren((Sprite) instance3);
          cannonSO.FireAfterDeadPosition = new Vector2(0.0f, 0.0f);
          goto case VerticalAlignment.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private SpriteObject GetCopterSpriteObject(Helicopter.Model.WorldObjects.Instances.Instance instance)
    {
      CopterSpriteObject parent = !(instance is MothershipCopter) 
                ? CopterSpriteObject.GetInstance() 
                : (CopterSpriteObject) MothershipCopterSpriteObject.GetInstance();

      parent.DeathSprite = CommonAnimatedSprite.GetInstance();
      parent.DeathSprite.Init("Effects/Explosion3/Explosion3XML");
      switch (((Copter) instance).UnitType)
      {
        case UnitType.None:
          parent.DeathSprite.Play();
          parent.ZIndex = instance.ZIndex;
          foreach (Weapon weapon in ((Copter) instance).Weapons)
          {
            if (weapon != null)
            {
              ISpriteObject copterWeaponSprite = this.GetCopterWeaponSprite((SpriteObject) parent, weapon);
              parent.AddChildren(copterWeaponSprite);
            }
          }
          return (SpriteObject) parent;
        case UnitType.LightHelicopter:
          parent.DeathSprite.Init("Effects/Explosion3/Explosion3XML");
          goto case UnitType.None;
        case UnitType.MediumHelicopter:
        case UnitType.HeavyHelicopter:
          parent.DeathSprite.Init("Effects/Explosion7Short/Explosion7ShortXML");
          goto case UnitType.None;
        case UnitType.Droid:
        case UnitType.Egg:
        case UnitType.ArmedEgg:
          parent.DeathSprite.Init("Effects/Explosion3/Explosion3XML");
          parent.DeathSprite.Scale = new Vector2(0.65f);
          goto case UnitType.None;
        case UnitType.Boss5:
          parent.DeathSprite.Init("Effects/Explosion7Big/Explosion7_BigXML");
          goto case UnitType.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void Release(ISpriteObject obj) => obj.Release();
  }
}
