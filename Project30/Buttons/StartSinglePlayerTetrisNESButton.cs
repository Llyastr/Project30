using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class StartSinglePlayerTetrisNESButton : Button
    {
        static readonly string ButtonText = "Single Player";

        public StartSinglePlayerTetrisNESButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, ButtonText, locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(new EnterNameTetrisNESScreen(BaseGame));
        }
    }
}
