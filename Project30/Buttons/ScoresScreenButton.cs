using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScoresScreenButton : Button
    {
        public ScoresScreenButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, "Leaderboard", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.AddScreen(new ScoresScreen(BaseGame));
        }
    }
}
