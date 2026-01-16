// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.SpriteObjects.WeaponSprites.PlasmaGunWeaponSprite
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.Model.SpriteObjects.WeaponSprites
{
  internal class PlasmaGunWeaponSprite : WeaponSpriteObject
  {
    private static readonly ObjectPool<PlasmaGunWeaponSprite> _pool = new ObjectPool<PlasmaGunWeaponSprite>((ICreation<PlasmaGunWeaponSprite>) new PlasmaGunWeaponSprite.Creator());

    public static PlasmaGunWeaponSprite GetInstance() => PlasmaGunWeaponSprite._pool.GetObject();

    protected override void ReleaseFromPool() => PlasmaGunWeaponSprite._pool.Release(this);

    public override void Init(SpriteObject parent, Weapon weapon)
    {
      this.Offset = weapon.BaseWeaponPosition;
      this.TexturePath = "GameWorld/Objects/Weapon/weaponS1_4";
      this.FireAnimation = CommonAnimatedSprite.GetInstance();
      if (parent is CopterSpriteObject)
        this.FireAnimation.Init("Effects/PlazmaShotRed/PlazmaShotXML");
      else
        this.FireAnimation.Init("Effects/PlazmaShot/PlazmaShotXML");
      this.FireAnimation.Origin = new Vector2(19f, 26f);
      weapon.ShootingState += new EventHandler(this.OnStateChanged);
      base.Init(parent, weapon);
      weapon.Fired -= new EventHandler<WeaponEventArgs>(this.OnWeaponFired);
    }

    private void OnStateChanged(object sender, EventArgs e)
    {
      PlasmaGunWeapon plasmaGunWeapon = (PlasmaGunWeapon) sender;
      if (this.FireAnimation == null)
        return;
      if (plasmaGunWeapon.IsShooting)
        this.FireAnimation.Play();
      else
        this.FireAnimation.Visible = false;
    }

    protected new class Creator : ICreation<PlasmaGunWeaponSprite>
    {
      public PlasmaGunWeaponSprite Create() => new PlasmaGunWeaponSprite();
    }
  }
}
