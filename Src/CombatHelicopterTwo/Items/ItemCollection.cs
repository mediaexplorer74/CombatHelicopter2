// Decompiled with JetBrains decompiler
// Type: Helicopter.Items.ItemCollection
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.Ammunition;
using Helicopter.Items.DeviceItems;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Patterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Helicopter.Items
{
  internal class ItemCollection
  {
    private static ItemCollection _instance;
    private PlayerPattern[] PlayerCopters;
    private UnlockConditionFactory _unlockFactory;

    public List<WeaponItem> WeaponItems { get; set; }

    public List<UpgradeItem> DeviceItems { get; set; }

    public List<HelicopterItem> HelicopterItems { get; set; }

    public List<AmmunitionItem> AmunitionItems { get; set; }

    public static ItemCollection Instance
    {
      get => ItemCollection._instance ?? (ItemCollection._instance = new ItemCollection());
    }

    private ItemCollection()
    {
      PlayerPattern[] playerPatternArray1 = new PlayerPattern[4];
      PlayerPattern[] playerPatternArray2 = playerPatternArray1;
      PlayerPattern playerPattern1 = new PlayerPattern();
      playerPattern1.Id = IdFactory.Instance.GetId();
      playerPattern1.Sprites = new List<SpriteDescription>()
      {
        new SpriteDescription()
        {
          SpriteId = "CopterBody",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 146, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter1/body",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "CopterPropeller",
          Offset = new Point(20, 0),
          FrameRate = 0.03f,
          FrameRectangle = new Rectangle(0, 0, 126, 6),
          TexturePath = "GameWorld/Objects/Hero/Copter1/flyAnimation",
          AnimatedSprite = true,
          ZIndex = 10
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged1",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 146, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter1/bodyDamage1",
          ZIndex = 10
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged2",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 146, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter1/bodyDamage2",
          ZIndex = 10
        }
      };
      playerPattern1.Energy = 1000f;
      playerPattern1.SpeedY = 200f;
      playerPattern1.WeaponPosition = new Point[1]
      {
        new Point(124, 36)
      };
      PlayerPattern playerPattern2 = playerPattern1;
      playerPatternArray2[0] = playerPattern2;
      PlayerPattern[] playerPatternArray3 = playerPatternArray1;
      PlayerPattern playerPattern3 = new PlayerPattern();
      playerPattern3.Id = IdFactory.Instance.GetId();
      playerPattern3.Sprites = new List<SpriteDescription>()
      {
        new SpriteDescription()
        {
          SpriteId = "CopterBody",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 146, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter2/body",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "CopterWing",
          Offset = new Point(75, 27),
          FrameRectangle = new Rectangle(0, 0, 26, 17),
          TexturePath = "GameWorld/Objects/Hero/Copter2/wing",
          ZIndex = 9
        },
        new SpriteDescription()
        {
          SpriteId = "CopterPropeller",
          Offset = new Point(20, 0),
          FrameRate = 0.03f,
          FrameRectangle = new Rectangle(0, 0, 126, 6),
          TexturePath = "GameWorld/Objects/Hero/Copter2/flyAnimation",
          AnimatedSprite = true,
          ZIndex = 10
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged1",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 146, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter2/bodyDamage1",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged2",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 146, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter2/bodyDamage2",
          ZIndex = 0
        }
      };
      playerPattern3.Energy = 1000f;
      playerPattern3.SpeedY = 100f;
      playerPattern3.WeaponPosition = new Point[2]
      {
        new Point(123, 34),
        new Point(80, 34)
      };
      PlayerPattern playerPattern4 = playerPattern3;
      playerPatternArray3[1] = playerPattern4;
      PlayerPattern[] playerPatternArray4 = playerPatternArray1;
      PlayerPattern playerPattern5 = new PlayerPattern();
      playerPattern5.Id = IdFactory.Instance.GetId();
      playerPattern5.Sprites = new List<SpriteDescription>()
      {
        new SpriteDescription()
        {
          SpriteId = "CopterBody",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 156, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter3/body",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "CopterWing",
          Offset = new Point(60, 22),
          FrameRectangle = new Rectangle(0, 0, 60, 22),
          TexturePath = "GameWorld/Objects/Hero/Copter3/wing",
          ZIndex = 9
        },
        new SpriteDescription()
        {
          SpriteId = "CopterPropeller",
          Offset = new Point(9, 0),
          FrameRate = 0.03f,
          FrameRectangle = new Rectangle(0, 0, 160, 7),
          TexturePath = "GameWorld/Objects/Hero/Copter3/flyAnimation",
          AnimatedSprite = true,
          ZIndex = 10
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged1",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 156, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter3/bodyDamage1",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged2",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 156, 47),
          TexturePath = "GameWorld/Objects/Hero/Copter3/bodyDamage2",
          ZIndex = 0
        }
      };
      playerPattern5.Energy = 1500f;
      playerPattern5.SpeedY = 200f;
      playerPattern5.WeaponPosition = new Point[2]
      {
        new Point(133, 35),
        new Point(68, 32)
      };
      PlayerPattern playerPattern6 = playerPattern5;
      playerPatternArray4[2] = playerPattern6;
      PlayerPattern[] playerPatternArray5 = playerPatternArray1;
      PlayerPattern playerPattern7 = new PlayerPattern();
      playerPattern7.Id = IdFactory.Instance.GetId();
      playerPattern7.Sprites = new List<SpriteDescription>()
      {
        new SpriteDescription()
        {
          SpriteId = "CopterBody",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 167, 50),
          TexturePath = "GameWorld/Objects/Hero/Copter4/body",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "CopterWing",
          Offset = new Point(81, 27),
          FrameRectangle = new Rectangle(0, 0, 49, 17),
          TexturePath = "GameWorld/Objects/Hero/Copter4/wing",
          ZIndex = 9
        },
        new SpriteDescription()
        {
          SpriteId = "CopterPropeller",
          Offset = new Point(20, 0),
          FrameRate = 0.03f,
          FrameRectangle = new Rectangle(0, 0, 148, 6),
          TexturePath = "GameWorld/Objects/Hero/Copter4/flyAnimation",
          AnimatedSprite = true,
          ZIndex = 10
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged1",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 167, 50),
          TexturePath = "GameWorld/Objects/Hero/Copter4/bodyDamage1",
          ZIndex = 0
        },
        new SpriteDescription()
        {
          SpriteId = "Damaged2",
          Offset = Point.Zero,
          FrameRectangle = new Rectangle(0, 0, 167, 50),
          TexturePath = "GameWorld/Objects/Hero/Copter4/bodyDamage2",
          ZIndex = 0
        }
      };
      playerPattern7.Energy = 1500f;
      playerPattern7.SpeedY = 200f;
      playerPattern7.WeaponPosition = new Point[2]
      {
        new Point(143, 37),
        new Point(90, 36)
      };
      PlayerPattern playerPattern8 = playerPattern7;
      playerPatternArray5[3] = playerPattern8;
      this.PlayerCopters = playerPatternArray1;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this._unlockFactory = new UnlockConditionFactory();
      this.WeaponItems = new List<WeaponItem>();
      this.DeviceItems = new List<UpgradeItem>();
      this.HelicopterItems = new List<HelicopterItem>();
      this.AmunitionItems = new List<AmmunitionItem>();
      this.InitItems();
    }

    private void InitCopterItems()
    {
      this.FirstCopterViper();
      this.SecondCopter();
      this.ThirdCopter();
      this.FourthCopter();
    }

    private void FirstCopterViper()
    {
      PlayerPattern playerCopter = this.PlayerCopters[0];
      playerCopter.Contour.Add(new Point(20, 0));
      playerCopter.Contour.Add(new Point(145, 0));
      playerCopter.Contour.Add(new Point(134, 40));
      playerCopter.Contour.Add(new Point(100, 45));
      playerCopter.Contour.Add(new Point(10, 30));
      playerCopter.Contour.Add(new Point(0, 12));
      playerCopter.Contour.UpdateRectangle();
      HelicopterItem helicopterItem1 = new HelicopterItem(playerCopter);
      helicopterItem1.Name = "Viper";
      helicopterItem1.HelicopterType = HelicopterType.Viper;
      HelicopterItem helicopterItem2 = helicopterItem1;
      CopterLayoutDescription layoutDescription1 = new CopterLayoutDescription();
      layoutDescription1.Name = "Viper";
      layoutDescription1.Copter = "Hangar/Copter1/Copter1";
      layoutDescription1.CopterDamaged = "Hangar/Copter1/Copter1Damaged";
      layoutDescription1.CopterPosition = new Vector2(161f, 129f);
      layoutDescription1.HeaderTexture = "Hangar/Copter1/headerViper";
      layoutDescription1.HeaderPosition = new Vector2(322f, 79f);
      layoutDescription1.LockedCopter = "Hangar/Copter1/Copter1_Unavaliable";
      layoutDescription1.LockedHeader = "Hangar/Copter1/headerViper_Unavaliable";
      layoutDescription1.FirstWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(580, 238, 80, 40),
        ItemPosition = new Vector2(565f, 241f)
      };
      CopterLayoutDescription layoutDescription2 = layoutDescription1;
      helicopterItem2.HangarDesc = (HangarLayoutDescription) layoutDescription2;
      helicopterItem1.UnlockCondition = this._unlockFactory.Viper();
      HelicopterItem helicopterItem3 = helicopterItem1;
      helicopterItem3.IsBought = true;
      this.HelicopterItems.Add(helicopterItem3);
    }

    private void SecondCopter()
    {
      PlayerPattern playerCopter = this.PlayerCopters[1];
      playerCopter.Contour.Add(new Point(20, 0));
      playerCopter.Contour.Add(new Point(145, 0));
      playerCopter.Contour.Add(new Point(133, 43));
      playerCopter.Contour.Add(new Point(80, 45));
      playerCopter.Contour.Add(new Point(10, 30));
      playerCopter.Contour.Add(new Point(0, 10));
      playerCopter.Contour.UpdateRectangle();
      HelicopterItem helicopterItem1 = new HelicopterItem(playerCopter);
      helicopterItem1.UnlockCondition = this._unlockFactory.Harbinger();
      helicopterItem1.Name = "Harbinger";
      helicopterItem1.HelicopterType = HelicopterType.Harbinger;
      HelicopterItem helicopterItem2 = helicopterItem1;
      CopterLayoutDescription layoutDescription1 = new CopterLayoutDescription();
      layoutDescription1.Name = "Harbinger";
      layoutDescription1.Copter = "Hangar/Copter2/Copter2";
      layoutDescription1.CopterDamaged = "Hangar/Copter2/Copter2Damaged";
      layoutDescription1.CopterPosition = new Vector2(161f, 128f);
      layoutDescription1.HeaderTexture = "Hangar/Copter2/headerHarbinger";
      layoutDescription1.HeaderPosition = new Vector2(264f, 78f);
      layoutDescription1.LockedCopter = "Hangar/Copter2/Copter2_Unavaliable";
      layoutDescription1.LockedHeader = "Hangar/Copter2/headerHarbinger_Unavaliable";
      layoutDescription1.FirstWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(570, 240, 80, 40),
        ItemPosition = new Vector2(568f, 241f)
      };
      layoutDescription1.SecondWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(410, 230, 140, 80),
        ItemPosition = new Vector2(400f, 233f)
      };
      layoutDescription1.UpgradeA = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(420, 167, 54, 47),
        ItemPosition = new Vector2(420f, 167f)
      };
      layoutDescription1.Wing = new ButtonDesc()
      {
        TextureName = "Hangar/Copter2/Copter2Wing",
        TextureNameDamaged = "Hangar/Copter2/Copter2WingDamaged",
        TexturePosition = new Vector2(412f, 214f)
      };
      CopterLayoutDescription layoutDescription2 = layoutDescription1;
      helicopterItem2.HangarDesc = (HangarLayoutDescription) layoutDescription2;
      helicopterItem1.HasSecondWeapon = true;
      helicopterItem1.HasUpgradeA = true;
      this.HelicopterItems.Add(helicopterItem1);
    }

    private void ThirdCopter()
    {
      PlayerPattern playerCopter = this.PlayerCopters[2];
      playerCopter.Contour.Add(new Point(10, 0));
      playerCopter.Contour.Add(new Point(160, 0));
      playerCopter.Contour.Add(new Point(145, 43));
      playerCopter.Contour.Add(new Point(60, 43));
      playerCopter.Contour.Add(new Point(0, 35));
      playerCopter.Contour.Add(new Point(0, 15));
      playerCopter.Contour.UpdateRectangle();
      HelicopterItem helicopterItem1 = new HelicopterItem(playerCopter);
      helicopterItem1.Name = "Avenger";
      helicopterItem1.HelicopterType = HelicopterType.Avenger;
      helicopterItem1.UnlockCondition = this._unlockFactory.Avenger();
      HelicopterItem helicopterItem2 = helicopterItem1;
      CopterLayoutDescription layoutDescription1 = new CopterLayoutDescription();
      layoutDescription1.Name = "Avenger";
      layoutDescription1.Copter = "Hangar/Copter3/Copter3";
      layoutDescription1.CopterDamaged = "Hangar/Copter3/Copter3Damaged";
      layoutDescription1.CopterPosition = new Vector2(147f, 132f);
      layoutDescription1.HeaderTexture = "Hangar/Copter3/headerAvenger";
      layoutDescription1.HeaderPosition = new Vector2(283f, 78f);
      layoutDescription1.LockedCopter = "Hangar/Copter3/Copter3_Unavaliable";
      layoutDescription1.LockedHeader = "Hangar/Copter3/headerAvenger_Unavaliable";
      layoutDescription1.FirstWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(575, 242, 68, 38),
        ItemPosition = new Vector2(580f, 242f)
      };
      layoutDescription1.SecondWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(355, 234, 138, 80),
        ItemPosition = new Vector2(373f, 232f)
      };
      layoutDescription1.UpgradeA = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(441, 160, 54, 48),
        ItemPosition = new Vector2(441f, 160f)
      };
      layoutDescription1.Wing = new ButtonDesc()
      {
        TextureName = "Hangar/Copter3/Copter3Wing",
        TextureNameDamaged = "Hangar/Copter3/Copter3WingDamaged",
        TexturePosition = new Vector2(352f, 201f)
      };
      CopterLayoutDescription layoutDescription2 = layoutDescription1;
      helicopterItem2.HangarDesc = (HangarLayoutDescription) layoutDescription2;
      helicopterItem1.HasSecondWeapon = true;
      helicopterItem1.HasUpgradeA = true;
      this.HelicopterItems.Add(helicopterItem1);
    }

    private void FourthCopter()
    {
      PlayerPattern playerCopter = this.PlayerCopters[3];
      playerCopter.Contour.Add(new Point(20, 0));
      playerCopter.Contour.Add(new Point(165, 0));
      playerCopter.Contour.Add(new Point(155, 45));
      playerCopter.Contour.Add(new Point(100, 45));
      playerCopter.Contour.Add(new Point(15, 30));
      playerCopter.Contour.Add(new Point(0, 15));
      playerCopter.Contour.UpdateRectangle();
      HelicopterItem helicopterItem1 = new HelicopterItem(playerCopter);
      helicopterItem1.Name = "Grim Reaper";
      helicopterItem1.UnlockCondition = this._unlockFactory.GrimReaper();
      helicopterItem1.HelicopterType = HelicopterType.GrimReaper;
      HelicopterItem helicopterItem2 = helicopterItem1;
      CopterLayoutDescription layoutDescription1 = new CopterLayoutDescription();
      layoutDescription1.Name = "Grim Reaper";
      layoutDescription1.Copter = "Hangar/Copter4/Copter4";
      layoutDescription1.CopterDamaged = "Hangar/Copter4/Copter4Damaged";
      layoutDescription1.CopterPosition = new Vector2(125f, 125f);
      layoutDescription1.HeaderTexture = "Hangar/Copter4/headerGrimReaper";
      layoutDescription1.HeaderPosition = new Vector2(240f, 78f);
      layoutDescription1.LockedCopter = "Hangar/Copter4/Copter4_Unavaliable";
      layoutDescription1.LockedHeader = "Hangar/Copter4/headerGrimReaper_Unavaliable";
      layoutDescription1.FirstWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(583, 238, 68, 38),
        ItemPosition = new Vector2(592f, 244f)
      };
      layoutDescription1.SecondWeapon = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(405, 234, 138, 44),
        ItemPosition = new Vector2(428f, 234f)
      };
      layoutDescription1.UpgradeA = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(420, 169, 54, 48),
        ItemPosition = new Vector2(420f, 169f)
      };
      layoutDescription1.UpgradeB = new ButtonDesc()
      {
        EntryRectangle = new Rectangle(353, 167, 54, 48),
        ItemPosition = new Vector2(315f, 167f)
      };
      layoutDescription1.Wing = new ButtonDesc()
      {
        TextureName = "Hangar/Copter4/Copter4Wing",
        TextureNameDamaged = "Hangar/Copter4/Copter4WingDamaged",
        TexturePosition = new Vector2(393f, 211f)
      };
      CopterLayoutDescription layoutDescription2 = layoutDescription1;
      helicopterItem2.HangarDesc = (HangarLayoutDescription) layoutDescription2;
      helicopterItem1.HasSecondWeapon = true;
      helicopterItem1.HasUpgradeA = true;
      helicopterItem1.HasUpgradeB = true;
      this.HelicopterItems.Add(helicopterItem1);
    }

    private void InitAmunitionItems()
    {
      this.AmunitionItems.Clear();
      this.AmunitionItems.AddRange((IEnumerable<AmmunitionItem>) new List<AmmunitionItem>()
      {
        AmmunitionFactory.GetHealthPack100(),
        AmmunitionFactory.GetShield25(),
        AmmunitionFactory.GetShield50(),
        AmmunitionFactory.GetBallisticShieldModule(),
        AmmunitionFactory.GetMisslesShieldUnit(),
        AmmunitionFactory.GetCollisionsModule(),
        AmmunitionFactory.GetDamageIncrease50()
      });
    }

    private void InitItems()
    {
      this.InitWeaponItems();
      this.InitAmunitionItems();
      this.InitCopterItems();
      this.InitUpgradeItems();
    }

    private void InitUpgradeItems()
    {
      this.DeviceItems.Clear();
      List<UpgradeItem> deviceItems1 = this.DeviceItems;
      DamageControlSystemV1 damageControlSystemV1_1 = new DamageControlSystemV1();
      damageControlSystemV1_1.Type = UpgradeType.DamageControlSystemV1;
      damageControlSystemV1_1.UnlockCondition = this._unlockFactory.DamageControlSystemV1();
      DamageControlSystemV1 damageControlSystemV1_2 = damageControlSystemV1_1;
      deviceItems1.Add((UpgradeItem) damageControlSystemV1_2);
      List<UpgradeItem> deviceItems2 = this.DeviceItems;
      DamageControlSystemV2 damageControlSystemV2_1 = new DamageControlSystemV2();
      damageControlSystemV2_1.Type = UpgradeType.DamageControlSystemV2;
      damageControlSystemV2_1.UnlockCondition = this._unlockFactory.DamageControlSystemV2();
      DamageControlSystemV2 damageControlSystemV2_2 = damageControlSystemV2_1;
      deviceItems2.Add((UpgradeItem) damageControlSystemV2_2);
      List<UpgradeItem> deviceItems3 = this.DeviceItems;
      DamageControlSystemV3 damageControlSystemV3_1 = new DamageControlSystemV3();
      damageControlSystemV3_1.Type = UpgradeType.DamageControlSystemV3;
      damageControlSystemV3_1.UnlockCondition = this._unlockFactory.DamageControlSystemV3();
      DamageControlSystemV3 damageControlSystemV3_2 = damageControlSystemV3_1;
      deviceItems3.Add((UpgradeItem) damageControlSystemV3_2);
      List<UpgradeItem> deviceItems4 = this.DeviceItems;
      SystemCompensationCrush compensationCrush1 = new SystemCompensationCrush();
      compensationCrush1.Type = UpgradeType.SystemCompensationCrush;
      compensationCrush1.UnlockCondition = this._unlockFactory.SystemCompensationCrush();
      SystemCompensationCrush compensationCrush2 = compensationCrush1;
      deviceItems4.Add((UpgradeItem) compensationCrush2);
      List<UpgradeItem> deviceItems5 = this.DeviceItems;
      BulletControlSystem bulletControlSystem1 = new BulletControlSystem();
      bulletControlSystem1.Type = UpgradeType.BulletControlSystem;
      bulletControlSystem1.UnlockCondition = this._unlockFactory.BulletControlSystem();
      BulletControlSystem bulletControlSystem2 = bulletControlSystem1;
      deviceItems5.Add((UpgradeItem) bulletControlSystem2);
      List<UpgradeItem> deviceItems6 = this.DeviceItems;
      EnergyRegenerationSystemV1 regenerationSystemV1_1 = new EnergyRegenerationSystemV1();
      regenerationSystemV1_1.Type = UpgradeType.EnergyRegenerationSystemV1;
      regenerationSystemV1_1.UnlockCondition = this._unlockFactory.EnergyRegenerationSystemV1();
      EnergyRegenerationSystemV1 regenerationSystemV1_2 = regenerationSystemV1_1;
      deviceItems6.Add((UpgradeItem) regenerationSystemV1_2);
      List<UpgradeItem> deviceItems7 = this.DeviceItems;
      EnergyRegenerationSystemV2 regenerationSystemV2_1 = new EnergyRegenerationSystemV2();
      regenerationSystemV2_1.Type = UpgradeType.EnergyRegenerationSystemV2;
      regenerationSystemV2_1.UnlockCondition = this._unlockFactory.EnergyRegenerationSystemV2();
      EnergyRegenerationSystemV2 regenerationSystemV2_2 = regenerationSystemV2_1;
      deviceItems7.Add((UpgradeItem) regenerationSystemV2_2);
      List<UpgradeItem> deviceItems8 = this.DeviceItems;
      EnergyRegenerationSystemV3 regenerationSystemV3_1 = new EnergyRegenerationSystemV3();
      regenerationSystemV3_1.Type = UpgradeType.EnergyRegenerationSystemV3;
      regenerationSystemV3_1.UnlockCondition = this._unlockFactory.EnergyRegenerationSystemV3();
      EnergyRegenerationSystemV3 regenerationSystemV3_2 = regenerationSystemV3_1;
      deviceItems8.Add((UpgradeItem) regenerationSystemV3_2);
      List<UpgradeItem> deviceItems9 = this.DeviceItems;
      EnergyRegenerationSystemV4 regenerationSystemV4_1 = new EnergyRegenerationSystemV4();
      regenerationSystemV4_1.Type = UpgradeType.EnergyRegenerationSystemV4;
      regenerationSystemV4_1.UnlockCondition = this._unlockFactory.EnergyRegenerationSystemV4();
      EnergyRegenerationSystemV4 regenerationSystemV4_2 = regenerationSystemV4_1;
      deviceItems9.Add((UpgradeItem) regenerationSystemV4_2);
      List<UpgradeItem> deviceItems10 = this.DeviceItems;
      TargetAssistentSystem targetAssistentSystem1 = new TargetAssistentSystem();
      targetAssistentSystem1.Type = UpgradeType.TargetAssistentSystem;
      targetAssistentSystem1.UnlockCondition = this._unlockFactory.TargetAssistentSystem();
      TargetAssistentSystem targetAssistentSystem2 = targetAssistentSystem1;
      deviceItems10.Add((UpgradeItem) targetAssistentSystem2);
      List<UpgradeItem> deviceItems11 = this.DeviceItems;
      EnhanchedRechargeSystem enhanchedRechargeSystem1 = new EnhanchedRechargeSystem();
      enhanchedRechargeSystem1.Type = UpgradeType.EnhanchedRechargeSystem;
      enhanchedRechargeSystem1.UnlockCondition = this._unlockFactory.EnhanchedRechargeSystem();
      EnhanchedRechargeSystem enhanchedRechargeSystem2 = enhanchedRechargeSystem1;
      deviceItems11.Add((UpgradeItem) enhanchedRechargeSystem2);
      List<UpgradeItem> deviceItems12 = this.DeviceItems;
      IncreasedCapacitySystem increasedCapacitySystem1 = new IncreasedCapacitySystem();
      increasedCapacitySystem1.Type = UpgradeType.IncreasedCapacitySystem;
      increasedCapacitySystem1.UnlockCondition = this._unlockFactory.IncreasedCapacitySystem();
      IncreasedCapacitySystem increasedCapacitySystem2 = increasedCapacitySystem1;
      deviceItems12.Add((UpgradeItem) increasedCapacitySystem2);
      List<UpgradeItem> deviceItems13 = this.DeviceItems;
      HotPlasmaModule hotPlasmaModule1 = new HotPlasmaModule();
      hotPlasmaModule1.Type = UpgradeType.HotPlasmaModule;
      hotPlasmaModule1.UnlockCondition = this._unlockFactory.HotPlasmaModule();
      HotPlasmaModule hotPlasmaModule2 = hotPlasmaModule1;
      deviceItems13.Add((UpgradeItem) hotPlasmaModule2);
      List<UpgradeItem> deviceItems14 = this.DeviceItems;
      CriticalDamageSystemV1 criticalDamageSystemV1_1 = new CriticalDamageSystemV1();
      criticalDamageSystemV1_1.Type = UpgradeType.CriticalDamageSystemV1;
      criticalDamageSystemV1_1.UnlockCondition = this._unlockFactory.CriticalDamageSystemV1();
      CriticalDamageSystemV1 criticalDamageSystemV1_2 = criticalDamageSystemV1_1;
      deviceItems14.Add((UpgradeItem) criticalDamageSystemV1_2);
      List<UpgradeItem> deviceItems15 = this.DeviceItems;
      CriticalDamageSystemV2 criticalDamageSystemV2_1 = new CriticalDamageSystemV2();
      criticalDamageSystemV2_1.Type = UpgradeType.CriticalDamageSystemV2;
      criticalDamageSystemV2_1.UnlockCondition = this._unlockFactory.CriticalDamageSystemV2();
      CriticalDamageSystemV2 criticalDamageSystemV2_2 = criticalDamageSystemV2_1;
      deviceItems15.Add((UpgradeItem) criticalDamageSystemV2_2);
      List<UpgradeItem> deviceItems16 = this.DeviceItems;
      PDUSystemV1 pduSystemV1_1 = new PDUSystemV1();
      pduSystemV1_1.Type = UpgradeType.PDUSystemV1;
      pduSystemV1_1.UnlockCondition = this._unlockFactory.PDUSystemV1();
      PDUSystemV1 pduSystemV1_2 = pduSystemV1_1;
      deviceItems16.Add((UpgradeItem) pduSystemV1_2);
      List<UpgradeItem> deviceItems17 = this.DeviceItems;
      PDUSystemV2 pduSystemV2_1 = new PDUSystemV2();
      pduSystemV2_1.Type = UpgradeType.PDUSystemV2;
      pduSystemV2_1.UnlockCondition = this._unlockFactory.PDUSystemV2();
      PDUSystemV2 pduSystemV2_2 = pduSystemV2_1;
      deviceItems17.Add((UpgradeItem) pduSystemV2_2);
      List<UpgradeItem> deviceItems18 = this.DeviceItems;
      UpgradedWarhead upgradedWarhead1 = new UpgradedWarhead();
      upgradedWarhead1.Type = UpgradeType.UpgradedWarhead;
      upgradedWarhead1.UnlockCondition = this._unlockFactory.UpgradedWarhead();
      UpgradedWarhead upgradedWarhead2 = upgradedWarhead1;
      deviceItems18.Add((UpgradeItem) upgradedWarhead2);
      List<UpgradeItem> deviceItems19 = this.DeviceItems;
      HarvestingSystem harvestingSystem1 = new HarvestingSystem();
      harvestingSystem1.Type = UpgradeType.HarvestingSystem;
      harvestingSystem1.UnlockCondition = this._unlockFactory.HarvestingSystem();
      HarvestingSystem harvestingSystem2 = harvestingSystem1;
      deviceItems19.Add((UpgradeItem) harvestingSystem2);
    }

    private void InitWeaponItems()
    {
      this.WeaponItems.Clear();
      WeaponType[] weaponTypeArray1 = new WeaponType[4]
      {
        WeaponType.SingleMachineGun,
        WeaponType.DualMachineGun,
        WeaponType.Vulcan,
        WeaponType.PlasmaGun
      };
      WeaponType[] weaponTypeArray2 = new WeaponType[5]
      {
        WeaponType.RocketLauncher,
        WeaponType.DualRocketLauncher,
        WeaponType.HomingRocket,
        WeaponType.Shield,
        WeaponType.CasseteRocket
      };
      foreach (WeaponType type in weaponTypeArray1)
      {
        WeaponItem weaponItem1 = this.CreateWeaponItem(type);
        weaponItem1.Slot = Slot.First;
        switch (type)
        {
          case WeaponType.SingleMachineGun:
            weaponItem1.Name = "Single Machine Gun";
            WeaponItem weaponItem2 = weaponItem1;
            ItemLayoutDescription layoutDescription1 = new ItemLayoutDescription();
            layoutDescription1.OnCopterTexture = "Hangar/Weapon/weaponS1_1";
            layoutDescription1.OnShopTexture = "Hangar/Items/Wepons/weaponShopS1_1";
            layoutDescription1.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS1_1";
            layoutDescription1.HeaderTexture = "Hangar/Items/Wepons/headerSingleGun";
            ItemLayoutDescription layoutDescription2 = layoutDescription1;
            weaponItem2.HangarDesc = (HangarLayoutDescription) layoutDescription2;
            weaponItem1.UnlockCondition = this._unlockFactory.SingleMachineGun();
            weaponItem1.Rate = WeaponRate.Medium;
            weaponItem1.IsBought = true;
            break;
          case WeaponType.DualMachineGun:
            weaponItem1.Name = "Dual Machine Gun";
            WeaponItem weaponItem3 = weaponItem1;
            ItemLayoutDescription layoutDescription3 = new ItemLayoutDescription();
            layoutDescription3.OnCopterTexture = "Hangar/Weapon/weaponS1_2";
            layoutDescription3.OnShopTexture = "Hangar/Items/Wepons/weaponShopS1_2";
            layoutDescription3.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS1_2";
            layoutDescription3.HeaderTexture = "Hangar/Items/Wepons/headerDualGun";
            ItemLayoutDescription layoutDescription4 = layoutDescription3;
            weaponItem3.HangarDesc = (HangarLayoutDescription) layoutDescription4;
            weaponItem1.UnlockCondition = this._unlockFactory.DualMachineGun();
            weaponItem1.Rate = WeaponRate.Fast;
            break;
          case WeaponType.PlasmaGun:
            weaponItem1.Name = "Plasma gun";
            WeaponItem weaponItem4 = weaponItem1;
            ItemLayoutDescription layoutDescription5 = new ItemLayoutDescription();
            layoutDescription5.OnCopterTexture = "Hangar/Weapon/weaponS1_4";
            layoutDescription5.OnShopTexture = "Hangar/Items/Wepons/weaponShopS1_4";
            layoutDescription5.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS1_4";
            layoutDescription5.HeaderTexture = "Hangar/Items/Wepons/headerPlazmaGun";
            ItemLayoutDescription layoutDescription6 = layoutDescription5;
            weaponItem4.HangarDesc = (HangarLayoutDescription) layoutDescription6;
            weaponItem1.UnlockCondition = this._unlockFactory.PlasmaGun();
            weaponItem1.Rate = WeaponRate.Fast;
            break;
          case WeaponType.Vulcan:
            weaponItem1.Name = "Vulcan";
            WeaponItem weaponItem5 = weaponItem1;
            ItemLayoutDescription layoutDescription7 = new ItemLayoutDescription();
            layoutDescription7.OnCopterTexture = "Hangar/Weapon/weaponS1_3";
            layoutDescription7.OnShopTexture = "Hangar/Items/Wepons/weaponShopS1_3";
            layoutDescription7.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS1_3";
            layoutDescription7.HeaderTexture = "Hangar/Items/Wepons/headerVulcan";
            ItemLayoutDescription layoutDescription8 = layoutDescription7;
            weaponItem5.HangarDesc = (HangarLayoutDescription) layoutDescription8;
            weaponItem1.UnlockCondition = this._unlockFactory.Vulcan();
            weaponItem1.Rate = WeaponRate.VeryFast;
            break;
        }
        this.WeaponItems.Add(weaponItem1);
      }
      foreach (WeaponType type in weaponTypeArray2)
      {
        WeaponItem weaponItem6 = this.CreateWeaponItem(type);
        weaponItem6.Slot = Slot.Second;
        switch (type)
        {
          case WeaponType.RocketLauncher:
            weaponItem6.Name = "Rocket Launcher";
            WeaponItem weaponItem7 = weaponItem6;
            ItemLayoutDescription layoutDescription9 = new ItemLayoutDescription();
            layoutDescription9.OnCopterTexture = "Hangar/Weapon/weaponS2_1";
            layoutDescription9.OnShopTexture = "Hangar/Items/Wepons/weaponShopS2_1";
            layoutDescription9.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS2_1";
            layoutDescription9.HeaderTexture = "Hangar/Items/Wepons/headerRocketLauncher";
            ItemLayoutDescription layoutDescription10 = layoutDescription9;
            weaponItem7.HangarDesc = (HangarLayoutDescription) layoutDescription10;
            weaponItem6.UnlockCondition = this._unlockFactory.RocketLauncher();
            weaponItem6.Rate = WeaponRate.Slow;
            break;
          case WeaponType.DualRocketLauncher:
            weaponItem6.Name = "Dual Rocket Launcher";
            WeaponItem weaponItem8 = weaponItem6;
            ItemLayoutDescription layoutDescription11 = new ItemLayoutDescription();
            layoutDescription11.OnCopterTexture = "Hangar/Weapon/weaponS2_2";
            layoutDescription11.OnShopTexture = "Hangar/Items/Wepons/weaponShopS2_2";
            layoutDescription11.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS2_2";
            layoutDescription11.HeaderTexture = "Hangar/Items/Wepons/headerDualRocketLauncher";
            ItemLayoutDescription layoutDescription12 = layoutDescription11;
            weaponItem8.HangarDesc = (HangarLayoutDescription) layoutDescription12;
            weaponItem6.UnlockCondition = this._unlockFactory.DualRocketLauncher();
            weaponItem6.Rate = WeaponRate.Medium;
            break;
          case WeaponType.CasseteRocket:
            weaponItem6.Name = "Cluster bomb";
            WeaponItem weaponItem9 = weaponItem6;
            ItemLayoutDescription layoutDescription13 = new ItemLayoutDescription();
            layoutDescription13.OnCopterTexture = "Hangar/Weapon/weaponS2_5";
            layoutDescription13.OnShopTexture = "Hangar/Items/Wepons/weaponShopS2_5";
            layoutDescription13.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS2_5";
            layoutDescription13.HeaderTexture = "Hangar/Items/Wepons/headerClusterBomb";
            ItemLayoutDescription layoutDescription14 = layoutDescription13;
            weaponItem9.HangarDesc = (HangarLayoutDescription) layoutDescription14;
            weaponItem6.UnlockCondition = this._unlockFactory.ClusterBomb();
            weaponItem6.Rate = WeaponRate.Slow;
            break;
          case WeaponType.Shield:
            weaponItem6.Name = "Shield";
            WeaponItem weaponItem10 = weaponItem6;
            ItemLayoutDescription layoutDescription15 = new ItemLayoutDescription();
            layoutDescription15.OnCopterTexture = "Hangar/Weapon/weaponS2_4";
            layoutDescription15.OnShopTexture = "Hangar/Items/Wepons/weaponShopS2_4";
            layoutDescription15.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS2_4";
            layoutDescription15.HeaderTexture = "Hangar/Items/Wepons/headerEnergyShield";
            ItemLayoutDescription layoutDescription16 = layoutDescription15;
            weaponItem10.HangarDesc = (HangarLayoutDescription) layoutDescription16;
            weaponItem6.UnlockCondition = this._unlockFactory.Shield();
            weaponItem6.Rate = WeaponRate.Slow;
            break;
          case WeaponType.HomingRocket:
            weaponItem6.Name = "Homing Rocket";
            WeaponItem weaponItem11 = weaponItem6;
            ItemLayoutDescription layoutDescription17 = new ItemLayoutDescription();
            layoutDescription17.OnCopterTexture = "Hangar/Weapon/weaponS2_3";
            layoutDescription17.OnShopTexture = "Hangar/Items/Wepons/weaponShopS2_3";
            layoutDescription17.OnShopBoughtTexture = "Hangar/ItemsGreen/Wepons/weaponShopS2_3";
            layoutDescription17.HeaderTexture = "Hangar/Items/Wepons/headerHomingRocket";
            ItemLayoutDescription layoutDescription18 = layoutDescription17;
            weaponItem11.HangarDesc = (HangarLayoutDescription) layoutDescription18;
            weaponItem6.UnlockCondition = this._unlockFactory.HomingRocket();
            weaponItem6.Rate = WeaponRate.Slow;
            break;
        }
        this.WeaponItems.Add(weaponItem6);
      }
    }

    public WeaponItem CreateWeaponItem(WeaponType type)
    {
      WeaponDescription descriptionForType = new WeaponDescriptionManager().GetDescriptionForType(type);
      return new WeaponItem()
      {
        Damage = descriptionForType.Damage,
        WeaponType = type
      };
    }

    public WeaponItem GetWeapon(WeaponType type)
    {
      return this.WeaponItems.FirstOrDefault<WeaponItem>((Func<WeaponItem, bool>) (x => x != null && x.WeaponType == type));
    }

    public WeaponItem GetWeapon(string id)
    {
      return this.WeaponItems.FirstOrDefault<WeaponItem>((Func<WeaponItem, bool>) (x => x != null && x.Id == id));
    }

    public HelicopterItem GetHelicopter(HelicopterType type)
    {
      return this.HelicopterItems.FirstOrDefault<HelicopterItem>((Func<HelicopterItem, bool>) (x => x != null && x.HelicopterType == type));
    }

    public HelicopterItem GetHelicopter(string id)
    {
      return this.HelicopterItems.FirstOrDefault<HelicopterItem>((Func<HelicopterItem, bool>) (x => x != null && x.Id == id));
    }

    public UpgradeItem GetUpgrade(UpgradeType type)
    {
      return this.DeviceItems.FirstOrDefault<UpgradeItem>((Func<UpgradeItem, bool>) (x => x != null && x.Type == type));
    }

    public UpgradeItem GetUpgrade(string id)
    {
      return this.DeviceItems.FirstOrDefault<UpgradeItem>((Func<UpgradeItem, bool>) (x => x != null && x.Id == id));
    }

    public AmmunitionItem GetAmmunition(string id)
    {
      return this.AmunitionItems.FirstOrDefault<AmmunitionItem>((Func<AmmunitionItem, bool>) (x => x != null && x.Id == id));
    }

    public HealthAmmunitionItem GetHealthAmunition(string value)
    {
      if (string.IsNullOrEmpty(value))
        return (HealthAmmunitionItem) null;
      HealthAmmunitionItem healthAmunition = new HealthAmmunitionItem();
      AmmunitionItem healthPack50 = AmmunitionFactory.GetHealthPack50();
      healthAmunition.HangarDesc = healthPack50.HangarDesc;
      healthAmunition.Volume = float.Parse(value, (IFormatProvider) CultureInfo.InvariantCulture);
      return healthAmunition;
    }
  }
}
