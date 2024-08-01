using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono_RoomEditor.Content.Components;
using StoneShard_Mono_RoomEditor.Content.NPCs;
using StoneShard_Mono_RoomEditor.Content.Players;
using StoneShard_Mono_RoomEditor.Content.Tiles;
using StoneShard_Mono_RoomEditor.Extensions;
using StoneShard_Mono_RoomEditor.Managers;
using System.Collections.Generic;
using System.Linq;

namespace StoneShard_Mono_RoomEditor.Content.Rooms
{
    public class Room : GameContent
    {
        public List<Entity> Entities;

        public List<NPC> NPCs;

        public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)RealSize.X, (int)RealSize.Y);

        public Rectangle TileRectangle => Rectangle.Divide(Main.TileSize);

        public Vector2 TileMapSize => BackGround.GetSize() / Main.TileSize;

        public Vector2 RealSize => BackGround.GetSize();

        public Vector2 TilePostion => Position / Main.TileSize;

        public Vector2 Position;

        public Player LocalPlayer;

        public bool inHouse;

        public Timer Timer;

        public Room LastRoom;

        public Texture2D BackGround;

        public int[,] TileMap;

        public Vector2 ViewPortPos;

        public Viewport ViewPort;

        public Matrix View;

        public static Room Empty => new EmptyRoom();

        public override string Name => GetType().Name;

        public override void SetDefaults()
        {
            Entities = new();

            NPCs = new();

            Timer = new();

            base.SetDefaults();
        }

        public virtual void DrawBack(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(BackGround, Position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }

        public virtual void DrawAlphaBlend(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is Tile && !entity.UseAdditive)
                {
                    entity.PreDraw(spriteBatch, gameTime);
                    entity.Draw(spriteBatch, gameTime);
                }
            }
        }

        public virtual void DrawAdditive(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is Tile && entity.UseAdditive)
                {
                    entity.PreDraw(spriteBatch, gameTime);
                    entity.Draw(spriteBatch, gameTime);
                }
            }
        }

        public virtual void DrawPlayer(SpriteBatch spriteBatch, GameTime gameTime)
        {
            LocalPlayer?.Draw(spriteBatch, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawBack(spriteBatch, gameTime);

            DrawAlphaBlend(spriteBatch, gameTime);

            DrawPlayer(spriteBatch, gameTime);

            DrawAdditive(spriteBatch, gameTime);
        }

        public override void SetStaticDefaults()
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in Entities)
                if (entity is not Player) entity.Update(gameTime);
        }

        public int this[int x, int y]
        {
            get => TileMap[y, x];
            set => TileMap[y, x] = value;
        }

        public void RegisterEntity(Entity entity, Vector2 position = default)
        {
            entity.CurrentRoom = this;

            entity.SetPos(position);

            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity) => Entities.Remove(entity);

        public virtual void Enter(Room lastRoom)
        {

        }

        public void LoadData(RoomData data)
        {
            Name = data.Name;
            BackGround = Main.TextureManager[TexType.Tile, data.BackgroundPath];

            foreach (var entity in data.Entities)
            {
                if(entity.Type == "Tile")
                {
                    if(entity.Mod == "StoneShard")
                    {
                        if (entity.Name == "Door")
                        {

                        }
                        else
                        {
                            var ct = typeof(ContentInstance<>);

                            var tileType = typeof(Room).Assembly.GetTypes().Where(t => t.Name == entity.Name).First();

                            Entity e = (Entity)ct.MakeGenericType(tileType).GetMethod("NewTile")
                                .Invoke(null, new object[] { 1, entity.DrawOffset, entity.TexturePath });

                            RegisterEntity(e, entity.Position);
                        }
                    }
                }
            }
        }
    }

    internal class EmptyRoom : Room
    {
        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }
    }

    public class RoomData
    {
        public int Width;

        public int Height;

        public string Name;

        public string BackgroundPath;

        public int[,] Reachable;

        public List<EntityData> Entities;
    }
}
