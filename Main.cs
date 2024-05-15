using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono.UIComponents;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Scenes;
using System;

namespace StoneShard_Mono
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Vector2 GameSize => new(GameWidth, GameHeight);

        public static int GameHeight => Instance._graphics.PreferredBackBufferHeight;
        public static int GameWidth => Instance._graphics.PreferredBackBufferWidth;

        public static int TileSize = 52;

        internal static string GamePath => Environment.CurrentDirectory;

        public static string CurrentLanguage;

        public static Main Instance;

        public static TextureManager TextureManager;
        public static FontManager FontManager;
        public static LocalizationManager LocalizationManager;

        public static Scene CurrentScene;

        public static Random Random;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1200,
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

            var cursor = MouseCursor.FromTexture2D(TextureManager[TexType.UI, "cursor"], 0, 0);
            Mouse.SetCursor(cursor);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            CurrentScene = new GameScene();
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
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone);

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