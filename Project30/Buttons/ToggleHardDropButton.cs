using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ToggleHardDropButton : ToggleButton
    {
        public ToggleHardDropButton(BaseGame baseGame, VerticalScrollSubscreen scrollSubscreen,
                                    int locationX, int locationY, int width, int height)
            : base(baseGame, scrollSubscreen, locationX, locationY, width, height)
        {
            Bool = BaseGame.SettingsSaveData.IsEnableHardDrop;
        }

        public override void OnClick()
        {
            base.OnClick();

            BaseGame.SettingsSaveData.IsEnableHardDrop = Bool;

            BaseGame.SaveSettingsData();
        }
    }
}
