using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class Input
    {
        public Keys Up;
        public Keys Down;
        public Keys Left;
        public Keys Right;
        public Keys Button1;
        public Keys Button2;

        public Input(Keys up, Keys down, Keys left, Keys right, Keys button1, Keys button2)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            Button1 = button1;
            Button2 = button2;
        }

        public static Input GetPlayer1Input(SettingsSaveData settingsSaveData)
        {
            Dictionary<string, Keys> Keys = settingsSaveData.Keys;

            return new Input(Keys["UpPlayer1"], Keys["DownPlayer1"],
                             Keys["LeftPlayer1"], Keys["RightPlayer1"],
                             Keys["Button1Player1"], Keys["Button2Player1"]);
        }

        public static Input GetPlayer2Input(SettingsSaveData settingsSaveData)
        {
            Dictionary<string, Keys> Keys = settingsSaveData.Keys;

            return new Input(Keys["UpPlayer2"], Keys["DownPlayer2"],
                             Keys["LeftPlayer2"], Keys["RightPlayer2"],
                             Keys["Button1Player2"], Keys["Button2Player2"]);
        }
    }
}
