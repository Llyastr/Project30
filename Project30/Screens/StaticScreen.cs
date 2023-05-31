using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class StaticScreen : Screen
    {
        protected readonly string BackgroundTextureName;
        protected Texture2D BackgroundTexture
        {
            get { return BaseGame.GetTexture(BackgroundTextureName); }
        }

        protected Button[] Buttons;

        protected virtual Rectangle BackgroundRectangle
        {
            get { return new Rectangle(0, 0, ScreenWidth, ScreenHeight); }
        }

        public StaticScreen(BaseGame baseGame, string backgroundTextureName)
            : base(baseGame)
        {
            BackgroundTextureName = backgroundTextureName;
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState,
                                    MouseState previousMouseState)
        {
            foreach (Button button in Buttons)
            {
                button.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.White);
            DrawButtons(spriteBatch);
        }

        protected void DrawButtons(SpriteBatch spriteBatch)
        {
            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
