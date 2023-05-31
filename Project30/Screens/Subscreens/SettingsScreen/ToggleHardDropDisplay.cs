using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ToggleHardDropDisplay : ButtonCastDisplay
    {
        public ToggleHardDropDisplay(BaseGame baseGame, SettingsSubscreen settingsSubscreen,
                                     string text, float locationFloatX, float locationFloatY)
            : base(baseGame, settingsSubscreen, text, locationFloatX, locationFloatY)
        {
            RecastKeyButton = new ToggleHardDropButton(baseGame, settingsSubscreen,
                                                       ButtonLocationX, ButtonLocationY,
                                                       ButtonWidth, ButtonHeight);
        }
    }
}
