using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class SettingsSaveData
    {
        public int ScreenWidth;
        public int ScreenHeight;

        public Dictionary<string, Keys> Keys;

        public bool IsEnableHardDrop;

        public SettingsSaveData()
        {

        }

        public SettingsSaveData(SettingsSaveDataZipped zippedSaveFile)
        {
            ScreenWidth = zippedSaveFile.ScreenWidth;
            ScreenHeight = zippedSaveFile.ScreenHeight;

            Keys = new Dictionary<string, Keys>();

            foreach (KeyCastingSaveData keyCastingSaveData in zippedSaveFile.KeysCastingSaveDatas)
            {
                Keys.Add(keyCastingSaveData.KeyName, keyCastingSaveData.Key);
            }

            IsEnableHardDrop = zippedSaveFile.IsEnableHardDrop;
        }
    }
}
