using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class Piece
    {
        BaseGame BaseGame;

        TetrisGame TetrisGame;

        int RowCount
        {
            get { return TetrisGame.RowCount; }
        }
        int ColCount
        {
            get { return TetrisGame.ColCount; }
        }
        int StateSize
        {
            get { return RowCount * ColCount; }
        }
        int SquareSize
        {
            get { return TetrisGame.SquareSize; }
        }
        int BoardLocationX
        {
            get { return TetrisGame.BoardLocationX; }
        }
        int BoardLocationY
        {
            get { return TetrisGame.BoardLocationY; }
        }

        bool[] State;
        public int ScaleLocation
        {
            get;
            private set;
        }
        int ScaleLocationColCount
        {
            get { return ColCount + 1; }
        }
        int ScaleLocationCol
        {
            get { return ScaleLocation % ScaleLocationColCount; }
        }
        int ScaleLocationRow
        {
            get { return (ScaleLocation - ScaleLocationCol) / ScaleLocationColCount; }
        }

        int ScaleColCount
        {
            get { return ColCount + 4; }
        }

        int[] MoveSeekOffsets = { -1, 1, 12, -2, 2, -12 };

        public readonly int PieceType;
        public int PieceTypeForLockAnimation
        {
            get { return PieceType + 8; }
        }

        readonly string TextureName = "WhiteSquareWithTriangle";
        Texture2D WhiteSquareTexture
        {
            get { return BaseGame.GetTexture(TextureName); }
        }

        Color Colour
        {
            get { return Board.IntToColour(PieceType); }
        }

        public Piece(BaseGame baseGame, TetrisGame tetrisGame, int pieceType)
        {
            BaseGame = baseGame;

            TetrisGame = tetrisGame;

            PieceType = pieceType;

            SetFromTypeNumber(PieceType);

            ScaleLocation = 5;
        }

        public void DrawOnBoard(SpriteBatch spriteBatch)
        {
            int[] ConvertedIndexes = GetConvertedIndexes();

            foreach (int i in ConvertedIndexes)
            {
                int Col = i % ColCount;
                int Row = (i - Col) / ColCount;

                int DrawLocationX = BoardLocationX + SquareSize * Col;
                int DrawLocationY = BoardLocationY + SquareSize * Row;

                Rectangle DrawRectangle = new Rectangle(DrawLocationX, DrawLocationY, SquareSize, SquareSize);

                spriteBatch.Draw(WhiteSquareTexture, DrawRectangle, Colour);
            }
        }

        public void DrawAtLocation(SpriteBatch spriteBatch, int locationX, int locationY)
        {
            void DrawSquare(int drawLocationX, int drawLocationY)
            {
                Rectangle Rectangle = new Rectangle(drawLocationX, drawLocationY, SquareSize, SquareSize);
                spriteBatch.Draw(WhiteSquareTexture, Rectangle, Colour);
            }

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int DrawLocationX = locationX + col * SquareSize;
                    int DrawLocationY = locationY + row * SquareSize;
                    int StateIndex = row * 4 + col;
                    bool IsFilled = State[StateIndex];

                    if (IsFilled)
                    {
                        DrawSquare(DrawLocationX, DrawLocationY);
                    }
                }
            }
        }

        public void DrawGhostPiece(SpriteBatch spriteBatch)
        {
            int StoredLocation = ScaleLocation;

            DoHardDrop();

            int[] ConvertedIndexes = GetConvertedIndexes();

            foreach (int i in ConvertedIndexes)
            {
                int Col = i % ColCount;
                int Row = (i - Col) / ColCount;

                int DrawLocationX = BoardLocationX + SquareSize * Col;
                int DrawLocationY = BoardLocationY + SquareSize * Row;

                Rectangle DrawRectangle = new Rectangle(DrawLocationX, DrawLocationY, SquareSize, SquareSize);

                spriteBatch.Draw(WhiteSquareTexture, DrawRectangle, Color.Gray * 0.325f);
            }

            ScaleLocation = StoredLocation;
        }

        private void SetFromTypeNumber(int PieceType)
        {
            //I, S, Z, L, F, O, T
            // 0  1  2  3
            // 4  5  6  7
            // 8  9  10 11
            // 12 13 14 15
            void SetFromArray(int[] array)
            {
                foreach (int i in array)
                {
                    State[i] = true;
                }
            }

            int[] GetFilledArray()
            {
                switch (PieceType)
                {
                    case 1: return new int[] { 4, 5, 6, 7 };
                    case 2: return new int[] { 1, 2, 4, 5 };
                    case 3: return new int[] { 0, 1, 5, 6 };
                    case 4: return new int[] { 0, 4, 5, 6 };
                    case 5: return new int[] { 2, 4, 5, 6 };
                    case 6: return new int[] { 1, 2, 5, 6 };
                    case 7: return new int[] { 1, 4, 5, 6 };
                }
                throw new Exception("index out of bounds");
            }

            State = new bool[16];

            SetFromArray(GetFilledArray());
        }

        private bool IsValidLocation()
        {
            if (!IsPieceOnBoard())
            {
                return false;
            }

            return !IsPieceOnOccupied();
        }

        private bool IsPieceOnBoard()
        {
            int[] ScaleIndexes = GetScaleSizeIndexes();

            foreach (int i in ScaleIndexes)
            {
                if (!IsScaleIndexOnBoard(i))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPieceOnOccupied()
        {
            bool[] OccupiedSquares = TetrisGame.Board.GetBoolState();

            int[] ConvertedIndexes = GetConvertedIndexes();

            foreach (int i in ConvertedIndexes)
            {
                if (OccupiedSquares[i])
                {
                    return true;
                }
            }

            return false;
        }

        private int[] GetScaleSizeIndexes()
        {
            int GetScaleIndex(int row, int col)
            {
                int ScaleRow = ScaleLocationRow + row;
                int ScaleCol = ScaleLocationCol + col;

                return ScaleRow * ScaleColCount + ScaleCol;
            }

            List<int> TempInt = new List<int>();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int StateIndex = row * 4 + col;
                    bool IsFilled = State[StateIndex];

                    if (IsFilled)
                    {
                        TempInt.Add(GetScaleIndex(row, col));
                    }
                }
            }

            return TempInt.ToArray();
        }

        private bool IsScaleIndexOnBoard(int square)
        {
            int ScaleCol = square % ScaleColCount;

            bool IsValidColumn = !(ScaleCol == 0 || ScaleCol == 1 || 
                                   ScaleCol == ScaleColCount - 1 || ScaleCol == ScaleColCount - 2);

            int ScaleSize = ScaleColCount * RowCount;
            bool IsWithinBounds = (square >= 0 && square < ScaleSize);

            return IsValidColumn && IsWithinBounds;
        }

        private int ConvertScaleSize(int square)
        {
            int ScaleCol = square % ScaleColCount;
            int ScaleRow = (square - ScaleCol) / ScaleColCount;

            return square - 2 - (ScaleRow * 4);
        }

        public int[] GetConvertedIndexes()
        {
            int[] ScaleIndexes = GetScaleSizeIndexes();
            List<int> TempInt = new List<int>();

            foreach (int i in ScaleIndexes)
            {
                TempInt.Add(ConvertScaleSize(i));
            }

            return TempInt.ToArray();
        }

        public bool DoDrop()
        {
            ScaleLocation += ScaleLocationColCount;
            if (!IsValidLocation())
            {
                ScaleLocation -= ScaleLocationColCount;
                return true;
            }
            return false;
        }

        public bool TestDrop()
        {
            ScaleLocation += ScaleLocationColCount;
            bool IsDropValid = IsValidLocation();
            ScaleLocation -= ScaleLocationColCount;

            return IsDropValid;
        }

        public int DoHardDrop()
        {
            int Safety = 0;
            int LinesFallen = 0;
            while (!DoDrop())
            {
                Safety++;
                if (Safety > 24)
                {
                    break;
                }
                LinesFallen++;
            }
            return LinesFallen;
        }

        public void DoMoveRight()
        {
            ScaleLocation += 1;
            if (!IsValidLocation())
            {
                ScaleLocation -= 1;
            }

            /* // FOR TESTING
            int Offset = 12;
            ScaleLocation += Offset;
            if (!IsValidLocation())
            {
                ScaleLocation -= Offset;
            }
            */
        }

        public void DoMoveLeft()
        {
            ScaleLocation -= 1;
            if (!IsValidLocation())
            {
                ScaleLocation += 1;
            }
        }

        public void DoRotateClockwise()
        {
            RotateClockwise();
            if (IsValidLocation())
            {
                return;
            }

            foreach (int offset in MoveSeekOffsets)
            {
                ScaleLocation += offset;
                if (IsValidLocation())
                {
                    return;
                }
                ScaleLocation -= offset;
            }

            RotateCounterClockwise();
        }

        public void DoRotateCounterClockwise()
        {
            RotateCounterClockwise();
            if (IsValidLocation())
            {
                return;
            }

            foreach (int offset in MoveSeekOffsets)
            {
                ScaleLocation += offset;
                if (IsValidLocation())
                {
                    return;
                }
                ScaleLocation -= offset;
            }

            RotateClockwise();
        }

        private void RotateClockwise()
        {
            if (PieceType == 6)
            {
                return;
            }

            if (PieceType == 1)
            {
                RotateClockwise4by4();
            }
            else
            {
                RotateClockwise3by3();
            }
        }

        private void RotateCounterClockwise()
        {
            if (PieceType == 6)
            {
                return;
            }

            if (PieceType == 1)
            {
                RotateCounterClockwise4by4();
            }
            else
            {
                RotateCounterClockwise3by3();
            }
        }

        private void RotateClockwise4by4()
        {
            int[] NextIndexOrder = { 12, 8, 4, 0, 13, 9, 5, 1, 14, 10, 6, 2, 15, 11, 7, 3 };
            RotateByOrder(NextIndexOrder);
        }

        private void RotateCounterClockwise4by4()
        {
            int[] NextIndexOrder = { 3, 7, 11, 15, 2, 6, 10, 14, 1, 5, 9, 13, 0, 4, 8, 12 };
            RotateByOrder(NextIndexOrder);
        }

        private void RotateClockwise3by3()
        {
            int[] NextIndexOrder = { 8, 4, 0, 3, 9, 5, 1, 7, 10, 6, 2, 11, 12, 13, 14, 15 };
            RotateByOrder(NextIndexOrder);
        }

        private void RotateCounterClockwise3by3()
        {
            int[] NextIndexOrder = { 2, 6, 10, 3, 1, 5, 9, 7, 0, 4, 8, 11, 12, 13, 14, 15 };
            RotateByOrder(NextIndexOrder);
        }

        private void RotateByOrder(int[] nextIndexOrder)
        {
            List<bool> TempState = new List<bool>();
            foreach (int i in nextIndexOrder)
            {
                TempState.Add(State[i]);
            }
            State = TempState.ToArray();
        }

        public void DoAddToBoard()
        {
            int[] ConvertedIndexes = GetConvertedIndexes();

            TetrisGame.Board.AddToState(ConvertedIndexes, PieceType);
        }

        public static Piece MakeRandomPiece(BaseGame baseGame, TetrisGame tetrisGame)
        {
            Random Random = new Random();
            int NextPieceType = Random.Next(1, 8);

            return new Piece(baseGame, tetrisGame, NextPieceType);
        }
    }
}
