using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class SettingsScreen : StaticScreen
    {
        private readonly static string TextureName = "EmptyBackground";
        private readonly Color BackgroundColour = Color.DarkBlue;

        private readonly SettingsSubscreen SettingsSubscreen;

        public SettingsScreen(BaseGame baseGame)
            : base(baseGame, TextureName)
        {
            Buttons = new Button[0];

            SettingsSubscreen = new SettingsSubscreen(baseGame);
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState,
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            SettingsSubscreen.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyDown(Keys.Escape))
            {
                BaseGame.ScreenManager.CloseScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, BackgroundColour);

            SettingsSubscreen.Draw(spriteBatch);

            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
