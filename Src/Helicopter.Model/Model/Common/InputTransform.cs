using Microsoft.Xna.Framework;

namespace Helicopter.Model.Common
{
    public static class InputTransform
    {
        public static int ViewportX { get; set; }
        public static int ViewportY { get; set; }
        public static int ViewportWidth { get; set; }
        public static int ViewportHeight { get; set; }
        public static int GameWidth { get; set; }
        public static int GameHeight { get; set; }

        public static Vector2 WindowToGame(Vector2 p)
        {
            float x = (p.X - ViewportX) * (GameWidth / (float)ViewportWidth);
            float y = (p.Y - ViewportY) * (GameHeight / (float)ViewportHeight);
            return new Vector2(x, y);
        }

        public static bool IsInsideViewport(Vector2 p)
        {
            return p.X >= ViewportX &&
                   p.X < ViewportX + ViewportWidth &&
                   p.Y >= ViewportY &&
                   p.Y < ViewportY + ViewportHeight;
        }
    }
}
