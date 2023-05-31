using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class LevelSelectButton : Button
    {
        LevelSelectScreen LevelSelectScreen;

        private readonly int Level;

        public LevelSelectButton(BaseGame baseGame, LevelSelectScreen levelSelectScreen, int level, 
                                 int locationX, int locationY, int width, int height)
            : base(baseGame, level.ToString(), locationX, locationY, width, height)
        {
            LevelSelectScreen = levelSelectScreen;

            Level = level;
        }

        public override void OnClick()
        {
            LevelSelectScreen.OnButtonClick(Level);
        }
    }
}
