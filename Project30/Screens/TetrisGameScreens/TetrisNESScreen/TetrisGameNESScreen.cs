using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGameNESScreen : TetrisGameScreen
    {
        readonly static string TextureName = "EmptyBackground";

        private readonly TetrisGame TetrisGame;

        private int TetrisGameLocationX
        {
            get { return (int)(BaseGame.ScreenWidth / 2.580645161290f); }
        }
        private int TetrisGameLocationY
        {
            get { return (int)(BaseGame.ScreenWidth / 44.44444444f); }
        }

        public TetrisGameNESScreen(BaseGame baseGame, string playerName, int startLevel)
            : base(baseGame, TextureName, startLevel)
        {
            Input Input = Input.GetPlayer1Input(BaseGame.SettingsSaveData);

            TetrisGame = new TetrisGameFromSettings(BaseGame, this, Input, playerName, startLevel, 
                                                    TetrisGameLocationX, TetrisGameLocationY);

            TetrisGames.Add(TetrisGame);

            Buttons = new Button[0];
        }
        
        public override void Update(GameTime gameTime, 
                                    KeyboardState keyboardState, 
                                    KeyboardState previousKeyboardState, 
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            TetrisGame.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.DarkBlue);

            TetrisGame.Draw(spriteBatch);
        }
    }
}
