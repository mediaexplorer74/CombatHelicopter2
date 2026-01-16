// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Sounds.Audio
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.Sounds
{
  public class Audio
  {
    private bool _enableSound;
    private static readonly Helicopter.Model.Sounds.Audio _instance = new Helicopter.Model.Sounds.Audio();
    private readonly Dictionary<string, SoundInstanceDesc> _instances = new Dictionary<string, SoundInstanceDesc>();

    public bool EnableSound
    {
      get => this._enableSound;
      set => this._enableSound = value;
    }

    public static Helicopter.Model.Sounds.Audio Instance => Helicopter.Model.Sounds.Audio._instance;

    public float MasterVolume
    {
      get => SoundEffect.MasterVolume;
      set => SoundEffect.MasterVolume = MathHelper.Clamp(value, 0.0f, 1f);
    }

    public static bool IsPlaying(string fileName)
    {
      return Helicopter.Model.Sounds.Audio.Instance.EnableSound && Helicopter.Model.Sounds.Audio.Instance._instances.ContainsKey(fileName) && Helicopter.Model.Sounds.Audio.Instance._instances[fileName].IsAnyPlaying();
    }

    public static void PlayMusic(string songFilename, bool repeated)
    {
      Song resource = ResourcesManager.Instance.GetResource<Song>(songFilename);
      MediaPlayer.IsRepeating = repeated;
      MediaPlayer.Play(resource);
    }

    public static void InitSound(string filename, int maxInstances)
    {
      if (Helicopter.Model.Sounds.Audio.Instance._instances.ContainsKey(filename))
        return;
      SoundInstanceDesc soundInstanceDesc = new SoundInstanceDesc(filename, maxInstances);
      Helicopter.Model.Sounds.Audio.Instance._instances[filename] = soundInstanceDesc;
    }

    public static SoundEffectInstance PlaySound(string fileName, bool isLooped)
    {
      if (!Helicopter.Model.Sounds.Audio.Instance.EnableSound)
        return (SoundEffectInstance) null;
      if (Helicopter.Model.Sounds.Audio.Instance._instances.ContainsKey(fileName))
        return Helicopter.Model.Sounds.Audio.Instance._instances[fileName].PlayFreeInstance(isLooped);
      throw new NullReferenceException(string.Format("Sound {0} is not initialized", (object) fileName));
    }

    public static void StopAllSounds()
    {
      foreach (KeyValuePair<string, SoundInstanceDesc> instance in Helicopter.Model.Sounds.Audio.Instance._instances)
        instance.Value.StopAll();
    }

    public static void StopAllSounds(string filename)
    {
      if (!Helicopter.Model.Sounds.Audio.Instance._instances.ContainsKey(filename))
        return;
      Helicopter.Model.Sounds.Audio.Instance._instances[filename].StopAll();
    }
  }
}
