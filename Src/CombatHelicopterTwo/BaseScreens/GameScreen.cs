// Decompiled with JetBrains decompiler
// Type: Helicopter.BaseScreens.GameScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens.Controls;
using Microsoft.Xna.Framework;
using System.IO;

#nullable disable
namespace Helicopter.BaseScreens
{
  public abstract class GameScreen
  {
    private bool otherScreenHasFocus;

    public bool OnTop { get; set; }

    public bool IsPopup { get; protected set; }

    public bool IsActive => !this.otherScreenHasFocus;

    public ScreenManager ScreenManager { get; internal set; }

    public bool IsFullyLoaded { get; set; }

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw(DrawContext drawContext)
    {
    }

    public virtual void Deserialize(Stream stream)
    {
    }

    public void ExitScreen() => this.ScreenManager.RemoveScreen(this);

    public virtual void HandleInput(InputState input)
    {
    }

    public virtual void LoadContent()
    {
    }

    public virtual void Serialize(Stream stream)
    {
    }

    public virtual void UnloadContent()
    {
    }

    public virtual void TransitionComplete()
    {
    }

    public abstract void OnBackButton();
  }
}
