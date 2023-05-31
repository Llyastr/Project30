using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TitleScreen : StaticScreen
    {
        readonly static string TitleBackgroundTextureName = "TitleBackground";

        readonly float ButtonsInitialLocationFloatX = 0.67f;
        readonly float ButtonsIniitalLocationFloatY = 0.301f;
        readonly float ButtonGapFloatX = 0.03f;
        readonly float ButtonGapFloatY = 0.11f;
        readonly float ButtonWidthFloat = 0.16f;
        readonly float ButtonHeightFloat = 0.07f;

        int ButtonsInitialLocationX
        {
            get { return (int)(ScreenWidth * ButtonsInitialLocationFloatX); }
        }
        int ButtonsInitialLocationY
        {
            get { return (int)(ScreenHeight * ButtonsIniitalLocationFloatY); }
        }
        int ButtonGapX
        {
            get { return (int)(ScreenWidth * ButtonGapFloatX); }
        }
        int ButtonGapY
        {
            get { return (int)(ScreenHeight * ButtonGapFloatY); }
        }
        int ButtonWidth
        {
            get { return (int)(ScreenWidth * ButtonWidthFloat); }
        }
        int ButtonHeight
        {
            get { return (int)(ScreenHeight * ButtonHeightFloat); }
        }

        public TitleScreen(BaseGame baseGame)
            : base(baseGame, TitleBackgroundTextureName)
        {
            List<Button> TempButtons = new List<Button>()
            {
                new StartButton(baseGame,
                                ButtonsInitialLocationX + ButtonGapX * 0,
                                ButtonsInitialLocationY + ButtonGapY * 0,
                                ButtonWidth,
                                ButtonHeight),
                new ScoresScreenButton(baseGame,
                                       ButtonsInitialLocationX + ButtonGapX * 1,
                                       ButtonsInitialLocationY + ButtonGapY * 1,
                                       ButtonWidth,
                                       ButtonHeight),
                new SettingsButton(baseGame,
                                   ButtonsInitialLocationX + ButtonGapX * 2,
                                   ButtonsInitialLocationY + ButtonGapY * 2,
                                   ButtonWidth,
                                   ButtonHeight),
                new ExitButton(baseGame,
                               ButtonsInitialLocationX + ButtonGapX * 3,
                               ButtonsInitialLocationY + ButtonGapY * 3,
                               ButtonWidth,
                               ButtonHeight),
            };

            Buttons = TempButtons.ToArray();
        }
    }
}
