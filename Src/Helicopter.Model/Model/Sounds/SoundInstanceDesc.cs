// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.Sounds.SoundInstanceDesc
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.Common;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Model.Sounds
{
  internal class SoundInstanceDesc
  {
    public SoundEffect Effect;
    public string Name;
    public List<SoundEffectInstance> EffectInstances;
    public int MaxNumberOfInstances;

    public SoundInstanceDesc(string name, int maxInstances)
    {
      if (maxInstances < 1)
        throw new ArgumentOutOfRangeException("maxInstances number should be more then 1");
      this.Name = name;
      this.Effect = ResourcesManager.Instance.GetResource<SoundEffect>(name);
      this.EffectInstances = new List<SoundEffectInstance>();
      this.MaxNumberOfInstances = maxInstances;
      for (int index = 0; index < maxInstances; ++index)
        this.CreateInstance();
    }

    public bool CreateInstance()
    {
      if (this.EffectInstances.Count >= this.MaxNumberOfInstances)
        return false;
      this.EffectInstances.Add(this.Effect.CreateInstance());
      return true;
    }

    public SoundEffectInstance PlayFreeInstance(bool isLooped)
    {
      foreach (SoundEffectInstance effectInstance in this.EffectInstances)
      {
        if (effectInstance.State != SoundState.Playing)
        {
          if (!effectInstance.IsLooped && isLooped)
            effectInstance.IsLooped = true;
          effectInstance.Play();
          return effectInstance;
        }
      }
      return (SoundEffectInstance) null;
    }

    public bool IsAnyPlaying()
    {
      return this.EffectInstances.Any<SoundEffectInstance>((Func<SoundEffectInstance, bool>) (x => x.State == SoundState.Playing));
    }

    public void StopAll()
    {
      foreach (SoundEffectInstance effectInstance in this.EffectInstances)
        effectInstance.Stop(true);
    }

    public void RemoveAllInstances()
    {
      this.StopAll();
      this.EffectInstances.Clear();
    }
  }
}
