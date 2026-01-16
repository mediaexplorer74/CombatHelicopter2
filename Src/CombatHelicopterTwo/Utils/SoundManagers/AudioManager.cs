// Decompiled with JetBrains decompiler
// Type: Helicopter.Utils.SoundManagers.AudioManager
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Sounds;

#nullable disable
namespace Helicopter.Utils.SoundManagers
{
  public class AudioManager
  {
    internal BackgroundSounds BackgroundSounds = BackgroundSounds.Instance;
    internal GameplaySounds GameplaySounds = GameplaySounds.Instance;
    internal HangarSounds HangarSounds = HangarSounds.Instance;

    public void SoundStateChanged(object sender, BooleanEventArgs e)
    {
      SettingsGame.Sound = e.State;
      if (!SettingsGame.Sound)
        this.StopAllSounds();
      else
        this.BackgroundSounds.PlayTheme();
    }

    private void StopAllSounds() => Audio.StopAllSounds();
  }
}
