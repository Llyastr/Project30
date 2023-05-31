using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class SaveButton : Button
    {
        TetrisGame TetrisGame;

        readonly string AfterClickedText = "Game Saved";
        readonly string IdleText = "Save";

        const int AfterClickTimerMax = 59;

        int AfterClickTimer;
        bool IsAfterClick
        {
            get { return AfterClickTimer > 0; }
        }

        protected override string Text
        {
            get
            {
                if (IsAfterClick)
                {
                    return AfterClickedText;
                }
                else
                {
                    return IdleText;
                }
            }
        }

        public SaveButton(BaseGame baseGame, TetrisGame tetrisGame, 
                          int locationX, int locationY, int width, int height)
            : base(baseGame, "Save", locationX, locationY, width, height)
        {
            TetrisGame = tetrisGame;
        }

        public override void Update(GameTime gameTime, 
                                    KeyboardState keyboardState, 
                                    KeyboardState previousKeyboardState, 
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            AfterClickTimer = Math.Max(AfterClickTimer - 1, 0);
        }

        public override void OnClick()
        {
            TetrisGame.SaveGameStateData();
            AfterClickTimer = AfterClickTimerMax;
        }
    }
}
