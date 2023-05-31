using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class GameTypeSelectionScreen : StaticScreen
    {
        readonly static string TextureName = "EmptyBackground";
        readonly Color BackgroundColour = Color.DarkBlue;
        readonly Color TextColour = Color.White;

        readonly string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        readonly string PromptText = "Select game type:";
        Vector2 PromptTextMeasure
        {
            get { return Font.MeasureString(PromptText); }
        }

        private int PromptGap
        {
            get { return (int)(ScreenWidth / 22.22222222f); }
        }

        int PromptTextLocationX
        {
            get { return (int)(ScreenWidth - PromptTextMeasure.X) / 2; }
        }
        int PromptTextLocationY
        {
            get { return ScreenHeight / 2 - PromptGap; }
        }
        Vector2 PromptTextLocation
        {
            get { return new Vector2(PromptTextLocationX, PromptTextLocationY); }
        }

        private int ButtonWidth
        {
            get { return (int)(ScreenWidth / 5.333333f); }
        }
        private int ButtonHeight
        {
            get { return (int)(ScreenWidth / 16.6666666f); }
        }
        private int ButtonGapX
        {
            get { return (int)(ScreenWidth / 26.66666666f); }
        }
        private int ButtonGapY
        {
            get { return (int)(ScreenWidth / 36.3636363636f); }
        }

        int LevelSelectButtonLocationY
        {
            get { return (ScreenHeight - ButtonHeight) / 2 + ButtonGapY; }
        }

        public GameTypeSelectionScreen(BaseGame baseGame)
            : base(baseGame, TextureName)
        {
            int TotalButtonsCount = 2;

            int TotalButtonLength = TotalButtonsCount * ButtonWidth + 
                                     (TotalButtonsCount - 1) * ButtonGapX;
            int InitialButtonLocationX = (ScreenWidth - TotalButtonLength) / 2;

            int GetButtonLocationX(int i)
            {
                return InitialButtonLocationX + (ButtonWidth + ButtonGapX) * i;
            }

            List<Button> TempButtons = new List<Button>()
            {
                new StartSinglePlayerTetrisNESButton(baseGame,
                                                     GetButtonLocationX(0),
                                                     LevelSelectButtonLocationY,
                                                     ButtonWidth,
                                                     ButtonHeight),
                new StartTwoPlayerTetrisNESButton(baseGame,
                                                  GetButtonLocationX(1),
                                                  LevelSelectButtonLocationY,
                                                  ButtonWidth,
                                                  ButtonHeight)
            };
            Buttons = TempButtons.ToArray();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, BackgroundColour);
            DrawButtons(spriteBatch);

            spriteBatch.DrawString(Font, PromptText, PromptTextLocation, TextColour);
        }
    }
}
