// Modified by MediaExplorer (2026)
// Type: Helicopter.Playing.GameProcess
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.Screen;

#nullable disable
namespace Helicopter.Playing
{
  public class GameProcess
  {
    private static GameProcess _instance;

    public static GameProcess Instance
    {
      get => GameProcess._instance ?? (GameProcess._instance = new GameProcess());
    }

    internal StoryModeHistory StoryModeHistory { get; set; }

    public StoryGameSession StoryGameSession { get; private set; }

    public ChallengeGameSession ChallengeGameSession { get; private set; }

    internal Gamer Gamer { get; private set; }

    internal Navigator Navigator { get; private set; }

    public GameProcess()
    {
      this.StoryModeHistory = new StoryModeHistory();
      this.StoryGameSession = new StoryGameSession(Gamer.Instance);
      this.ChallengeGameSession = new ChallengeGameSession(Gamer.Instance);
      this.Navigator = new Navigator();
    }

    public void Init(ScreenManager screenManager)
    {
      this.StoryGameSession.ScreenManager = screenManager;
      this.ChallengeGameSession.ScreenManager = screenManager;
      this.Navigator.ScreenManager = screenManager;
    }
  }
}
