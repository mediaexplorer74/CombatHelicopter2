// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.FireControlFactory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.GamePlay
{
  internal class FireControlFactory
  {
    public static FireControl GetButtonForWeapon(
      Weapon weapon,
      Vector2 position,
      Rectangle contactArea)
    {
      Sprite textureWithoutBorder = (Sprite) null;
      Sprite reloadTexture = (Sprite) null;
      Sprite sprite1;
      Sprite sprite2;
      switch (weapon.Type)
      {
        case WeaponType.SingleMachineGun:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_1");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_1Select");
          break;
        case WeaponType.DualMachineGun:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_2");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_2Select");
          break;
        case WeaponType.RocketLauncher:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_1a");
          textureWithoutBorder = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_1");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_1SelectA");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.DualRocketLauncher:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_2a");
          textureWithoutBorder = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_2");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_2SelectA");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.CasseteRocket:
        case WeaponType.CasseteRocketUpdated:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_4a");
          textureWithoutBorder = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_4");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_4SelectA");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.Shield:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_5a");
          textureWithoutBorder = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_5");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_5SelectA");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.HomingRocket:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_3a");
          textureWithoutBorder = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_3");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2_3SelectA");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.PlasmaGun:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_4");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_4Select");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.Vulcan:
          sprite1 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_3a");
          sprite2 = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot1/butWeaponS1_3SelectA");
          reloadTexture = ResourcesManager.Instance.GetSprite("UI/Weapon Button/slot2/butWeaponS2Charge");
          break;
        case WeaponType.CannonMachineGun:
        case WeaponType.CannonRocketLauncher:
        case WeaponType.CannonDualMachineGun:
          throw new Exception("These weapons are for enemies only");
        default:
          throw new ArgumentOutOfRangeException();
      }
      return new FireControl(weapon, sprite1, textureWithoutBorder, sprite2, reloadTexture, position, contactArea);
    }
  }
}
