using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGameNESTwoPlayerScreen : TetrisGameScreen
    {
        readonly static string TextureName = "EmptyBackground";

        TetrisGame TetrisGame1;
        TetrisGame TetrisGame2;

        const int TetrisGame1LocationX = 36;
        const int TetrisGame2LocationX = 900;
        const int TetrisGameLocationY = 36;

        public TetrisGameNESTwoPlayerScreen(BaseGame baseGame, string playerName1, string playerName2, int startLevel)
            : base(baseGame, TextureName, startLevel)
        {
            Input Input1 = Input.GetPlayer2Input(BaseGame.SettingsSaveData);
            Input Input2 = Input.GetPlayer1Input(BaseGame.SettingsSaveData);

            TetrisGame1 = new TetrisGameFromSettings(BaseGame, this, Input1, playerName1, startLevel,
                                                     TetrisGame1LocationX, TetrisGameLocationY);
            TetrisGame2 = new TetrisGameFromSettings(baseGame, this, Input2, playerName2, startLevel,
                                                     TetrisGame2LocationX, TetrisGameLocationY);

            TetrisGames.Add(TetrisGame1);
            TetrisGames.Add(TetrisGame2);

            Buttons = new Button[0];
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState,
                                    MouseState previousMouseState)
        {
            TetrisGame1.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
            TetrisGame2.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.DarkBlue);

            TetrisGame1.Draw(spriteBatch);
            TetrisGame2.Draw(spriteBatch);
        }
    }
}
