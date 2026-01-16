// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Sounds.GameplaySounds
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances.Weapons;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.Sounds
{
  public class GameplaySounds
  {
    private const string Win = "Sounds/guns+utililty(mp3)/win";
    private const string Death = "Sounds/guns+utililty(mp3)/death";
    private const string PlasmaStart = "Sounds/guns+utililty(mp3)/plasma gun start";
    private const string PlasmaHit = "Sounds/guns+utililty(mp3)/plasma gun hit";
    private const string VulcanHitSound = "Sounds/guns+utililty(mp3)/bullet hits metal";
    private const string MachineGun1 = "Sounds/guns+utililty(mp3)/machine gun (single & dual)_new";
    private const string MissleSound = "Sounds/guns+utililty(mp3)/missle_new";
    private const string ClusterMissleSound = "Sounds/guns+utililty(mp3)/missle2";
    private const string ExplosionSound = "Sounds/guns+utililty(mp3)/explosion_new";
    private const string VulcanLoop = "Sounds/guns+utililty(mp3)/vulcan loop";
    private const string VulcanEnd = "Sounds/guns+utililty(mp3)/vulcan end";
    private const string VulcanCantShoot = "Sounds/guns+utililty(mp3)/vulcan can't shoot";
    private const string PlasmaLoop = "Sounds/guns+utililty(mp3)/laser1";
    private const string ExplosionMedium = "Sounds/guns+utililty(mp3)/explosion_medium";
    private const string Shield = "Sounds/guns+utililty(mp3)/energy shield hum (loop)";
    private const string ShieldCantShoot = "Sounds/guns+utililty(mp3)/plasma can't shoot";
    private static GameplaySounds _instance;
    private bool _isOddExplosion;
    private Dictionary<KeyValuePair<object, string>, SoundEffectInstance> _activeSounds = new Dictionary<KeyValuePair<object, string>, SoundEffectInstance>();

    public static GameplaySounds Instance
    {
      get => GameplaySounds._instance ?? (GameplaySounds._instance = new GameplaySounds());
    }

    public void PlayShotSound(WeaponType type)
    {
      switch (type)
      {
        case WeaponType.CopterSingleMachineGun:
        case WeaponType.CopterDualMachineGun:
        case WeaponType.BossSingleMachineGun:
        case WeaponType.BossDualMachineGun:
        case WeaponType.SingleMachineGun:
        case WeaponType.DualMachineGun:
        case WeaponType.CannonMachineGun:
        case WeaponType.CannonDualMachineGun:
          Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/machine gun (single & dual)_new", false);
          break;
        case WeaponType.CopterRocketLauncher:
        case WeaponType.CopterSharkRocketLauncher:
        case WeaponType.BossRocketLauncher:
        case WeaponType.RocketLauncher:
        case WeaponType.DualRocketLauncher:
        case WeaponType.HomingRocket:
        case WeaponType.CannonRocketLauncher:
          Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/missle_new", false);
          break;
        case WeaponType.CasseteRocket:
        case WeaponType.CasseteRocketUpdated:
          Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/missle2", false);
          break;
      }
    }

    protected GameplaySounds() => this.InitSounds();

    public void InitSounds()
    {
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/win", 1);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/death", 1);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/bullet hits metal", 1);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/vulcan loop", 1);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/vulcan end", 1);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/vulcan can't shoot", 1);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/energy shield hum (loop)", 2);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/plasma can't shoot", 2);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/plasma gun start", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/plasma gun hit", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/laser1", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/explosion_new", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/explosion_medium", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/machine gun (single & dual)_new", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/missle_new", 5);
      Helicopter.Model.Sounds.Audio.InitSound("Sounds/guns+utililty(mp3)/missle2", 5);
    }

    public void PlayVulcanLoop(object sender)
    {
      this.PlaySoundInstance(sender, "Sounds/guns+utililty(mp3)/vulcan loop", true);
    }

    public void PlayVulcanEnd(object sender)
    {
      this.StopInstance(sender, "Sounds/guns+utililty(mp3)/vulcan loop");
      Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/vulcan end", false);
    }

    public void PlayVulcanCantShoot()
    {
      Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/vulcan can't shoot", false);
    }

    public void PlayHittedSound()
    {
      Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/bullet hits metal", false);
    }

    public void PlayExplosion()
    {
      if (this._isOddExplosion)
      {
        Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/explosion_new", false);
        this._isOddExplosion = false;
      }
      else
      {
        this.PlayExplosionDroid();
        this._isOddExplosion = true;
      }
    }

    public void PlayExplosionDroid()
    {
      Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/explosion_medium", false);
    }

    public void OnUnitHitted(object sender, EventArgs e) => this.PlayHittedSound();

    public void OnFire(object sender, WeaponEventArgs e) => this.PlayShotSound(e.WeaponType);

    public void PlayPlasmaGunLoop(object sender)
    {
      this.PlaySoundInstance(sender, "Sounds/guns+utililty(mp3)/laser1", true);
    }

    public void StopPlasmaLoop(object sender)
    {
      this.StopInstance(sender, "Sounds/guns+utililty(mp3)/laser1");
    }

    public void PlayPlasmaGunHitting(object sender)
    {
      this.PlaySoundInstance(sender, "Sounds/guns+utililty(mp3)/plasma gun hit", true);
    }

    public void StopPlasmaGunHitting(object sender)
    {
      this.StopInstance(sender, "Sounds/guns+utililty(mp3)/plasma gun hit");
    }

    public void PlayPlasmaGunEnd(object sender)
    {
      this.StopPlasmaLoop(sender);
      this.StopPlasmaGunHitting(sender);
      this.StopPlasmaStart(sender);
    }

    public void PlayWin() => Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/win", false);

    public void PlayLose() => Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/death", false);

    public void PlayPlasmaStart(object sender)
    {
      this.PlaySoundInstance(sender, "Sounds/guns+utililty(mp3)/plasma gun start", false);
    }

    public void StopPlasmaStart(object sender)
    {
      this.StopInstance(sender, "Sounds/guns+utililty(mp3)/plasma gun start");
    }

    public void PlayShield(object sender)
    {
      this.PlaySoundInstance(sender, "Sounds/guns+utililty(mp3)/energy shield hum (loop)", true);
    }

    public void StopPlayShield(object sender)
    {
      this.StopInstance(sender, "Sounds/guns+utililty(mp3)/energy shield hum (loop)");
    }

    public void PlayShieldCantShoot()
    {
      Helicopter.Model.Sounds.Audio.PlaySound("Sounds/guns+utililty(mp3)/plasma can't shoot", false);
    }

    private void StopInstance(object sender, string fileName)
    {
      KeyValuePair<object, string> key = new KeyValuePair<object, string>(sender, fileName);
      if (!this._activeSounds.ContainsKey(key))
        return;
      this._activeSounds[key]?.Stop(true);
      this._activeSounds.Remove(key);
    }

    private void PlaySoundInstance(object sender, string fileName, bool isLooped)
    {
      this.StopInstance(sender, fileName);
      SoundEffectInstance soundEffectInstance = Helicopter.Model.Sounds.Audio.PlaySound(fileName, isLooped);
      if (soundEffectInstance == null)
        return;
      this._activeSounds[new KeyValuePair<object, string>(sender, fileName)] = soundEffectInstance;
    }
  }
}
