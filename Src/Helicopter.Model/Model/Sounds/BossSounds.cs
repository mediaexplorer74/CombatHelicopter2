// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Sounds.BossSounds
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;

#nullable disable
namespace Helicopter.Model.Sounds
{
  public class BossSounds
  {
    private const string Explosion1 = "Sounds/boss music/boss 1st explosion";
    private const string Explosion23 = "Sounds/boss music/boss 2nd&3rd explosions";
    private const string Explosion4 = "Sounds/boss music/boss final explosion";
    private const string Sparlkes = "Sounds/boss music/boss sparlkes loop";
    private const string Door = "Sounds/boss music/boss door opens";
    private static BossSounds _instance;

    public static BossSounds Instance
    {
      get => BossSounds._instance ?? (BossSounds._instance = new BossSounds());
    }

    public void PlayExplosion(int explosion)
    {
      string empty = string.Empty;
      string fileName;
      switch (explosion)
      {
        case 1:
          fileName = "Sounds/boss music/boss 1st explosion";
          break;
        case 2:
        case 3:
          fileName = "Sounds/boss music/boss 2nd&3rd explosions";
          break;
        case 4:
          fileName = "Sounds/boss music/boss final explosion";
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (explosion), string.Format("Unknown explosion number '{0}'.", (object) explosion));
      }
      Audio.PlaySound(fileName, false);
    }

    public void PlaySparlkes() => Audio.PlaySound("Sounds/boss music/boss sparlkes loop", true);

    public void PlayDoor() => Audio.PlaySound("Sounds/boss music/boss door opens", false);

    protected BossSounds() => this.InitSounds();

    public void InitSounds()
    {
      Audio.InitSound("Sounds/boss music/boss 1st explosion", 1);
      Audio.InitSound("Sounds/boss music/boss 2nd&3rd explosions", 1);
      Audio.InitSound("Sounds/boss music/boss final explosion", 1);
      Audio.InitSound("Sounds/boss music/boss sparlkes loop", 1);
      Audio.InitSound("Sounds/boss music/boss door opens", 1);
    }

    public void StopSounds()
    {
      Audio.StopAllSounds("Sounds/boss music/boss 1st explosion");
      Audio.StopAllSounds("Sounds/boss music/boss 2nd&3rd explosions");
      Audio.StopAllSounds("Sounds/boss music/boss final explosion");
      Audio.StopAllSounds("Sounds/boss music/boss sparlkes loop");
      Audio.StopAllSounds("Sounds/boss music/boss door opens");
    }
  }
}
