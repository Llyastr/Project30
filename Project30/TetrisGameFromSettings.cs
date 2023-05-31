using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class TetrisGameFromSettings : TetrisGame
    {
        public override int RowCount { get { return 22; } }
        public override int ColCount { get { return 10; } }
        public override bool DoARE { get { return true; } }
        public override int AREDelay { get { return 17; } }
        public override bool DoPieceLockAnimation { get { return true; } }
        public override int PieceLockAnimationDuration { get { return 17; } }
        protected override int LinesPerLevel { get { return 10; } }
        protected override float SoftDropSpeed { get { return 0.5f; } }
        protected override int DASLockLongDelay { get { return 16; } }
        protected override int DASLockShortDelay { get { return 6; } }
        protected override bool DoPieceDropLingering { get { return false; } }
        protected override float PieceLingeringRatio { get; }
        protected override bool EnableDownIsDASKey { get { return false; } }
        protected override int HardDropDoubleTapWindow { get { return 8; } }

        protected override bool EnableHardDrop { get { return IsEnableHardDropReadonly; } }

        private readonly bool IsEnableHardDropReadonly;

        public TetrisGameFromSettings(BaseGame baseGame, TetrisGameScreen gameScreen, Input input, string playerName, 
                                      int startLevel, int locationX, int locationY)
            : base(baseGame, gameScreen, input, startLevel, playerName, locationX, locationY)
        {
            IsEnableHardDropReadonly = baseGame.SettingsSaveData.IsEnableHardDrop;
        }
    }
}
