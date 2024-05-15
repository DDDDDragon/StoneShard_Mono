using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoneShard_Mono.Animations;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Players;
using StoneShard_Mono.Rooms;
using StoneShard_Mono.UIComponents;
using StoneShard_Mono.Utils;
using System;

namespace StoneShard_Mono.Scenes
{
    public class GameScene : Scene
    {
        public GameScene() 
        {
            var room = new Room("tavernInside_floor_1");

            CurrentRoom = room;

            Timer = new();

            selectBox = Main.TextureManager[TexType.Tile, "selectBox"];

            wayPoint = Main.TextureManager[TexType.Tile, "wayPoint"];

            Player = new Jonna();
        }

        public Room CurrentRoom;

        public Player Player;

        public Texture2D selectBox;

        public Texture2D wayPoint;

        public MouseState PreviousMouseState;

        public MouseState MouseState;

        public Vector2 PreviousMouseTile;

        public Vector2 MouseTile;

        public Timer Timer;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawRectangle(new(0, 0, Main.GameWidth, Main.GameHeight), GameColors.RoomDark);

            CurrentRoom?.DrawBack(spriteBatch, gameTime);

            Player?.Draw(spriteBatch, gameTime);

            CurrentRoom?.DrawFore(spriteBatch, gameTime);

            spriteBatch.Draw(selectBox, (MouseTile * 2 / 3 + PreviousMouseTile / 3) * Main.TileSize + new Vector2(2, 2), Color.Black);


            if (Reachable((int)MouseTile.X, (int)MouseTile.Y))
                spriteBatch.Draw(selectBox, (MouseTile * 2 / 3 + PreviousMouseTile / 3) * Main.TileSize, Color.White);
            else
                spriteBatch.Draw(selectBox, (MouseTile * 2 / 3 + PreviousMouseTile / 3) * Main.TileSize, Color.Red);

            var wayfinder = new WayFinder(delegate (Vector2 vec) { return Reachable((int)vec.X, (int)vec.Y); }, Player.TilePosition, MouseTile, CurrentRoom);

            if(!Player.IsMove) Player.CurrentPath = wayfinder.FindPath();

            foreach (var item in Player.CurrentPath)
            {
                var rect = new Rectangle(0, 0, wayPoint.Width, wayPoint.Height);
                var scale = new Vector2((float)(0.7 + Math.Cos((gameTime.TotalGameTime.TotalMilliseconds / 200) + Player.CurrentPath.IndexOf(item)) / 6));
                var offset = new Vector2(Main.TileSize - wayPoint.Width * scale.X) / 2;

                if (Player.CurrentPath.IndexOf(item) == 0)
                {
                    if (Vector2.Distance(item, Player.TargetTilePos) == 1)
                    {
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, (item + (Player.TargetTilePos - item) / 2) * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, (item + (Player.TargetTilePos - item) / 2) * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    if (Vector2.Distance(item, Player.CurrentPath[Player.CurrentPath.IndexOf(item) - 1]) == 1)
                    {
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, (item + (Player.CurrentPath[Player.CurrentPath.IndexOf(item) - 1] - item) / 2) * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, item * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        spriteBatch.Draw(wayPoint, (item + (Player.CurrentPath[Player.CurrentPath.IndexOf(item) - 1] - item) / 2) * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    }
                }
            }
        }

        public bool Reachable(int x, int y)
        {
            if (CurrentRoom.TileRectangle.Intersects(new(x, y, 1, 1)))
                return CurrentRoom[x - (int)CurrentRoom.Position.X / Main.TileSize, y - (int)CurrentRoom.Position.Y / Main.TileSize] == 0;
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            CurrentRoom?.Update(gameTime);

            PreviousMouseState = MouseState;

            MouseState = Mouse.GetState();

            PreviousMouseTile = MouseTile;

            MouseTile = new Vector2(MouseState.X / Main.TileSize, MouseState.Y / Main.TileSize);

            if (Reachable((int)MouseTile.X, (int)MouseTile.Y))
            {
                if (PreviousMouseState.LeftButton == ButtonState.Pressed && MouseState.LeftButton == ButtonState.Released)
                {
                    if(Player.CurrentAnimation is EntityMoveChain moveChain && Player.CurrentAnimation.MaxTime != 0)
                        moveChain.ShouldBreak = true;
                    else
                    {
                        var chain = new EntityMoveChain();

                        foreach (var pos in Player.CurrentPath)
                            chain.RegisterAnimation(new EntityMoveAnimation(pos));

                        Player.PlayAnimation(chain);
                    }
                }

                if (PreviousMouseState.RightButton == ButtonState.Pressed && MouseState.RightButton == ButtonState.Released)
                {
                    var dir = (MouseTile - Player.TilePosition).X;
                    Player.Direction = dir > 0 ? 1 : (dir == 0 ? Player.Direction : -1);
                    Player.TilePosition = MouseTile;
                    Player.PreMoveTilePos = MouseTile;
                    Player.TargetTilePos = MouseTile;
                }
            }
            
            Player.Update(gameTime);
        }
    }
}
