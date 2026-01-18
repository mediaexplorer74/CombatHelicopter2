using Helicopter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using Helicopter.Model.Common;

namespace HelocopterSL
{
    public sealed class Game1 : Game
    {
        HelicopterGame _helicopter;
        SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        public static readonly int minViewportWidth = 800;
        public static readonly int minViewportHeight = 480;
        public static int gameWidth;
        public static int gameHeight;
        public static Rectangle viewportRectangle;
        private static RenderTarget2D nativeRenderTarget;
        private int _lastBackBufferWidth;
        private int _lastBackBufferHeight;
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
            this._graphics.IsFullScreen = true; // set false for desktop version
            this.IsMouseVisible = true;

            Game1.gameWidth = Game1.minViewportWidth;
            Game1.gameHeight = Game1.minViewportHeight;

            nativeRenderTarget = new RenderTarget2D(this.GraphicsDevice, Game1.gameWidth, Game1.gameHeight);
            // Устанавливаем начальные размеры TouchPanel равными игровым размерам
            TouchPanel.DisplayWidth = Game1.gameWidth;
            TouchPanel.DisplayHeight = Game1.gameHeight;
            this._graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this._graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this._graphics.ApplyChanges();
            
            // После ApplyChanges обновляем размеры TouchPanel и Viewport
            TouchPanel.DisplayWidth = this.GraphicsDevice.Viewport.Width;
            TouchPanel.DisplayHeight = this.GraphicsDevice.Viewport.Height;
            UpdateViewport();

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
            if (this.GraphicsDevice.Viewport.Width != _lastBackBufferWidth || this.GraphicsDevice.Viewport.Height != _lastBackBufferHeight)
            {
                UpdateViewport();
            }
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
            this.GraphicsDevice.SetRenderTarget(nativeRenderTarget);
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            _helicopter.Draw(gameTime);
            this.GraphicsDevice.SetRenderTarget(null);
            this._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearClamp, null, null);
            this._spriteBatch.Draw(nativeRenderTarget, viewportRectangle, Color.White);
            this._spriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateViewport()
        {
            int windowWidth = this.GraphicsDevice.Viewport.Width;
            int windowHeight = this.GraphicsDevice.Viewport.Height;
            float scaleX = windowWidth / (float)Game1.gameWidth;
            float scaleY = windowHeight / (float)Game1.gameHeight;
            float scale = Math.Min(scaleX, scaleY);
            int vpWidth = (int)(Game1.gameWidth * scale);
            int vpHeight = (int)(Game1.gameHeight * scale);
            int vpX = (windowWidth - vpWidth) / 2;
            int vpY = (windowHeight - vpHeight) / 2;
            viewportRectangle = new Rectangle(vpX, vpY, vpWidth, vpHeight);
            _lastBackBufferWidth = windowWidth;
            _lastBackBufferHeight = windowHeight;
            InputTransform.ViewportX = vpX;
            InputTransform.ViewportY = vpY;
            InputTransform.ViewportWidth = vpWidth;
            InputTransform.ViewportHeight = vpHeight;
            InputTransform.GameWidth = Game1.gameWidth;
            InputTransform.GameHeight = Game1.gameHeight;
            // Устанавливаем размеры TouchPanel равными размеру окна для правильной работы InputState
            // InputState будет преобразовывать координаты из оконных в игровые
            TouchPanel.DisplayWidth = this.GraphicsDevice.Viewport.Width;
            TouchPanel.DisplayHeight = this.GraphicsDevice.Viewport.Height;
        }
    }
}
