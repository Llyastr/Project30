using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class StartTwoPlayerTetrisNESButton : Button
    {
        static readonly string ButtonText = "Two Player";

        public StartTwoPlayerTetrisNESButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, ButtonText, locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(new EnterNameTetrisNESTwoPlayerScreen1(BaseGame));
        }
    }
}
