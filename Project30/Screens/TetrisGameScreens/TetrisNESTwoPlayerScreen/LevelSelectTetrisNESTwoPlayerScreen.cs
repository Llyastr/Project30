using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class LevelSelectTetrisNESTwoPlayerScreen : LevelSelectScreen
    {
        readonly string PlayerName1;
        readonly string PlayerName2;

        public LevelSelectTetrisNESTwoPlayerScreen(BaseGame baseGame, string playerName1, string playerName2)
            : base(baseGame, playerName1)
        {
            PlayerName1 = playerName1;
            PlayerName2 = playerName2;
        }

        public override void OnButtonClick(int level)
        {
            Screen NextTetrisGameScreen = new TetrisGameNESTwoPlayerScreen(BaseGame, PlayerName1, PlayerName2, level);
            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(NextTetrisGameScreen);
        }
    }
}
