using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class LevelSelectScreen : StaticScreen
    {
        private readonly static string TextureName = "EmptyBackground";
        readonly Color BackgroundColour = Color.DarkBlue;
        readonly Color TextColour = Color.White;

        private readonly string FontName = "Font24";
        private SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        private readonly string PromptText = "Select starting level:";
        private Vector2 PromptTextMeasure
        {
            get { return Font.MeasureString(PromptText); }
        }

        private readonly int[] StartLevels = new int[] { 0, 5, 9, 19, 30 };

        private int PromptGap
        {
            get { return (int)(ScreenWidth / 22.22222222f); }
        }

        private int PromptTextLocationX
        {
            get { return (int)(ScreenWidth - PromptTextMeasure.X) / 2; }
        }
        private int PromptTextLocationY
        {
            get { return ScreenHeight / 2 - PromptGap; }
        }
        private Vector2 PromptTextLocation
        {
            get { return new Vector2(PromptTextLocationX, PromptTextLocationY); }
        }

        private int LevelSelectButtonWidth
        {
            get { return (int)(ScreenWidth / 20f); }
        }
        private int LevelSelectButtonHeight
        {
            get { return (int)(ScreenWidth / 20f); }
        }
        private int LevelSelectButtonGapX
        {
            get { return (int)(ScreenWidth / 53.333333f); }
        }
        private int LevelSelectButtonGapY
        {
            get { return (int)(ScreenWidth / 66.66666666f); }
        }

        private int LevelSelectButtonLocationY
        {
            get { return (ScreenHeight - LevelSelectButtonHeight) / 2 + LevelSelectButtonGapY; }
        }

        protected string PlayerName;

        public LevelSelectScreen(BaseGame baseGame, string playerName)
            : base(baseGame, TextureName)
        {
            PlayerName = playerName;

            int MaxLevel = StartLevels.Length;

            int TotalButtonLength = MaxLevel * LevelSelectButtonWidth + (MaxLevel - 1) * LevelSelectButtonGapX;
            int InitialButtonLocationX = (ScreenWidth - TotalButtonLength) / 2;
            List<Button> TempButtons = new List<Button>();

            for (int i = 0; i < MaxLevel; i++)
            {
                int ButtonLocationX = InitialButtonLocationX + (LevelSelectButtonWidth + LevelSelectButtonGapX) * i;

                LevelSelectButton NextLevelSelectButton = new LevelSelectButton(baseGame,
                                                                                this,
                                                                                StartLevels[i],
                                                                                ButtonLocationX,
                                                                                LevelSelectButtonLocationY,
                                                                                LevelSelectButtonWidth,
                                                                                LevelSelectButtonHeight);
                TempButtons.Add(NextLevelSelectButton);
            }

            Buttons = TempButtons.ToArray();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, BackgroundColour);
            DrawButtons(spriteBatch);

            spriteBatch.DrawString(Font, PromptText, PromptTextLocation, TextColour);
        }

        public virtual void OnButtonClick(int level)
        {
            throw new Exception("OnButtonClick not yet Implemented");
        }
    }
}
