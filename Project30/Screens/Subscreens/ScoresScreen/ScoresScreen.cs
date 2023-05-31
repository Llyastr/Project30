using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ScoresScreen : StaticScreen
    {
        readonly static string TextureName = "EmptyBackground";
        readonly Color BackgroundColour = Color.DarkBlue;
        
        readonly float ButtonsInitialLocationFloatX = 0.67f;
        readonly float ButtonsIniitalLocationFloatY = 0.3f;
        readonly float ButtonGapFloatX = 0.03f;
        readonly float ButtonGapFloatY = 0.11f;
        readonly float ButtonWidthFloat = 0.16f;
        readonly float ButtonHeightFloat = 0.07f;

        int ButtonsInitialLocationX
        {
            get { return (int)(ScreenWidth * ButtonsInitialLocationFloatX); }
        }
        int ButtonsInitialLocationY
        {
            get { return (int)(ScreenHeight * ButtonsIniitalLocationFloatY); }
        }
        int ButtonGapX
        {
            get { return (int)(ScreenWidth * ButtonGapFloatX); }
        }
        int ButtonGapY
        {
            get { return (int)(ScreenHeight * ButtonGapFloatY); }
        }
        int ButtonWidth
        {
            get { return (int)(ScreenWidth * ButtonWidthFloat); }
        }
        int ButtonHeight
        {
            get { return (int)(ScreenHeight * ButtonHeightFloat); }
        }

        ScoresSubscreen ScoresSubscreen;

        public ScoresScreen(BaseGame baseGame)
            : base(baseGame, TextureName)
        {
            Buttons = new Button[0];

            ScoresSubscreen = new ScoresSubscreen(baseGame);
        }

        public ScoresScreen(BaseGame baseGame, float initialScrollLocationFloat)
            : base(baseGame, TextureName)
        {
            Buttons = new Button[0];

            ScoresSubscreen = new ScoresSubscreen(baseGame, initialScrollLocationFloat);
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState,
                                    KeyboardState previousKeyboardState,
                                    MouseState mouseState,
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            ScoresSubscreen.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyDown(Keys.Escape))
            {
                BaseGame.ScreenManager.CloseScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, BackgroundColour);

            ScoresSubscreen.Draw(spriteBatch);

            foreach (Button button in Buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
