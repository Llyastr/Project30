using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ExitButton : Button
    {
        public ExitButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, "Exit", locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.Exit();
        }
    }
}
