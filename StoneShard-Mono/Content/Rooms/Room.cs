using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono.Content.Components;
using StoneShard_Mono.Content.NPCs;
using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Tiles;
using StoneShard_Mono.Extensions;
using System.Collections.Generic;

namespace StoneShard_Mono.Content.Rooms
{
    public abstract class Room : GameContent
    {
        public List<Entity> Entities;

        public List<NPC> NPCs;

        public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)RealSize.X, (int)RealSize.Y);

        public Rectangle TileRectangle => Rectangle.Divide(Main.TileSize);

        public Vector2 TileMapSize => Background.GetSize() / Main.TileSize;

        public Vector2 RealSize => Background.GetSize();

        public Vector2 TilePostion => Position / Main.TileSize;

        public Vector2 Position;

        public Player LocalPlayer;

        public bool inHouse;

        public Timer Timer;

        public Room LastRoom;

        public Texture2D Background;

        public string BackgroundPath;

        public int[,] TileMap;

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
            spriteBatch.Draw(Background, Position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }

        public virtual void DrawAlphaBlend(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(var entity in Entities)
            {
                if(entity is Tile && !entity.UseAdditive)
                    entity.Draw(spriteBatch, gameTime);
            }
        }

        public virtual void DrawAdditive(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is Tile && entity.UseAdditive)
                    entity.Draw(spriteBatch, gameTime);
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
            if (Main.RoomID.ContainsKey(GetType().Name)) return;

            Main.RoomID[GetType().Name] = Main.RoomID.Count + 1;
        }

        public override void Update(GameTime gameTime)
        {
            var vec = (Main.GameSize - RealSize) / 2;
            Position = new Vector2((int)vec.X / Main.TileSize, (int)vec.Y / Main.TileSize) * Main.TileSize;

            Main.LocalPlayer = LocalPlayer;

            foreach (var entity in Entities)
                if(entity is not Player) entity.Update(gameTime);

            if (!Main.PlayerTurn)
                NPCsAction();

            if (CheckAllNPCsDoneAction())
                Main.GameScene?.TurnController.EndNPCTurn();

            if (this != Main.GameScene.CurrentRoom)
            {
                RemoveEntity(LocalPlayer);
                LocalPlayer = null;
            }
        }

        public virtual void UpdatePlayer(GameTime gameTime)
        {
            LocalPlayer?.Update(gameTime);
        }

        public virtual void NPCsAction()
        {
            foreach (var npc in NPCs)
            {
                npc.ActionDone = false;
                npc.DoAction();
            }
        }

        public bool CheckAllNPCsDoneAction()
        {
            foreach (var npc in NPCs)
            {
                if (npc.ActionDone) continue;
                else return false;
            }
            return true;
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

        public RoomData ToData()
        {
            RoomData roomData = new RoomData(TileMapSize);

            roomData.Name = Name;
            roomData.BackgroundPath = BackgroundPath;

            foreach (var entity in Entities)
            {
                EntityData data = new();

                data.Name = entity.GetType().Name;
                data.Mod = entity.Mod;
                data.Position = entity.TilePosition;
                data.TexturePath = entity.TexturePath;

                if (entity is Tile tile)
                {
                    data.Type = "Tile";
                    data.DrawOffset = tile.DrawOffset;
                }

                if(entity is NPC npc)
                {

                }

                roomData.Entities.Add(data);
            }

            return roomData;
        }

        public virtual void Enter(Room lastRoom)
        {

        }
    }

    internal class EmptyRoom : Room 
    {
        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) { }
    }

    public class RoomData
    {
        public RoomData(int width, int height)
        {
            Entities = new();
            Width = width;
            Height = height;
            Reachable = new int[height, width];
        }

        public RoomData(Vector2 roomSize) : this((int)roomSize.X, (int)roomSize.Y) { }

        public int Width;

        public int Height;

        public string Name;

        public string BackgroundPath;

        public int[,] Reachable;

        public List<EntityData> Entities;
    }
}
