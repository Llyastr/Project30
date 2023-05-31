using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class LevelSelectTetrisNESScreen : LevelSelectScreen
    {
        public LevelSelectTetrisNESScreen(BaseGame baseGame, string playerName)
            : base(baseGame, playerName)
        {

        }

        public override void OnButtonClick(int level)
        {
            Screen NextTetrisGameScreen = new TetrisGameNESScreen(BaseGame, PlayerName, level);
            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(NextTetrisGameScreen);
        }
    }
}
