using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class RecastKeyScreen : StaticScreen
    {
        private readonly static string TitleBackgroundTextureName = "WhiteBackgroundWithBorder";

        private readonly string FontName = "Font24";
        private SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        private readonly string Text = "Select new key:";

        private readonly float BackgroundTransparency = 0.87345f;

        private readonly string LinkedKeyName;

        private List<Keys> AllKeys;

        private readonly int TextLocationX;
        private readonly int TextLocationY;

        private Vector2 TextLocation
        {
            get { return new Vector2(TextLocationX, TextLocationY); }
        }

        private readonly Color TextColour = Color.Black;

        public RecastKeyScreen(BaseGame baseGame, string linkedKeyName)
            : base(baseGame, TitleBackgroundTextureName)
        {
            Buttons = new Button[0];

            LinkedKeyName = linkedKeyName;

            AllKeys = new List<Keys>()
            {
                Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.I, Keys.O, Keys.P, Keys.A, Keys.S, Keys.D,
                Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N,
                Keys.M, Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.LeftShift, Keys.RightShift, Keys.OemQuestion,
                Keys.OemComma, Keys.OemPeriod,
            };

            TextLocationX = (int)(ScreenWidth - Font.MeasureString(Text).X) / 2;
            TextLocationY = (int)(ScreenHeight - Font.MeasureString(Text).Y) / 2;
        }

        public override void Update(GameTime gameTime, 
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            foreach (Keys key in AllKeys)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    BaseGame.SettingsSaveData.Keys[LinkedKeyName] = key;
                    BaseGame.SaveSettingsData();

                    BaseGame.ScreenManager.CloseScreen();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.White * BackgroundTransparency);

            spriteBatch.DrawString(Font, Text, TextLocation, TextColour);
        }
    }
}
