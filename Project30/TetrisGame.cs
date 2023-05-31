using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGame
    {
        protected BaseGame BaseGame;

        private int ScreenHeight
        {
            get { return BaseGame.ScreenHeight; }
        }

        protected TetrisGameScreen GameScreen;

        public Board Board;
        private TetrisGameInfoDisplay GameInfo;

        private Button[] Buttons;

        protected Piece Piece;
        protected Piece NextPiece;

        protected Input Input;

        readonly string LockSoundName = "PieceLockSound";
        SoundEffect LockSound
        {
            get { return BaseGame.GetSoundEffect(LockSoundName); }
        }

        public int SquareSize 
        { 
            get { return (int)(ScreenHeight / 25.0); } 
        }

        public virtual int RowCount { get { return 22; } }
        public virtual int ColCount { get { return 10; } }
        public virtual bool DoARE { get; }
        public virtual int AREDelay { get; }
        public virtual bool DoPieceLockAnimation { get; }
        public virtual int PieceLockAnimationDuration { get; }
        protected virtual int LinesPerLevel { get; }
        protected virtual float SoftDropSpeed { get; }
        protected virtual int DASLockLongDelay { get; }
        protected virtual int DASLockShortDelay { get; }
        protected virtual bool DoPieceDropLingering { get; }
        protected virtual float PieceLingeringRatio { get; }
        protected virtual bool EnableHardDrop { get; }
        protected virtual int HardDropDoubleTapWindow { get; }
        protected virtual bool EnableDownIsDASKey { get; }

        public readonly int LocationX;
        public readonly int LocationY;

        public int BoardLocationX
        {
            get { return LocationX; }
        }
        public int BoardLocationY
        {
            get { return LocationY; }
        }

        private int NextPieceLocationX
        {
            get { return LocationX + (int)(12.83333f * SquareSize); }
        }
        private int NextPieceLocationY
        {
            get { return LocationY + (int)(4.2222f * SquareSize); }
        }

        private int ButtonLocationX
        {
            get { return LocationX + (int)(11.2f * SquareSize); }
        }
        private int SaveButtonLocationY
        {
            get { return LocationY + (int)(15.83343f * SquareSize); }
        }
        private int PauseButtonLocationY
        {
            get { return LocationY + (int)(17.2843f * SquareSize); }
        }
        private int ButtonWidth
        {
            get { return (int)(6.5f * SquareSize); }
        }
        private int ButtonHeight
        {
            get { return (int)(1.3f * SquareSize); }
        }

        private const int PauseCountMax = 3;
        private const int PauseFramesPerCountdown = 40;
        private int PauseFrames;
        private int PauseCount;
        public bool IsPausedInternal
        {
            get { return PauseCount != 0; }
        }
        public bool IsPausedInternalIndefinate
        {
            get { return PauseCount == -1 || PauseCount == PauseCountMax + 1; }
        }
        public bool IsPausedInternalCounting
        {
            get { return PauseCount > 0; }
        }
        private bool IsGameScreenActive
        {
            get { return GameScreen.IsActive; }
        }
        private bool IsPaused
        {
            get { return IsPausedInternal || !IsGameScreenActive; }
        }

        protected readonly string PlayerName;

        public int Score;
        public int Lines;
        public int HighScore;
        public int TetrisLines;
        public int TetrisRate
        {
            get
            {
                if (Lines == 0)
                {
                    return 0;
                }
                else
                {
                    return TetrisLines * 100 / Lines;
                }
            }
        }
        protected readonly int StartLevel;
        public int Level
        {
            get
            {
                if (Lines / LinesPerLevel < StartLevel)
                {
                    return StartLevel;
                }
                else
                {
                    return Lines / LinesPerLevel;
                }
            }
        }

        private int FramesUntilNextFall;

        private int DASLockTimer;

        private int HardDropTimer;

        public TetrisGame(BaseGame baseGame, TetrisGameScreen gameScreen, Input input, 
                          int startLevel, string playerName, int locationX, int locationY)
        {
            BaseGame = baseGame;

            GameScreen = gameScreen;

            LocationX = locationX;
            LocationY = locationY;

            Board = new Board(baseGame, this, BoardLocationX, BoardLocationY, SquareSize);
            GameInfo = new TetrisGameInfoDisplay(baseGame, this);
            Input = input;

            StartLevel = startLevel;

            PlayerName = playerName;

            PauseCount = PauseCountMax;

            List<Button> TempButtons = new List<Button>
            {
                new SaveButton(baseGame, this, ButtonLocationX, SaveButtonLocationY,
                               ButtonWidth, ButtonHeight),
                new PauseGameButton(baseGame, this, ButtonLocationX, PauseButtonLocationY,
                                ButtonWidth, ButtonHeight),
            };

            Buttons = TempButtons.ToArray();

            LoadSaveData();
            ResetSaveData();

            FramesUntilNextFall = GetFramesPerGridcell();
        }

        public void Update(GameTime gameTime,
                           KeyboardState keyboardState,
                           KeyboardState previousKeyboardState,
                           MouseState mouseState,
                           MouseState previousMouseState)
        {
            if (Board.Dieded())
            {
                SaveScoreData();
                Reset();
            }

            if (keyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
            {
                TogglePause();
            }

            if (!IsPaused)
            {
                if (Board.IsLineClearingAnimation)
                {
                    Board.UpdateLineClearAnimation();
                    if (!Board.IsLineClearingAnimation)
                    {
                        DoGoNextAndClear();
                    }
                }
                else
                {
                    DoUpdateDrop(keyboardState, previousKeyboardState);
                    DoUpdateMoveLeftRight(keyboardState, previousKeyboardState);
                    DoUpdateRotations(keyboardState, previousKeyboardState);
                    DoUpdateHardDrop(keyboardState, previousKeyboardState);
                }
            }
            else if (IsPausedInternalCounting)
            {
                IncrementPauseCount();
            }

            foreach (Button button in Buttons)
            {
                button.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
            }
        }

        private void DoUpdateDrop(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            void ResetFallTime()
            {
                if (DoPieceDropLingering && !Piece.TestDrop() && keyboardState.IsKeyDown(Input.Down))
                {
                    FramesUntilNextFall = (int)(GetFramesPerGridcell() * PieceLingeringRatio);
                    return;
                }

                if (keyboardState.IsKeyDown(Input.Down))
                {
                    FramesUntilNextFall = (int)(GetFramesPerGridcell() * SoftDropSpeed);
                }
                else
                {
                    FramesUntilNextFall = GetFramesPerGridcell();
                }
            }

            if (keyboardState.IsKeyDown(Input.Down) && previousKeyboardState.IsKeyUp(Input.Down))
            {
                if (!Piece.DoDrop())
                {
                    ResetFallTime();
                    Score++;
                }
            }
            if (keyboardState.IsKeyUp(Input.Down) && previousKeyboardState.IsKeyDown(Input.Down))
            {
                FramesUntilNextFall += (int)(GetFramesPerGridcell() * (1 - SoftDropSpeed));
            }

            FramesUntilNextFall--;
            if (FramesUntilNextFall <= 0)
            {
                if (Piece.DoDrop())
                {
                    DoSendToClearAnimation();
                }
                else if (keyboardState.IsKeyDown(Input.Down))
                {
                    Score++;
                }
                ResetFallTime();
            }
        }

        private void DoUpdateMoveLeftRight(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            void HandleDASTimer()
            {
                bool IsDASKeyHeld = previousKeyboardState.IsKeyDown(Input.Left) ||
                                    previousKeyboardState.IsKeyDown(Input.Right) ||
                                    (EnableDownIsDASKey && previousKeyboardState.IsKeyDown(Input.Down));

                if (IsDASKeyHeld)
                {
                    DASLockTimer = DASLockShortDelay;
                }
                else
                {
                    DASLockTimer = DASLockLongDelay;
                }
            }

            DASLockTimer = Math.Max(0, DASLockTimer - 1);

            if (keyboardState.IsKeyDown(Input.Left) && DASLockTimer <= 0)
            {
                Piece.DoMoveLeft();
                HandleDASTimer();
            }
            if (keyboardState.IsKeyDown(Input.Right) && DASLockTimer <= 0)
            {
                Piece.DoMoveRight();
                HandleDASTimer();
            }

            if (keyboardState.IsKeyUp(Input.Left) && keyboardState.IsKeyUp(Input.Right))
            {
                DASLockTimer = 0;
            }
        }

        private void DoUpdateRotations(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            if (keyboardState.IsKeyDown(Input.Up) && previousKeyboardState.IsKeyUp(Input.Up))
            {
                Piece.DoRotateClockwise();
            }
            if (keyboardState.IsKeyDown(Input.Button1) && previousKeyboardState.IsKeyUp(Input.Button1))
            {
                Piece.DoRotateCounterClockwise();
            }
        }

        private void DoUpdateHardDrop(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            if (!EnableHardDrop)
            {
                return;
            }

            HardDropTimer = Math.Min(HardDropTimer + 1, HardDropDoubleTapWindow);

            if ((keyboardState.IsKeyDown(Input.Button2) && previousKeyboardState.IsKeyUp(Input.Button2)) ||
                (keyboardState.IsKeyDown(Input.Down) && previousKeyboardState.IsKeyUp(Input.Down)) &&
                HardDropTimer < HardDropDoubleTapWindow)
            {
                DoHardDrop();
            }

            if (keyboardState.IsKeyDown(Input.Down))
            {
                HardDropTimer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameInfo.Draw(spriteBatch);

            if (!IsPaused)
            {
                Board.Draw(spriteBatch);

                if (!Board.IsLineClearingAnimation)
                {
                    Piece.DrawGhostPiece(spriteBatch);
                    Piece.DrawOnBoard(spriteBatch);
                }
            }
            else
            {
                Board.Draw(spriteBatch);
                Piece.DrawOnBoard(spriteBatch);
                if (IsPausedInternalIndefinate)
                {
                    Board.DrawPaused(spriteBatch);
                }
                else
                {
                    Board.DrawWithNumber(spriteBatch, PauseCount);
                }
            }

            NextPiece.DrawAtLocation(spriteBatch, NextPieceLocationX, NextPieceLocationY);

            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        private void LoadSaveData()
        {
            GameStateSaveData SaveData = SaveManager.Load();

            Score = SaveData.Score;
            Lines = SaveData.Lines;
            HighScore = SaveManager.LoadHighScore();
            Board.SetState(SaveData.BoardState);
            Piece = new Piece(BaseGame, this, SaveData.PieceType);
            NextPiece = new Piece(BaseGame, this, SaveData.NextPieceType);
            TetrisLines = SaveData.TetrisLines;
        }

        public void SaveGameStateData()
        {
            GameStateSaveData NextSaveData = new GameStateSaveData()
            {
                Score = this.Score,
                Lines = this.Lines,
                BoardState = this.Board.State,
                PieceType = this.Piece.PieceType,
                NextPieceType = this.NextPiece.PieceType,
                TetrisLines = this.TetrisLines,
            };

            SaveManager.Save(NextSaveData);
        }

        private void ResetSaveData()
        {
            Random Random = new Random();
            GameStateSaveData NextSaveData = new GameStateSaveData()
            {
                Score = 0,
                Lines = 0,
                BoardState = null,
                PieceType = Random.Next(1, 8),
                NextPieceType = Random.Next(1, 8),
                TetrisLines = 0,
            };
            SaveManager.Save(NextSaveData);
        }

        private void SaveScoreData()
        {
            ScoreData NextScoreData = new ScoreData()
            {
                PlayerName = this.PlayerName,
                DateTime = $"{DateTime.Now}",
                LinesCleared = Lines,
                Score = this.Score,
            };

            SaveManager.SaveScoreData(NextScoreData);
        }

        public void TogglePause()
        {
            if (IsPausedInternalIndefinate)
            {
                PauseCount = PauseCountMax;
            }
            else
            {
                PauseCount = -1;
            }
            PauseFrames = 0;
        }

        public void DoSetPauseMax()
        {
            PauseCount = PauseCountMax + 1;
            PauseFrames = PauseFramesPerCountdown;
        }

        private void IncrementPauseCount()
        {
            PauseFrames += 1;
            if (PauseFrames > PauseFramesPerCountdown)
            {
                PauseCount = Math.Max(PauseCount - 1, 0);
                PauseFrames = 0;
            }
        }

        private void DoSendToClearAnimation()
        {
            LockSound.Play(0f, 0f, 0f);
            AddPieceToBoard();
            Board.FindAndSendRowsToClearAnimation();

            if (!Board.IsLineClearingAnimation && !DoARE)
            {
                DoGoNextAndClear();
            }
        }

        private void DoGoNextAndClear()
        {
            DoGoNextPiece();
            DoClearLines();
        }

        private void AddPieceToBoard()
        {
            int[] PieceIndexes = Piece.GetConvertedIndexes();

            if (DoPieceLockAnimation)
            {
                Board.AddToState(PieceIndexes, Piece.PieceTypeForLockAnimation);
            }
            else
            {
                Board.AddToState(PieceIndexes, Piece.PieceType);
            }
        }

        private void DoClearLines()
        {
            int LinesCleared = Board.DoClearRows();
            Score += CalculatePointsScored(LinesCleared);
            Lines += LinesCleared;
            if (LinesCleared == 4)
            {
                TetrisLines += 4;
            }
        }

        private void DoHardDrop()
        {
            int LinesDropped = Piece.DoHardDrop();

            Score += LinesDropped * 2;
            DoSendToClearAnimation();
            FramesUntilNextFall = GetFramesPerGridcell();
        }

        protected virtual void SetPiecesRandomInitial()
        {
            Piece = Piece.MakeRandomPiece(BaseGame, this);
            NextPiece = Piece.MakeRandomPiece(BaseGame, this);
        }

        protected virtual void SetPiecesInitial(int pieceType, int nextPieceType)
        {
            Piece = new Piece(BaseGame, this, pieceType);
            NextPiece = new Piece(BaseGame, this, nextPieceType);
        }

        protected virtual void DoGoNextPiece()
        {
            int PreviousPieceType = Piece.PieceType;

            Piece = NextPiece;

            Random Random = new Random();
            int NextPieceType = Random.Next(1, 8);
            if (NextPieceType == PreviousPieceType)
            {
                NextPieceType = Random.Next(1, 8);
            }

            NextPiece = new Piece(BaseGame, this, NextPieceType);
        }

        private void Reset()
        {
            HighScore = Math.Max(HighScore, Score);
            Score = 0;
            Lines = 0;
            SetPiecesRandomInitial();
            Board.Reset();
            TetrisLines = 0;

            SaveGameStateData();
            LoadSaveData();
        }

        private int GetFramesPerGridcell()
        {
            switch (Level)
            {
                case 0: return 48;
                case 1: return 43;
                case 2: return 38;
                case 3: return 33;
                case 4: return 28;
                case 5: return 23;
                case 6: return 18;
                case 7: return 13;
                case 8: return 8;
                case 9: return 6;
                case 10: return 5;
                case 11: return 5;
                case 12: return 5;
                case 13: return 4;
                case 14: return 4;
                case 15: return 4;
                case 16: return 3;
                case 17: return 3;
                case 18: return 3;
                case 19: return 2;
                case 20: return 2;
                case 21: return 2;
                case 22: return 2;
                case 23: return 2;
                case 24: return 2;
                case 25: return 2;
                case 26: return 2;
                case 27: return 2;
                case 28: return 2;
            }
            return 1;
        }

        private int CalculatePointsScored(int linesCleared)
        {
            int PointsScored = 0;
            switch (linesCleared)
            {
                case 0:
                    PointsScored = 0;
                    break;
                case 1:
                    PointsScored = 40;
                    break;
                case 2:
                    PointsScored = 100;
                    break;
                case 3:
                    PointsScored = 300;
                    break;
                case 4:
                    PointsScored = 1200;
                    break;
            }
            return PointsScored * (Level + 1);
        }
    }
}
