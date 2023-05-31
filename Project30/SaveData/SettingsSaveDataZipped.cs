using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    [Serializable]
    public class SettingsSaveDataZipped
    {
        public int ScreenWidth;
        public int ScreenHeight;

        public List<KeyCastingSaveData> KeysCastingSaveDatas;

        public bool IsEnableHardDrop;

        public SettingsSaveDataZipped()
        {

        }
    }
}
