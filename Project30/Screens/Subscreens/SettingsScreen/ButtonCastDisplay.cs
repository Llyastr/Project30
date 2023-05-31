using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ButtonCastDisplay
    {
        private readonly BaseGame BaseGame;

        private readonly SettingsSubscreen SettingsSubscreen;

        protected Button RecastKeyButton;

        int ScreenWidth
        {
            get { return BaseGame.ScreenWidth; }
        }
        int ScreenHeight
        {
            get { return BaseGame.ScreenHeight; }
        }

        readonly string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        readonly static Color TextColour = Color.White;

        private int ScrollLocation
        {
            get { return SettingsSubscreen.ScrollLocation; }
        }

        private readonly float LocationFloatX;
        private readonly float LocationFloatY;

        private int LocationX
        {
            get { return (int)(ScreenWidth * LocationFloatX); }
        }
        private int LocationY
        {
            get { return (int)(ScreenHeight * LocationFloatY - ScrollLocation); }
        }

        private readonly float TextLocationFloatX = 0.04f;
        private readonly float ButtonLocationFloatX = 0.4f;
        private readonly float ButtonWidthFloat = 0.2f;
        private readonly float ButtonHeightFloat = 0.1f;

        private int TextLocationX
        {
            get { return (int)(ScreenWidth * TextLocationFloatX) + LocationX; }
        }
        private int TextLocationY
        {
            get { return (int)(ButtonHeight - Font.MeasureString(Text).Y) / 2 + LocationY; }
        }

        Vector2 TextLocation
        {
            get { return new Vector2(TextLocationX, TextLocationY); }
        }

        protected int ButtonLocationX
        {
            get { return (int)(ScreenWidth * ButtonLocationFloatX) + LocationX; }
        }
        protected int ButtonLocationY
        {
            get { return LocationY; }
        }
        protected int ButtonWidth
        {
            get { return (int)(ScreenWidth * ButtonWidthFloat); }
        }
        protected int ButtonHeight
        {
            get { return (int)(ScreenHeight * ButtonHeightFloat); }
        }

        private readonly string Text;

        public ButtonCastDisplay(BaseGame baseGame, SettingsSubscreen settingsSubscreen, 
                                 string text, string linkedKeyName,
                                 float locationFloatX, float locationFloatY)
        {
            BaseGame = baseGame;

            SettingsSubscreen = settingsSubscreen;

            LocationFloatX = locationFloatX;
            LocationFloatY = locationFloatY;

            Text = text;

            RecastKeyButton = new RecastKeyButton(baseGame, settingsSubscreen, linkedKeyName, 
                                                  ButtonLocationX, ButtonLocationY,
                                                  ButtonWidth, ButtonHeight);
        }

        public ButtonCastDisplay(BaseGame baseGame, SettingsSubscreen settingsSubscreen,
                                 string text, float locationFloatX, float locationFloatY)
        {
            BaseGame = baseGame;

            SettingsSubscreen = settingsSubscreen;

            LocationFloatX = locationFloatX;
            LocationFloatY = locationFloatY;

            Text = text;
        }

        public void Update(GameTime gameTime,
                           KeyboardState keyboardState,
                           KeyboardState previousKeyboardState,
                           MouseState mouseState,
                           MouseState previousMouseState)
        {
            RecastKeyButton.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, TextLocation, TextColour);

            RecastKeyButton.Draw(spriteBatch);
        }
    }
}
