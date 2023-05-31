using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TitleButton : Button
    {
        public TitleButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, "Title", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.CloseAll();
            BaseGame.ScreenManager.AddScreen(new TitleScreen(BaseGame));
        }
    }
}
