using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class PauseGameButton : Button
    {
        TetrisGame TetrisGame;

        private bool IsPaused
        {
            get { return TetrisGame.IsPausedInternalIndefinate; }
        }

        private readonly string PausedText = "Resume";
        private readonly string UnpausedText = "Pause";
        protected override string Text
        {
            get
            {
                if (IsPaused)
                {
                    return PausedText;
                }
                else
                {
                    return UnpausedText;
                }
            }
        }

        public PauseGameButton(BaseGame baseGame, TetrisGame tetrisGame, 
                           int locationX, int locationY, int width, int height)
            : base(baseGame, "Pause", locationX, locationY, width, height)
        {
            TetrisGame = tetrisGame;
        }

        public override void OnClick()
        {
            TetrisGame.TogglePause();
        }
    }
}
