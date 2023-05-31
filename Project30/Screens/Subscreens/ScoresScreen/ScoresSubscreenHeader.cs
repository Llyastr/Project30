using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScoresSubscreenHeader
    {
        BaseGame BaseGame;

        ScoresSubscreen ScoresSubscreen;

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

        readonly string DateString = "Date";
        readonly string PlayerNameString = "Player Name";
        readonly string ScoreString = "Score";
        readonly string LinesClearedString = "Lines";

        public ScoresSubscreenHeader(BaseGame baseGame, ScoresSubscreen scoresScubscreen,
                                     float locationFloatX, float locationFloatY)
        {
            BaseGame = baseGame;

            ScoresSubscreen = scoresScubscreen;

            LocationFloatX = locationFloatX;
            LocationFloatY = locationFloatY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, DateString, DateLocation, TextColour);
            spriteBatch.DrawString(Font, PlayerNameString, PlayerNameLocation, TextColour);
            spriteBatch.DrawString(Font, ScoreString, ScoreLocation, TextColour);
            spriteBatch.DrawString(Font, LinesClearedString, LinesClearedLocation, TextColour);
        }
    }
}
