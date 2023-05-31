using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class EnterNameTetrisNESTwoPlayerScreen1 : EnterPlayerNameScreen
    {
        protected override string PromptText
        {
            get { return "Enter name for Player 1:"; }
        }

        public EnterNameTetrisNESTwoPlayerScreen1(BaseGame baseGame)
            : base(baseGame)
        {
            
        }

        public override void OnEnterPress()
        {
            if (PlayerName.Length < MinNameLength)
            {
                return;
            }

            Screen NextScreen = new EnterNameTetrisNESTwoPlayerScreen2(BaseGame, PlayerName);

            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(NextScreen);
        }
    }
}
