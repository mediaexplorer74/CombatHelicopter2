using Helicopter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace HelocopterSL
{
    public sealed class Game1 : Game
    {
        HelicopterGame _helicopter;
        SpriteBatch _spriteBatch;        
        private GraphicsDeviceManager _graphics;        
        public static readonly int minViewportWidth = 384;
        public static readonly int minViewportHeight = 216;
        public static int gameWidth;
        public static int gameHeight;
        public static Rectangle viewportRectangle;
        private static RenderTarget2D nativeRenderTarget;
        private static RenderTarget2D renderTarget_zoom1;
        private static RenderTarget2D renderTarger_zoom0dot5;
        public static double scaleX;
        public static double scaleY;
        public static MouseState currentMouseState;
        public static MouseState previousMouseState;
        public static TouchCollection currentTouchState;
        public static TouchCollection previousTouchState;
        public static KeyboardState previousKeyboardState;
        public static KeyboardState currentKeyboardState;
        public static GamePadState previousGamePadState;
        public static GamePadState currentGamePadState;
        
        private bool gameInitialized;

        public static Texture2D white;


        public Game1()
        {
            this._graphics = new GraphicsDeviceManager((Game)this);
            this._graphics.GraphicsProfile = GraphicsProfile.Reach;
            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this._graphics.IsFullScreen = false; // set it *true* for W10M 
            this.IsMouseVisible = true;

            Game1.gameWidth = Game1.minViewportWidth;
            Game1.gameHeight = Game1.minViewportHeight;

            Game1.scaleX = (1f * GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / Game1.gameWidth);
            Game1.scaleY = (1f * GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / Game1.gameHeight);

            double scale = MathHelper.Min((float)Game1.scaleX, (float)Game1.scaleY);

            // RnD: my very fast & stupid "scale problem solve/solving" =)
            // Is your screen in portrait mode?
            if (Math.Abs(Game1.scaleX - Game1.scaleY) > 2)
            {
                // yea, "W10M" screen mode ;)
                Game1.scaleX = scale;
                Game1.scaleY = scale * 1.9f;
            }
            else
            {
                // no, "Desktop-PC/notebook" (Win11) screen mode ;)
                Game1.scaleX = scale;
                Game1.scaleY = scale * 0.9f;

                this._graphics.IsFullScreen = false; // set it *false* for "big-screen" devices 
            }

            Game1.renderTarget_zoom1 = new RenderTarget2D(this.GraphicsDevice, Game1.gameWidth, Game1.gameHeight);
            Game1.renderTarger_zoom0dot5 = new RenderTarget2D(this.GraphicsDevice, Game1.gameWidth * 2, Game1.gameHeight * 2);

            Game1.viewportRectangle = new Rectangle(
                (int)(this.GraphicsDevice.DisplayMode.Width - Game1.gameWidth * Game1.scaleX) / 2,
                (int)(this.GraphicsDevice.DisplayMode.Height - Game1.gameHeight * Game1.scaleY) / 2,
                (int)(Game1.gameWidth * Game1.scaleX),
                (int)(Game1.gameHeight * Game1.scaleY)
                );

            this._graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            this._graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            this._graphics.ApplyChanges();

            Game1.previousKeyboardState = Keyboard.GetState();
            Game1.previousGamePadState = GamePad.GetState(PlayerIndex.One);

            Game1.previousMouseState = Mouse.GetState();
            Game1.previousTouchState = TouchPanel.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._spriteBatch = new SpriteBatch(this.GraphicsDevice);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _helicopter = new HelicopterGame();
            _helicopter.OnActivated(this, EventArgs.Empty);
            _helicopter.Init(GraphicsDevice, Content);
        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Game1.currentKeyboardState = Keyboard.GetState();
            Game1.currentGamePadState = GamePad.GetState(PlayerIndex.One);
            Game1.currentMouseState = Mouse.GetState();
            Game1.currentTouchState = TouchPanel.GetState();

            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _helicopter.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this._spriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue); //
            
            _helicopter.Draw(gameTime);

            this._spriteBatch.End();
      
            base.Draw(gameTime);
        }
    }
}
