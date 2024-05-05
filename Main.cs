using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Scenes;
using System;

namespace StoneShard_Mono
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int GameHeight => Instance._graphics.PreferredBackBufferHeight;
        public static int GameWidth => Instance._graphics.PreferredBackBufferWidth;

        internal static string GamePath => Environment.CurrentDirectory;

        public static Main Instance;

        public static TextureManager TextureManager;
        public static FontManager FontManager;
        public static LocalizationManager LocalizationManager;

        public static Scene CurrentScene;

        public static Random Random;

        public static string CurrentLanguage;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1200,
                HardwareModeSwitch = false,
                PreferMultiSampling = false
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;
            Random = new Random();
            TextureManager = new TextureManager();
            FontManager = new FontManager();
            LocalizationManager = new LocalizationManager();
            CurrentLanguage = "en_US";
        }

        protected override void Initialize()
        {
            TextureManager.Load();
            FontManager.Load();
            LocalizationManager.Load();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            CurrentScene = new MenuScene();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            CurrentScene.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static string GetText(string key, string language = "")
        {
            return language == "" 
                ? LocalizationManager[CurrentLanguage, key]
                : LocalizationManager[language, key];
        }
    }
}