// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.Navigator
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.Screen.LeaderBoard;
using Helicopter.Screen.MainMenu;
using Helicopter.Screen.MapScreen;
using Helicopter.Screen.TrophyRoom;
using System;

#nullable disable
namespace Helicopter.Screen
{
  internal class Navigator
  {
    public ScreenManager ScreenManager { get; set; }

    public GameScreen ShowScreen(ScreenType screenType)
    {
      this.ScreenManager.ExitAllScreens();
      switch (screenType)
      {
        case ScreenType.MainMenu:
          return this.ShowMainMenu();
        case ScreenType.Map:
          return this.ShowMap();
        case ScreenType.Leaderboard:
          return this.ShowLeaderboard();
        case ScreenType.TrophyRoom:
          return this.ShowTrophyRoom();
        default:
          throw new ArgumentOutOfRangeException(nameof (screenType));
      }
    }

    private GameScreen ShowTrophyRoom()
    {
      TrophyRoomScreen screen = new TrophyRoomScreen();
      this.ScreenManager.AddScreen((GameScreen) screen);
      return (GameScreen) screen;
    }

    private GameScreen ShowLeaderboard()
    {
      LeaderBoardScreen screen = new LeaderBoardScreen();
      this.ScreenManager.AddScreen((GameScreen) screen);
      return (GameScreen) screen;
    }

    private GameScreen ShowMap()
    {
      ChoseLevelMapScreen screen = new ChoseLevelMapScreen();
      this.ScreenManager.AddScreen((GameScreen) screen);
      return (GameScreen) screen;
    }

    private GameScreen ShowMainMenu()
    {
      MainMenuScreen screen = new MainMenuScreen();
      this.ScreenManager.AddScreen((GameScreen) screen);
      return (GameScreen) screen;
    }
  }
}
