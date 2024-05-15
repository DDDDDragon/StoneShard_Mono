using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Managers;
using StoneShard_Mono.UIComponents;
using System;
using System.Collections;

namespace StoneShard_Mono.Rooms
{
    [Serializable]
    public class Room
    {
        public int[] TileMap;

        [JsonIgnore]
        public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)RealSize.X, (int)RealSize.Y);

        [JsonIgnore]
        public Rectangle TileRectangle => Rectangle.Divide(Main.TileSize);

        [JsonIgnore]
        public Vector2 TileMapSize => BackGround.GetSize() / Main.TileSize;

        [JsonIgnore]
        public Vector2 RealSize => BackGround.GetSize();

        [JsonIgnore]
        public Vector2 Position;

        [JsonIgnore]
        public Vector2 TilePostion => Position / Main.TileSize;

        [JsonIgnore]
        public Texture2D BackGround;

        [JsonIgnore]
        public Texture2D ForeGround_Opaque;

        public bool inHouse;

        public string Name;

        public Room(string roomName)
        {
            BackGround = Main.TextureManager[TexType.Tile, roomName + "\\background"];
            ForeGround_Opaque = Main.TextureManager[TexType.Tile, roomName + "\\foreground_opaque"];
            inHouse = true;
            TileMap = new int[(int)TileMapSize.X * (int)TileMapSize.Y];
            TileMap = new int[]
            {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1,
                1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            };
        }

        public static Room Initialize(string json)
        {
            var room = JsonConvert.DeserializeObject<Room>(json);
            room.BackGround = Main.TextureManager[TexType.Tile, room.Name + "\\background"];
            return room;
        }

        public virtual void DrawBack(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(BackGround, Position, Color.White);
        }

        public virtual void DrawFore(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(ForeGround_Opaque, Position, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            var vec = (Main.GameSize - RealSize) / 2;
            Position = new Vector2((int)vec.X / Main.TileSize, (int)vec.Y / Main.TileSize) * Main.TileSize;
        }

        public int this[int x, int y]
        {
            get => TileMap[x + y * (int)TileMapSize.X];
            set => TileMap[x + y * (int)TileMapSize.X] = value;
        }
    }
}
