using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScoresSubscreen : VerticalScrollSubscreen
    {
        ScoreDisplay[] ScoreDisplays;

        Button BackScrollButton;

        ScoresSubscreenHeader Header;

        private readonly int MaxShownScores = 100;

        private readonly float HeaderGapFloat = 0.089f;
        private int HeaderGap
        {
            get { return (int)(ScreenHeight * HeaderGapFloat); }
        }
        private readonly float ScoreDisplayGapFloat = 0.1f;
        private int ScoreDisplayGap
        {
            get { return (int)(ScreenHeight * ScoreDisplayGapFloat); }
        }

        private readonly float ScoreDisplayLocationFloatX = 0.088f;

        private readonly float ExtraSpaceAtBottom = -0.52523f;

        private readonly int BackScrollButtonGap = 16;
        private int BackScrollButtonLocationX
        {
            get { return (ScreenWidth - BackScrollButtonWidth) / 2; }
        }
        private int BackScrollButtonWidth
        {
            get { return 180; }
        }
        private int BackScrollButtonHeight
        {
            get { return 56; }
        }

        public ScoresSubscreen(BaseGame baseGame)
            : base(baseGame)
        {
            Initialize(baseGame);
        }

        public ScoresSubscreen(BaseGame baseGame, float initialScrollLocationFloat)
            : base(baseGame, initialScrollLocationFloat)
        {
            Initialize(baseGame);
        }

        private void Initialize(BaseGame baseGame)
        {
            SaveScoreData SaveScoreData = SaveManager.LoadScoreData();
            int ScoreDataCount = SaveScoreData.ScoreDatas.Count;

            MaxLocationFloatX = Math.Clamp(ScoreDisplayGapFloat * ScoreDataCount + ExtraSpaceAtBottom, 0f, 
                                           ScoreDisplayGapFloat * MaxShownScores + ExtraSpaceAtBottom);
            MinLocationFloatX = 0f;

            int CurrentSlot = 1;

            Header = new ScoresSubscreenHeader(baseGame, this, ScoreDisplayLocationFloatX, HeaderGapFloat);

            List<ScoreDisplay> TempScoreDisplays = new List<ScoreDisplay>();
            foreach (ScoreData scoreData in SaveScoreData.ScoreDatas)
            {
                TempScoreDisplays.Add(new ScoreDisplay(baseGame,
                                                       this,
                                                       ScoreDisplayLocationFloatX,
                                                       HeaderGapFloat + ScoreDisplayGapFloat * CurrentSlot,
                                                       CurrentSlot,
                                                       scoreData.DateTime,
                                                       scoreData.PlayerName,
                                                       scoreData.Score,
                                                       scoreData.LinesCleared));

                CurrentSlot++;
                
                if (CurrentSlot > MaxShownScores)
                {
                    break;
                }
            }

            BackScrollButton = new BackScrollButton(baseGame,
                                                    this,
                                                    BackScrollButtonLocationX,
                                                    HeaderGap + ScoreDisplayGap * CurrentSlot + BackScrollButtonGap,
                                                    BackScrollButtonWidth,
                                                    BackScrollButtonHeight);

            ScoreDisplays = TempScoreDisplays.ToArray();
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState, 
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            foreach (ScoreDisplay scoreDisplay in ScoreDisplays)
            {
                scoreDisplay.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
            }

            BackScrollButton.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Header.Draw(spriteBatch);

            foreach (ScoreDisplay scoreDisplay in ScoreDisplays)
            {
                scoreDisplay.Draw(spriteBatch);
            }

            BackScrollButton.Draw(spriteBatch);
        }
    }
}
