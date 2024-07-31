using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Rooms.Osbrook;
using StoneShard_Mono.Content.Scenes;
using StoneShard_Mono.Loaders;
using StoneShard_Mono.Managers;
using System;
using System.Collections.Generic;
using System.IO;

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

        public static GameScene GameScene => CurrentScene as GameScene;

        public static Random Random;

        public static RenderTarget2D RenderTarget;

        public static bool PlayerTurn => GameScene.TurnController.PlayerTurn;

        public static string CursorType;

        public static Player LocalPlayer;

        public static Dictionary<string, int> RoomID;

        public static Dictionary<string, int> TileID;

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
            CursorType = "";
            LocalPlayer = null;

            RoomID = new();
            TileID = new();
        }
        protected override void Initialize()
        {
            TextureManager.Load();
            FontManager.Load();
            LocalizationManager.Load();

            RenderTarget = new(_graphics.GraphicsDevice, GameWidth, GameHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

            SetCursor("cursor");

            EntityLoader.Load();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            CurrentScene = new GameScene();

            GameScene.GoToRoom(ContentInstance<tavernInside_floor_1>.Instance);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Reset();

            CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, sortMode: SpriteSortMode.BackToFront);

            CurrentScene.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Reset()
        {
            if (GameScene == null) return;

            GameScene.DrawSelectBox = true;
        }

        public static string GetText(string key, string language = "")
        {
            return language == "" 
                ? LocalizationManager[CurrentLanguage, key]
                : LocalizationManager[language, key];
        }

        public static void SetCursor(string texID)
        {
            var cursor = MouseCursor.FromTexture2D(TextureManager[TexType.UI, texID], 0, 0);
            Mouse.SetCursor(cursor);
            CursorType = texID;
        }
    }
}