using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class DeleteScoreButton : VerticalScrollButton
    {
        ScoreDisplay ScoreDisplay;

        protected override string ButtonTextureName
        {
            get { return "DeleteButton"; }
        }

        protected override Color ButtonHoverColour
        {
            get { return Color.Red; }
        }

        private int SaveSlotIndex
        {
            get { return ScoreDisplay.SlotNumber - 1; }
        }

        public DeleteScoreButton(BaseGame baseGame, VerticalScrollSubscreen scrollScreen, ScoreDisplay scoreDisplay,
                                 int locationX, int locationY, int width, int height)
            : base(baseGame, scrollScreen, "", locationX, locationY, width, height)
        {
            ScoreDisplay = scoreDisplay;
        }

        public override void OnClick()
        {
            SaveManager.DeleteScoreData(SaveSlotIndex);
            BaseGame.ScreenManager.CloseScreen();
            BaseGame.ScreenManager.AddScreen(new ScoresScreen(BaseGame, ScrollSubscreen.ScrollLocationFloat));
        }
    }
}
