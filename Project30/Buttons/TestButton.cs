using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TestButton : Button
    {
        public TestButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, "Test", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.AddScreen(new EnterPlayerNameScreen(BaseGame));
        }
    }
}
