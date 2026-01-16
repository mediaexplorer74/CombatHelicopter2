// Decompiled with JetBrains decompiler
// Type: Helicopter.Utils.SoundManagers.BackgroundSounds
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.Sounds;
using System;

#nullable disable
namespace Helicopter.Utils.SoundManagers
{
  internal class BackgroundSounds
  {
    private const string MenuTheme = "Sounds/guns+utililty(mp3)/1+_short";
    private const string HangarTheme = "Sounds/guns+utililty(mp3)/garage";
    private const string CopterBlades = "Sounds/guns+utililty(mp3)/player's blade";
    public const string GameplaySound = "Sounds/gameplay music(mp3)/gameplay 2";
    public const string BossBattleSound = "Sounds/boss music/boss loop cut_vol";
    public const string WinSound = "Sounds/UI/win";
    public const string EpicWinSound = "Sounds/UI/epic win popup";
    private static BackgroundSounds _instance;
    private BackgroundSounds.Theme State;

    public static BackgroundSounds Instance
    {
      get => BackgroundSounds._instance ?? (BackgroundSounds._instance = new BackgroundSounds());
    }

    public bool BossBattle { get; set; }

    protected BackgroundSounds() => this.InitSounds();

    public void PlayGameplayTheme()
    {
      string fileName = this.BossBattle ? "Sounds/boss music/boss loop cut_vol" : "Sounds/gameplay music(mp3)/gameplay 2";
      this.State = BackgroundSounds.Theme.Gameplay;
      if (Audio.IsPlaying(fileName))
        return;
      this.StopTheme();
      this.PlayBlades();
      Audio.PlaySound(fileName, true);
    }

    public void PlayMenuTheme()
    {
      this.State = BackgroundSounds.Theme.MainMenu;
      if (Audio.IsPlaying("Sounds/guns+utililty(mp3)/1+_short"))
        return;
      this.StopTheme();
      Audio.PlaySound("Sounds/guns+utililty(mp3)/1+_short", false);
    }

    public void StopTheme()
    {
      Audio.StopAllSounds("Sounds/guns+utililty(mp3)/1+_short");
      Audio.StopAllSounds("Sounds/guns+utililty(mp3)/garage");
      Audio.StopAllSounds("Sounds/guns+utililty(mp3)/player's blade");
      Audio.StopAllSounds("Sounds/gameplay music(mp3)/gameplay 2");
      Audio.StopAllSounds("Sounds/boss music/boss loop cut_vol");
      Audio.StopAllSounds("Sounds/UI/win");
      Audio.StopAllSounds("Sounds/UI/epic win popup");
    }

    public void InitSounds()
    {
      Audio.InitSound("Sounds/guns+utililty(mp3)/1+_short", 1);
      Audio.InitSound("Sounds/guns+utililty(mp3)/garage", 1);
      Audio.InitSound("Sounds/guns+utililty(mp3)/player's blade", 1);
      Audio.InitSound("Sounds/gameplay music(mp3)/gameplay 2", 1);
      Audio.InitSound("Sounds/boss music/boss loop cut_vol", 1);
      Audio.InitSound("Sounds/UI/win", 1);
      Audio.InitSound("Sounds/UI/epic win popup", 1);
    }

    public void PlayHangerTheme()
    {
      this.State = BackgroundSounds.Theme.Hangar;
      this.StopTheme();
      Audio.PlaySound("Sounds/guns+utililty(mp3)/garage", true);
    }

    public void PlayBlades() => Audio.PlaySound("Sounds/guns+utililty(mp3)/player's blade", true);

    public void PlayWin() => Audio.PlaySound("Sounds/UI/win", false);

    public void PlayEpicWin() => Audio.PlaySound("Sounds/UI/epic win popup", false);

    public void PlayTheme()
    {
      switch (this.State)
      {
        case BackgroundSounds.Theme.MainMenu:
          this.PlayMenuTheme();
          break;
        case BackgroundSounds.Theme.Hangar:
          this.PlayBlades();
          break;
        case BackgroundSounds.Theme.Gameplay:
          this.PlayGameplayTheme();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private enum Theme
    {
      MainMenu,
      Hangar,
      Gameplay,
    }
  }
}
