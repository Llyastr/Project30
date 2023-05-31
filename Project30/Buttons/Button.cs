using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class Button
    {
        protected BaseGame BaseGame;

        protected virtual string ButtonTextureName
        {
            get { return "ButtonTexture"; }
        }
        Texture2D ButtonTexture
        {
            get { return BaseGame.GetTexture(ButtonTextureName); }
        }

        string FontName = "Font24";
        SpriteFont Font
        {
            get { return BaseGame.GetFont(FontName); }
        }

        protected virtual string Text { get; set; }

        protected bool IsHovered;

        protected Vector2 TextMeasure
        {
            get { return Font.MeasureString(Text); }
        }
        protected virtual int TextLocationX
        {
            get { return LocationX + (Width - (int)TextMeasure.X) / 2; }
        }
        protected virtual int TextLocationY
        {
            get { return LocationY + (Height - (int)TextMeasure.Y) / 2; }
        }
        Vector2 TextLocation
        {
            get { return new Vector2(TextLocationX, TextLocationY); }
        }

        protected virtual int LocationX { get; }
        protected virtual int LocationY { get; }
        protected readonly int Width;
        protected readonly int Height;

        Rectangle Rectangle
        {
            get { return new Rectangle(LocationX, LocationY, Width, Height); }
        }

        protected virtual Color ButtonHoverColour
        {
            get { return Color.CadetBlue; }
        }
        protected virtual Color ButtonUnhoverColour
        {
            get { return Color.White; }
        }
        protected virtual Color ButtonColour
        {
            get
            {
                if (IsHovered)
                {
                    return ButtonHoverColour;
                }
                else
                {
                    return ButtonUnhoverColour;
                }
            }
        }

        protected virtual Color TextHoverColour
        {
            get { return Color.Blue; }
        }
        protected virtual Color TextUnhoverColour
        {
            get { return Color.Black; }
        }
        protected Color TextColour
        {
            get
            {
                if (IsHovered)
                {
                    return TextHoverColour;
                }
                else
                {
                    return TextUnhoverColour;
                }
            }
        }

        public Button(BaseGame baseGame, string text, 
                      int locationX, int locationY, int width, int height)
        {
            BaseGame = baseGame;

            Text = text;

            LocationX = locationX;
            LocationY = locationY;
            Width = width;
            Height = height;
        }

        public virtual void Update(GameTime gameTime,
                                   KeyboardState keyboardState, 
                                   KeyboardState previousKeyboardState,
                                   MouseState mouseState,
                                   MouseState previousMouseState)
        {
            Point MousePoint = new Point(mouseState.X, mouseState.Y);

            if (this.Rectangle.Contains(MousePoint))
            {
                IsHovered = true;
                if (mouseState.LeftButton == ButtonState.Pressed && 
                    previousMouseState.LeftButton == ButtonState.Released)
                {
                    OnClick();
                }
            }
            else
            {
                IsHovered = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ButtonTexture, Rectangle, ButtonColour);
            spriteBatch.DrawString(Font, Text, TextLocation, TextColour);
        }

        public virtual void OnClick()
        {
            throw new Exception("Button OnClick not yet implemented");
        }
    }
}
