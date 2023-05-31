using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGameScreen : StaticScreen
    {
        readonly static string TextureName = "EmptyBackground";

        protected readonly string PlayerName;
        protected readonly int StartLevel;

        protected readonly List<TetrisGame> TetrisGames;

        public TetrisGameScreen(BaseGame baseGame, string playerName, int startLevel)
            : base(baseGame, TextureName)
        {
            Buttons = new Button[0];

            PlayerName = playerName;
            StartLevel = startLevel;

            TetrisGames = new List<TetrisGame>();
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState,
                                    MouseState previousMouseState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                foreach (TetrisGame tetrisGame in TetrisGames)
                {
                    tetrisGame.DoSetPauseMax();
                }

                BaseGame.ScreenManager.AddScreen(new PauseScreen(BaseGame));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.DarkBlue);
        }
    }
}
