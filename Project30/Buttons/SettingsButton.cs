using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class SettingsButton : Button
    {
        private static readonly string ButtonTextStatic = "Settings";

        public SettingsButton(BaseGame baseGame, int locationX, int locationY, int width, int height)
            : base(baseGame, ButtonTextStatic, locationX, locationY, width, height)
        {

        }

        public override void OnClick()
        {
            BaseGame.ScreenManager.AddScreen(new SettingsScreen(BaseGame));
        }
    }
}
