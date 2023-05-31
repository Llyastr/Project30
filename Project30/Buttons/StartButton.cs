using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class StartButton : Button
    {
        public StartButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, "Start", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.AddScreen(new GameTypeSelectionScreen(BaseGame));
        }
    }
}
