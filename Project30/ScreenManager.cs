using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScreenManager
    {
        BaseGame BaseGame;

        List<Screen> ActiveScreens;

        public Screen ActiveScreen
        {
            get { return ActiveScreens[ActiveScreens.Count - 1]; }
        }

        public ScreenManager(BaseGame baseGame)
        {
            BaseGame = baseGame;

            ActiveScreens = new List<Screen>()
            {
                new TitleScreen(baseGame)
            };
        }

        public void Update(GameTime gameTime,
                           KeyboardState keyboardState,
                           KeyboardState previousKeyboardState,
                           MouseState mouseState,
                           MouseState previousMouseState)
        {
            ActiveScreen.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Screen screen in ActiveScreens)
            {
                screen.Draw(spriteBatch);
            }
        }

        public void AddScreen(Screen screen)
        {
            ActiveScreens.Add(screen);
        }

        public void CloseScreen()
        {
            ActiveScreens.RemoveAt(ActiveScreens.Count - 1);
        }

        public void CloseAll()
        {
            ActiveScreens.Clear();
        }
    }
}
