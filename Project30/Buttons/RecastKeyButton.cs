using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class RecastKeyButton : VerticalScrollButton
    {
        protected override string Text
        {
            get { return LinkedKey.ToString(); }
        }

        private readonly string LinkedKeyName;

        private Keys LinkedKey
        {
            get { return BaseGame.SettingsSaveData.Keys[LinkedKeyName]; }
        }

        public RecastKeyButton(BaseGame baseGame, VerticalScrollSubscreen scrollScreen, string linkedKeyName,
                               int locationX, int locationY, int width, int height)
             : base(baseGame, scrollScreen, "", locationX, locationY, width, height)
        {
            LinkedKeyName = linkedKeyName;
        }

        public override void OnClick()
        {
            Screen NextScreen = new RecastKeyScreen(BaseGame, LinkedKeyName);

            BaseGame.ScreenManager.AddScreen(NextScreen);
        }
    }
}
