using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class BackScrollButton : VerticalScrollButton
    {
        public BackScrollButton(BaseGame baseGame, VerticalScrollSubscreen scrollSubscreen,
                                int locationX, int locationY, int width, int height)
            : base(baseGame, scrollSubscreen, "Back", locationX, locationY, width, height)
        {
            
        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.CloseScreen();
        }
    }
}
