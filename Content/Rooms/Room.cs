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

        public Player Player;

        public bool inHouse;

        public Timer Timer;

        public Room LastRoom;

        public static string RoomName;

        internal static Texture2D BackGround;

        public static int[,] TileMap;

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

        public virtual void DrawTiles(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(var entity in Entities)
            {
                if(entity is Tile)
                    entity.Draw(spriteBatch, gameTime);
            }
        }

        public virtual void DrawPlayer(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Player?.Draw(spriteBatch, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawBack(spriteBatch, gameTime);

            DrawTiles(spriteBatch, gameTime);

            DrawPlayer(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            var vec = (Main.GameSize - RealSize) / 2;
            Position = new Vector2((int)vec.X / Main.TileSize, (int)vec.Y / Main.TileSize) * Main.TileSize;

            Main.LocalPlayer = Player;

            foreach (var entity in Entities)
                if(!(entity is Player)) entity.Update(gameTime);

            if (!Main.PlayerTurn)
                NPCsAction();

            if(CheckAllNPCsDoneAction())
                Main.GameScene?.TurnController.EndNPCTurn();
        }

        public virtual void UpdatePlayer(GameTime gameTime)
        {
            Player?.Update(gameTime);
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

            entity.TilePosition = position;

            Entities.Add(entity);
        }

        public virtual void Enter()
        {

        }
    }
}
