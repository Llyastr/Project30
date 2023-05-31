using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGameMobile : TetrisGame
    {
        public override int RowCount { get { return 22; } }
        public override int ColCount { get { return 10; } }
        public override bool DoARE { get { return true; } }
        public override int AREDelay { get { return 17; } }
        public override bool DoPieceLockAnimation { get { return true; } }
        public override int PieceLockAnimationDuration { get { return 17; } }
        protected override int LinesPerLevel { get { return 20; } }
        protected override float SoftDropSpeed { get { return 0.03f; } }
        protected override int DASLockLongDelay { get { return 7; } }
        protected override int DASLockShortDelay { get { return 1; } }
        protected override bool DoPieceDropLingering { get { return true; } }
        protected override float PieceLingeringRatio { get { return 0.64f; } }
        protected override bool EnableHardDrop { get { return true; } }
        protected override int HardDropDoubleTapWindow { get { return 9; } }
        protected override bool EnableDownIsDASKey { get { return true; } }

        readonly int MaxBagSize = 7;
        List<int> NextPieceBag;

        public TetrisGameMobile(BaseGame baseGame, TetrisGameScreen gameScreen, Input input, 
                                int startLevel, string playerName, int locationX, int locationY)
            : base(baseGame, gameScreen, input, startLevel, playerName, locationX, locationY)
        {
            NextPieceBag = new List<int>();
        }

        protected override void SetPiecesInitial(int pieceType, int nextPieceType)
        {
            GenerateNewBag();

            Piece = GetNextInBag();
            NextPiece = GetNextInBag();
        }

        protected override void DoGoNextPiece()
        {
            Piece = NextPiece;
            NextPiece = GetNextInBag();
        }

        private void GenerateNewBag()
        {
            NextPieceBag.Clear();

            Random Random = new Random();
            List<int> TempInts = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7
            };

            for (int i = MaxBagSize; i > 0; i--)
            {
                int NextIndex = Random.Next(0, i);
                int NextInt = TempInts[NextIndex];
                NextPieceBag.Add(NextInt);
                TempInts.Remove(NextInt);
            }
        }

        private Piece GetNextInBag()
        {
            if (NextPieceBag.Count == 0)
            {
                GenerateNewBag();
            }

            int NextInt = NextPieceBag[0];
            NextPieceBag.RemoveAt(0);

            return new Piece(BaseGame, this, NextInt);
        }
    }
}
