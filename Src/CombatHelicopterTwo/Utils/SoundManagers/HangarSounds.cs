// Modified by MediaExplorer (2026)
// Type: Helicopter.Utils.SoundManagers.HangarSounds
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Model.Sounds;

#nullable disable
namespace Helicopter.Utils.SoundManagers
{
  internal class HangarSounds
  {
    private const string CopterSlide = "Sounds/guns+utililty(mp3)/copter garage slide";
    private const string Equip = "Sounds/guns+utililty(mp3)/equip heavy weapon";
    private const string RepairSound = "Sounds/guns+utililty(mp3)/repair";
    private const string BottomMenuSlide = "Sounds/guns+utililty(mp3)/garage equip slide beep";
    private const string Bought = "Sounds/guns+utililty(mp3)/cash";
    private static HangarSounds _instance;

    public static HangarSounds Instance
    {
      get => HangarSounds._instance ?? (HangarSounds._instance = new HangarSounds());
    }

    protected HangarSounds() => this.InitSounds();

    private void InitSounds()
    {
      Audio.InitSound("Sounds/guns+utililty(mp3)/copter garage slide", 1);
      Audio.InitSound("Sounds/guns+utililty(mp3)/equip heavy weapon", 1);
      Audio.InitSound("Sounds/guns+utililty(mp3)/repair", 1);
      Audio.InitSound("Sounds/guns+utililty(mp3)/garage equip slide beep", 1);
      Audio.InitSound("Sounds/guns+utililty(mp3)/cash", 1);
    }

    public void PlayCopterSlide()
    {
      Audio.PlaySound("Sounds/guns+utililty(mp3)/copter garage slide", false);
    }

    public void PlayEquiped()
    {
      Audio.PlaySound("Sounds/guns+utililty(mp3)/equip heavy weapon", false);
    }

    public void PlayRepair() => Audio.PlaySound("Sounds/guns+utililty(mp3)/repair", false);

    public void PlayBottomMenuSlide()
    {
      Audio.PlaySound("Sounds/guns+utililty(mp3)/garage equip slide beep", false);
    }

    public void PlayBought() => Audio.PlaySound("Sounds/guns+utililty(mp3)/cash", false);
  }
}
