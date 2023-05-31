using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class SettingsSubscreen : VerticalScrollSubscreen
    {
        private ButtonCastDisplay[] ButtonCastDisplays;

        private readonly List<Button> Buttons = new List<Button>();

        private readonly float HeaderGapFloat = 0.011f;
        private int HeaderGap
        {
            get { return (int)(ScreenHeight * HeaderGapFloat); }
        }

        private readonly float ScoreDisplayGapFloat = 0.13f;
        private int ScoreDisplayGap
        {
            get { return (int)(ScreenHeight * ScoreDisplayGapFloat); }
        }

        private readonly float ButtonCastDisplayLocationFloatX = 0.183f;

        private readonly float ExtraSpaceAtBottom = -0.17523f;

        private readonly int BackScrollButtonGap = 70;
        private int BackScrollButtonLocationX
        {
            get { return (ScreenWidth - BackScrollButtonWidth) / 2; }
        }
        private int BackScrollButtonWidth
        {
            get { return 240; }
        }
        private int BackScrollButtonHeight
        {
            get { return 70; }
        }

        private int ResetButtonWidth
        {
            get { return 500; }
        }
        private int ResetButtonHeight
        {
            get { return 96; }
        }
        private int ResetButtonLocationX
        {
            get { return (ScreenWidth - ResetButtonWidth) / 2; }
        }
        private readonly int ResetButtonGap = 14;

        private readonly int ToggleButtonCount = 1;
        private readonly int ScreenSizeRefactorCount = 2;
        private readonly string EnableHardDropToggleText = "Enable hard drop";

        public SettingsSubscreen(BaseGame baseGame)
            : base(baseGame)
        {
            Initialize(baseGame);
        }

        public SettingsSubscreen(BaseGame baseGame, float initialScrollLocationFloat)
            : base(baseGame, initialScrollLocationFloat)
        {
            Initialize(baseGame);
        }

        private void Initialize(BaseGame baseGame)
        {
            List<Tuple<string, string>> TextKeyPairs = GetTextKeyPairs();

            int TextKeyPairCount = TextKeyPairs.Count;

            MaxLocationFloatX = Math.Max(ScoreDisplayGapFloat * (TextKeyPairCount + ToggleButtonCount + ScreenSizeRefactorCount) 
                + ExtraSpaceAtBottom, 0f);
            MinLocationFloatX = 0f;

            int CurrentSlot = 1;

            List<ButtonCastDisplay> TempButtonCastDisplays = new List<ButtonCastDisplay>();
            foreach (Tuple<string, string> textKeyPair in TextKeyPairs)
            {
                TempButtonCastDisplays.Add(new ButtonCastDisplay(baseGame,
                                                       this,
                                                       textKeyPair.Item1,
                                                       textKeyPair.Item2,
                                                       ButtonCastDisplayLocationFloatX,
                                                       HeaderGapFloat + ScoreDisplayGapFloat * CurrentSlot));

                CurrentSlot++;
            }

            TempButtonCastDisplays.Add(new ToggleHardDropDisplay(baseGame,
                                                       this,
                                                       EnableHardDropToggleText,
                                                       ButtonCastDisplayLocationFloatX,
                                                       HeaderGapFloat + ScoreDisplayGapFloat * CurrentSlot));
            CurrentSlot++;

            Button Refactor1600x900Button = new ScreenSizeRefactorButton(baseGame,
                                                    this,
                                                    ResetButtonLocationX,
                                                    HeaderGap + ScoreDisplayGap * CurrentSlot + ResetButtonGap,
                                                    ResetButtonWidth,
                                                    ResetButtonHeight,
                                                    1600, 900);
            CurrentSlot++;

            Button Refactor2400x1350Button = new ScreenSizeRefactorButton(baseGame,
                                                    this,
                                                    ResetButtonLocationX,
                                                    HeaderGap + ScoreDisplayGap * CurrentSlot + ResetButtonGap,
                                                    ResetButtonWidth,
                                                    ResetButtonHeight,
                                                    2400, 1350);

            Button ResetDefualtButton = new ResetSettingsButton(baseGame,
                                                    this,
                                                    ResetButtonLocationX,
                                                    HeaderGap + ScoreDisplayGap * CurrentSlot + ResetButtonGap,
                                                    ResetButtonWidth,
                                                    ResetButtonHeight);
            CurrentSlot++;

            Button BackScrollButton = new BackScrollButton(baseGame,
                                                    this,
                                                    BackScrollButtonLocationX,
                                                    HeaderGap + ScoreDisplayGap * CurrentSlot + ResetButtonGap 
                                                    + ResetButtonHeight + BackScrollButtonGap,
                                                    BackScrollButtonWidth,
                                                    BackScrollButtonHeight);

            Buttons.Add(ResetDefualtButton);
            //Buttons.Add(Refactor2400x1350Button);
            //Buttons.Add(Refactor1600x900Button);
            Buttons.Add(BackScrollButton);

            ButtonCastDisplays = TempButtonCastDisplays.ToArray();
        }

        private List<Tuple<string, string>> GetTextKeyPairs()
        {
            return new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Player1 Move Left", "LeftPlayer1"),
                new Tuple<string, string>("Player1 Move Right", "RightPlayer1"),
                new Tuple<string, string>("Player1 Soft Drop", "DownPlayer1"),
                new Tuple<string, string>("Player1 Hard Drop", "Button2Player1"),
                new Tuple<string, string>("Player1 Rotate Counterclockwise", "Button1Player1"),
                new Tuple<string, string>("Player1 Rotate Clockwise", "UpPlayer1"),

                new Tuple<string, string>("Player2 Move Left", "LeftPlayer2"),
                new Tuple<string, string>("Player2 Move Right", "RightPlayer2"),
                new Tuple<string, string>("Player2 Soft Drop", "DownPlayer2"),
                new Tuple<string, string>("Player2 Hard Drop", "Button2Player2"),
                new Tuple<string, string>("Player2 Rotate Counterclockwise", "Button1Player2"),
                new Tuple<string, string>("Player2 Rotate Clockwise", "UpPlayer2"),
            };
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState,
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            foreach (ButtonCastDisplay buttonCastDisplay in ButtonCastDisplays)
            {
                buttonCastDisplay.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
            }

            foreach (Button button in Buttons)
            {
                button.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (ButtonCastDisplay buttonCastDisplay in ButtonCastDisplays)
            {
                buttonCastDisplay.Draw(spriteBatch);
            }

            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
