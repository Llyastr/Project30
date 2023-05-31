using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class EnterNameTetrisNESTwoPlayerScreen2 : EnterPlayerNameScreen
    {
        readonly string PlayerName1;

        protected override string PromptText
        {
            get { return "Enter name for Player 2:"; }
        }

        public EnterNameTetrisNESTwoPlayerScreen2(BaseGame baseGame, string playerName1)
            : base(baseGame)
        {
            PlayerName1 = playerName1;
        }

        public override void OnEnterPress()
        {
            if (PlayerName.Length < MinNameLength)
            {
                return;
            }

            Screen LevelSelectScreen = new LevelSelectTetrisNESTwoPlayerScreen(BaseGame, PlayerName1, PlayerName);

            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(LevelSelectScreen);
        }
    }
}
