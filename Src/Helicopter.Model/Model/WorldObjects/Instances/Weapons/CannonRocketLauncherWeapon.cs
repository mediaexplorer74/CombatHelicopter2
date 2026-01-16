// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.Instances.Weapons.CannonRocketLauncherWeapon
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons.Bullets;
using Helicopter.Model.WorldObjects.Modifiers;

#nullable disable
namespace Helicopter.Model.WorldObjects.Instances.Weapons
{
  internal class CannonRocketLauncherWeapon(Instance owner) : RocketLauncherWeapon(owner)
  {
    public override Bullet GetBullet()
    {
      RocketBullet instance = RocketBullet.GetInstance();
      instance.HasStartStage = false;
      instance.DamageType = DamageType.Rocket;
      return (Bullet) instance;
    }
  }
}
