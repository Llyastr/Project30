using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGameInfoDisplay
    {
        BaseGame BaseGame;

        TetrisGame TetrisGame;

        readonly string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        int Score
        {
            get { return TetrisGame.Score; }
        }
        int Lines
        {
            get { return TetrisGame.Lines; }
        }
        int HighScore
        {
            get { return Math.Max(TetrisGame.HighScore, TetrisGame.Score); }
        }
        int Level
        {
            get { return TetrisGame.Level; }
        }
        int TetrisRate
        {
            get { return TetrisGame.TetrisRate; }
        }

        int LocationX
        {
            get { return TetrisGame.LocationX; }
        }
        int LocationY
        {
            get { return TetrisGame.LocationY; }
        }
        int SquareSize
        {
            get { return TetrisGame.SquareSize; }
        }

        const float LocationUnitsX = 11.2f;
        const float LocationUnitsGapY = 1.5f;
        const float LocationInitialUnitsY = 7f;


        public int ScoreLocationX
        {
            get { return LocationX + (int)(LocationUnitsX * SquareSize); }
        }
        public int ScoreLocationY
        {
            get { return LocationY + (int)((LocationInitialUnitsY + LocationUnitsGapY * 0) * SquareSize ); }
        }
        Vector2 ScoreLocation
        {
            get { return new Vector2(ScoreLocationX, ScoreLocationY); }
        }

        public int LinesLocationX
        {
            get { return LocationX + (int)(LocationUnitsX * SquareSize); }
        }
        public int LinesLocationY
        {
            get { return LocationY + (int)((LocationInitialUnitsY + LocationUnitsGapY * 1) * SquareSize); }
        }
        Vector2 LinesLocation
        {
            get { return new Vector2(LinesLocationX, LinesLocationY); }
        }

        public int HighScoreLocationX
        {
            get { return LocationX + (int)(LocationUnitsX * SquareSize); }
        }
        public int HighScoreLocationY
        {
            get { return LocationY + (int)((LocationInitialUnitsY + LocationUnitsGapY * 2) * SquareSize); }
        }
        Vector2 HighScoreLocation
        {
            get { return new Vector2(HighScoreLocationX, HighScoreLocationY); }
        }

        public int LevelLocationX
        {
            get { return LocationX + (int)(LocationUnitsX * SquareSize); }
        }
        public int LevelLocationY
        {
            get { return LocationY + (int)((LocationInitialUnitsY + LocationUnitsGapY * 3) * SquareSize); }
        }
        Vector2 LevelLocation
        {
            get { return new Vector2(LevelLocationX, LevelLocationY); }
        }

        public int TetrisRateLocationX
        {
            get { return LocationX + (int)(LocationUnitsX * SquareSize); }
        }
        public int TetrisRateLocationY
        {
            get { return LocationY + (int)((LocationInitialUnitsY + LocationUnitsGapY * 4) * SquareSize); }
        }
        Vector2 TetrisRateLocation
        {
            get { return new Vector2(TetrisRateLocationX, TetrisRateLocationY); }
        }

        public TetrisGameInfoDisplay(BaseGame baseGame, TetrisGame tetrisGame)
        {
            BaseGame = baseGame;
            TetrisGame = tetrisGame;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Score: " + Score.ToString(), ScoreLocation, Color.White);
            spriteBatch.DrawString(Font, "Line: " + Lines.ToString(), LinesLocation, Color.White);
            spriteBatch.DrawString(Font, "HighScore: " + HighScore.ToString(), HighScoreLocation, Color.White);
            spriteBatch.DrawString(Font, "Level: " + Level.ToString(), LevelLocation, Color.White);
            spriteBatch.DrawString(Font, $"Tetris Rate: {TetrisRate}%", TetrisRateLocation, Color.White);
        }
    }
}
