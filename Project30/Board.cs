using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class Board
    {
        BaseGame BaseGame;

        TetrisGame TetrisGame;

        private int RowCount
        {
            get { return TetrisGame.RowCount; }
        }
        private int ColCount
        {
            get { return TetrisGame.ColCount; }
        }
        private int StateSize
        {
            get { return RowCount * ColCount; }
        }

        private bool DoARE
        {
            get { return TetrisGame.DoARE; }
        }
        private int AREDelay
        {
            get { return TetrisGame.AREDelay; }
        }

        readonly string WhiteSquareTextureName = "WhiteSquareGrayOutline";
        Texture2D WhiteSquareTexture
        {
            get { return BaseGame.GetTexture(WhiteSquareTextureName); }
        }
        readonly string WhiteSquareWithTriangleTextureName = "WhiteSquareWithTriangle";
        Texture2D WhiteSquareWithTriangleTexture
        {
            get { return BaseGame.GetTexture(WhiteSquareWithTriangleTextureName); }
        }
        readonly string PauseTextureName = "PauseTexture";
        Texture2D PauseTexture
        {
            get { return BaseGame.GetTexture(PauseTextureName); }
        }
        readonly string WhiteBackgroundName = "EmptyBackground";
        Texture2D WhiteBackgroundTexture
        {
            get { return BaseGame.GetTexture(WhiteBackgroundName); }
        }

        readonly string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        readonly Color TextColour = Color.Black;
        readonly float PauseTransparency = 0.571235f;

        private readonly string PausedText = "Paused"; 

        readonly int LocationX;
        readonly int LocationY;

        readonly int SquareSize;

        public int[] State
        {
            get;
            private set;
        }

        const int BorderGap = 4;

        readonly int MaxLineClearFrames = 17;
        private int LineClearFrames;

        public bool IsLineClearingAnimation
        {
            get { return LineClearFrames > 0; }
        }

        private float LineClearTransparency
        {
            get { return (float)LineClearFrames / MaxLineClearFrames; }
        }

        private bool IsDoPieceLockAnimation
        {
            get { return TetrisGame.DoPieceLockAnimation; }
        }
        private int PieceLockAnimationDurationMax
        {
            get { return TetrisGame.PieceLockAnimationDuration; }
        }

        private int PieceLockAnimationDuration
        {
            get { return Math.Max(0, LineClearFrames - (MaxLineClearFrames - PieceLockAnimationDurationMax)); }
        }

        private float PieceLockAnimationTransparency
        {
            get { return (float)PieceLockAnimationDuration / PieceLockAnimationDurationMax; }
        }

        public Board(BaseGame baseGame, TetrisGame tetrisGame, int locationX, int locationY, int squareSize)
        {
            BaseGame = baseGame;

            TetrisGame = tetrisGame;

            LocationX = locationX;
            LocationY = locationY;

            SquareSize = squareSize;

            State = new int[StateSize];
        }

        public void UpdateLineClearAnimation()
        {
            LineClearFrames = Math.Max(LineClearFrames - 1, 0);

            if (IsDoPieceLockAnimation && PieceLockAnimationDuration <= 0)
            {
                for (int i = 0; i < StateSize; i++)
                {
                    if (State[i] >= 9 && State[i] <= 15)
                    {
                        State[i] -= 8;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            void DrawSquare(int locationX, int locationY, int colour)
            {
                int ScaledColour = colour;
                if (colour >= 9 && colour <= 15)
                {
                    ScaledColour -= 8;
                }

                Color Colour = IntToColour(ScaledColour);
                Rectangle Rectangle = new Rectangle(locationX, locationY, SquareSize, SquareSize);

                if (colour == 0)
                {
                    spriteBatch.Draw(WhiteSquareTexture, Rectangle, Colour);
                }
                
                if (colour >= 1 && colour <= 7)
                {
                    spriteBatch.Draw(WhiteSquareWithTriangleTexture, Rectangle, Colour);
                }

                if (colour == 8)
                {
                    spriteBatch.Draw(WhiteSquareTexture, Rectangle, IntToColour(0));
                    spriteBatch.Draw(WhiteSquareWithTriangleTexture, Rectangle, Colour * LineClearTransparency);
                }

                if (colour >= 9 && colour <= 15)
                {
                    spriteBatch.Draw(WhiteSquareWithTriangleTexture, Rectangle, Colour);
                    spriteBatch.Draw(WhiteSquareWithTriangleTexture, Rectangle, 
                                     Color.Silver * PieceLockAnimationTransparency);
                }
            }

            int BackdropLocationX = LocationX - BorderGap;
            int BackdropLocationY = LocationY - BorderGap;
            int BackdropWidth = ColCount * SquareSize + BorderGap * 2;
            int BackdropHeight = RowCount * SquareSize + BorderGap * 2;

            Rectangle Rectangle = new Rectangle(BackdropLocationX, BackdropLocationY, BackdropWidth, BackdropHeight);
            spriteBatch.Draw(WhiteBackgroundTexture, Rectangle, Color.Black);

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    int DrawLocationX = LocationX + col * SquareSize;
                    int DrawLocationY = LocationY + row * SquareSize;

                    int Colour = GetState(row, col);

                    DrawSquare(DrawLocationX, DrawLocationY, Colour);
                }
            }

            int TopBarWidth = SquareSize * ColCount;
            int TopBarHeight = SquareSize * 2;
            Rectangle TopBarRectangle = new Rectangle(LocationX, LocationY, TopBarWidth, TopBarHeight);

            spriteBatch.Draw(WhiteSquareTexture, TopBarRectangle, IntToColour(0));
        }

        public void DrawPaused(SpriteBatch spriteBatch)
        {
            Vector2 TextMeasure = Font.MeasureString(PausedText);

            int Width = ColCount * SquareSize;
            int Height = RowCount * SquareSize;

            Rectangle Rectangle = new Rectangle(LocationX, LocationY, Width, Height);

            int TextLocationX = LocationX + (int)(Width - TextMeasure.X) / 2;
            int TextLocationY = LocationY + (int)(Height - TextMeasure.Y) / 2;

            Vector2 TextLocation = new Vector2(TextLocationX, TextLocationY);

            spriteBatch.Draw(PauseTexture, Rectangle, Color.White * PauseTransparency);
            spriteBatch.DrawString(Font, PausedText, TextLocation, TextColour);
        }

        public void DrawWithNumber(SpriteBatch spriteBatch, int n)
        {
            Vector2 TextMeasure = Font.MeasureString(n.ToString());

            int Width = ColCount * SquareSize;
            int Height = RowCount * SquareSize;

            Rectangle Rectangle = new Rectangle(LocationX, LocationY, Width, Height);

            int TextLocationX = LocationX + (int)(Width - TextMeasure.X) / 2;
            int TextLocationY = LocationY + (int)(Height - TextMeasure.Y) / 2;

            Vector2 TextLocation = new Vector2(TextLocationX, TextLocationY);

            spriteBatch.Draw(PauseTexture, Rectangle, Color.White * PauseTransparency);
            spriteBatch.DrawString(Font, n.ToString(), TextLocation, TextColour);
        }

        public bool[] GetBoolState()
        {
            bool[] TempState = new bool[StateSize];
            for (int i = 0; i < StateSize; i++)
            {
                if (State[i] != 0)
                {
                    TempState[i] = true;
                }
            }
            return TempState;
        }

        private int GetState(int row, int col)
        {
            return State[row * ColCount + col];
        }

        public void Reset()
        {
            State = new int[StateSize];
        }

        public void AddToState(int[] squares, int colour)
        {
            foreach (int i in squares)
            {
                State[i] = colour;
            }
        }

        public void SetState(int[] nextState)
        {
            if (nextState == null)
            {
                Reset();
                return;
            }
            State = nextState;
        }

        public int DoClearRows()
        {
            int RowToBeUpdated = FindFullRow();
            int LinesScored = 0;
            while (RowToBeUpdated != -1)
            {
                RemoveRow(RowToBeUpdated);
                DropFromRow(RowToBeUpdated);
                RowToBeUpdated = FindFullRow();
                LinesScored++;
            }
            return LinesScored;
        }

        public bool Dieded()
        {
            for (int i = 0; i < ColCount * 2; i++)
            {
                if (State[i] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            State = new int[StateSize];
        }

        private bool[] GetRow(int rowNumber)
        {
            bool[] TempBool = new bool[ColCount];

            int CurrentLoopIndex = rowNumber * ColCount;

            for (int i = 0; i < ColCount; i++)
            {
                if (State[CurrentLoopIndex] != 0)
                {
                    TempBool[i] = true;
                }

                CurrentLoopIndex++;
            }

            return TempBool;
        }

        private void RemoveRow(int rowNumber)
        {
            int CurrentLoopIndex = rowNumber * ColCount;

            for (int i = 0; i < ColCount; i++)
            {
                State[CurrentLoopIndex] = 0;

                CurrentLoopIndex++;
            }
        }

        private void DropFromRow(int rowNumber)
        {
            int MaxIndex = (rowNumber * ColCount) - 1;
            for (int i = MaxIndex; i >= 0; i--)
            {
                State[i + ColCount] = State[i];
                State[i] = 0;
            }
        }

        private bool CheckFullRow(int rowNumber)
        {
            bool[] Row = GetRow(rowNumber);
            foreach (bool square in Row)
            {
                if (!square)
                {
                    return false;
                }
            }
            return true;
        }

        private int FindFullRow()
        {
            for (int i = RowCount - 1; i > 0; i--)
            {
                if (CheckFullRow(i))
                {
                    return i;
                }
            }
            return -1;
        }

        private int[] FindAllFullRows()
        {
            List<int> TempInt = new List<int>();

            for (int i = RowCount - 1; i > 0; i--)
            {
                if (CheckFullRow(i))
                {
                    TempInt.Add(i);
                }
            }

            return TempInt.ToArray();
        }

        private void SendRowToClearAnimation(int rowNumber)
        {
            int CurrentLoopIndex = rowNumber * ColCount;

            for (int i = CurrentLoopIndex; i < CurrentLoopIndex + ColCount; i++)
            {
                State[i] = 8;
            }
        }

        public void FindAndSendRowsToClearAnimation()
        {
            int[] RowsToClear = FindAllFullRows();

            if (RowsToClear.Length <= 0)
            {
                if (DoARE)
                {
                    LineClearFrames = AREDelay;
                }
                return;
            }

            foreach (int row in RowsToClear)
            {
                SendRowToClearAnimation(row);
            }

            LineClearFrames = MaxLineClearFrames;
        }

        public static Color IntToColour(int i)
        {
            //I, S, Z, L, F, O, T
            switch (i)
            {
                case 0: return new Color(40, 40, 40);
                case 1: return Color.PowderBlue;
                case 2: return Color.MediumSeaGreen;
                case 3: return Color.Red;
                case 4: return Color.RoyalBlue;
                case 5: return Color.Orange;
                case 6: return new Color(230, 220, 20, 255);
                case 7: return Color.MediumPurple;
                case 8: return Color.SlateGray;
            }
            throw new Exception("int not in range for IntToColour");
        }
    }
}
