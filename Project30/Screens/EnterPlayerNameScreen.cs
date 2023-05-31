using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class EnterPlayerNameScreen : StaticScreen
    {
        static readonly string TextureName = "EmptyBackground";

        readonly string WhiteLineTextureName = "WhiteLine";
        Texture2D WhiteLineTexture
        {
            get { return BaseGame.GetTexture(WhiteLineTextureName); }
        }

        readonly string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        readonly Color BackgroundColour = Color.DarkBlue;
        readonly Color TextColour = Color.White;

        protected virtual string PromptText
        {
            get { return "Enter player name:"; }
        }
        Vector2 PromptTextMeasure
        {
            get { return Font.MeasureString(PromptText); }
        }

        protected string PlayerName;
        Vector2 PlayerNameMeasure
        {
            get { return Font.MeasureString(PlayerName); }
        }

        const int WhiteLineGap = 3;
        const int FramesPerBlink = 42;

        private int BlinkFrames;
        private bool IsBlinked;

        const int PromptGap = 72;

        protected const int MaxNameLength = 22;
        protected const int MinNameLength = 1;

        const int BackHoldTimerMax = 32;
        private int BackHoldTimer;

        int PromptTextLocationX
        {
            get { return (int)(ScreenWidth - PromptTextMeasure.X) / 2; }
        }
        int PromptTextLocationY
        {
            get { return PlayerNameLocationY - PromptGap; }
        }
        Vector2 PromptTextLocation
        {
            get { return new Vector2(PromptTextLocationX, PromptTextLocationY); }
        }

        int PlayerNameLocationX
        {
            get { return (int)(ScreenWidth - PlayerNameMeasure.X - WhiteLineWidth) / 2 ; }
        }
        int PlayerNameLocationY
        {
            get { return (int)(ScreenHeight- PlayerNameMeasure.Y) / 2; }
        }
        Vector2 PlayerNameLocation
        {
            get { return new Vector2(PlayerNameLocationX, PlayerNameLocationY); }
        }

        int WhiteLineLocationX
        {
            get { return (int)(ScreenWidth + PlayerNameMeasure.X) / 2 + WhiteLineGap; }
        }
        int WhiteLineLocationY
        {
            get { return PlayerNameLocationY; }
        }
        int WhiteLineWidth
        {
            get { return 2; }
        }
        int WhiteLineHeight
        {
            get { return (int)PromptTextMeasure.Y; }
        }
        Rectangle WhiteLineRectangle
        {
            get { return new Rectangle(WhiteLineLocationX, WhiteLineLocationY, WhiteLineWidth, WhiteLineHeight); }
        }

        public EnterPlayerNameScreen(BaseGame baseGame)
            : base(baseGame, TextureName)
        {
            PlayerName = "";
        }

        public override void Update(GameTime gameTime, 
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            DoUpdateBlink();
            DoUpdateKeys(keyboardState, previousKeyboardState);

            if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                OnEnterPress();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, BackgroundColour);

            spriteBatch.DrawString(Font, PromptText, PromptTextLocation, TextColour);
            spriteBatch.DrawString(Font, PlayerName, PlayerNameLocation, TextColour);

            if (IsBlinked)
            {
                spriteBatch.Draw(WhiteLineTexture, WhiteLineRectangle, TextColour);
            }
        }

        public virtual void OnEnterPress()
        {
            throw new Exception("OnEnterPress not yet implemented");
        }

        private void DoUpdateBlink()
        {
            BlinkFrames = Math.Max(0, BlinkFrames - 1);
            if (BlinkFrames == 0)
            {
                BlinkFrames = FramesPerBlink;
                IsBlinked = !IsBlinked;
            }
        }

        private void DoUpdateKeys(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            void CheckKey(Keys key)
            {
                if (keyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                {
                    string Letter = key.ToString();
                    if (key == Keys.OemPeriod)
                    {
                        Letter = ".";
                    }
                    if (Letter.Length > 1)
                    {
                        Letter = Letter[Letter.Length - 1].ToString();
                    }

                    if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                    {
                        PlayerName += Letter;
                    }
                    else
                    {
                        PlayerName += Letter.ToLower();
                    }
                }
            }

            Keys[] AllKeys = new Keys[]
            {
                Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.A, 
                Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, Keys.Z, Keys.X, Keys.C, 
                Keys.V, Keys.B, Keys.N, Keys.M, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, 
                Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.OemPeriod
            };

            if (PlayerName.Length <= MaxNameLength)
            {
                foreach (Keys key in AllKeys)
                {
                    CheckKey(key);
                }
                if (keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    PlayerName += " ";
                }
            }

            BackHoldTimer = Math.Max(0, BackHoldTimer - 1);
            if (keyboardState.IsKeyDown(Keys.Back) && BackHoldTimer <= 0)
            {
                if (previousKeyboardState.IsKeyUp(Keys.Back))
                {
                    BackHoldTimer = BackHoldTimerMax;
                }

                if (PlayerName.Length != 0)
                {
                    PlayerName = PlayerName.Substring(0, PlayerName.Length - 1);
                }
            }
            if (keyboardState.IsKeyUp(Keys.Back))
            {
                BackHoldTimer = 0;
            }
        }
    }
}
