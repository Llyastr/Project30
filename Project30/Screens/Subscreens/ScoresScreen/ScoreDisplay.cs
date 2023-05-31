using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScoreDisplay
    {
        BaseGame BaseGame;

        ScoresSubscreen ScoresSubscreen;

        Button DeleteScoreButton;

        int ScreenWidth
        {
            get { return BaseGame.ScreenWidth; }
        }
        int ScreenHeight
        {
            get { return BaseGame.ScreenHeight; }
        }

        readonly string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        readonly static Color TextColour = Color.White;

        private int ScrollLocation
        {
            get { return ScoresSubscreen.ScrollLocation; }
        }

        private readonly float LocationFloatX;
        private readonly float LocationFloatY;

        private int LocationX
        {
            get { return (int)(ScreenWidth * LocationFloatX); }
        }
        private int LocationY
        {
            get { return (int)(ScreenHeight * LocationFloatY - ScrollLocation); }
        }

        private readonly float SlotNumberWidthFloat = 0.04f;
        private readonly float DateWidthFloat = 0.28f;
        private readonly float PlayerNameWidthFloat = 0.28f;
        private readonly float ScoreWidthFloat = 0.13f;
        private readonly float LinesClearedWidthFloat = 0.08f;

        private int SlotNumberWidth
        {
            get { return (int)(ScreenWidth * SlotNumberWidthFloat); }
        }
        private int DateWidth
        {
            get { return (int)(ScreenWidth * DateWidthFloat); }
        }
        private int PlayerNameWidth
        {
            get { return (int)(ScreenWidth * PlayerNameWidthFloat); }
        }
        private int ScoreWidth
        {
            get { return (int)(ScreenWidth * ScoreWidthFloat); }
        }
        private int LinesClearedWidth
        {
            get { return (int)(ScreenWidth * LinesClearedWidthFloat); }
        }

        private int SlotNumberLocationX
        {
            get { return LocationX; }
        }
        private int DateLocationX
        {
            get { return LocationX + SlotNumberWidth; }
        }
        private int PlayerNameLocationX
        {
            get { return DateLocationX + DateWidth; }
        }
        private int ScoreLocationX
        {
            get { return PlayerNameLocationX + PlayerNameWidth; }
        }
        private int LinesClearedLocationX
        {
            get { return ScoreLocationX + ScoreWidth; }
        }

        Vector2 SlotNumberLocation
        {
            get { return new Vector2(SlotNumberLocationX, LocationY); }
        }
        Vector2 DateLocation
        {
            get { return new Vector2(DateLocationX, LocationY); }
        }
        Vector2 PlayerNameLocation
        {
            get { return new Vector2(PlayerNameLocationX, LocationY); }
        }
        Vector2 ScoreLocation
        {
            get { return new Vector2(ScoreLocationX, LocationY); }
        }
        Vector2 LinesClearedLocation
        {
            get { return new Vector2(LinesClearedLocationX, LocationY); }
        }

        private int DeleteScoreButtonLocationX
        {
            get { return LinesClearedLocationX + LinesClearedWidth; }
        }
        private int DeleteScoreButtonLocationY
        {
            get { return ScrollLocation + LocationY; }
        }
        private int DeleteScoreButtonWidth
        {
            get { return 36; }
        }
        private int DeleteScoreButtonHeight
        {
            get { return 36; }
        }

        public readonly int SlotNumber;
        readonly string Date;
        readonly string PlayerName;
        readonly int Score;
        readonly int LinesCleared;

        public ScoreDisplay(BaseGame baseGame, ScoresSubscreen scoresScubscreen,
                            float locationFloatX, float locationFloatY,
                            int slotNumber, string date, string playerName, int score, int linesCleared)
        {
            BaseGame = baseGame;

            ScoresSubscreen = scoresScubscreen;

            LocationFloatX = locationFloatX;
            LocationFloatY = locationFloatY;

            SlotNumber = slotNumber;
            Date = date;
            PlayerName = playerName;
            Score = score;
            LinesCleared = linesCleared;

            DeleteScoreButton = new DeleteScoreButton(baseGame,
                                                      ScoresSubscreen,
                                                      this,
                                                      DeleteScoreButtonLocationX,
                                                      DeleteScoreButtonLocationY,
                                                      DeleteScoreButtonWidth,
                                                      DeleteScoreButtonHeight);
        }

        public void Update(GameTime gameTime,
                           KeyboardState keyboardState,
                           KeyboardState previousKeyboardState,
                           MouseState mouseState,
                           MouseState previousMouseState)
        {
            DeleteScoreButton.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, $"{SlotNumber}.", SlotNumberLocation, TextColour);
            spriteBatch.DrawString(Font, $"{Date}", DateLocation, TextColour);
            spriteBatch.DrawString(Font, $"{PlayerName}", PlayerNameLocation, TextColour);
            spriteBatch.DrawString(Font, $"{Score}", ScoreLocation, TextColour);
            spriteBatch.DrawString(Font, $"{LinesCleared}", LinesClearedLocation, TextColour);

            DeleteScoreButton.Draw(spriteBatch);
        }
    }
}
