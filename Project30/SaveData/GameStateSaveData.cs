using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    [Serializable]
    public class GameStateSaveData
    {
        public int Lines;
        public int Score;
        public int[] BoardState;
        public int PieceType;
        public int NextPieceType;
        public int TetrisLines;

        public GameStateSaveData()
        {

        }
    }
}
