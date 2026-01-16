// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.DrawContext
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public struct DrawContext
  {
    public GraphicsDevice Device;
    public GameTime GameTime;
    public SpriteBatch SpriteBatch;
    public Texture2D BlankTexture;
    public Vector2 DrawOffset;
  }
}
