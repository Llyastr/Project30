using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class Screen
    {
        protected BaseGame BaseGame;

        public bool IsActive
        {
            get { return BaseGame.ScreenManager.ActiveScreen == this; }
        }

        protected int ScreenWidth
        {
            get { return BaseGame.ScreenWidth; }
        }
        protected int ScreenHeight
        {
            get { return BaseGame.ScreenHeight; }
        }

        public Screen(BaseGame baseGame)
        {
            BaseGame = baseGame;
        }

        public virtual void Update(GameTime gameTime,
                                   KeyboardState keyboardState,
                                   KeyboardState previousKeyboardState,
                                   MouseState mouseState,
                                   MouseState previousMouseState)
        {
            throw new Exception("Screen Update not set");
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new Exception("Screen Draw not set");
        }
    }
}
