using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class ResetSettingsButton : VerticalScrollButton
    {
        private readonly string ButtonTextNonLingered = "Restore default";
        private readonly string ButtonTextLingered = "Default settings restored";

        protected override string Text
        { 
            get
            {
                if (IsLingering)
                {
                    return ButtonTextLingered;
                }
                else
                {
                    return ButtonTextNonLingered;
                }
            }
        }

        private readonly int MaxFramesLingered = 126;

        private int FramesLingered;

        private bool IsLingering
        {
            get { return FramesLingered > 0; }
        }

        public ResetSettingsButton(BaseGame baseGame, VerticalScrollSubscreen scrollScreen,
                               int locationX, int locationY, int width, int height)
             : base(baseGame, scrollScreen, "", locationX, locationY, width, height)
        {
            
        }

        public override void Update(GameTime gameTime, 
                                    KeyboardState keyboardState, 
                                    KeyboardState previousKeyboardState, 
                                    MouseState mouseState, 
                                    MouseState previousMouseState)
        {
            base.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);

            if (FramesLingered > 0)
            {
                FramesLingered = Math.Max(FramesLingered - 1, 0);
            }
        }

        public override void OnClick()
        {
            BaseGame.ResetSettingsSaveData();

            FramesLingered = MaxFramesLingered;
        }
    }
}
