using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class BackButton : Button
    {
        public BackButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, "Back", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.CloseScreen();
        }
    }
}
