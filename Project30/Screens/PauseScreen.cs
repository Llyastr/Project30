using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class PauseScreen : StaticScreen
    {
        readonly static string TextureName = "PauseBackground";
        readonly string EmptyBackgroundTextureName = "EmptyBackground";
        Texture2D EmptyBackgroundTexture
        {
            get { return BaseGame.GetTexture(EmptyBackgroundTextureName); }
        }

        const float BackgroundTransparency = 0.6f;

        readonly float ButtonsInitialLocationFloatX = 0.20f;
        readonly float ButtonsIniitalLocationFloatY = 0.20f;
        readonly float ButtonGapFloatX = -0.03f;
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

        public PauseScreen(BaseGame baseGame)
            : base(baseGame, TextureName)
        {
            List<Button> TempButtons = new List<Button>()
            {
                new ScoresScreenButton(baseGame,
                                       ButtonsInitialLocationX + ButtonGapX * 0,
                                       ButtonsInitialLocationY + ButtonGapY * 0,
                                       ButtonWidth,
                                       ButtonHeight),
                new TitleButton(baseGame,
                                ButtonsInitialLocationX + ButtonGapX * 1,
                                ButtonsInitialLocationY + ButtonGapY * 1,
                                ButtonWidth,
                                ButtonHeight),
                new BackButton(baseGame,
                               ButtonsInitialLocationX + ButtonGapX * 2,
                               ButtonsInitialLocationY + ButtonGapY * 2,
                               ButtonWidth,
                               ButtonHeight),
                new ExitButton(baseGame,
                               ButtonsInitialLocationX + ButtonGapX * 3,
                               ButtonsInitialLocationY + ButtonGapY * 3,
                               ButtonWidth,
                               ButtonHeight),
            };

            Buttons = TempButtons.ToArray();
        }

        public override void Update(GameTime gameTime,
                                    KeyboardState keyboardState, 
                                    KeyboardState previousKeyboardState, 
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                BaseGame.ScreenManager.CloseScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EmptyBackgroundTexture, BackgroundRectangle, Color.White * BackgroundTransparency);

            base.Draw(spriteBatch);
        }
    }
}
