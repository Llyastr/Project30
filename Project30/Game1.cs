using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Project30
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public KeyboardState KeyboardState;
        public KeyboardState PreviousKeyboardState;
        public MouseState MouseState;
        public MouseState PreviousMouseState;

        public Dictionary<string, Texture2D> Textures;
        public Dictionary<Tuple<string, int>, SpriteFont> Fonts;
        public Dictionary<string, SoundEffect> SoundEffects;
        public Dictionary<string, Song> Songs;

        BaseGame BaseGame;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(16.666666f);

            BaseGame = new BaseGame(this);

            SetScreenSize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures = new Dictionary<string, Texture2D>()
            {
                { "ButtonTexture", Content.Load<Texture2D>("WhiteButton") },
                { "WhiteSquare", Content.Load<Texture2D>("WhiteSquare") },
                { "WhiteSquareGrayOutline", Content.Load<Texture2D>("WhiteSquareLightGrayOutline") },
                { "WhiteSquareWithTriangle", Content.Load<Texture2D>("WhiteSquareWithTriangleAndLightGrayThickBorderGradient") },
                { "TitleBackground", Content.Load<Texture2D>("TitleBackground1") },
                { "EmptyBackground", Content.Load<Texture2D>("EmptyBackground") },
                { "WhiteLine", Content.Load<Texture2D>("WhiteLine") },
                { "DeleteButton", Content.Load<Texture2D>("DeleteButton") },
                { "PauseTexture", Content.Load<Texture2D>("PauseTexture") },
                { "PauseBackground", Content.Load<Texture2D>("PauseBackground2") },
                { "WhiteBackgroundWithBorder", Content.Load<Texture2D>("WhiteBackgroundWithBorder") },
            };

            Fonts = new Dictionary<Tuple<string, int>, SpriteFont>()
            {
                { new Tuple<string, int>("Font24", 1600), Content.Load<SpriteFont>("galleryFont") },
                { new Tuple<string, int>("Font24", 2400), Content.Load<SpriteFont>("galleryFontLarge") },
            };

            SoundEffects = new Dictionary<string, SoundEffect>()
            {
                { "PieceLockSound", Content.Load<SoundEffect>("TetrisSound1wav") }
            };

            Songs = new Dictionary<string, Song>()
            {
                
            };
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            BaseGame.Update(gameTime, KeyboardState, PreviousKeyboardState, MouseState, PreviousMouseState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            _spriteBatch.Begin();

            BaseGame.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void HandleInput()
        {
            PreviousKeyboardState = KeyboardState;
            PreviousMouseState = MouseState;
            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();
        }

        public void SetScreenSize()
        {
            _graphics.PreferredBackBufferWidth = BaseGame.ScreenWidth;
            _graphics.PreferredBackBufferHeight = BaseGame.ScreenHeight;
            _graphics.ApplyChanges();
        }
    }
}
