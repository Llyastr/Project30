using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project30
{
    class BaseGame
    {
        private readonly Game1 OperatingGame;

        public readonly ScreenManager ScreenManager;
        public SettingsSaveData SettingsSaveData { get; private set; }

        public int ScreenWidth
        {
            get { return SettingsSaveData.ScreenWidth; }
        }
        public int ScreenHeight
        {
            get { return SettingsSaveData.ScreenHeight; }
        }

        public BaseGame(Game1 operatingGame)
        {
            OperatingGame = operatingGame;

            SettingsSaveData = SaveManager.LoadSettingsSaveData();

            ScreenManager = new ScreenManager(this);
        }

        public void Update(GameTime gameTime,
                           KeyboardState keyboardState,
                           KeyboardState previousKeyboardState,
                           MouseState mouseState,
                           MouseState previousMouseState)
        {
            ScreenManager.Update(gameTime, keyboardState, previousKeyboardState, mouseState, previousMouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ScreenManager.Draw(spriteBatch);
        }

        public void SaveSettingsData()
        {
            SaveManager.SaveSettingsData(SettingsSaveData);
        }

        public void ResetSettingsSaveData()
        {
            SettingsSaveData = SaveManager.GetDefaultSettingsSaveData();
            SaveSettingsData();
        }

        public Texture2D GetTexture(string textureName)
        {
            return OperatingGame.Textures[textureName];
        }

        public SpriteFont GetFont(string fontName)
        {
            return OperatingGame.Fonts[new Tuple<string, int>(fontName, ScreenWidth)];
        }

        public SoundEffect GetSoundEffect(string soundEffectName)
        {
            return OperatingGame.SoundEffects[soundEffectName];
        }

        public Song GetSong(string songName)
        {
            return OperatingGame.Songs[songName];
        }

        public void SetScreenSize(int nextWidth, int nextHeight)
        {
            SettingsSaveData.ScreenWidth = nextWidth;
            SettingsSaveData.ScreenHeight = nextHeight;
            SaveSettingsData();
            OperatingGame.SetScreenSize();
        }

        public void Exit()
        {
            OperatingGame.Exit();
        }
    }
}
