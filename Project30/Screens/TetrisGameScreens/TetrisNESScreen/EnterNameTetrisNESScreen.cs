using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class EnterNameTetrisNESScreen : EnterPlayerNameScreen
    {
        public EnterNameTetrisNESScreen(BaseGame baseGame)
            : base(baseGame)
        {

        }

        public override void OnEnterPress()
        {
            if (PlayerName.Length < MinNameLength)
            {
                return;
            }

            Screen LevelSelectScreen = new LevelSelectTetrisNESScreen(BaseGame, PlayerName);

            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(LevelSelectScreen);
        }
    }
}
